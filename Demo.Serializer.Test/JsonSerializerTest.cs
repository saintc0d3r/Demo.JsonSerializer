using System;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo.Serializer.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class JsonSerializerTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Can_serialize_a_coordinate_in_a_jagged_array()
        {
            // Arrange
            const string json = @"{""coordinates"":[[1897144200714.0774,4750701.9671]]}";
            var geometry = new Geometry( new [] {1897144200714.0774, 4750701.9671});
            JavaScriptSerializer serialiser = new JavaScriptSerializer();

            // Act
            string actual = serialiser.Serialize(geometry);
            actual = !string.IsNullOrEmpty(actual) ? actual.ToLower() : actual;

            // Assert
            StringAssert.Contains(json, actual);
        }

        [TestMethod]
        public void Can_deserialize_a_geometry_from_json_string()
        {
            // Arrange
            const string json = @"{""coordinates"":[[1897144200714.0774,4750701.9671]]}";
            JavaScriptSerializer serialiser = new JavaScriptSerializer();

            // Act
            Geometry actual = serialiser.Deserialize<Geometry>(json);

            // Assert
            Assert.IsNotNull(actual.Coordinates);
            Assert.AreEqual(1,actual.Coordinates.Length);
            Assert.IsTrue(Math.Abs(1897144200714.077 - actual.Coordinates[0][0]) < 0.001);
            Assert.IsTrue(Math.Abs(4750701.9671 - actual.Coordinates[0][1]) < 0.001);
        }
    }

    [DataContract]
    public class Geometry
    {
        public Geometry()
        {            
        }

        public Geometry(double x, double y)
        {
            Coordinates = new double[1][];
            Coordinates[0] = new [] { x, y };
        }

        public Geometry(double[] coordinate)
        {
            Coordinates = new double[1][];
            Coordinates[0] = coordinate;
        }

        [DataMember(Name = "coordinate")]
        public double[][] Coordinates { set; get; }

    }
}
