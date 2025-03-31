using Microsoft.EntityFrameworkCore;
using RealNest.Web.Models.Foundations.Newss;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealNest.Web.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<News> Newss { get; set; }

        public async ValueTask<News> InsertNewsAsync(News news) =>
            await InsertAsync(news);

        public async ValueTask<IQueryable<News>> SelectAllNewssAsync() =>
            await SelectAllAsync<News>();

        public async ValueTask<News> SelectNewsByIdAsync(int newsId) =>
            await SelectAsync<News>(newsId);

        public async ValueTask<News> UpdateNewsAsync(News news) =>
            await UpdateAsync(news);

        public async ValueTask<News> DeleteNewsAsync(News news) =>
            await DeleteAsync(news);
    }
}
