using Crm.Sales.Plugins.Models.CrmEntities;
using Microsoft.Xrm.Sdk;

namespace Crm.Sales.Plugins.BusinessLogic
{
    public static class CreateSkillsLogic
    {
        public static void CreateSkills(kh_skill skill,
            IOrganizationService organisationService, ITracingService tracingService)
        {
            tracingService.Trace($"Creating skills for skill with id: {skill.Id}");
            if (skill.kh_skilllevel == kh_skillleveloptions.Expert)
            {
                var skill1 = new kh_skill 
                {
                    kh_name = skill.kh_name,
                    kh_skillcategoryid = skill.kh_skillcategoryid,
                    kh_skilllevel = kh_skillleveloptions.Intermediate
                };
                organisationService.Create(skill1);

                var skill2 = new kh_skill
                {
                    kh_name = skill.kh_name,
                    kh_skillcategoryid = skill.kh_skillcategoryid,
                    kh_skilllevel = kh_skillleveloptions.Senior
                };
                organisationService.Create(skill2);

            }
            else if (skill.kh_skilllevel == kh_skillleveloptions.Senior)
            {
                var skill1 = new kh_skill
                {
                    kh_name = skill.kh_name,
                    kh_skillcategoryid = skill.kh_skillcategoryid,
                    kh_skilllevel = kh_skillleveloptions.Intermediate
                };
                organisationService.Create(skill1);
                var skill2 = new kh_skill
                {
                    kh_name = skill.kh_name,
                    kh_skillcategoryid = skill.kh_skillcategoryid,
                    kh_skilllevel = kh_skillleveloptions.Expert
                };
                organisationService.Create(skill2);
            }
            else
            {
                var skill1 = new kh_skill
                {
                    kh_name = skill.kh_name,
                    kh_skillcategoryid = skill.kh_skillcategoryid,
                    kh_skilllevel = kh_skillleveloptions.Senior
                };
                organisationService.Create(skill1);

                var skill2 = new kh_skill
                {
                    kh_name = skill.kh_name,
                    kh_skillcategoryid = skill.kh_skillcategoryid,
                    kh_skilllevel = kh_skillleveloptions.Expert
                };
                organisationService.Create(skill2);
            }
        }
    }
}
