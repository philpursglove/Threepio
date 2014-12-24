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
        public void GetAll_Returns_Multiple_Characters()
        {
            var result = Character.GetAll();

            Assert.IsInstanceOf<List<Character>>(result);
        }
    }
}
