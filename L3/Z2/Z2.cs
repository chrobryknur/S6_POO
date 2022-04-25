using System;
using System.Collections.Generic;
using System.Text;

namespace L3
{
  class Z2
  {
    /*
     * Docelowo powstaną cztery klasy - jedna główna, agregująca pozostałe klasy spełniające SRP.
     * Nie jest potrzebnych więcej klas, a mniejsza ich liczba musiałaby spowodować złamanie SRP.
     * Refaktoryzacja klasy naruszającej SRP nie oznacza, że każda jej metoda powinna trafić do
     * oddzielnej klasy, ponieważ metody te mogą działać w podobny sposób np. na tych samych obiektach
     * i można je wszystkie przenieść do wspólnej klasy, która będzie odpowiedzialna za operacje tylko na tych obiektach
    */

    static void Main(string[] args)
    {
      ReportPrinter reportPrinter = new ReportPrinter();
      reportPrinter.PrintReport();

      Document document = new Document();
      DocumentFormatter documentFormatter = new DocumentFormatter();
      Printer printer = new Printer();

      ImprovedReportPrinter improvedReportPrinter = new ImprovedReportPrinter(document, documentFormatter, printer);
      improvedReportPrinter.PrintReport();
    }

    public class ReportPrinter
    {
      public ReportPrinter()
      {
        data = "DATA";
      }
      public string GetData()
      {
        return data;
      }

      public void FormatDocument()
      {
        data = data.ToLower();
      }

      public void PrintReport()
      {
        FormatDocument();
        Console.WriteLine(GetData());
      }

      private string data;
    }

    public class ImprovedReportPrinter
    {
      public ImprovedReportPrinter(Document _document, DocumentFormatter _documentFormatter, Printer _printer)
      {
        document = _document;
        documentFormatter = _documentFormatter;
        printer = _printer;
      }

      public void PrintReport()
      {
        documentFormatter.FormatDocument(document);
        printer.Print(document.GetData());
      }

      private readonly Document document;
      private readonly DocumentFormatter documentFormatter;
      private readonly Printer printer;
    }

    public class Document
    {
      public Document()
      {
        data = "DATA";
      }
      public void SetData(string _data)
      {
        data = _data;
      }
      public string GetData()
      {
        return data;
      }

      private string data;
    }

    public class DocumentFormatter
    {
      public void FormatDocument(Document document)
      {
        document.SetData(document.GetData().ToLower());
      }
    }

    public class Printer
    {
      public void Print(string data)
      {
        Console.WriteLine(data);
      }
    }
  }
}