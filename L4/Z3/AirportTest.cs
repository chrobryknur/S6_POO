using Microsoft.VisualStudio.TestTools.UnitTesting;
using Z3;

namespace ObjectPoolTest
{
    [TestClass]
    public class AirportTest
    {
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void Invalid_capacity_of_airport_should_throw_an_exception()
        {
            Airport airport = new Airport(0);
        }
        [TestMethod]
        public void Capacity_of_airport_not_exceeded_should_return_two_airplanes_correctly()
        {
            Airport airport = new Airport(1);
            Airplane airplane = airport.GetAirplane();
            Assert.IsNotNull(airplane);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void Capacity_of_airport_exceeded_should_throw_exception()
        {
            Airport airport = new Airport(1);
            Airplane airplane = airport.GetAirplane();
            Assert.IsNotNull(airplane);
            Airplane airplane2 = airport.GetAirplane();
        }

        [TestMethod]
        public void Should_return_the_same_plane_after_release()
        {
            Airport airport = new Airport(1);
            Airplane airplane = airport.GetAirplane();
            airport.ReleaseAirplane(airplane);
            Airplane airplane2 = airport.GetAirplane();
            Assert.AreEqual(airplane, airplane2);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void Should_throw_an_exception_when_trying_to_release_plane_not_from_the_airport()
        {
            Airport airport = new Airport(1);
            Airplane airplane = new Airplane();
            airport.ReleaseAirplane(airplane);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void Should_throw_an_exception_when_trying_to_release_plane_not_from_a_different_airport()
        {
            Airport airport1 = new Airport(1);
            Airport airport2 = new Airport(1);
            Airplane airplane = airport1.GetAirplane();
            airport2.ReleaseAirplane(airplane);
        }

    }
}