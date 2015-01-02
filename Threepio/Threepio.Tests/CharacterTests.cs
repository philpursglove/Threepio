using NUnit.Framework;
using System.Collections.Generic;

namespace Threepio.Tests
{
    [TestFixture]
    public class CharacterTests
    {

        [Test]
        public async void GetPage_Returns_Multiple_Characters()
        {
            var result = await Character.GetPage();

            Assert.IsInstanceOf<List<Character>>(result);
        }

        [Test]
        public async void GetPage2_Returns_Different_Results_To_GetPage1()
        {
            var result1 = await Character.GetPage(1);
            var result2 = await Character.GetPage(2);

            Assert.AreNotEqual(result1, result2);
        }
    }
}
