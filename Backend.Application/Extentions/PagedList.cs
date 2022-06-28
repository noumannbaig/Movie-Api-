using Backend.Application.DataTransferObjects.Shared;
using Backend.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Extentions
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> items, int count, RequestPageParameter pageParams)
        {
            CurrentPage = pageParams.PageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageParams.PageSize);
            PageSize = pageParams.PageSize;
            TotalCount = count;
            AddRange(items);
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public static PagedList<T> CreateAsync(IQueryable<T> source, RequestPageParameter pageParams)
        {
            if (source == null) throw new ArgumentNullException("source");
            int count = source.Count();

            // Create a parameter to pass into the Lambda expression
            var parameter = Expression.Parameter(typeof(T), "Entity");

            //  create the selector part, but support child properties (it works without . too)
            String[] childProperties = pageParams.OrderBy.Split('.');
            MemberExpression property = Expression.Property(parameter, childProperties[0]);
            for (int i = 1; i < childProperties.Length; i++)
            {
                property = Expression.Property(property, childProperties[i]);
            }

            LambdaExpression selector = Expression.Lambda(property, parameter);

            string methodName = (pageParams.OrderType.ToUpper() == "DESC") ? "OrderByDescending" : "OrderBy";

            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName,
                                            new Type[] { source.ElementType, property.Type },
                                            source.Expression, Expression.Quote(selector));

            var res = source.Provider.CreateQuery<T>(resultExp);

            var items = res.Skip((pageParams.PageNumber - 1) * pageParams.PageSize)
                .Take(pageParams.PageSize);

            return new PagedList<T>(items, count, pageParams);
        }


        public static PaginationResponseModel GetPaginationResponse(IQueryable<T> source, RequestPageParameter pageParams)
        {
            if (source == null) throw new ArgumentNullException("source");
            int count = source.Count();

            if (!string.IsNullOrEmpty(pageParams.OrderBy))
            {

                // Create a parameter to pass into the Lambda expression
                var parameter = Expression.Parameter(typeof(T), "Entity");

                //  create the selector part, but support child properties (it works without . too)
                String[] childProperties = pageParams.OrderBy.Split('.');
                MemberExpression property = Expression.Property(parameter, childProperties[0]);
                for (int i = 1; i < childProperties.Length; i++)
                {
                    property = Expression.Property(property, childProperties[i]);
                }

                LambdaExpression selector = Expression.Lambda(property, parameter);

                string methodName = (pageParams.OrderType == "DESC") ? "OrderByDescending" : "OrderBy";

                MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName,
                                                new Type[] { source.ElementType, property.Type },
                                                source.Expression, Expression.Quote(selector));

                source = source.Provider.CreateQuery<T>(resultExp);
            }

            var items = source.Skip((pageParams.PageNumber - 1) * pageParams.PageSize)
                .Take(pageParams.PageSize);
            return new PaginationResponseModel
            {
                Items = items.ToList(),
                CurrentPage = pageParams.PageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageParams.PageSize),
                PageSize = pageParams.PageSize,
                TotalCount = count
            };
        }
    }
}
