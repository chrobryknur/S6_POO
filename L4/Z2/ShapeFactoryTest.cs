using Microsoft.VisualStudio.TestTools.UnitTesting;
using Z2;

namespace OpenDelegateFactoryTest
{
    [TestClass]
    public class ShapeFactoryTest
    {
        [TestMethod]
        public void Square_worker_should_create_square_when_given_right_parameters()
        {
            ShapeFactory shapeFactory = new ShapeFactory();
            IShapeFactoryWorker squareWorker = new SquareWorker();
            shapeFactory.RegisterWorker(squareWorker);

            IShape square = shapeFactory.CreateShape("Square", 1.0);

            Assert.IsInstanceOfType(square, typeof(Square));
            Assert.AreEqual(1.0, square.CalculateArea());
        }

        [TestMethod]
        public void Rectangle_worker_should_create_rectangle_when_given_right_parameters()
        {
            ShapeFactory shapeFactory = new ShapeFactory();
            IShapeFactoryWorker rectangleWorker = new RectangleWorker();
            shapeFactory.RegisterWorker(rectangleWorker);

            IShape rectangle = shapeFactory.CreateShape("Rectangle", 1.0, 2.0);

            Assert.IsInstanceOfType(rectangle, typeof(Rectangle));
            Assert.AreEqual(2.0, rectangle.CalculateArea());
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void Factory_should_throw_an_exception_if_no_worker_producing_rectangle_exists()
        {
            ShapeFactory shapeFactory = new ShapeFactory();
            IShape rectangle = shapeFactory.CreateShape("Rectangle", 1.0, 2.0);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void Factory_should_throw_an_exception_if_two_workers_producing_the_same_shape_exist()
        {
            ShapeFactory shapeFactory = new ShapeFactory();
            IShapeFactoryWorker rectangleWorker1 = new RectangleWorker();
            IShapeFactoryWorker rectangleWorker2 = new RectangleWorker();
            shapeFactory.RegisterWorker(rectangleWorker1);
            shapeFactory.RegisterWorker(rectangleWorker2);

            IShape rectangle = shapeFactory.CreateShape("Rectangle", 1.0, 2.0);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void Factory_should_throw_an_exception_if_wrong_parameters_for_given_shape_were_passed()
        {
            ShapeFactory shapeFactory = new ShapeFactory();
            IShapeFactoryWorker squareWorker = new SquareWorker();
            shapeFactory.RegisterWorker(squareWorker);

            IShape square = shapeFactory.CreateShape("Square", "1.0");
        }

    }
}