﻿using RhyoliteERP.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.School.AlumniHistories
{
   public interface IAlumniHistoryManager : Abp.Domain.Services.IDomainService
    {

        Task<IEnumerable<object>> ListAll(Guid academicYearId);
        Task CreateBatch(List<AlumniHistory> entityList);
    }
}
