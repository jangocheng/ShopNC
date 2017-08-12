using ShopNC.Entity.Mapping;
using ShopNC.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNC.Repository
{
    public partial class PermissionRepository:BaseRepository<Permission>,IPermissionRepository
    {
    }
}
