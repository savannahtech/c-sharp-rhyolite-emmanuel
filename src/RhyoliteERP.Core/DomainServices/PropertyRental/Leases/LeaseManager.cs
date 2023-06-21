using Abp.Domain.Repositories;
using RhyoliteERP.Models.PropertyRental;
using RhyoliteERP.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhyoliteERP.DomainServices.PropertyRental.Leases
{
    public class LeaseManager: Abp.Domain.Services.DomainService, ILeaseManager
    {
        private readonly IRepository<Lease, Guid> _repositoryLease;
        private readonly IRepository<Property, Guid> _repositoryProperty;
        private readonly IRepository<PropertyUnit, Guid> _repositoryPropertyUnit;
        private readonly IRepository<LeaseTenant, Guid> _repositoryLeaseTenant;
        private readonly IRepository<ResidentAccount, Guid> _repositoryResidentAccount;

        public LeaseManager(IRepository<Lease, Guid> repositoryLease, IRepository<LeaseTenant, Guid> repositoryLeaseTenant, IRepository<ResidentAccount, Guid> repositoryResidentAccount, IRepository<Property, Guid> repositoryProperty, IRepository<PropertyUnit, Guid> repositoryPropertyUnit)
        {
            _repositoryLease = repositoryLease;
            _repositoryLeaseTenant = repositoryLeaseTenant;
            _repositoryResidentAccount = repositoryResidentAccount;
            _repositoryProperty = repositoryProperty;
            _repositoryPropertyUnit = repositoryPropertyUnit;
        }

        public async Task Create(Lease entity)
        {

            foreach (var tenant in entity.TenantOrCosigners)
            {
                var leaseTenant = new LeaseTenant
                {
                    Id= tenant.Id,
                    TenantIdentifier = tenant.TenantIdentifier,
                    FirstName = tenant.FirstName,
                    LastName = tenant.LastName,
                    PrimaryEmail= tenant.PrimaryEmail,
                    PrimaryPhoneNo= tenant.PrimaryPhoneNo,
                    SecondaryEmail = tenant.SeconadaryEmail,
                    SecondaryPhoneNo = tenant.SeconadaryPhoneNo,
                    DateOfBirth= tenant.DateOfBirth,
                    TaxIdentificationNo= tenant.TaxIdentificationNo,
                    EmergencyContactEmail= tenant.EmergencyContactEmail,
                    EmergencyContactName= tenant.EmergencyContactName,
                    EmergencyContactPhoneNo= tenant.EmergencyContactPhoneNo,
                    EmergencyContactRelationshipToTenant = tenant.EmergencyContactRelationshipToTenant,
                    CountryId = tenant.CountryId,
                    CountryName= tenant.CountryName,
                    RegionOrState = tenant.RegionOrState,
                    City = tenant.City,
                    LeasedPropertyId = tenant.LeasedPropertyId,
                    LeasedPropertyName = tenant.LeasedPropertyName,
                    LeasedPropertyUnitId = tenant.LeasedPropertyUnitId,
                    LeasedPropertyUnitNo = tenant.LeasedPropertyUnitNo,

                };

                await _repositoryLeaseTenant.InsertAsync(leaseTenant);
                 
            }

            if (entity.TenantOrCosigners.Any())
            {
                //create resident account.

                await _repositoryResidentAccount.InsertAsync(new ResidentAccount
                {
                    LeaseTenantId = entity.TenantOrCosigners[0].Id,
                    LeaseTenantIdentifier = entity.TenantOrCosigners[0].TenantIdentifier,
                    AccountCaption = entity.PropertyUnitId != Guid.Empty ? $"{entity.PropertyName}-{entity.PropertyUnitName} : {entity.TenantOrCosigners[0].FirstName} {entity.TenantOrCosigners[0].LastName}" : $"{entity.PropertyName} : {entity.TenantOrCosigners[0].FirstName} {entity.TenantOrCosigners[0].LastName}",
                    BalanceAfter = 0,
                    BalanceBefore = 0,
                    CurrentBalance = 0,
                    LeasedPropertyId = entity.PropertyId,
                    LeasedPropertyName = entity.PropertyName,
                    LeasedPropertyUnitId = entity.PropertyUnitId,
                    LeasedPropertyUnitNo = entity.PropertyUnitName,

                });
            }

            // set property / unit as occupied.

            if (entity.PropertyUnitId != Guid.Empty)
            {
                var propertyUnitInfo = await _repositoryPropertyUnit.FirstOrDefaultAsync(entity.PropertyUnitId);

                if (propertyUnitInfo != null)
                {
                    propertyUnitInfo.IsRented = true;

                    await _repositoryPropertyUnit.UpdateAsync(propertyUnitInfo);
                }
            }
            else
            {

                var propertyInfo = await _repositoryProperty.FirstOrDefaultAsync(entity.PropertyId);

                if (propertyInfo != null)
                {
                    propertyInfo.IsRented = true;

                    await _repositoryProperty.UpdateAsync(propertyInfo);
                }
            }

            await _repositoryLease.InsertAsync(entity);
        }

        public async Task Update(Lease entity)
        {
            await _repositoryLease.UpdateAsync(entity);
        }

        public async Task<object> GetAsync(Guid id)
        {
            return await _repositoryLease.FirstOrDefaultAsync(id);
        }

        public async Task<object> ListAll()
        {
            return await _repositoryLease.GetAllListAsync();
        }
        public async Task Delete(Guid id)
        {
            await _repositoryLease.DeleteAsync(id);
        }
    }
}
