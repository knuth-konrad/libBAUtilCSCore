using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using libBAUtilCoreCS;
using static libBAUtilCoreCS.baUtil;
using static libBAUtilCoreCS.DateTimeHelper;
using static libBAUtilCoreCS.FilesystemHelper;
using static libBAUtilCoreCS.StringHelper;


namespace libBAUtilCSTest
{
  class Program
  {
    static void Main(string[] args)
    {
      ///  VB.NET / C# equivalents: https://sites.harding.edu/fmccown/vbnet_csharp_comparison.html


      Console.WriteLine("*** Strings");
      BlankLine();

      StringUtils();
      BlankLine();

      Console.WriteLine("*** Files/Folders");
      BlankLine();

      IOUtils();
      BlankLine();

      /*
      Console.WriteLine("*** DateTimeHelper");
      BlankLine();

      DateTimeUtils();
      BlankLine();
      */

      AnyKey();

      Environment.Exit(0);
    }

    static void StringUtils()
    {

      string sSource = System.String.Empty;

      WriteIndent("** Left()", 1);
      BlankLine();

      sSource = "1234567890";
      WriteIndent("- Left(sSource, 3)", 1);
      WriteIndent(System.String.Format("- Before: {0}, after: {1}", sSource, Left(sSource, 3)), 2);

      WriteIndent("- Left(sSource, 12)", 1);
      WriteIndent(System.String.Format("- Before: {0}, after: {1}", sSource, Left(sSource, 12)), 2);

      WriteIndent("- Left(sSource, 0)", 1);
      WriteIndent(System.String.Format("- Before: {0}, after: {1}", sSource, Left(sSource, 0)), 2);

      WriteIndent("- Left(sSource, -3)", 1);
      WriteIndent(System.String.Format("- Before: {0}, after: {1}", sSource, Left(sSource, -3)), 2);
      BlankLine();


      WriteIndent("** Right()", 1);
      BlankLine();

      sSource = "1234567890";
      WriteIndent("- Right(sSource, 3)", 1);
      WriteIndent(System.String.Format("- Before: {0}, after: {1}", sSource, Right(sSource, 3)), 2);

      WriteIndent("- Right(sSource, 12)", 1);
      WriteIndent(System.String.Format("- Before: {0}, after: {1}", sSource, Right(sSource, 12)), 2);

      WriteIndent("- Right(sSource, 0)", 1);
      WriteIndent(System.String.Format("- Before: {0}, after: {1}", sSource, Right(sSource, 0)), 2);

      WriteIndent("- Right(sSource, -3)", 1);
      WriteIndent(System.String.Format("- Before: {0}, after: {1}", sSource, Right(sSource, -3)), 2);
      BlankLine();


      WriteIndent("** Mid()", 1);
      BlankLine();

      sSource = "1234567890";
      //WriteIndent("- Mid(sSource, -1)", 1);
      //try
      //{
      //   WriteIndent(System.String.Format("- Before: {0}, after: {1}", sSource, Mid(sSource, -1)), 2);  // Should result in Exception
      //}
      //catch (Exception ex)
      //   {
      //   WriteIndent("Exception: " + ex.Message, 2);
      //}

      WriteIndent("- Mid(sSource, 0)", 1);
      WriteIndent(System.String.Format("- Before: {0}, after: {1}", sSource, Mid(sSource, 0)), 2);

      WriteIndent("- Mid(sSource, 11)", 1);
      WriteIndent(System.String.Format("- Before: {0}, after: {1}", sSource, Mid(sSource, 11)), 2);

      WriteIndent("- Mid(sSource, 0, 2)", 1);
      WriteIndent(System.String.Format("- Before: {0}, after: {1}", sSource, Mid(sSource, 0, 2)), 2);

      WriteIndent("- Mid(sSource, 0, 11)", 1);
      WriteIndent(System.String.Format("- Before: {0}, after: {1}", sSource, Mid(sSource, 0, 11)), 2);

      WriteIndent("- Mid(sSource, 11, 11)", 1);
      WriteIndent(System.String.Format("- Before: {0}, after: {1}", sSource, Mid(sSource, 11, 11)), 2);
      BlankLine();

      WriteIndent("- Bytes2FormattedString(1024): " + Bytes2FormattedString(1024, false), 1);
      WriteIndent("- Bytes2FormattedString(1025): " + Bytes2FormattedString(1025, false), 1);
      WriteIndent("- Bytes2FormattedString(1024 ^ 2): " + Bytes2FormattedString((UInt64)System.Math.Pow(1024, 2), false), 1);
      WriteIndent("- Bytes2FormattedString(1024 ^ 2 + 1025): " + Bytes2FormattedString((UInt64)System.Math.Pow(1024, 2) + 1025, false), 1);
      BlankLine();

    }  // StringUtils

