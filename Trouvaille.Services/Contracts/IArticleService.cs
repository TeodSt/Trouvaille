using System.Collections.Generic;
using Trouvaille.Models;

namespace Trouvaille.Services.Contracts
{
    public interface IArticleService
    {
        IEnumerable<Article> GetAllArticles(int currentPage, int maxRows);

        IEnumerable<Article> GetArticlesByUserId(string userId);

        IEnumerable<Article> GetArticlesByContinent(string continentName);

        IEnumerable<Article> GetArticlesByTitle(string title);

        Article GetArticleById(string id);

        void AddArticle(Article article);

        void DeleteArticle(Article article);

        int GetCountOfArticles();
    }
}
