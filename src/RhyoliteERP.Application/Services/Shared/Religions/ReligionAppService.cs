﻿using AutoMapper;
using RhyoliteERP.DomainServices.Shared.Religions;
using RhyoliteERP.Models.Shared;
using RhyoliteERP.Services.Shared.Religions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.Shared.Religions
{
   public class ReligionAppService: RhyoliteERPAppServiceBase, IReligionAppService
    {
        private readonly IReligionManager _religionManager;
        private readonly IMapper _mapper;

        public ReligionAppService(IReligionManager religionManager, IMapper mapper)
        {
            _religionManager = religionManager;
            _mapper = mapper;
        }


        public async Task<IEnumerable<object>> ListAll()
        {
            return await _religionManager.ListAll();
        }

        public async Task<object> Create(CreateReligionInput input)
        {
            var obj = _mapper.Map<Religion>(input);
            return await _religionManager.Create(obj);
        }

        public async Task Update(UpdateReligionInput input)
        {
            var obj = _mapper.Map<Religion>(input);
            await _religionManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _religionManager.Delete(Id);

        }
    }
}
