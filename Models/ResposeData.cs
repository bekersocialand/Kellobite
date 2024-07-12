using System.Collections.Generic;

namespace Kelloggs.Models
{
    public class ResponseData
    {
        public bool Success { get; set; }
        public bool Send { get; set; }
    }

    public class Parameter
    {
        public string Field { get; set; }
        public string Value { get; set; }
        public object Object { get; set; }
    }

    public class Parameters
    {
        public List<Parameter> Parameter { get; set; }
    }

    public class ResponseJson<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<T> Data { get; set; }
    }
    public class SingleResponseJson<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public class ValidateResponseJson
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}