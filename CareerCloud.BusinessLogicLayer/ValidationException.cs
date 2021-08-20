namespace CareerCloud.BusinessLogicLayer
{
    using System;
    public class ValidationException:Exception
    {
        public int Code { get; set; }
        public ValidationException(int code, string message): base(message)
        {
            Code = code;
        }
    }
}
