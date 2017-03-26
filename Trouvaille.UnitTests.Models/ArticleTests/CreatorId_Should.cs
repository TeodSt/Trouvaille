using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Trouvaille.Models;

namespace Trouvaille.UnitTests.Models.ArticleTests
{
    [TestFixture]
    class CreatorId_Should
    {
        private const string PropertyName = "CreatorId";

        [Test]
        public void BeAProperty_InArticleClass()
        {
            // Arrange            
            Article article = new Article();

            // Act
            var actual = article.GetType().GetProperty(PropertyName).Name;

            // Assert
            Assert.AreEqual(PropertyName, actual);
        }
    }
}
