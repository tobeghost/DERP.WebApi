using System.Collections.Generic;

namespace DERP.WebApi.Domain.Common;

public class PagedResponse<T>
{
    public int TotalCount { get; set; }
    public int TotalPageCount { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public ICollection<T> Items { get; set; }
}