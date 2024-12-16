using System;

namespace RealNest.Web.Models.Foundations.Newss
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string NewsPicture { get; set; }
        public DateTimeOffset CreatedDate=DateTimeOffset.Now;
    }
}
