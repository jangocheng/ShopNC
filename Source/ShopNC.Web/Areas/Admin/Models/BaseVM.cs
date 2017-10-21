using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopNC.Web.Areas.Admin.Models
{
    public class BaseVM
    {
        /// <summary>
        /// 逻辑上的删除，非物理删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}