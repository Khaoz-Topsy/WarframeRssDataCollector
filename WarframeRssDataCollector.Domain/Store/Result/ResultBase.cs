using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarframeRssDataCollector.Domain.Store.Result
{
    public class ResultBase<T>
    {
        public ResultBase(T Result, bool Success, string Exception)
        {
            this.Result = Result;
            this.Success = Success;
            this.ExceptionString = Exception;
        }

        public T Result { get; set; }
        public bool Success { get; set; }
        public string ExceptionString { get; set; }
    }
}
