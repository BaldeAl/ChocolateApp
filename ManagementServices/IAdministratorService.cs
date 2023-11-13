using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementServices
{
    public interface IAdministratorService
    {
        Task<bool> ValidateCredentialsAsync(string login, string password);
    }
}
