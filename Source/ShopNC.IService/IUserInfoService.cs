﻿using ShopNC.Entity.Mapping;
using ShopNC.IService.Cond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNC.IService
{
    public partial interface IUserInfoService:IBaseService<UserInfo>
    {
        Task<IQueryable<UserInfo>> GetPageUserInfo(UserInfoCond cond);
    }
}
