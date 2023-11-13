using FileServices;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementServices
{
    public class BillingService : IBillingService
    {
        private readonly IFileService<PurchasedArticle> _orderFileService;
        private readonly IFileService<Article> _articleFileService;
        private readonly IFileService<Buyer> _buyerFileService;

        public BillingService(
            IFileService<PurchasedArticle> orderFileService,
            IFileService<Article> articleFileService,
            IFileService<Buyer> buyerFileService)
        {
            _orderFileService = orderFileService;
            _articleFileService = articleFileService;
            _buyerFileService = buyerFileService;
        }

        public async Task GenerateTotalSalesReportAsync(string reportFilePath)
        {
            var orders = await _orderFileService.LoadAsync();
            var articles = await _articleFileService.LoadAsync();

            var report = orders.GroupJoin(
                articles,
                order => order.ArticleId,
                article => article.Id,
                (order, articleCollection) => new { order, articleCollection }
            )
            .SelectMany(
                x => x.articleCollection.DefaultIfEmpty(),
                (x, y) => new { x.order, Article = y }
            )
            .GroupBy(x => x.Article.Id)
            .Select(group => new
            {
                ArticleId = group.Key,
                TotalSales = group.Sum(x => x.Article.Price * x.order.Quantity)
            });

            await WriteReportAsync(report, reportFilePath, "Total Sales Report");
        }

        public async Task GenerateSalesReportByBuyerAsync(string reportFilePath)
        {
            var orders = await _orderFileService.LoadAsync();
            var buyers = await _buyerFileService.LoadAsync();

            var report = from order in orders
                         join buyer in buyers on order.BuyerId equals buyer.Id
                         group order by buyer into buyerGroup
                         select new
                         {
                             BuyerName = $"{buyerGroup.Key.Name} {buyerGroup.Key.FirstName}",
                             TotalPurchases = buyerGroup.Sum(o => o.Quantity)
                         };

            await WriteReportAsync(report, reportFilePath, "Sales Report By Buyer");
        }

        public async Task GenerateSalesReportByDateAsync(string reportFilePath, DateTime date)
        {
            var orders = await _orderFileService.LoadAsync();

            var report = orders
                .Where(order => order.PurchaseDate.Date == date.Date)
                .GroupBy(order => order.PurchaseDate)
                .Select(group => new
                {
                    Date = group.Key,
                    TotalSales = group.Sum(x => x.Quantity)
                });

            await WriteReportAsync(report, reportFilePath, "Sales Report By Date");
        }

        private async Task WriteReportAsync<T>(IEnumerable<T> reportData, string filePath, string reportTitle)
        {
            var sb = new StringBuilder();
            sb.AppendLine(reportTitle);
            sb.AppendLine(new string('-', reportTitle.Length));
            sb.AppendLine();

            foreach (var item in reportData)
            {
                sb.AppendLine(item.ToString());
            }

            await File.WriteAllTextAsync(filePath, sb.ToString());
        }
    }
}
