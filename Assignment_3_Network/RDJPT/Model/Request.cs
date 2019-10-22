using System;
using Newtonsoft.Json;

namespace Assignment_3_Network.RDJPT
{
    public class Request
    {
        public Method? Method { get; set; }
        public string Path { get; set; }
        public string Date { get; set; }
        public dynamic Body { get; set; }   
    }
}
