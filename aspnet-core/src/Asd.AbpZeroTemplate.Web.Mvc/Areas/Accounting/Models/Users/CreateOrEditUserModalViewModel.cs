﻿using System.Linq;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Asd.AbpZeroTemplate.Authorization.Users.Dto;
using Asd.AbpZeroTemplate.Security;
using Asd.AbpZeroTemplate.Web.Areas.Accounting.Models.Common;

namespace Asd.AbpZeroTemplate.Web.Areas.Accounting.Models.Users
{
    [AutoMapFrom(typeof(GetUserForEditOutput))]
    public class CreateOrEditUserModalViewModel : GetUserForEditOutput, IOrganizationUnitsEditViewModel
    {
        public bool CanChangeUserName => User.UserName != AbpUserBase.AdminUserName;

        public int AssignedRoleCount
        {
            get { return Roles.Count(r => r.IsAssigned); }
        }

        public bool IsEditMode => User.Id.HasValue;

        public PasswordComplexitySetting PasswordComplexitySetting { get; set; }
    }
}