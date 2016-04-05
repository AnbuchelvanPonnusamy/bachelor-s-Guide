using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomGuideDataLayer
{
    using System.ComponentModel;

    public enum RGResult
    {
        [Description("Sucess")]
        Success,

        [Description("Failure")]
        Failure
    }

    public class RGResponse<T>
    {
        public RGResponse(RGResult res, T data, string errMsg)
        {
            this.Result = res;
            this.Data = data;
            this.ErrorMessage = errMsg;
        }

        public RGResponse(T data)
        {
            this.Result = RGResult.Failure;
            this.Data = data;
            this.ErrorMessage = string.Empty;
        }

        public RGResult Result { get; set; }

        public T Data { get; set; }

        public string ErrorMessage { get; set; }
    }
}
