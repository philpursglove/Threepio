using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;

namespace Threepio.Tests
{
    [TestFixture]
    public class CharacterTests
    {
        [Test]
        public void Get_Minus1_Throws_404()
        {
            Action act = () => Character.Get(-1);

            act.ShouldThrow<WebException>();
        }

        [Test]
        public void GetPage_Returns_Multiple_Characters()
        {
            var result = Character.GetPage();

            Assert.IsInstanceOf<List<Character>>(result);
        }

        [Test]
        public void GetPage2_Returns_Different_Results_To_GetPage1()
        {
            var result1 = Character.GetPage(1);
            var result2 = Character.GetPage(2);

            Assert.AreNotEqual(result1, result2);
        }
    }
}
