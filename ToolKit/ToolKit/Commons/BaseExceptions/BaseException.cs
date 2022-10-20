namespace ToolKit.Commons.BaseExceptions
{
    /// <summary>
    /// 普通业务异常实体
    /// </summary>
    public class BaseException : Exception
    {
        /// <summary>
        /// 构造方法 - 无自定义异常码
        /// </summary>
        /// <param name="message"></param>
        public BaseException(string message) : base(message) { }
    }
}
