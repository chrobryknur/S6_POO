using System;
using System.Collections.Generic;
using System.Text;

namespace L3
{
  class Z7
  {
    public static void Main(string[] args)
    {
      Document document = new Document("DATA");
      DocumentFormatter formatter = new DocumentFormatter();
      ReportPrinter printer = new ReportPrinter();

      ReportComposer reportComposer = new ReportComposer(document, formatter, printer);
      reportComposer.Report();
    }

    public class ReportComposer
    {
      private IDocument document;
      private IFormatter formatter;
      private IReportPrinter reportPrinter;

      public ReportComposer(IDocument _document, IFormatter _formatter, IReportPrinter _reportPrinter)
      {
        document = _document;
        formatter = _formatter;
        reportPrinter = _reportPrinter;
      }

      public void Report()
      {
        formatter.FormatDocument(document);
        reportPrinter.PrintReport(document);
      }
    }

    public interface IDocument
    {
      string GetData();
      void SetData(string data);
    }

    public class Document: IDocument
    {
      public Document(string _data)
      {
        data = _data;
      }

      public string GetData()
      {
        return data;
      }

      public void SetData(string _data)
      {
        data = _data;
      }

      private string data;
    }

    public interface IFormatter
    {
      void FormatDocument(IDocument document);
    }

    public class DocumentFormatter: IFormatter
    {
      public void FormatDocument(IDocument document)
      {
        string data = document.GetData();
        data = data.ToLower();
        document.SetData(data);
      }
    }

    public interface IReportPrinter
    {
      void PrintReport(IDocument report);
    }

    public class ReportPrinter: IReportPrinter
    {
      public void PrintReport(IDocument report)
      {
        Console.WriteLine(report.GetData());
      }
    }
  }
}