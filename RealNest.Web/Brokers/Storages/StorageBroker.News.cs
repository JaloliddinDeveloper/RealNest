using Microsoft.EntityFrameworkCore;
using RealNest.Web.Models.Foundations.Newss;
using System.Linq;
using System.Threading.Tasks;

namespace RealNest.Web.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<News> Newss { get; set; }

        public async ValueTask<News> InsertNewsAsync(News News) =>
            await InsertAsync(News);

        public async ValueTask<IQueryable<News>> SelectAllNewssAsync() =>
            await SelectAllAsync<News>();

        public async ValueTask<News> SelectNewsByIdAsync(int NewsId) =>
            await SelectAsync<News>(NewsId);

        public async ValueTask<News> UpdateNewsAsync(News News) =>
            await UpdateAsync(News);

        public async ValueTask<News> DeleteNewsAsync(News News) =>
            await DeleteAsync(News);
    }
}
