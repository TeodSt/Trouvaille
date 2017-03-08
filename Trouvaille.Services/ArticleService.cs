﻿using System.Collections.Generic;
using Bytes2you.Validation;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services.Contracts;

namespace Trouvaille.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IGenericRepository<Article> articleRepository;
        private readonly IUnitOfWork unitOfWork;

        public ArticleService(IGenericRepository<Article> articleRepository, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(articleRepository, "articleRepository").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();
            this.articleRepository = articleRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Article> GetAllArticles()
        {
            IEnumerable<Article> articles = this.articleRepository.GetAll();

            return articles;
        }

        public Article GetArticleById(int id)
        {
            Article article = this.articleRepository.GetById(id);

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