using NUnit.Framework;
using Trouvaille.Models;

namespace Trouvaille.UnitTests.Models.ImageTests
{
    [TestFixture]
    public class CreatedOn_Should
    {
        private const string PropertyName = "CreatedOn";

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
