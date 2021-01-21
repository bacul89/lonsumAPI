using System.Net;
using System.Net.Http;
using ApiTemplate.Commons.Helpers;
using ApiTemplate.Responses;

namespace ApiTemplate.Extensions
{
    public static class ResponseExtensions
    {
        public static HttpResponseMessage ToHttpResponse(this IResponseHandler response)
        {
            var status = response.DidError
                ? HttpStatusCode.InternalServerError
                : response.DidError && response.ErrorMessage.ToLower().Contains("properly")
                    ? HttpStatusCode.NotAcceptable
                    : HttpStatusCode.OK;

            var result = new HttpResponseMessage
            {
                StatusCode = status,
                Content = new ObjectContent<object>(response, new BrowserJsonFormatter(), "application/json")
            };

            return result;
        }

        public static HttpResponseMessage ToHttpResponse<TModel>(this ISingleResponse<TModel> response)
        {
            var status = HttpStatusCode.OK;

            if (response.DidError)
                status = response.Message.ToLower().Contains("incorrect")
                    ? HttpStatusCode.BadRequest
                    : HttpStatusCode.InternalServerError;
            else if (response.Model == null)
                status = HttpStatusCode.NotFound;

            var result = new HttpResponseMessage
            {
                StatusCode = status,
                Content = new ObjectContent<object>(response, new BrowserJsonFormatter(), "application/json")
            };

            return result;
        }

        public static HttpResponseMessage ToHttpResponse<TModel>(this IListResponse<TModel> response)
        {
            var status = HttpStatusCode.OK;

            if (response.DidError)
                status = HttpStatusCode.InternalServerError;
            else if (response.Model == null)
                status = HttpStatusCode.NotFound;


            var result = new HttpResponseMessage
            {
                StatusCode = status,
                Content = new ObjectContent<object>(response, new BrowserJsonFormatter(), "application/json")
            };

            return result;
        }
    }
}