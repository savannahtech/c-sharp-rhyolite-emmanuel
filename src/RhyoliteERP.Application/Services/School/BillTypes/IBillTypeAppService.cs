using Abp.Application.Services;
using RhyoliteERP.Services.School.BillTypes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Services.School.BillTypes
{
   public interface IBillTypeAppService: IApplicationService
    {
        Task<object> ListAll();
        Task<object> Create(CreateBillTypeInput input);
        Task Update(UpdateBillTypeInput input);
        Task Delete(Guid Id);

    }
}
