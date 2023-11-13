using FileServices;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingServices;

namespace ManagementServices
{
    public class OrderService : IOrderService
    {
        private readonly IFileService<PurchasedArticle> _fileService;
        private readonly ILoggingService _loggingService;

        public OrderService(IFileService<PurchasedArticle> fileService)
        {
            _fileService = fileService;
        }

        public async Task AddOrderAsync(PurchasedArticle order)
        {
            var orders = await _fileService.LoadAsync();
            var orderList = new List<PurchasedArticle>(orders) { order };
            await _fileService.SaveAsync(orderList);
          
        }
    }
}
