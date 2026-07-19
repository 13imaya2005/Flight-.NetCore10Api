using System.Collections.Generic;

namespace Flight_API.Dto
{
    public class PagedResultDto<T>
    {
        public IEnumerable<T> Data { get; set; } = new List<T>();

        public int TotalRecords { get; set; }
    }
}