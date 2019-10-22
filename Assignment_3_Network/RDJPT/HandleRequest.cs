using System;
using System.Text.RegularExpressions;
using Assignment_3_Network.RDJPT.Data;
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
                try
                {
                    var body = JsonConvert.DeserializeObject<Category>(request.Body);
                    request.Body = body;
                }
                catch (Exception ex)
                {
                    return request;
                }
            }

            return request;
        }

        public static Response ValidateRequest(Request request)
        {
            var response = new Response();

            response.Status = MissingValues(request);
            if (response.Status.Length > 0) return response;

            response.Status = IllegalValues(request);
            if (response.Status.Length > 0) return response;

            // GET
            if (request.Method == Method.read.ToString())
            {
                // "/api/categories/"
                if (request.Path.Contains(APIData.CategoryPath))
                {
                    // Receive all catefories
                    if (request.Path == APIData.CategoryPath)
                    {
                        response.Status = ResponseMessage.Ok;
                        response.Body = APIData.Categories.ToJson();
                        return response;
                    }
                    // Getting the number from the path if any
                    var categoryId = GetCategoryId(request.Path);
                    if (categoryId == null)
                    {
                        response.Status = ResponseMessage.BadRequest;
                        return response;
                    }
                    // If the number is bigger than the count of the list
                    if (categoryId > APIData.Categories.Count)
                    {
                        response.Status = ResponseMessage.NotFound;
                        return response;
                    }
                    response.Status = ResponseMessage.Ok;
                    response.Body = APIData.Categories[(int)(categoryId - 1)].ToJson();

                    return response;
                }
                response.Status = ResponseMessage.BadRequest;
                return response;
            }

            // CREATE
            else if (request.Method == Method.create.ToString())
            {
                if (request.Path != APIData.CategoryPath)
                {
                    response.Status = ResponseMessage.BadRequest;
                    return response;
                }
                return response;
            }

            // UPDATE
            else if (request.Method == Method.update.ToString())
            {
                if (request.Path.Contains(APIData.CategoryPath))
                {

                    // Getting the number from the path if any
                    var categoryId = GetCategoryId(request.Path);
                    if (categoryId == null)
                    {
                        response.Status = ResponseMessage.BadRequest;
                        return response;
                    }

                    APIData.Categories[(int)categoryId].Name = request.Body.Name;
                    response.Status = ResponseMessage.Updated;
                }
                return response;
            }

            // DELETE
            else if (request.Method == Method.delete.ToString())
            {
                if (request.Path.Contains(APIData.CategoryPath))
                {
                    var categoryId = GetCategoryId(request.Path);
                    if (categoryId == null)
                    {
                        response.Status = ResponseMessage.BadRequest;
                        return response;
                    }
                }
                return response;
            }
                return response;


        }

        public static string MissingValues(Request request)
        {
            var response = "";
            if (request.Method == null)
            {
                response += ResponseMessage.MissingMethod;
            }
            if (request.Path == null)
            {
                response += ResponseMessage.MissingResource;
            }
            if (request.Date == null)
            {
                response += ResponseMessage.MissingDate;
            }
            if (request.Body == null && request.Method != Method.read.ToString() && request.Method != Method.delete.ToString())
            {
                response += ResponseMessage.MissingBody;
            }

            return response;
        }

        public static string IllegalValues(Request request)
        {
            var response = "";
            if (!Enum.IsDefined(typeof(Method), request.Method))
            {
                response += ResponseMessage.IllegalMethod;
            }
            if (!isUnixTimeSeciondFormat(request.Date))
            {
                response += ResponseMessage.IllegalDate;
            }
            if (request.Body != null)
            {
                if (request.Body.GetType() != typeof(Category) && request.Method != Method.read.ToString())
                {
                    response += ResponseMessage.IllegalBody;
                }
            }
            return response;
        }

        public static bool isUnixTimeSeciondFormat(string date)
        {
            if (!long.TryParse(date, out long dateInt)) return false;
            try
            {
                DateTimeOffset.FromUnixTimeSeconds(dateInt);
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
            return true;
        }

        public static int? GetCategoryId(string path)
        {
            string stringNumber = Regex.Replace(path, "[^0-9]", "");
            if (stringNumber.Length < 1) return null;

            int intNumber = int.Parse(stringNumber);
            return intNumber;
        }
    }
}
