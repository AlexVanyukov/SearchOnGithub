using System.Runtime.InteropServices;

namespace Logic.Entities
{
    public class PageList<T>
    {
        private PageList(List<T> items, int page, int pageSize, int totalCount)
        {
            Items = items;
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public List<T> Items { get; set; }
        public int Page {  get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasNExtPage => Page * PageSize < TotalCount;
        public bool HasPreviouspage => PageSize > 1;

        public static PageList<T> Create(IQueryable<T> query, int page, int pageSize)
        {
            int totalCount = query.Count();
            var items = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new(items, page, pageSize, totalCount);
        }
    }
}
