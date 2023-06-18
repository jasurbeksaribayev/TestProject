namespace TestProject.Service.Exceptions
{
    public class TestProjectException : Exception
    {
        public int Code { get; set; }
        public TestProjectException(int code, string message) : base(message)
        {
            Code = code;
        }
    }
}
