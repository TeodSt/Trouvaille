using NUnit.Framework;
using Trouvaille.Models;

namespace Trouvaille.UnitTests.Models.ArticleTests
{
    [TestFixture]
    public class Id_Should
    {
        private const string PropertyName = "Id";

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
