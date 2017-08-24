using System.Collections.Generic;

namespace App.Core.Interface.Data
{
    public interface IPagedList<T> : IList<T>
    {
        int PageNumber { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
    }
}
