using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trouvaille.Models;

namespace Trouvaille.UnitTests.Models.ContinentTests
{
    [TestFixture]
    public class Id_Should
    {
        private const string PropertyName = "Id";

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
    }
}
