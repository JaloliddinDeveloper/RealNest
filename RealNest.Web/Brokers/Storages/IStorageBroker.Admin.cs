using RealNest.Web.Models.Foundations.Admins;
using RealNest.Web.Models.Foundations.Houses;
using System.Threading.Tasks;

namespace RealNest.Web.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Admin> InsertAdminAsync(Admin admin);
        ValueTask<Admin> DeleteAdminAsync(Admin admin);
        ValueTask<Admin> SelectAdminByEmailAsync(string adminName);
    }
}
