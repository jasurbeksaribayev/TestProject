using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Service.Exceptions
{
    public class TestProjectException : Exception
    {
        public int Code { get; set; }
        public TestProjectException(int code , string message) : base(message)
        {
            Code = code;
        }
    }
}
