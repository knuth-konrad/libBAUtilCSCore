using System;
// using System.Collections.Generic;
using System.Reflection;
// using System.Text;

using static libBAUtilCoreCS.StringHelper;


namespace libBAUtilCoreCS
{
  /// <summary>
  /// General purpose console application helpers
  /// </summary>
  public class ConsoleHelper
  {

    // ToDo: Define colors for signaling certain states/events, e.g. green foreground = "good", red = "error"

    #region Declarations
    private static ConHelperData moConData = new ConHelperData();

    // Console cursor position WaitIndicator
    private Int32 mThdCurLeft, mThdCurTop, mThdSpinDelay;
    private bool mThdNewLine;
    private System.Threading.Thread thdWaitIndicator;
    #endregion

    #region Properties - Public
    static ConHelperData ConsoleData
    {
      get { return moConData; }
      set { moConData = value; }
    }
    #endregion

    #region AppIntro
    /// <summary>
    /// Display an application intro
    /// </summary>
    /// <param name="appName">Name of the application</param>
    /// <param name="versionMajor">Major version</param>
    /// <param name="versionMinor">Minor version</param>
    /// <param name="versionRevision">Revision</param>
    /// <param name="versionBuild">Build</param>

    public static void AppIntro(string appName, Int32 versionMajor,
                                              Int32 versionMinor = 0,
                                              Int32 versionRevision = 0,
                                              Int32 versionBuild = 0)

    {
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine(Chr(16) + " " + appName + " v" +
                     versionMajor.ToString() + "." +
                     versionMinor.ToString() + "." +
                     versionRevision.ToString() + "." +
                     versionBuild.ToString() +
                     " " + Chr(17));
      Console.ForegroundColor = ConsoleColor.Gray;
    }


    /// <summary>
    /// Display an application intro
    /// </summary>
    /// <param name="appName">Name of the application</param>

    public static void AppIntro(string appName)

    {
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine(Chr(16) + " " + appName + " " + Chr(17));
      Console.ForegroundColor = ConsoleColor.Gray;
    }

    /// <summary>
    /// Display an application intro
    /// </summary>
    /// <param name="appName">Name of the application</param>
    /// <param name="versionMajor">Major version</param>
    public static void AppIntro(string appName, Int32 versionMajor)
    {
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine(Chr(16) + " " + appName + " v" + versionMajor.ToString() + ".0 " + Chr(17));
      Console.ForegroundColor = ConsoleColor.Gray;
    }

    /// <summary>
    /// Display an application intro
    /// </summary>
    /// <param name="mainAssembly">Assembly of the main executable</param>
    /// <example>
    /// AppIntro(System.Refelction.Assembly.GetEntryAssembly())
    /// </example>
    /// <remarks>
    /// See https://docs.microsoft.com/en-us/dotnet/api/system.version?view=net-5.0
    /// </remarks>
    public static void AppIntro(Assembly mainAssembly)
    {
      AssemblyName assemName = mainAssembly.GetName();
      Version ver = assemName.Version;

      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine(Chr(16) + " {0} v{1}", assemName.Name, ver.ToString() + " " + Chr(17));
      Console.ForegroundColor = ConsoleColor.Gray;
    }
    #endregion  // "AppIntro"

    #region AppCopyright

    /// <summary>
    /// Display a copyright notice.
    /// </summary>
    /// <param name="trailingBlankLine">Add a blank line afterwards.</param>
    /// <param name="showAuthor">Show <see cref="ConHelperData.Author"/></param>
    public static void AppCopyright(bool trailingBlankLine = true, bool showAuthor = false)
    {
      AppCopyright(DateTime.Now.Year.ToString(), ConsoleData.Company, trailingBlankLine, showAuthor);
    }

    /// <summary>
    /// Display a copyright notice.
    /// </summary>
    /// <param name="companyName">Copyright owner</param>
    /// <param name="trailingBlankLine">Add a blank line afterwards.</param>
    /// <param name="showAuthor">Show <see cref="ConHelperData.Author"/></param>
    public static void AppCopyright(string companyName, bool trailingBlankLine = true, bool showAuthor = false)
    {
      AppCopyright(DateTime.Now.Year.ToString(), companyName, trailingBlankLine, showAuthor);
    }

