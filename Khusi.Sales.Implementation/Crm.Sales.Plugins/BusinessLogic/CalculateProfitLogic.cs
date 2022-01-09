using Crm.Sales.Plugins.Models.CrmEntities;
using Microsoft.Xrm.Sdk;

namespace Crm.Sales.Plugins.BusinessLogic
{
    public static class CalculateProfitLogic
    {
        public static void CalculateProfit(kh_resourcerequirement resourcerequirement,
            IOrganizationService organisationService, ITracingService tracingService)
        {
            var updateEntity = new kh_resourcerequirement
            {
                Id = resourcerequirement.Id
            };

            // fixed costs profit calculations
            if (resourcerequirement.kh_resourcefixedcost != null && resourcerequirement.kh_commissionpercantage.HasValue)
            {
                var profitAmount = (resourcerequirement.kh_commissionpercantage.Value / 100)
                    * resourcerequirement.kh_resourcefixedcost.Value;
                updateEntity.kh_profitamount = new Money(profitAmount);
                organisationService.Update(updateEntity);
                return;
            }
            else if (resourcerequirement.kh_resourcefixedcost == null || !resourcerequirement.kh_commissionpercantage.HasValue)
            {
                updateEntity.kh_profitamount = null;
                organisationService.Update(updateEntity);
                return;
            }

            // hourly rates profit calculations
            if (resourcerequirement.kh_resourcehourlyrate != null && resourcerequirement.kh_commissionpercantage.HasValue)
            {
                var profitAmount = (resourcerequirement.kh_commissionpercantage.Value / 100)
                    * resourcerequirement.kh_resourcehourlyrate.Value;
                updateEntity.kh_profitamount = new Money(profitAmount);
                organisationService.Update(updateEntity);
                return;
            }
            else if (resourcerequirement.kh_resourcehourlyrate == null || !resourcerequirement.kh_commissionpercantage.HasValue)
            {
                updateEntity.kh_profitamount = null;
                organisationService.Update(updateEntity);
                return;
            }            
        }
    }
}
