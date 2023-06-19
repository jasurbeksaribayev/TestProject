using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Service.IServices.Auth
{
    public interface IAuthService
    {
        ValueTask<string> GenerateToken(string email);
    }
}
