using HashidsNet;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.Authorization
{
    public class ValidateTenant: RhyoliteERPAppServiceBase, IValidateTenant
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHashids _hashids;
        public ValidateTenant(IHttpContextAccessor httpContextAccessor, IHashids hashids)
        {
            _httpContextAccessor = httpContextAccessor;
            _hashids = hashids;
        }


        public async Task<int> Validate()
        {
            if (!_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("TenantId", out var accountSecret))
            {
                throw new Exception("Account Secret is required");

            }

            var rawId = _hashids.Decode(accountSecret);

            if (rawId.Length == 0)
            {
                throw new Exception("Account Secret is required");

            }

            return rawId[0];

        }

    }
}
