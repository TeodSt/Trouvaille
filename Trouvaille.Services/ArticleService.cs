using System.Collections.Generic;
using Bytes2you.Validation;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services.Contracts;
using System;
using System.Linq;

namespace Trouvaille.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IEfGenericRepository<Article> articleRepository;
        private readonly IUnitOfWork unitOfWork;

        public ArticleService(IEfGenericRepository<Article> articleRepository, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(articleRepository, "articleRepository").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();

            this.articleRepository = articleRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Article> GetAllArticles(int page, int maxRows)
        {
            IEnumerable<Article> articles = this.articleRepository.GetAll()
                                         .Skip((page - 1) * maxRows)
                                         .Take(maxRows);

            return articles;
        }

        public IEnumerable<Article> GetAllArticles()
        {
            IEnumerable<Article> articles = this.articleRepository.GetAll();

            return articles;
        }

        public int GetCountOfArticles()
        {
            int count = this.articleRepository.All.Count();
            return count;
        }

        public Article GetArticleById(string id)
        {
            //TODO: Think of a better way to make this!
            Guid guid = new Guid(id);
            Article article = this.articleRepository.GetById(guid);

            return article;
        }

        public void AddArticle(Article article)
        {
            Guard.WhenArgument(article, "article").IsNull().Throw();

            using (this.unitOfWork)
            {
                this.articleRepository.Add(article);
                this.unitOfWork.Commit();
            }
        }

        public void DeleteArticle(string articleId)
        {
            Guard.WhenArgument(articleId, "articleId").IsNull().Throw();

            var article = this.GetArticleById(articleId);
            Guard.WhenArgument(article, "article").IsNull().Throw();

            using (this.unitOfWork)
            {
                this.articleRepository.Delete(article);
                this.unitOfWork.Commit();
            }
        }

        public IEnumerable<Article> GetArticlesByUserId(string userId)
        {
            Guard.WhenArgument(userId, "userId").IsNull().Throw();
            IEnumerable<Article> articles = this.articleRepository.GetAll(x => x.CreatorId == userId);

            return articles;
        }

        public IEnumerable<Article> GetArticlesByContinent(string continentName)
        {
            Guard.WhenArgument(continentName, "continentName").IsNull().Throw();
            IEnumerable<Article> articles = this.articleRepository.GetAll(x => x.Country.Continent.Name == continentName);

            return articles;
        }

        public IEnumerable<Article> GetArticlesByTitle(string title)
        {
            Guard.WhenArgument(title, "title").IsNull().Throw();
            IEnumerable<Article> articles = this.articleRepository.GetAll(x => x.Title.ToLower().Contains(title.ToLower()));

            return articles;
        }

        public IEnumerable<Article> GetArticlesByUsername(string username)
        {
            Guard.WhenArgument(username, "username").IsNull().Throw();
            IEnumerable<Article> articles = this.articleRepository.GetAll(x => x.Creator.UserName == username);

            return articles;
        }
    }
}
