using Crm.Sales.Plugins.BusinessLogic;
using Crm.Sales.Plugins.Models.CrmEntities;
using Microsoft.Xrm.Sdk;
using System;

namespace Crm.Sales.Plugins.Plugins
{
    public class ResourceRequirementSetProfit : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider
                .GetService(typeof(IPluginExecutionContext));
            var tracingService = (ITracingService)serviceProvider
                .GetService(typeof(ITracingService));

            IOrganizationServiceFactory serviceFactory =
                  (IOrganizationServiceFactory)serviceProvider.GetService
                  (typeof(IOrganizationServiceFactory));
            IOrganizationService orgService = serviceFactory.CreateOrganizationService(context.UserId);

            if (context.PostEntityImages.ContainsKey("PostImage"))
            {
                var postImage = context.PostEntityImages["PostImage"].ToEntity<kh_resourcerequirement>();
                CalculateProfitLogic.CalculateProfit(postImage, orgService, tracingService);
            }
            else
            {
                throw new InvalidPluginExecutionException("Post image is missing");
            }
        }
    }
}