    static void IOUtils()
    {
      string sSource = System.String.Empty;

      WriteIndent("** NormalizePath", 1);
      BlankLine();

      sSource = "C:\\DATA";
      WriteIndent("- NormalizePath(sSource)", 1);
      WriteIndent(System.String.Format("- Before: {0}, after: {1}", sSource, NormalizePath(sSource)), 2);
      sSource = "C:\\DATA\\";
      WriteIndent("- NormalizePath(sSource)", 1);
      WriteIndent(System.String.Format("- Before: {0}, after: {1}", sSource, NormalizePath(sSource)), 2);
      sSource = "www.domain.tld";
      WriteIndent("- NormalizePath(sSource, ' / ')", 1);
      WriteIndent(System.String.Format("- Before: {0}, after: {1}", sSource, NormalizePath(sSource, "/")), 2);
      sSource = "www.domain.tld/";
      WriteIndent("- NormalizePath(sSource, ' / ')", 1);
      WriteIndent(System.String.Format("- Before: {0}, after: {1}", sSource, NormalizePath(sSource, "/")), 2);
      sSource = "\\www.domain.tld";
      WriteIndent("- NormalizePath(sSource, '\\', False)", 1);
      WriteIndent(System.String.Format("- Before: {0}, after: {1}", sSource, NormalizePath(sSource, "\\", false)), 2);
      sSource = "www.domain.tld";
      WriteIndent("- NormalizePath(sSource, '\\', False)", 1);
      WriteIndent(System.String.Format("- Before: {0}, after: {1}", sSource, NormalizePath(sSource, "\\", false)), 2);
      BlankLine();

      WriteIndent("** BackupFile", 1);
      sSource = @"C:\DATA\CSharp\zLibrary\BasicAware\libBAUtilCoreCS\libBAUtilCSCoreTest\DATA\1.tmp";
      string sRet = sSource;

      for (Int32 i = 0; i <= 9; i++)
      {
        WriteIndent(System.String.Format("{0:00}) BackupFile {2}: {1}", i + 1,
           BackupFile(sSource, sSource, out sRet, true).ToString(), sRet), 1);
      }

    }  // IOUtils()

    static void DateTimeUtils()
    {


      WriteIndent("** IATADateTime", 1);
      BlankLine();

      // * Constructors
      // Default (empty)
      DateTimeHelper dtm = new DateTimeHelper();


      WriteIndent("- New()", 1);
      WriteIndent(System.String.Format("- .ToDateIATA: {0}", dtm.ToDateIATA()), 2);
      WriteIndent(System.String.Format("- .ToDateIATA: {0}", dtm.ToDateIATA(DateTimeHelper.IATADateType.DateLong)), 2);
      BlankLine();

      // DateTime
      dtm = new DateTimeHelper(new DateTime(2019, 4, 9));
      WriteIndent("- New(New DateTime(2019, 4, 9))", 1);
      WriteIndent(System.String.Format("- .ToDateIATA: {0}", dtm.ToDateIATA()), 2);
      WriteIndent(System.String.Format("- .ToDateIATA: {0}", dtm.ToDateIATA(DateTimeHelper.IATADateType.DateLong)), 2);
      BlankLine();

      // Ticks
      dtm = new DateTimeHelper(9999999999999);
      WriteIndent("- New(9999999999999)", 1);
      WriteIndent(System.String.Format("- .ToDateIATA: {0}", dtm.ToDateIATA()), 2);
      WriteIndent(System.String.Format("- .ToDateIATA: {0}", dtm.ToDateIATA(DateTimeHelper.IATADateType.DateLong)), 2);
      BlankLine();

      // Year, Month, Day
      dtm = new DateTimeHelper(2019, 4, 10);
      WriteIndent("- New(2019, 4, 10)", 1);
      WriteIndent(System.String.Format("- .ToDateIATA: {0}", dtm.ToDateIATA()), 2);
      WriteIndent(System.String.Format("- .ToDateIATA: {0}", dtm.ToDateIATA(DateTimeHelper.IATADateType.DateLong)), 2);
      BlankLine();

      // IATA date
      dtm = new DateTimeHelper("10apr");
      WriteIndent("- New('10apr')", 1);
      WriteIndent(System.String.Format("- .ToDateIATA: {0}", dtm.ToDateIATA()), 2);
      WriteIndent(System.String.Format("- .ToDateIATA: {0}", dtm.ToDateIATA(DateTimeHelper.IATADateType.DateLong)), 2);
      BlankLine();

      // GetTimeFormats()
      dtm = new DateTimeHelper("10apr19");
      WriteIndent("- New('10apr19') - GetDateFormats()", 1);
      foreach (string s in dtm.GetDateTimeFormats())
      {
        WriteIndent(s, 3);
      }

      // GetLastDayInMonth()
      WriteIndent("- GetLastDayInMonth()", 1);
      for (Int32 i = 1; i <= 12; i++)
      {
        WriteIndent(System.String.Format("Month: {0}, last day in month: {1}", i, DateTimeHelper.GetLastDayInMonth(i, 2020).ToString()), 2);
      }

    }  // DateTimeUtils()

    static void AnyKey()
    {
      BlankLine();
      Console.WriteLine("-- Press ENTER --");
      Console.ReadLine();

    }  // AnyKey

    static void BlankLine(Boolean bolAddSeperator = false, Int32 lLines = 1)
    {
      if (bolAddSeperator == true)
      {
        Console.WriteLine("---");
      }

      for (Int32 i = 1; i <= lLines; i++)
      {
        Console.WriteLine();
      }
    }  // BlankLine

    static void WriteIndent(string sText, Int32 lIndent, Boolean bolAddNewLine = true)
    {

      if (bolAddNewLine == true)
      {
        Console.WriteLine(System.String.Concat(new System.String(' ', lIndent)) + sText);
      }
      else
      {
        Console.Write(System.String.Concat(new System.String(' ', lIndent)) + sText);
      }
    }  // WriteIndent



  }
}
