using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopNC.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopNC.IService;
using ShopNC.Entity.Mapping;
using Autofac;
namespace ShopNC.Service.Tests
{
    [TestClass()]
    public class BaseServiceTests
    {
       
        IUserInfoService bll = new UserInfoService();
        IPermissionService permissionBLL = new PermissionService();

        [TestMethod()]
        public void AddEntityTest()
        {
            var permission=new Permission(){Name="测试",Path="/Admin/User/Add"};
            permissionBLL.AddEntity(permission);

            var group = new PermissionGroup { Name = "管理员", 
                Permissions = new  List<Permission>{
                    permission,
                    new Permission(){Name="添加管理员",Path="/Admin/User/Add"}
                } 
            };

            UserRole role = new UserRole()
            {
                CNName = "Admin",
                ENName = "普通管理员",
                PermissionGroups = new List<PermissionGroup>() { group}
            };

            UserInfo user = new UserInfo()
            {
                UserName = "test1",
                Password = "111111",
                Roles = new List<UserRole>() { role },
                Permissions=new List<Permission>(){permission}
            };
            bll.AddEntity(user);

            Assert.IsTrue(user.ID > 0);
        }

        [TestMethod()]
        public void BaseServiceTest()
        {
            Assert.Fail();
        }
        [TestMethod()]
        public void UpdateEntityTest()
        {
            var user = bll.LoadEntity(p=>true).FirstOrDefault();
            user.TrueName = "edite";
            var result= bll.UpdateEntity(user);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void DeleteEntityTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void LoadEntityTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void LoadPageEntityTest()
        {
            Assert.Fail();
        }
    }
}
