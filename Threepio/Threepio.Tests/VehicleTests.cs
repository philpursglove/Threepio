using System;
using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Threepio.Tests
{
    [TestFixture]
    class VehicleTests
    {
        [Test]
        public void Vehicle_18_Returns_A_Vehicle()
        {
            Vehicle vehicle = Vehicle.Get(18);

            Assert.IsNotNull(vehicle);
        }

        [Test]
        public void Vehicle_Minus1_Throws_A_404()
        {
            Action act = () => Vehicle.Get(-1);

            act.ShouldThrow<WebException>();
        }

        [Test]
        public void GetAll_Returns_Multiple_Vehicles()
        {
            var result = Vehicle.GetAll();

            Assert.IsInstanceOf<List<Vehicle>>(result);
        }
    }
}