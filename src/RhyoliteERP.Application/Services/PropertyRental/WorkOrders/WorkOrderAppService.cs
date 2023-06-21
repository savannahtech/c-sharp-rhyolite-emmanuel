using AutoMapper;
using RhyoliteERP.DomainServices.PropertyRental.Vendors;
using RhyoliteERP.DomainServices.PropertyRental.WorkOrders;
using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.Services.PropertyRental.WorkOrders.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.PropertyRental.WorkOrders
{
    public class WorkOrderAppService:RhyoliteERPAppServiceBase, IWorkOrderAppService
    {
        private readonly IWorkOrderManager _workOrderManager;
        private readonly IMapper _mapper;

        public WorkOrderAppService(IWorkOrderManager workOrderManager, IMapper mapper)
        {
            _workOrderManager = workOrderManager;
            _mapper = mapper;
        }

        public async Task<object> ListAll()
        {
            return await _workOrderManager.ListAll();
        }

        public async Task Create(CreateWorkOrderInput input)
        {
            var obj = _mapper.Map<WorkOrder>(input);
            await _workOrderManager.Create(obj);
        }

        public async Task Update(UpdateWorkOrderInput input)
        {
            var obj = _mapper.Map<WorkOrder>(input);
            await _workOrderManager.Update(obj);
        }

        public async Task Delete(Guid Id)
        {
            await _workOrderManager.Delete(Id);

        }
    }
}
