using FileServices;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementServices
{
    public class BuyerService : IBuyerService
    {
        private readonly IFileService<Buyer> _fileService;

        public BuyerService(IFileService<Buyer> fileService)
        {
            _fileService = fileService;
        }

        public async Task AddBuyerAsync(Buyer buyer)
        {
            var buyers = await _fileService.LoadAsync();
            var buyerList = new List<Buyer>(buyers) { buyer };
            await _fileService.SaveAsync(buyerList);
        }
    }
}
