﻿using Microsoft.AspNetCore.Mvc;
using Asd.AbpZeroTemplate.DashboardCustomization;
using Asd.AbpZeroTemplate.DashboardCustomization.Dto;
using Asd.AbpZeroTemplate.Web.Areas.Accounting.Models.CustomizableDashboard;
using Asd.AbpZeroTemplate.Web.Controllers;
using System.Linq;
using System.Threading.Tasks;
using Asd.AbpZeroTemplate.Web.Areas.Accounting.Startup;

namespace Asd.AbpZeroTemplate.Web.Areas.Accounting.Controllers
{
    public abstract class CustomizableDashboardControllerBase : AbpZeroTemplateControllerBase
    {
        protected readonly IDashboardCustomizationAppService DashboardCustomizationAppService;
        protected readonly DashboardViewConfiguration DashboardViewConfiguration;

        protected CustomizableDashboardControllerBase(
            DashboardViewConfiguration dashboardViewConfiguration, 
            IDashboardCustomizationAppService dashboardCustomizationAppService)
        {
            DashboardViewConfiguration = dashboardViewConfiguration;
            DashboardCustomizationAppService = dashboardCustomizationAppService;
        }

        public async Task<PartialViewResult> AddWidgetModal(string dashboardName, string pageId)
        {
            var userDashboard = await DashboardCustomizationAppService.GetUserDashboard(
                new GetDashboardInput
                {
                    DashboardName = dashboardName,
                    Application = AbpZeroTemplateDashboardCustomizationConsts.Applications.Mvc
                }
            );

            var page = userDashboard.Pages.Single(p => p.Id == pageId);

            var filteredWidgetsByPermission = DashboardCustomizationAppService.GetAllWidgetDefinitions(new GetDashboardInput() { DashboardName = dashboardName })
                .Where(widgetDef => page.Widgets.All(widgetOnPage => widgetOnPage.WidgetId != widgetDef.Id))
                .ToList();

            var viewModel = new AddWidgetViewModel
            {
                Widgets = filteredWidgetsByPermission,
                DashboardName = dashboardName,
                PageId = pageId
            };

            return PartialView("~/Areas/Accounting/Views/Shared/Components/CustomizableDashboard/_AddWidgetModal.cshtml", viewModel);
        }

        protected async Task<ActionResult> GetView(string dashboardName)
        {
            var dashboardDefinition = DashboardCustomizationAppService.GetDashboardDefinition(
                new GetDashboardInput
                {
                    DashboardName = dashboardName,
                    Application = AbpZeroTemplateDashboardCustomizationConsts.Applications.Mvc
                }
            );

            var userDashboard = await DashboardCustomizationAppService.GetUserDashboard(new GetDashboardInput
                {
                    DashboardName = dashboardName,
                    Application = AbpZeroTemplateDashboardCustomizationConsts.Applications.Mvc
                }
            );

            // Show only view defined widgets
            foreach (var userDashboardPage in userDashboard.Pages)
            {
                userDashboardPage.Widgets = userDashboardPage.Widgets
                    .Where(w => DashboardViewConfiguration.WidgetViewDefinitions.ContainsKey(w.WidgetId)).ToList();
            }

            dashboardDefinition.Widgets = dashboardDefinition.Widgets.Where(dw => userDashboard.Pages.Any(p => p.Widgets.Select(w => w.WidgetId).Contains(dw.Id))).ToList();

            return View("~/Areas/Accounting/Views/Shared/Components/CustomizableDashboard/Index.cshtml",
                new CustomizableDashboardViewModel(
                    dashboardDefinition,
                    userDashboard)
                );
        }
    }
}