using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class DashboardModel
    {
        public List<string> TrendingCategories { get; set; }

        public List<BookmarkDashboardModel> FavoriteLinks { get; set; }

    }
}
