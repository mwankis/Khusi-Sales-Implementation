using Crm.Sales.Plugins.Models.CrmEntities;
using Crm.Sales.Plugins.Plugins;
using FakeXrmEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;

namespace Crm.Sales.Plugins.Tests
{
    [TestClass]
    public class ProfitCalculationTests
    {
        private XrmFakedContext _context;
        private XrmFakedTracingService _trace;
        private IOrganizationService _service;
        private XrmFakedPluginExecutionContext _pluginContext;
        private kh_resourcerequirement _resourcerequirement;

        [TestInitialize]
        public void Init()
        {
            _context = new XrmFakedContext();
            _trace = _context.GetFakeTracingService();
            _service = _context.GetOrganizationService();
            _pluginContext = _context.GetDefaultPluginContext();

            _resourcerequirement = new kh_resourcerequirement
            {
                Id = Guid.NewGuid(),
                
            };

           

            _context.Initialize(new List<Entity> { _resourcerequirement });
        }

        [ExpectedException(typeof(InvalidPluginExecutionException), "Post image is missing")]
        [TestMethod]
        public void Plugin_MissingPostImage()
        {
            // act
            _context.ExecutePluginWithTarget<ResourceRequirementSetProfit>(_pluginContext, _resourcerequirement);
        }

        [TestMethod]
        public void Plugin_PostImageHasValue()
        {
            // arrange
            _resourcerequirement.kh_resourcefixedcost = new Money(100);
            _resourcerequirement.kh_commissionpercantage = 10;
            _pluginContext.PostEntityImages = new EntityImageCollection()
            {
                new KeyValuePair<string, Entity>("PostImage", _resourcerequirement)
            };

            // execute
            _context.ExecutePluginWithTarget<ResourceRequirementSetProfit>(_pluginContext, _resourcerequirement);
            var reseourceRequirement = GetResourcerequirement(_resourcerequirement.Id);
            var traceLog = _trace.DumpTrace();

            // assert
            Assert.AreEqual(reseourceRequirement.kh_profitamount, new Money(10));
        }


        public kh_resourcerequirement GetResourcerequirement(Guid id)
        {
            var entity = _service.Retrieve(kh_resourcerequirement.EntityLogicalName, id, new ColumnSet(true));
            return entity.ToEntity<kh_resourcerequirement>();
        }
    }
}
