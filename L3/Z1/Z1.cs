using System;
using System.Collections.Generic;
using System.Text;

namespace L3
{
  class Z1
  {
    /*
     * Creator - klasa RegularPolygonFactory jest odpowiedzialna za tworzenie obiektów typu RegularPolygon
     * Information Expert - tylko klasy Triangle oraz Square posiadają wszystkie informacje wymagane do poprawnego policzenia pól obiektów o ich typach
     * Polymorphism - klasy Triangle i Square dziedziczą po klasie bazowej RegularPolygon, ponieważ sposób obliczania ich pól jest różny
     * LowCoupling - kolejni workerzy mogą być dodawani w stosunkowo łatwy sposób, bez konieczności zmiany poprzednio napisanego kodu (wynika to z małych zależności pomiędzy klasami)
    */

    static void Main(string[] args)
    {
      RegularPolygonFactory regularPolygonFactory = new RegularPolygonFactory();
      SquareWorker squareWorker = new SquareWorker();
      TriangleWorker triangleWorker = new TriangleWorker();

      regularPolygonFactory.AddWorker(squareWorker);
      regularPolygonFactory.AddWorker(triangleWorker);

      List<RegularPolygon> regularPolygons = new List<RegularPolygon>();

      regularPolygons.Add(regularPolygonFactory.CreateRegularPolygon("Square", 1));
      regularPolygons.Add(regularPolygonFactory.CreateRegularPolygon("Triangle", 1));
      regularPolygons.Add(regularPolygonFactory.CreateRegularPolygon("Square", 2));
      regularPolygons.Add(regularPolygonFactory.CreateRegularPolygon("Triangle", 2));
      regularPolygons.Add(regularPolygonFactory.CreateRegularPolygon("Square", 3));
      regularPolygons.Add(regularPolygonFactory.CreateRegularPolygon("Triangle", 3));

      var sumOfAreas = 0.0;

      foreach(var regularPolygon in regularPolygons)
      {
        sumOfAreas += regularPolygon.ComputeArea();
      }

      Console.WriteLine("Sum of areas: {0}", sumOfAreas);
    }

    public abstract class RegularPolygon
    {
      public RegularPolygon(double _sideLength)
      {
        sideLength = _sideLength;
      }
      public abstract double ComputeArea();
      protected double sideLength;
    }

    public class Square : RegularPolygon
    {
      public Square(double _sideLength) : base(_sideLength)
      { }

      public override double ComputeArea()
      {
        return sideLength * sideLength;
      }
    }

    public class Triangle : RegularPolygon
    {
      public Triangle(double _sideLength) : base(_sideLength)
      { }

      public override double ComputeArea()
      {
        return sideLength * sideLength * Math.Sqrt(3) / 4;
      }
    }

    public interface IFactoryWorker
    {
      bool ProducesPolygon(string polygon);
      RegularPolygon ProducePolygon(double sideLength);
    }

    public class SquareWorker : IFactoryWorker
    {
      public bool ProducesPolygon(string polygon)
      {
        return polygon == "Square";
      }

      public RegularPolygon ProducePolygon(double sideLength)
      {
        return new Square(sideLength);
      }
    }

    public class TriangleWorker : IFactoryWorker
    {
      public bool ProducesPolygon(string polygon)
      {
        return polygon == "Triangle";
      }

      public RegularPolygon ProducePolygon(double sideLength)
      {
        return new Triangle(sideLength);
      }
    }

    public class RegularPolygonFactory
    {
      public void AddWorker(IFactoryWorker factoryWorker)
      {
        factoryWorkers.Add(factoryWorker);
      }

      public RegularPolygon CreateRegularPolygon(string polygon, double sideLength)
      {
        foreach(var factoryWorker in factoryWorkers)
        {
          if(factoryWorker.ProducesPolygon(polygon))
          {
            return factoryWorker.ProducePolygon(sideLength);
          }
        }
        return null;
      }

      private List<IFactoryWorker> factoryWorkers = new List<IFactoryWorker>();
    }
  }
}