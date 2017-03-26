﻿using NUnit.Framework;
using Trouvaille.Models;

namespace Trouvaille.UnitTests.Models.ArticleTests
{
    [TestFixture]
    class ImagePath_Should
    {
        private const string PropertyName = "ImagePath";

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