    /// <summary>
    /// Display a copyright notice.
    /// </summary>
    /// <param name="conData">Copyright owner</param>
    /// <param name="trailingBlankLine">Add a blank line afterwards.</param>
    /// <param name="showAuthor">Show <see cref="ConHelperData.Author"/></param>
    public static void AppCopyright(ConHelperData conData, bool trailingBlankLine = true, bool showAuthor = false)
    {
      AppCopyright(DateTime.Now.Year.ToString(), conData.Company, trailingBlankLine, showAuthor);
    }

    /// <summary>
    /// Display a copyright notice.
    /// </summary>
    /// <param name="year">Copyrighted in year</param>
    /// <param name="companyName">Copyright owner</param>
    /// <param name="trailingBlankLine">Add a blank line afterwards.</param>
    /// <param name="showAuthor">Show <see cref="ConHelperData.Author"/></param>
    public static void AppCopyright(string year, string companyName, bool trailingBlankLine = true, bool showAuthor = false)
    {
      Console.WriteLine(System.String.Format("Copyright {0} {1} by {2}. All rights reserved.", Chr(169), year, companyName));
      if (showAuthor)
      {
        Console.WriteLine("Written by " + ConsoleData.Author);
      }

      if (trailingBlankLine == true)
      {
        Console.WriteLine("");
      }
    }
    #endregion  // "AppCopyright"

    #region AnyKey
    /// <summary>
    /// Pauses the program execution and waits for a key press
    /// </summary>
    public static void AnyKey()
    {
      AnyKey("-- Press any key to continue --", 0, 0);
    }  // AnyKey

    /// <summary>
    /// Pauses the program execution and waits for a key press
    /// </summary>
    /// <param name="blankLinesBefore">Number of blank lines before the message</param>
    /// <param name="blankLinesAfter">Number of blank lines after the message</param>
    public static void AnyKey(Int32 blankLinesBefore = 0,
                            Int32 blankLinesAfter = 0)
    {
      AnyKey("-- Press any key to continue --", blankLinesBefore, blankLinesAfter);
    }  // AnyKey

    /// <summary>
    /// Pauses the program execution and waits for a key press
    /// </summary>
    /// <param name="waitMessage">Pause message</param>
    /// <param name="blankLinesBefore">Number of blank lines before the message</param>
    /// <param name="blankLinesAfter">Number of blank lines after the message</param>
    public static void AnyKey(string waitMessage = "-- Press any key to continue --",
                            Int32 blankLinesBefore = 0,
                            Int32 blankLinesAfter = 0)
    {
      BlankLine(blankLinesBefore);
      Console.WriteLine(waitMessage);
      BlankLine(blankLinesAfter);
      Console.ReadKey(true);
    }  // AnyKey

    #endregion

    /// <summary>
    /// Insert a blank line at the current position.
    /// </summary>
    /// <param name="blankLines">Number of blank lines to insert.</param>
    /// <param name="addSeparatingLine"><see langword="true"/>: Add a visual separation indicator before the blank line(s)</param>
    public static void BlankLine(Int32 blankLines = 1, bool addSeparatingLine = false)
    {

      // Safe guard
      if (blankLines < 1)
      {
        blankLines = 1;
      }

      if (addSeparatingLine == true)
      {
        Console.WriteLine(ConHelperData.CON_SEPARATOR);
      }

      for (Int32 i = 0; i <= blankLines - 1; i++)
      {
        Console.WriteLine("");
      }

    }  // BlankLine

    #region WriteIndent

    /// <summary>
    /// Output text indented by (<paramref name="indentBy"/>) spaces
    /// </summary>
    /// <param name="text">Output text</param>
    /// <param name="indentBy">Number of leading spaces</param>
    /// <param name="addNewLine">Add a new line after <paramref name="text"/></param>
    public static void WriteIndent(string text, Int32 indentBy, bool addNewLine = true)
    {
      if (addNewLine == true)
      {
        Console.WriteLine(String.Concat(new String(Convert.ToChar(" "), indentBy) + text));
      }
      else
      {
        Console.Write(String.Concat(new String(Convert.ToChar(" "), indentBy) + text));
      }
    }  // WriteIndent

