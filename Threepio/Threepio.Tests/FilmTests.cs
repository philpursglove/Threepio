using NUnit.Framework;

namespace Threepio.Tests
{
    [TestFixture]
    public class FilmTests
    {
        [Test]
        public void Film_1_Returns_A_New_Hope()
        {
            Film aNewHope = Film.Get(1);

            Assert.AreEqual(aNewHope.Title, "A New Hope");
        }

        [Test]
        public void Film_99_Returns_Nothing()
        {
            Film nullFilm = Film.Get(99);

            Assert.IsNull(nullFilm);
        }



    }
}
