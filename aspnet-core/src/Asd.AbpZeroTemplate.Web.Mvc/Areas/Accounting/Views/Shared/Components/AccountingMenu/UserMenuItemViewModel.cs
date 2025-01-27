﻿using Abp.Application.Navigation;

namespace Asd.AbpZeroTemplate.Web.Areas.Accounting.Views.Shared.Components.AccountingMenu
{
    public class UserMenuItemViewModel
    {
        public UserMenuItem MenuItem { get; set; }

        public string CurrentPageName { get; set; }

        public int MenuItemIndex { get; set; }

        public int ItemDepth { get; set; }

        public bool RootLevel { get; set; }
        
        public bool IsTabMenuUsed { get; set; }
    }
}
