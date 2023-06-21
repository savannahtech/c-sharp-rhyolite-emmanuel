using AutoMapper;
using RhyoliteERP.DomainServices.School.Parents;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.Parents.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.Parents
{
   public class ParentAppService: Abp.Domain.Services.DomainService, IParentAppService
    {
        private readonly IParentManager _parentManager;
        private readonly IMapper _mapper;

        public ParentAppService(IParentManager parentManager, IMapper mapper)
        {
            _parentManager = parentManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<object>> ListAll(Guid id)
        {
            return await _parentManager.ListAll(id);
        }


        public async Task<IEnumerable<object>> ListAll()
        {
            return await _parentManager.ListAll();
        }


        public async Task<IEnumerable<object>> ListParentWards()
        {
            return await _parentManager.ListParentWards();
        }


        public async Task<object> GetParentInfo(Guid id)
        {
            return await _parentManager.GetParentInfo(id);
        }

        public async Task<object> Create(CreateParentInput input)
        {
            var obj = _mapper.Map<Parent>(input);
            return await _parentManager.Create(obj);
        }

        public async Task<object> Update(UpdateParentInput input)
        {
            var obj = _mapper.Map<Parent>(input);
            return await _parentManager.Update(obj);
        }


        public async Task Delete(Guid id)
        {
            await _parentManager.Delete(id);
        }

        public async Task<IEnumerable<object>> ListParents()
        {
            return await _parentManager.ListParents();
        }

    }
}
