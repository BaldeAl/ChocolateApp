using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementServices
{
    public interface IBillingService
    {
        Task GenerateTotalSalesReportAsync(string reportFilePath);
        Task GenerateSalesReportByBuyerAsync(string reportFilePath);
        Task GenerateSalesReportByDateAsync(string reportFilePath, DateTime date);
    }
}
