using Microsoft.EntityFrameworkCore;
using RealNest.Web.Models.Foundations.Admins;
using System.Threading.Tasks;

namespace RealNest.Web.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<Admin> InsertAdminAsync(Admin admin)=>
            await InsertAsync(admin);

        public async ValueTask<Admin> SelectAdminByEmailAsync(string adminName)
        {
            using var broker = new StorageBroker(this.configuration);
            return await broker.Admins.FirstOrDefaultAsync(u => u.AdminName == adminName);
        }
    }
}
