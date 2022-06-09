using System;
using System.IO;

namespace libBAUtilCoreCS
{

  /// <summary>
  /// Simple text file manipulation.
  /// </summary>
  class TextFileUtil
  {
    /// <summary>
    /// Read a text file from disk and return it as a string
    /// </summary>
    /// <param name="textFile">Text file incl. full path</param>
    /// <returns>Contents of <paramref name="textFile"/> as a string</returns>
    public static string TxtReadFile(string textFile)
    {
      string file = String.Empty;
      using (var reader = new StreamReader(textFile))
      {
        file = reader.ReadToEnd();
      }
      return file;

    }  // TxtReadFile

    /// <summary>
    /// Retrieve a line of a text file and return it as a string
    /// </summary>
    /// <param name="textFile">Text file incl. full path</param>
    /// <returns>The line at the current position of the text file</returns>
    public static string TxtReadLine(string textFile)
    {
      string line = String.Empty;
      using (var reader = new StreamReader(textFile))
      {
        line = reader.ReadLine();
      }
      return line;
    }  // TxtReadLine

    /// <summary>
    /// Write a line to a text file
    /// </summary>
    /// <param name="textFile">Text file incl. full path</param>
    /// <param name="textLine">Contents of new line</param>
    /// <param name="doAppend">
    /// <see langref="true"/>: append <paramref name="textLine"/> at the end of <paramref name="textFile"/><br />
    /// <see langref="false"/>: insert <paramref name="textLine"/> at the current position of <paramref name="textFile"/>
    /// </param>
    public static void TxtWriteLine(string textFile, string textLine, bool doAppend = true)
    {
      using (var writer = new StreamWriter(textFile, doAppend))
      {
        writer.WriteLine(textLine);
      }
    }  // TxtWriteLine

  }  // class FileUtil

}
