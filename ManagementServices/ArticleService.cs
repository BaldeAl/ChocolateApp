using FileServices;
using LoggingServices;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementServices
{
    public class ArticleService : IArticleService
    {
        private readonly IFileService<Article> _fileService;
        private readonly ILoggingService _loggingService;
        public ArticleService(IFileService<Article> fileService)
        {
            _fileService = fileService;
        }

        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            return await _fileService.LoadAsync();
        }

        public async Task AddArticleAsync(Article article)
        {
            var articles = await _fileService.LoadAsync();
            var articleList = new List<Article>(articles) { article };
            await _fileService.SaveAsync(articleList);
            
        }
        public async Task<Article> GetArticleByIdAsync(Guid id)
        {
            var articles = await _fileService.LoadAsync();
            return articles.FirstOrDefault(a => a.Id == id);
        }
    }
}