    /// <summary>
    /// Output text indented by (<paramref name="indentBy"/>) spaces
    /// </summary>
    /// <param name="text">Output text</param>
    /// <param name="indentBy">Number of leading spaces</param>
    /// <param name="addNewLine">Add a new line after the last line of <paramref name="text"/></param>
    public static void WriteIndent(string[] text, Int32 indentBy, bool addNewLine = true)
    {
      for (int i = 0; i <= text.Length - 2; i++)
      {
        Console.WriteLine(String.Concat(new String(Convert.ToChar(" "), indentBy) + text[i]));
      }
      if (addNewLine == true)
      {
        Console.WriteLine(String.Concat(new String(Convert.ToChar(" "), indentBy) + text[text.Length - 1]));
      }
      else
      {
        Console.Write(String.Concat(new String(Convert.ToChar(" "), indentBy) + text[text.Length - 1]));
      }
    }  // WriteIndent

    #endregion

    #region Constructor

    /// <summary>
    /// Class Constructor
    /// </summary>
    public ConsoleHelper() { }

    /// <summary>
    /// Class Constructor
    /// </summary>
    /// <param name="consoleData">Application developer, copyright holder and line separator.</param>
    public ConsoleHelper(ConHelperData consoleData)
    {
      moConData = consoleData;
    } // ConsoleHelper(ConHelperData consoleData)

    /// <summary>
    /// Class Constructor
    /// </summary>
    /// <param name="authorName">Application developer name</param>
    /// <param name="companyName">Company / Copyright holder name</param>
    /// <param name="lineSeparator">Line separator</param>
    public ConsoleHelper(string authorName, string companyName, string lineSeparator)
    {
      ConsoleData.Author = authorName;
      ConsoleData.Company = companyName;
      ConsoleData.LineSeparator = lineSeparator;
    } // ConsoleHelper(string authorName, string companyName, string lineSeparator)

    #endregion

    /// <summary>
    /// Starts a "spinning wheel" kinda wait time indicator
    /// </summary>
    /// <param name="newLine">Add a newline on the first post?</param>
    /// <param name="spinDelay">Spin steps in milliseconds</param>
    public void WaitIndicatorStart(bool newLine = true, Int32 spinDelay = 100)
    {

      mThdCurLeft = Console.CursorLeft;
      mThdCurTop = Console.CursorTop;
      mThdNewLine = newLine;
      mThdSpinDelay = spinDelay;
      thdWaitIndicator = new System.Threading.Thread(new System.Threading.ThreadStart(WaitIndicator));
      thdWaitIndicator.Start();

    }

    /// <summary>
    /// Stops a previously started <see cref="WaitIndicatorStart(bool, Int32)"/> wait time indicator.
    /// </summary>
    public void WaitIndicatorStop()
    {

      if (!(thdWaitIndicator == null))
      {
        thdWaitIndicator.Abort();
        thdWaitIndicator.Join();
      }

    }

    /// <summary>
    /// Object's finalize
    /// </summary>
    ~ConsoleHelper()
    {
      WaitIndicatorStop();
    }

    private void WaitIndicator()
    {
      Int32 lStep = 0;
      Int32 curLeftOld;
      Int32 curTopOld;
      bool blnBeenHere = false;
      bool curVisibleOld;

      string[] asChar = { "/", "-", "\\", "|" };

      // Safe guard
      if (Console.IsOutputRedirected) { return; }

      do
      {

        System.Threading.Thread.BeginCriticalRegion();

        curLeftOld = Console.CursorLeft;
        curTopOld = Console.CursorTop;
        curVisibleOld = Console.CursorVisible;

        Console.SetCursorPosition(mThdCurLeft, mThdCurTop);
        Console.CursorVisible = false;
        Console.Write(asChar[lStep]);

        if (blnBeenHere == false)
        {
          if (mThdNewLine == true)
          {
            curTopOld += 1;
            curLeftOld = 0;
          }
          blnBeenHere = true;
        }

        lStep += 1;
        if (lStep >= 3)
        {
          lStep = 0;
        }
        Console.SetCursorPosition(curLeftOld, curTopOld);
        Console.CursorVisible = curVisibleOld;
        System.Threading.Thread.EndCriticalRegion();

        System.Threading.Thread.Sleep(mThdSpinDelay);
      } while (true);

    }

  }  // class ConsoleHelper

}  // namespace libBAUtilCoreCS
