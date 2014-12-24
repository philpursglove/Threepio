using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;

namespace Threepio.Tests
{
    [TestFixture]
    public class FilmTests
    {
        [Test]
        public void Film_1_Returns_A_Film()
        {
            Film aNewHope = Film.Get(1);

            Assert.IsNotNull(aNewHope);
        }

        [Test]
        public void Film_Minus1_Throws_404()
        {
            Action act = () => Film.Get(-1);

            act.ShouldThrow<WebException>();
        }

        public void GetAll_Returns_Multiple_Films()
        {
            var result = Film.GetAll();

            Assert.IsInstanceOf<List<Film>>(result);
        }

    }
}
