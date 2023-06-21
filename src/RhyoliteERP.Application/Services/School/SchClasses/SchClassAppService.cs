using AutoMapper;
using RhyoliteERP.DomainServices.School.SchClasses;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.SchClasses.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.SchClasses
{
   public class SchClassAppService: RhyoliteERPAppServiceBase, ISchClassAppService
    {
        private readonly ISchClassManager _schClassManager;
        private readonly IMapper _mapper;

        public SchClassAppService(ISchClassManager schClassManager, IMapper mapper)
        {
            _schClassManager = schClassManager;
            _mapper = mapper;
        }


        public async Task<IEnumerable<object>> ListAll(Guid levelId)
        {
            return await _schClassManager.ListAll(levelId);
        }

        public async Task<IEnumerable<object>> ListAll()
        {
            return await _schClassManager.ListAll();
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _schClassManager.GetAsync(id);
        }
        

        public async Task<object> Create(CreateClassInput input)
        {
            var obj = _mapper.Map<SchClass>(input);
            return await _schClassManager.Create(obj);
        }

        public async Task Update(UpdateClassInput input)
        {
            var obj = _mapper.Map<SchClass>(input);
            await _schClassManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _schClassManager.Delete(Id);

        }
    }
}
