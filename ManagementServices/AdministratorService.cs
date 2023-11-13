using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using FileServices;

namespace ManagementServices
{
    public class AdministratorService : IAdministratorService
    {
        private readonly IFileService<Administrator> _fileService;

        public AdministratorService(IFileService<Administrator> fileService)
        {
            _fileService = fileService;
        }

        public async Task<bool> ValidateCredentialsAsync(string login, string password)
        {
            var administrators = await _fileService.LoadAsync();

            return administrators.Any(admin =>
                admin.Login == login && admin.Password == password);
        }
    }
}
