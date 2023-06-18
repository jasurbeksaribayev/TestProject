using TestProject.Data.GenericRepositories;
using TestProject.Data.IGenericRepositories;
using TestProject.Domain.Entities;
using TestProject.Service.IServices.Students;
using TestProject.Service.Services.Students;

namespace TestProject.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomerService(this IServiceCollection services)
        {
            // GenericRepositories
            services.AddScoped<IGenericRepository<Student>, GenericRepository<Student>>();


            // Services
            services.AddScoped<IStudentService, StudentService>();
        }
    }
}