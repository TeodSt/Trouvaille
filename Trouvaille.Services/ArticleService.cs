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

        public int GetCountOfArticles()
        {
            int count = this.articleRepository.GetAll().Count();
            return count;
        }

        public Article GetArticleById(string id)
        {
            Guid guid = new Guid(id);
            Article article = this.articleRepository.GetById(guid);

            return article;
        }

        public void AddArticle(Article article)
        {
            using (this.unitOfWork)
            {
                this.articleRepository.Add(article);
                this.unitOfWork.Commit();
            }
        }

        public void DeleteArticle(Article article)
        {
            using (this.unitOfWork)
            {
                this.articleRepository.Delete(article);
                this.unitOfWork.Commit();
            }
        }
    }
}
