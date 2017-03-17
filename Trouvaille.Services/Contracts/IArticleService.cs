﻿using System.Collections.Generic;
using Trouvaille.Models;

namespace Trouvaille.Services.Contracts
{
    public interface IArticleService
    {
        IEnumerable<Article> GetAllArticles();

        Article GetArticleById(string id);

        void AddArticle(Article article);

        void DeleteArticle(Article article);
    }
}
