using System;
using System.Collections.Generic;
using System.Text;

namespace MSIPoC1Mobile
{
    public class Response
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
