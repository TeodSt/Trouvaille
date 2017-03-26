using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Trouvaille.Models;

namespace Trouvaille.UnitTests.Models.ImageTests
{
    [TestFixture]
    class CreatorId_Should
    {
        private const string PropertyName = "CreatorId";

        [Test]
        public void BeAProperty_InArticleClass()
        {
            // Arrange            
            Picture picture = new Picture();

            // Act
            var actual = picture.GetType().GetProperty(PropertyName).Name;

            // Assert
            Assert.AreEqual(PropertyName, actual);
        }
    }
}
