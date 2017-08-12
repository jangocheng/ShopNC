using ShopNC.Entity.Mapping;
using ShopNC.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNC.Repository
{
    public partial class UserInfoRepository :BaseRepository<UserInfo>, IUserInfoRepository
    {
        private  IDBContextFactory dbContextFactory = new EFContextFactory();

        DbContext db;
        public UserInfoRepository()
        {
            db = dbContextFactory.GetCurrentContextInstence();

        }
        public async Task<bool> EditeUserInfo(UserInfo user)
        {
            db.Set<UserInfo>().Attach(user);
            db.Entry<UserInfo>(user).Property(p => p.Permissions).IsModified = false;
            db.Entry<UserInfo>(user).Property(p => p.Roles).IsModified = false;
            db.Entry<UserInfo>(user).State = EntityState.Modified;

            return true;

        }
    }
}
