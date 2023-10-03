using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class LoginResponse
    {
        public bool IsError { get; set; }
        public string Message { get; set; }
    }
}