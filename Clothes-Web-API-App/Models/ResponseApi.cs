using AutoMapper;

namespace Clothes_Web_API_App.Models
{
    public class ResponseApi<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;

        public static ResponseApi<T> GenerateErrorMessage(string message, T? data = default)
        {
            return new ResponseApi<T>
            {
                Success = false,
                Message = message,
                Data = data
            };
        }

        public static ResponseApi<T> GenerateSuccessMessage(string message, T? data = default)
        {
            return new ResponseApi<T>
            {
                Message = message,
                Data = data
            };
        }

        public static ResponseApi<T> CopyResponse<J>(ResponseApi<J> otherResponse, IMapper mapper)
        {
            var response = new ResponseApi<T>();
            response.Success = otherResponse.Success;
            response.Message = otherResponse.Message;
            response.Data = mapper.Map<T>(otherResponse.Data);

            return response;
        }
    }
}
