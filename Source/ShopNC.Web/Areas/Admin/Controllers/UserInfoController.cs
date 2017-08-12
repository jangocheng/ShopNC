using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ShopNC.IService;
using ShopNC.IService.Cond;
using Newtonsoft.Json;
using ShopNC.Entity.Mapping;
using ShopNC.Common;
using System.Linq.Expressions;
using ShopNC.Web.Areas.Admin.Models;

namespace ShopNC.Web.Areas.Admin.Controllers
{
    public class UserInfoController : BaseController
    {
        IUserInfoService userBll;
        public UserInfoController(IUserInfoService bll)
        {
            this.userBll = bll;
        }
        //
        // GET: /Admin/AdminUser/
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetPageUserInfo(UserInfoCond cond)
        {
            Expression<Func<UserInfo, bool>> whereLambda = p => true;
            if (!string.IsNullOrWhiteSpace(cond.UserName))
            {
                whereLambda = whereLambda.And(p => p.UserName.Contains(cond.UserName));
            }
            if (!string.IsNullOrWhiteSpace(cond.Email))
            {
                whereLambda = whereLambda.And<UserInfo>(p => p.Email.Contains(cond.Email));
            }

           // var data =await userBll.GetPageUserInfo(cond);
            int count=0;
            var data = await userBll.LoadPageEntityAsync(whereLambda, cond.Page, cond.Rows,out count, p => p.CreateTime, false);
            var result = new { total = cond.TotalRecord, rows =data };
            return Json(result);
            
        }

        public async Task<ActionResult> Edite(int id) 
        {
            var model = (await userBll.LoadEntityAsync(p => p.ID ==id)).FirstOrDefault();
            return View(model.MapTo<UserInfoVM>());
        }

        [HttpPost]
        public  JsonResult Edite(UserInfoVM user) 
        {
            var model = user.MapTo<UserInfo>();
            try
            {
                var result = userBll.UpdateEntity(model);

                if (result)
                {
                    return Json(new Form { Code = 1, Message = "修改成功!" });
                }
                else
                {
                    return Json(new Form { Code = 0, Message = "修改失败!" });
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
	}
}