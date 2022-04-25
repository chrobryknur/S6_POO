using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace L3
{
  class Z3
  {
    /*
    public class TaxCalculator
    {
      public double CalculateTax(double Price)
      {
        return Price * 0.22;
      }
    }

    public class Item
    {
      public double Price
      {
        get;
        set;
      }

      public string Name
      {
        get;
        set;
      }
    }

    public class CashRegister
    {
      public TaxCalculator taxCalc = new TaxCalculator();
      public double CalculatePrice(Item[] Items)
      {
        double _price = 0;
        foreach (Item item in Items)
        {
          _price += item.Price + taxCalc.CalculateTax(item.Price);
        }
        return _price;
      }
      public void PrintBill(Item[] Items)
      {
        foreach (var item in Items)
          Console.WriteLine("towar {0} : cena {1} + podatek {2}", item.Name, item.Price, taxCalc.CalculateTax(item.Price));
      }
    }
    */

    public static void Main(string[] args)
    {
      ITax tax = new CurrentPolishVAT();
      ITaxCalculator taxCalculator = new TaxCalculator(tax);
      IItemSorter itemSorter = new AlphabeticalSorter();
      CashRegister cashRegister = new CashRegister(taxCalculator, itemSorter);

      Item[] items =
      {
        new Item(1.50, "Guma"),
        new Item(10.20, "Parówki"),
        new Item(2.00, "Bułka")
      };

      cashRegister.PrintBill(items);
    }

    public interface ITax
    {
      double GetTaxRate();
    }

    public class CurrentPolishVAT : ITax
    {
      public double GetTaxRate()
      {
        return taxRate;
      }

      private double taxRate = 0.23;
    }
    public class OldPolishVAT : ITax
    {
      public double GetTaxRate()
      {
        return taxRate;
      }

      private double taxRate = 0.21;
    }

    public interface ITaxCalculator
    {
      double CalculateTax(double Price);
    }

    public class TaxCalculator : ITaxCalculator
    {
      public TaxCalculator(ITax _tax)
      {
        tax = _tax;
      }

      public double CalculateTax(double Price)
      {
        return Price * tax.GetTaxRate();
      }

      private ITax tax;
    }

    public class Item
    {
      public Item(double price, string name)
      {
        Price = price;
        Name = name;
      }
      public double Price;
      public string Name;
    }
    public interface IItemSorter
    {
      Item[] SortItems(Item[] items);
    }

    public class AlphabeticalSorter : IItemSorter
    {
      public Item[] SortItems(Item[] items)
      {
        return items.OrderBy(item => item.Name).ToArray();
      }
    }

    public class PriceSorter : IItemSorter
    {
      public Item[] SortItems(Item[] items)
      {
        return items.OrderBy(item => item.Price).ToArray();
      }
    }

    public class CashRegister
    {
      public CashRegister(ITaxCalculator _taxCalculator, IItemSorter _itemSorter)
      {
        taxCalculator = _taxCalculator;
        itemSorter = _itemSorter;
      }

      public void PrintBill(Item[] _items)
      {
        var items = itemSorter.SortItems(_items);
        foreach(var item in items)
        {
          Console.WriteLine("Item: {0}, Price (with tax): {1}", item.Name, item.Price + taxCalculator.CalculateTax(item.Price));
        }
      }

      private ITaxCalculator taxCalculator;
      private IItemSorter itemSorter;
    }

  }
}