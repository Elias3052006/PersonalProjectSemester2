using System.Collections.Generic;

namespace LogicLayer.Domain
{
    public class PagedResponse<T>
    {
        public IReadOnlyList<T> Items { get; }
        public int TotalCount { get; }
        public int Page { get; }
        public int PageSize { get; }
        public int TotalPages { get; }

        private PagedResponse(IReadOnlyList<T> items, int totalCount, int page, int pageSize, int totalPages)
        {
            Items = items;
            TotalCount = totalCount;
            Page = page;
            PageSize = pageSize;
            TotalPages = totalPages;
        }

        public static PagedResponse<T> Create(IReadOnlyList<T> allItems, int page, int pageSize)
        {
            int fixedPage = FixPage(page);
            int totalCount = allItems.Count;
            int totalPages = CountPages(totalCount, pageSize);
            List<T> pageItems = CutPage(allItems, fixedPage, pageSize);

            return new PagedResponse<T>(pageItems, totalCount, fixedPage, pageSize, totalPages);
        }

        private static int FixPage(int page)
        {
            if (page < 1)
            {
                return 1;
            }

            return page;
        }

        private static int CountPages(int totalCount, int pageSize)
        {
            int pages = totalCount / pageSize;

            if (pages * pageSize < totalCount)
            {
                pages = pages + 1;
            }

            return pages;
        }

        private static List<T> CutPage(IReadOnlyList<T> items, int page, int pageSize)
        {
            int start = (page - 1) * pageSize;
            int end = start + pageSize;

            List<T> pageItems = new List<T>();

            for (int i = start; i < items.Count; i++)
            {
                if (i >= end)
                {
                    break;
                }

                pageItems.Add(items[i]);
            }

            return pageItems;
        }
    }
}