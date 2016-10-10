using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Angular2Blank.Data.Extensions
{
    public static class QueryableExtensions
    {
        public static Task<T> FirstOrDefaultAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> expr)
        {
            return EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(query, expr);
        }
    }
}
