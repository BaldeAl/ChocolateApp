using Core;
using FileServices;
using LoggingServices;
using ManagementServices;
using Models;
using System.Text;

class Program
{

    static async Task Main(string[] args)
    {
        
        var fileService = new JsonFileService<Article>("articles.json");
        var fileOrder = new JsonFileService<PurchasedArticle>("order.json");
        IArticleService articleService = new ArticleService(fileService);
        IOrderService orderService = new OrderService(fileOrder);
        ILoggingService loggingService = new FileLoggingService("log.txt");
        
        // Affichage du menu et gestion des choix de l'utilisateur
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n1. Administrateur");
            Console.WriteLine("2. Utilisateur");
            Console.WriteLine("3. Quitter");
            Console.Write("Entrez votre choix : ");
            var choice = Console.ReadKey().KeyChar;
            Console.WriteLine();

            switch (choice)
            {
                case '1':
                    await Admin(loggingService);
                    break;
                case '2':
                    await User(loggingService);
                    break;
                case '3':
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Choix non valide.");
                    break;
            }
        }

    }
    private readonly ILoggingService _loggingService;

    private static async Task AddArticleAsync(IArticleService articleService)
    {

        Console.WriteLine("Ajout d'un nouvel article");

        Console.Write("Référence de l'article : ");
        string reference = Console.ReadLine();

        Console.Write("Prix de l'article : ");
        float price = float.Parse(Console.ReadLine()); 

        Article newArticle = new Article
        {
            Id = Guid.NewGuid(),
            Reference = reference,
            Price = price
        };

        await articleService.AddArticleAsync(newArticle);
        Console.WriteLine("Article ajouté avec succès.");
    }
    private static async Task ShowArticlesAsync(IArticleService articleService)
    {
        var articles = await articleService.GetAllArticlesAsync();
        Console.WriteLine("Articles disponibles :");
        foreach (var article in articles)
        {
            Console.WriteLine($"ID: {article.Id}, Référence: {article.Reference}, Prix: {article.Price}");
        }
    }
    private  static async Task Admin(ILoggingService loggingService)
    {
        var fileService = new JsonFileService<Article>("articles.json");
        IArticleService articleService = new ArticleService(fileService);
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n1. Ajouter un nouvel article");
            Console.WriteLine("2. Afficher les articles");
            Console.WriteLine("3. Quitter");
            Console.Write("Entrez votre choix : ");
            var choice = Console.ReadKey().KeyChar;
            Console.WriteLine();

            switch (choice)
            {
                case '1':
                    await AddArticleAsync(articleService);
                    break;
                case '2':
                    await ShowArticlesAsync(articleService);
                    break;
                case '3':
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Choix non valide.");
                    break;
            }
        }
    }
    
    private static async Task User(ILoggingService loggingService)
    {
        var fileService = new JsonFileService<Article>("articles.json");
        var fileOrder = new JsonFileService<PurchasedArticle>("order.json");
        IArticleService articleService = new ArticleService(fileService);
        IOrderService orderService = new OrderService(fileOrder);
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n1. Passer une commande");
            Console.WriteLine("2. Quitter");
            Console.Write("Entrez votre choix : ");
            var choice = Console.ReadKey().KeyChar;
            Console.WriteLine();

            switch (choice)
            {
                case '1':
                    var buyer = await CollectBuyerInformationAsync();
                    var purchasedArticle = await SelectAndOrderArticleAsync(articleService);
                    await PlaceOrderAsync(orderService, purchasedArticle);
                    break;
                case '2':
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Choix non valide.");
                    break;
            }
        }
    }
    
    private static async Task<Buyer> CollectBuyerInformationAsync()
    {
        Console.WriteLine("Entrez vos informations :");

        Console.Write("Nom : ");
        string lastName = Console.ReadLine();

        Console.Write("Prénom : ");
        string firstName = Console.ReadLine();

        Console.Write("Adresse : ");
        string address = Console.ReadLine();

        Console.Write("Téléphone : ");
        int phone = int.Parse(Console.ReadLine());

        return new Buyer
        {
            Id = Guid.NewGuid(),
            Name = lastName,
            FirstName = firstName,
            Address = address,
            Telephone = phone
        };
    }

    private static async Task<PurchasedArticle> SelectAndOrderArticleAsync(IArticleService articleService)
    {
        await ShowArticlesAsync(articleService); // cette méthode affiche les articles avec leur ID

        Console.Write("Entrez l'ID de l'article que vous souhaitez commander : ");
        Guid articleId = Guid.Parse(Console.ReadLine()); // Gestion des erreurs de formatage

        Console.Write("Entrez la quantité désirée : ");
        int quantity = int.Parse(Console.ReadLine()); // Gestion des erreurs de formatage

        return new PurchasedArticle
        {
            BuyerId = Guid.NewGuid(), // R l'ID de l'acheteur 
            ArticleId = articleId,
            Quantity = quantity,
            PurchaseDate = DateTime.Now
        };
    }

    private static async Task PlaceOrderAsync(IOrderService orderService, PurchasedArticle purchasedArticle)
    {
        await orderService.AddOrderAsync(purchasedArticle);
        Console.WriteLine("Commande passée avec succès.");
    }






}
