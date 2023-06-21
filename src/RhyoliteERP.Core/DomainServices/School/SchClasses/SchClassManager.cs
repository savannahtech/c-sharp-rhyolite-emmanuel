﻿using Abp.Domain.Repositories;
using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.SchClasses
{
   public class SchClassManager: Abp.Domain.Services.IDomainService, ISchClassManager
    {

        private readonly IRepository<SchClass, Guid> _repositorySchClass;
        private readonly IRepository<ClassStream, Guid> _repositoryClassStream;
        private readonly IRepository<Level, Guid> _repositoryLevel;

        public SchClassManager(IRepository<SchClass, Guid> repositorySchClass, IRepository<ClassStream, Guid> repositoryClassStream, IRepository<Level, Guid> repositoryLevel)
        {
            _repositorySchClass = repositorySchClass;
            _repositoryClassStream = repositoryClassStream;
            _repositoryLevel = repositoryLevel;
        }


        public async Task Delete(Guid id)
        {
            await _repositorySchClass.DeleteAsync(id);
        } 

        public async Task<object> Create(SchClass entity)
        {

            var schClass = await _repositorySchClass.FirstOrDefaultAsync(x => x.LevelId == entity.LevelId && x.ClassName == entity.ClassName && x.StreamId == entity.StreamId);
            if (schClass == null)
            {
                var stream = await _repositoryClassStream.FirstOrDefaultAsync(x => x.Id == entity.StreamId);
                var level = await _repositoryLevel.FirstOrDefaultAsync(x => x.Id == entity.LevelId);


                if (level != null)
                {
                    entity.LevelName = level.Name;
                }

                if (stream != null)
                {
                    entity.StreamName = stream.Name;
                }

                await _repositorySchClass.InsertAsync(entity);

                return new
                {
                    code = 200,
                    message = "successful"
                };
            }
            else
            {
                return new
                {
                    code = 400,
                    message = "Duplicate records are not allowed."
                };
            }

            
        }


        public async Task<object> GetAsync(Guid id)
        {
            return await _repositorySchClass.GetAsync(id);
        }


        public async Task<IEnumerable<object>> ListAll(Guid levelId)
        {
            var data = await _repositorySchClass.GetAllListAsync(a => a.LevelId == levelId);
            
            return data.OrderBy(c => c.ClassName);
        }

        public async Task<IEnumerable<object>> ListAll()
        {
            var data = await _repositorySchClass.GetAllListAsync();

            return data.OrderBy(c => c.ClassName);
        }

        public async Task Update(SchClass entity)
        {
            var stream = await _repositoryClassStream.FirstOrDefaultAsync(x => x.Id == entity.StreamId);
            var level = await _repositoryLevel.FirstOrDefaultAsync(x => x.Id == entity.LevelId);

            entity.LevelName = level.Name;
            if (stream != null)
            {
                entity.StreamName = stream.Name;
            }

            await _repositorySchClass.UpdateAsync(entity);
        }
    }
}
