using ManagementServices;
using LoggingServices;
using Models;
using System;

namespace Core
{
    public class ChocolateOrderingCore
    {
        private readonly IArticleService _articleService;
        private readonly IBuyerService _buyerService;
        private readonly IOrderService _orderService;
        private readonly IAdministratorService _administratorService;
        private readonly IBillingService _billingService;
        private readonly ILoggingService _loggingService;


        public ChocolateOrderingCore(
            IArticleService articleService,
            IBuyerService buyerService,
            IOrderService orderService,
            IAdministratorService administratorService,
            IBillingService billingService,
            ILoggingService loggingService)
        {
            _articleService = articleService;
            _buyerService = buyerService;
            _orderService = orderService;
            _administratorService = administratorService;
            _billingService = billingService;
            _loggingService = loggingService;
        }

      
        public async Task<bool> AddArticleAsync(Article article, string adminLogin, string adminPassword)
        {
            // Vérifier les identifiants de l'administrateur
            bool isAdminValid = await _administratorService.ValidateCredentialsAsync(adminLogin, adminPassword);
            if (!isAdminValid)
            {
                await _loggingService.LogAsync("Tentative de l'ajout d'article échouée : Identifiants administrateur invalides.");
                return false;
            }

            await _articleService.AddArticleAsync(article);
            await _loggingService.LogAsync($"Article ajouté par l'administrateur {adminLogin}: {article.Reference}");
            return true;
        }

        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            return await _articleService.GetAllArticlesAsync();
        }

   
    }

}