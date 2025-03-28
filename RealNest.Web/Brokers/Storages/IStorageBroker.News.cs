using RealNest.Web.Models.Foundations.Newss;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealNest.Web.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<News> InsertNewsAsync(News News);
        ValueTask<IQueryable<News>> SelectAllNewssAsync();
        ValueTask<News> SelectNewsByIdAsync(int NewsId);
        ValueTask<News> UpdateNewsAsync(News News);
        ValueTask<News> DeleteNewsAsync(News News);
        Task<List<News>> SelectAllNewssAsyncOrderBy();
    }
}
