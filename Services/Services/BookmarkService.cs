using AutoMapper;
using Data;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Services
{
    public class BookmarkService : ReadLaterService, IBookmarkService
    {
        private ReadLaterDataContext _ReadLaterDataContext;        

        private readonly ILogger<BookmarkService> _logger;

        IMapper autoMapper;

        public BookmarkService(ReadLaterDataContext readLaterDataContext, IHttpContextAccessor httpContextAccessor, ILogger<BookmarkService> logger)
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

        public List<CategoryModel> GetCategoriesList()
        {
            try
            {
                var userUid = GetAuthenticatedUserUid();
                var categories = autoMapper.Map<List<CategoryModel>>(_ReadLaterDataContext.Categories.Where(c => c.UserId.Equals(userUid)).ToList());

                if (categories == null)
                    return new List<CategoryModel>();
                else
                    return categories;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("CustomException");
            }

        }

        public Bookmark CreateBookmark(BookmarkModel bookmarkModel)
        {
            try
            {
                var bookmark = autoMapper.Map<Bookmark>(bookmarkModel);

                bookmark.CreateDate = DateTime.UtcNow;
                bookmark.UserId = GetAuthenticatedUserUid();

                _ReadLaterDataContext.Add(bookmark);
                _ReadLaterDataContext.SaveChanges();

                return bookmark;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("CustomException");
            }
        }

        public void DeleteBookmark(int Id)
        {
            try
            {
                var userUid = GetAuthenticatedUserUid();
                var bookmark = _ReadLaterDataContext.Bookmark.Where(b => b.ID == Id).FirstOrDefault();

                if (bookmark != null && bookmark.UserId.Equals(userUid))
                {
                    _ReadLaterDataContext.Bookmark.Remove(bookmark);
                    _ReadLaterDataContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Forbidden action");
                }
            }

            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("CustomException");
            }
}

        public BookmarkModel GetBookmark(int Id)
        {
            try
            {
                var userUid = GetAuthenticatedUserUid();

                var bookmark = _ReadLaterDataContext.Bookmark.Where(b => b.ID == Id && userUid.Equals(userUid)).FirstOrDefault();

                return autoMapper.Map<BookmarkModel>(bookmark);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("CustomException");
            }


        }

        public List<BookmarkModel> GetBookmarks(string description)
        {
            try
            {
                var userUid = GetAuthenticatedUserUid();

                var bookmarks = _ReadLaterDataContext.Bookmark
                    .Where(b => b.ShortDescription.Contains(description) || b.URL.Contains(description) && b.User.Equals(userUid))
                    .OrderBy(b => b.ShortDescription)
                    .ThenBy(b => b.CreateDate)
                    .ToList();

                if (bookmarks != null)
                    return autoMapper.Map<List<BookmarkModel>>(bookmarks);
                else
                    return new List<BookmarkModel>();                
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("CustomException");
            }
        }

        public List<BookmarkModel> GetBookmarks()
        {
            try
            {
                var userUid = GetAuthenticatedUserUid();

                return autoMapper.Map<List<BookmarkModel>>(_ReadLaterDataContext.Bookmark.ToList());
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("CustomException");
            }
        }
        public List<BookmarkModel> GetBookmarksByCategory(int bookmarkId)
        {
            try
            {
                var userUid = GetAuthenticatedUserUid();

                return autoMapper.Map<List<BookmarkModel>>(_ReadLaterDataContext.Bookmark.Where(b => b.CategoryId == bookmarkId && b.UserId.Equals(userUid)).ToList());
 
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("CustomException");
            }
        }

        public void UpdateBookmark(BookmarkModel bookmarkModel)
        {
            try
            {
                var userUid = GetAuthenticatedUserUid();

                var bookmark = autoMapper.Map<Bookmark>(bookmarkModel);

                if (bookmark != null & bookmark.UserId.Equals(userUid))
                {
                    _ReadLaterDataContext.Update(bookmark);
                    _ReadLaterDataContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Forbidden action");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("CustomException");
            }
        }

        public void SaveClick(int bookmarkId)
        {
            try
            {
                var bookmark = _ReadLaterDataContext.Bookmark.Where(b => b.ID == bookmarkId).SingleOrDefault();

                if (bookmark != null)
                {
                    var userBookmarkClick = new UserBookmarkClick()
                    {
                        BookmarkId = bookmark.ID,
                        UserId = bookmark.UserId,
                        ClickedOn = DateTime.UtcNow,
                    };

                    _ReadLaterDataContext.UserBookmarkClicks.Add(userBookmarkClick);
                    _ReadLaterDataContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("CustomException");
            }

        }

        public void SaveClick(string extLink)
        {
            try
            {
                var bookmark = _ReadLaterDataContext.Bookmark.Where(b => b.URL == extLink).SingleOrDefault();

                if (bookmark != null)
                {
                    var userBookmarkClick = new UserBookmarkClick()
                    {
                        BookmarkId = bookmark.ID,
                        UserId = bookmark.UserId,
                        ClickedOn = DateTime.UtcNow,
                        ExternalClick = true
                    };

                    _ReadLaterDataContext.UserBookmarkClicks.Add(userBookmarkClick);
                    _ReadLaterDataContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("CustomException");
            }

        }

    }
}
