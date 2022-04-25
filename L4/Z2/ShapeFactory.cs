using System;
using System.Collections.Generic;

namespace Z2
{
    public class ShapeFactory
    {
        public static void Main(string[] param)
        {
            
        }
        public ShapeFactory()
        {
            workers = new List<IShapeFactoryWorker>();
        }

        public void RegisterWorker(IShapeFactoryWorker worker)
        {
            workers.Add(worker);
        }

        public IShape CreateShape(string shapeName, params object[] parameters)
        {
            var workersCreatingShape = workers.Where(w => w.AcceptsParameters(shapeName, parameters));

            if (workersCreatingShape.Count() == 0)
            {
                throw new ArgumentException("No worker can create shape using given parameters");
            }

            if (workersCreatingShape.Count() > 1)
            {
                throw new ArgumentException("Ambiguous parameters passed");
            }

            var worker = workersCreatingShape.First();
            return worker.CreateShape(parameters);
        }

        private List<IShapeFactoryWorker> workers;
    }

    public interface IShapeFactoryWorker
    {
        bool AcceptsParameters(string shapeName, params object[] parameters);
        IShape CreateShape(params object[] parameters);
    }


    public class SquareWorker : IShapeFactoryWorker
    {
        public bool AcceptsParameters(string shapeName, params object[] parameters)
        {
            return shapeName == "Square" && parameters.Length == 1 && parameters[0] is double;
        }

        public IShape CreateShape(params object[] parameters)
        {
            return new Square(Convert.ToDouble(parameters[0]));
        }
    }
    public class RectangleWorker : IShapeFactoryWorker
    {
        public bool AcceptsParameters(string shapeName, params object[] parameters)
        {
            return shapeName == "Rectangle" && parameters.Length == 2 && parameters[0] is double && parameters[1] is double;
        }

        public IShape CreateShape(params object[] parameters)
        {
            return new Rectangle(Convert.ToDouble(parameters[0]), Convert.ToDouble(parameters[1]));
        }
    }

    public interface IShape
    {
        double CalculateArea();
    }

    public class Square : IShape
    {
        public Square(double sideLength)
        {
            this.sideLength = sideLength;
        }

        public double CalculateArea()
        {
            return sideLength * sideLength;
        }

        private double sideLength;
    }

    public class Rectangle : IShape
    {
        public Rectangle(double width, double height)
        {
            this.width = width;
            this.height = height;
        }

        public double CalculateArea()
        {
            return width * height;
        }

        private double width;
        private double height;
    }

}