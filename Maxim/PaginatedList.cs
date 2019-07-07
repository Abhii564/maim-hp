using maximFinal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maximFinal
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }


        public IDictionary<string, string> brandResults = new Dictionary<string, string>();
        public IDictionary<string, string> catResults = new Dictionary<string, string>();
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize, IDictionary<string, string> BrandR, IDictionary<string, string> CatR)
        {
            
            brandResults = BrandR;
            catResults = CatR;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<PaginatedList<SearchModel>> CreateAsync(IQueryable<SearchModel> source, int pageIndex, int pageSize, IDictionary<string,string> brandResults, IDictionary<string, string> catResults)
        {

            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<SearchModel>(items, count, pageIndex, pageSize, brandResults, catResults);
        }


    }
}
