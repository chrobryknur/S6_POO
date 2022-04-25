using System;
using System.Collections.Generic;
using System.Text;

namespace L3
{
  class Z4
  {
    /*
    public class Rectangle
    {
      public virtual int Width { get; set; }
      public virtual int Height { get; set; }
    }
    public class Square : Rectangle
    {
      public override int Width
      {
        get { return base.Width; }
        set { base.Width = base.Height = value;}
      }
      public override int Height
      {
        get { return base.Height; }
        set { base.Width = base.Height = value; }
      }
    }

    public class AreaCalculator
    {
      public int CalculateArea( Rectangle rect )
      {
        return rect.Width * rect.Height;
      }
    }

    public static void Main(string[] args)
    {
      int w = 4, h = 5;
      Rectangle rect = new Square() { Width = w, Height = h };
      AreaCalculator calc = new AreaCalculator();
      Console.WriteLine( "prostokąt o wymiarach {0} na {1} ma pole {2}", w, h, calc.CalculateArea( rect ) );
    }


    Taka implementacja powoduje złamanie zasady LSP, ponieważ oczekiwane jest, że zastępując obiekt typu Rectangle obiektem typu
    Square pole będzie wynosiło 4*5 = 20. Ta implementacja sprawi, że pole będzie wynosiło 4*4 = 16, bo obu parametrom Height i
    Width zostaną przypisane te same wartości. Aby zasada LSP była spełniona można utworzyć klasę Shape, po której będą dziedziczyły
    zarówno Rectangle jak i Square.
    */

    public abstract class Shape
    {
      public abstract double GetArea();
    }

    public class Square : Shape
    {
      public int Width { get; set; }
      public override double GetArea()
      {
        return Width * Width;
      }
    }

    public class Rectangle : Shape
    {
      public int Width { get; set; }
      public int Height { get; set; }

      public override double GetArea()
      {
        return Height * Width;
      }
    }
  }
}