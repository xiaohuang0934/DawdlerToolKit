namespace ToolKit.Commons.BaseResults
{
    /// <summary>
    /// 普通业务统一返回
    /// </summary>
    public class BaseResult
    {
        /// <summary>
        /// 自定义编码与标题
        /// </summary>
        /// <param name="result"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public BaseResult(object result, int? code = null, string message = "")
        {
            this.Result = result; this.Code = code; this.Message = message;
        }

        /// <summary>
        /// 普通业务成功
        /// </summary>
        /// <param name="result"></param>
        public BaseResult(object result)
        {
            this.Code = 200;
            this.Message = "请求成功";
            this.Result = result;
        }

        /// <summary>
        /// 普通业务异常
        /// </summary>
        /// <param name="result"></param>
        public BaseResult(string message)
        {
            this.Code = 500;
            this.Message = message;
        }

        public int? Code { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }
    }
}
