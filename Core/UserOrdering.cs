using ManagementServices;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class UserOrderingProcess
    {
        private readonly IArticleService _articleService;
        private readonly List<PurchasedArticle> _currentOrder;
        private Buyer _currentBuyer;

        public UserOrderingProcess(IArticleService articleService)
        {
            _articleService = articleService;
            _currentOrder = new List<PurchasedArticle>();
        }

        public async Task StartOrderingAsync()
        {
            Console.WriteLine("Entrer votre nom:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Entrer votre prénom:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Entrer votre adresse:");
            string address = Console.ReadLine();

            Console.WriteLine("Entrer votre téléphone:");
            string phone = Console.ReadLine();

            _currentBuyer = new Buyer { Name = lastName, FirstName = firstName, Address = address, Telephone = int.Parse(phone) };

            char key;
            do
            {
                Console.WriteLine("Choisissez un article (ID de l'article) ou appuyez sur 'F' pour finir, 'P' pour voir le prix:");
                var input = Console.ReadLine();

                if (input.Equals("F", StringComparison.OrdinalIgnoreCase)) break;
                if (input.Equals("P", StringComparison.OrdinalIgnoreCase)) { DisplayCurrentTotal(); continue; }

                if (Guid.TryParse(input, out Guid articleId))
                {
                    Console.WriteLine("Entrer la quantité:");
                    if (int.TryParse(Console.ReadLine(), out int quantity))
                    {
                        var article = await _articleService.GetArticleByIdAsync(articleId);
                        if (article != null)
                        {
                            _currentOrder.Add(new PurchasedArticle { ArticleId = article.Id, Quantity = quantity });
                        }
                        else
                        {
                            Console.WriteLine("Article non trouvé.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Quantité non valide.");
                    }
                }
                else
                {
                    Console.WriteLine("ID d'article non valide.");
                }

            } while (true);


            GenerateOrderSummary();
        }

        private void DisplayCurrentTotal()
        {
            float total = 0;
            foreach (var item in _currentOrder)
            {
                var article = _articleService.GetArticleByIdAsync(item.ArticleId).Result;
                total += article.Price * item.Quantity;
            }
            Console.WriteLine($"Total actuel: {total}");
        }


        private void GenerateOrderSummary()
        {
            StringBuilder sb = new StringBuilder();
            float total = 0;
            sb.AppendLine("Récapitulatif de la commande:");
            foreach (var item in _currentOrder)
            {
                var article = _articleService.GetArticleByIdAsync(item.ArticleId).Result;
                total += article.Price * item.Quantity;
                sb.AppendLine($"Article: {article.Reference}, Quantité: {item.Quantity}, Prix: {article.Price}");
            }
            sb.AppendLine($"Prix Total: {total}");

            string fileName = $"{_currentBuyer.Name}-{_currentBuyer.FirstName}-{DateTime.Now:dd-MM-yyyy-HH-mm}.txt";
            string directoryPath = Path.Combine("Orders", _currentBuyer.Name);
            Directory.CreateDirectory(directoryPath);
            File.WriteAllText(Path.Combine(directoryPath, fileName), sb.ToString());
            Console.WriteLine($"La commande a été sauvegardée dans {fileName}");
        }

        private async Task DisplayArticlesAsync()
        {
            var articles = await _articleService.GetAllArticlesAsync();
            Console.WriteLine("Liste des articles disponibles:");
            foreach (var article in articles)
            {
                Console.WriteLine($"ID: {article.Id}, Référence: {article.Reference}, Prix: {article.Price}");
            }
        }

    }
}
