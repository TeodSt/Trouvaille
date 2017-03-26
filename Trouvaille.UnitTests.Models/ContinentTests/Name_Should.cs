using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Trouvaille.Models;

namespace Trouvaille.UnitTests.Models.ContinentTests
{
    [TestFixture]
    class Name_Should
    {
        private const string PropertyName = "Name";

        [Test]
        public void BeAProperty_InContinentClass()
        {
            // Arrange
            Continent continent = new Continent();

            // Act
            var actual = continent.GetType().GetProperty(PropertyName).Name;

            // Assert
            Assert.AreEqual(PropertyName, actual);
        }

        [Test]
        public void HaveRequiredAttribute_InContinentClass()
        {
            // Arrange
            Continent continent = new Continent();

            // Act
            bool hasAttribute = continent.GetType()
                .GetProperty(PropertyName)
                .GetCustomAttributes(false)
                .Where(p => p.GetType() == typeof(RequiredAttribute))
                .Any();

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void HaveMinLengthAttribute_InContinentClass()
        {
            // Arrange
            Continent continent = new Continent();

            // Act
            bool hasAttribute = continent.GetType()
                .GetProperty(PropertyName)
                .GetCustomAttributes(false)
                .Where(p => p.GetType() == typeof(MinLengthAttribute))
                .Any();

            // Assert
            Assert.IsTrue(hasAttribute);
        }

        [Test]
        public void HaveMaxLengthAttribute_InContinentClass()
        {
            // Arrange
            Continent continent = new Continent();

            // Act
            bool hasAttribute = continent.GetType()
                .GetProperty(PropertyName)
                .GetCustomAttributes(false)
                .Where(p => p.GetType() == typeof(MaxLengthAttribute))
                .Any();

            // Assert
            Assert.IsTrue(hasAttribute);
        }
    }
}
