using System;
using Assignment_3_Network.RDJPT.Helper;
using Assignment_3_Network.RDJPT.Model;
using Newtonsoft.Json;

namespace Assignment_3_Network.RDJPT
{
    public class HandleRequest
    {

        public static Request ConvertRequest(string message)
        {
            var request = JsonConvert.DeserializeObject<Request>(message);
            if (request.Body != null)
            {
                var body = JsonConvert.DeserializeObject<Category>(request.Body);
                request.Body = body;
            }

            return request;
        }

        public static Response ValidateRequest(Request request)
        {
            var response = new Response();

            if (request.Method == Method.read)
            {
                return response;
            }

            else if (request.Method == Method.create)
            {
                return response;
            }

            else if (request.Method == Method.update)
            {
                return response;
            }

            else if (request.Method == Method.delete)
            {
                return response;
            }

            else
            {
                response.Status = ResponseMessage.MissingMethod;
                return response;
            }
        }
    }
}
