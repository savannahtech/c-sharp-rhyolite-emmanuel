using AutoMapper;
using RhyoliteERP.DomainServices.School.ClassStreams;
using RhyoliteERP.Models.School;
using RhyoliteERP.Services.School.ClassStreams.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.ClassStreams
{
   public class ClassStreamAppService: RhyoliteERPAppServiceBase, IClassStreamAppService
    {
        private readonly IClassStreamManager _classStreamManager;
        private readonly IMapper _mapper;

        public ClassStreamAppService(IClassStreamManager classStreamManager, IMapper mapper)
        {
            _classStreamManager = classStreamManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<object>> ListAll()
        {
            return await _classStreamManager.ListAll();
        }

        public async Task<object> Create(CreateClassStreamInput input)
        {
            var obj = _mapper.Map<ClassStream>(input);
            return await _classStreamManager.Create(obj);

        }

        public async Task Update(UpdateClassStreamInput input)
        {
            var obj = _mapper.Map<ClassStream>(input);
            await _classStreamManager.Update(obj);

        }


        public async Task Delete(Guid Id)
        {
            await _classStreamManager.Delete(Id);

        }
    }
}
