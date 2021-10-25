using Entity;
using Models;
using System.Collections.Generic;

namespace Services
{
    public interface IBookmarkService
    {
        Bookmark CreateBookmark(BookmarkModel bookmark);
        List<BookmarkModel> GetBookmarks();
        BookmarkModel GetBookmark(int Id);
        List<BookmarkModel> GetBookmarks(string description);
        List<BookmarkModel> GetBookmarksByCategory(int categoryId);
        void UpdateBookmark(BookmarkModel bookmark);
        void DeleteBookmark(int Id);
        public List<CategoryModel> GetCategoriesList();
        public void SaveClick(int bookmarkId);
        public void SaveClick(string extLink);

    }
}
