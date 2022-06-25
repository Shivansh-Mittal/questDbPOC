namespace QuestDbPOC.Models
{
    public class ApiResult
    {
        public string ErrorMessage { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }

        public ApiResult()
        {

        }

        public static ApiResult Error(string error)
        {
            ApiResult result = new ApiResult();
            result.ErrorMessage = error;
            result.Status = "Error";
            result.Message = null;
            return result;
        }

        public static ApiResult Success(string message)
        {
            ApiResult result = new ApiResult();
            result.ErrorMessage = null;
            result.Status = "Success";
            result.Message = message;
            return result;
        }
    }
}
