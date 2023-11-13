using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementServices
{
    public interface IBuyerService
    {
        Task AddBuyerAsync(Buyer buyer);
    }
}
