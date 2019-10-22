using System.Text.Json;
using Assignment_3_Network.RDJPT;
using Assignment_3_Network.RDJPT.Data;

namespace Assignment_3_Network
{
    class Program
    {
        static void Main(string[] args)
        {
            //var request = new
            //{
            //    Method = "update",
            //    Path = "/api/categories/1",
            //    Date = "03/09/19",
            //    Body = (new { cid = 1, name = "BeveragesTesting" }).ToJson()
            //};
            //Console.WriteLine(request.Method);
            var apiDate = new APIData();
            var tcpServer = new TcpServer();
        }
    }

    public static class Utils
    {
        public static string ToJson(this object data)
        {
            return JsonSerializer.Serialize(data, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}
