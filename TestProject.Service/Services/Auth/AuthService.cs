using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestProject.Data.IGenericRepositories;
using TestProject.Domain.Entities;
using TestProject.Service.Exceptions;
using TestProject.Service.IServices.Auth;

namespace TestProject.Service.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<Student> studentRepository;
        private readonly IConfiguration configuration;

        public AuthService(IGenericRepository<Student> studentRepository, IConfiguration configuration)
        {
            this.studentRepository = studentRepository;
            this.configuration = configuration;
        }
        public async ValueTask<string> GenerateToken(string email)
        {
            Student student = await studentRepository.GetAsync(u =>
                u.Email == email);

            if (student is null)
                throw new TestProjectException(400, "Email is incorrect");

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            byte[] tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);

            SecurityTokenDescriptor tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", student.Id.ToString()),
                    new Claim(ClaimTypes.Role, student.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddMonths(int.Parse(configuration["JWT:Lifetime"])),
                Issuer = configuration["JWT:Issuer"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }
    }
}

