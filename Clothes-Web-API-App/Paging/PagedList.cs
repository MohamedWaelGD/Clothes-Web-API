using AutoMapper;

namespace Clothes_Web_API_App.Paging
{
    public class PagedList<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public ICollection<T>? items { get; set; }

        public PagedList(ICollection<T> items, int currentPage, int pageSize, int totalCount)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            this.items = items;
        }

        public static PagedList<T> GetPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, pageNumber, pageSize, count);
        }

        public static PagedList<T> CopyPagedList<J>(PagedList<J> otherPagedList, IMapper mapper)
        {
            var data = mapper.Map<ICollection<T>>(otherPagedList.items);

            var pagedList = new PagedList<T>(data, otherPagedList.CurrentPage, otherPagedList.PageSize, otherPagedList.TotalCount);

            return pagedList;
        }
    }
}
