using AutoMapper;
using Data;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class DashboardService : ReadLaterService, IDashboardService
    {
        private ReadLaterDataContext _ReadLaterDataContext;

        private readonly ILogger<BookmarkService> _logger;

        IMapper autoMapper;

        public DashboardService(ReadLaterDataContext readLaterDataContext, IHttpContextAccessor httpContextAccessor, ILogger<BookmarkService> logger)
            : base(httpContextAccessor)
        {
            _ReadLaterDataContext = readLaterDataContext;
            _logger = logger;

            var autoMapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BookmarkModel, Bookmark>().ReverseMap();
                cfg.CreateMap<CategoryModel, Category>().ReverseMap();
            });
            autoMapper = autoMapperConfiguration.CreateMapper();
        }

        public DashboardModel GetDashboardData()
        {
            try
            {
                var userUid = GetAuthenticatedUserUid();

                var trendingCategories = _ReadLaterDataContext.UserBookmarkClicks
                    .Where(x => x.ClickedOn >= DateTime.UtcNow.AddDays(-7))
                    .GroupBy(x => x.Bookmark.Category.Name)
                    .OrderBy(x => x.Count())
                    .Take(5)
                    .Select(x => x.Key)
                    .ToList();


                var favoriteLinks = _ReadLaterDataContext.UserBookmarkClicks
                    .Where(x => x.UserId.Equals(userUid))
                    .GroupBy(x => x.Bookmark.ShortDescription)
                    .OrderBy(x => x.Count())
                    .Take(5)
                    .Select(x => new BookmarkDashboardModel
                    {
                        Url = x.Key,
                        ClicksCount = x.Count()
                    })
                    .ToList();

                var dashboarModel = new DashboardModel();

                dashboarModel.FavoriteLinks = favoriteLinks ?? new List<BookmarkDashboardModel>();
                dashboarModel.TrendingCategories = trendingCategories ?? new List<string>();

                return dashboarModel;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("CustomException");
            }

        }
    }
}
