using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trouvaille.Data.Contracts;
using Trouvaille.Models;
using Trouvaille.Services;

namespace Trouvaille.UnitTests.Services.ArticleServiceTests
{
    [TestFixture]
    public class DeleteArticle_Should
    {
        /*
         public void DeleteArticle(string id)
        {
            var article = this.GetArticleById(id);

            if (article == null)
            {
                throw new ArgumentNullException("Article cannot be found");
            }

            using (this.unitOfWork)
            {
                this.articleRepository.Delete(article);
                this.unitOfWork.Commit();
            }
        }
         */

        [Test]
        public void CallRepositoryMethodGetByIdOnce()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();
            Guid articleId = Guid.NewGuid();
            var article = new Article() { Id = articleId };

            mockedRepository.Setup(x => x.GetById(articleId)).Returns(article);
            var articleService = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            articleService.DeleteArticle(articleId.ToString());

            // Assert
            mockedRepository.Verify(x => x.GetById(articleId), Times.Once);
        }

        [Test]
        public void CallRepositoryMethodDeleteOnce()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();
            Guid articleId = Guid.NewGuid();
            var article = new Article() { Id = articleId };

            mockedRepository.Setup(x => x.GetById(articleId)).Returns(article);
            var articleService = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            articleService.DeleteArticle(articleId.ToString());

            // Assert
            mockedRepository.Verify(x => x.Delete(It.IsAny<Article>()), Times.Once);
        }

        [Test]
        public void CallUnitOfWorkMethodCommitOnce()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();
            Guid articleId = Guid.NewGuid();
            var article = new Article() { Id = articleId };

            mockedRepository.Setup(x => x.GetById(articleId)).Returns(article);
            var articleService = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            articleService.DeleteArticle(articleId.ToString());

            // Assert
            mockedUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void ThrowArgumentNullException_WhenArticleIdIsNull()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();
            var articleService = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);

            // Act 
            var exception = Assert.Throws<ArgumentNullException>(() => articleService.DeleteArticle(null));

            // Assert
            StringAssert.IsMatch("articleId", exception.ParamName);
        }

        [Test]
        public void ThrowArgumentNullException_WhenArticleFoundIsNull()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IEfGenericRepository<Article>>();
            var articleService = new ArticleService(mockedRepository.Object, mockedUnitOfWork.Object);
            string articleId = Guid.NewGuid().ToString();

            // Act 
            var exception = Assert.Throws<ArgumentNullException>(() => articleService.DeleteArticle(articleId));

            // Assert
            StringAssert.IsMatch("article", exception.ParamName);
        }
    }
}
