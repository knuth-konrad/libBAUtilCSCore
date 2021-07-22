using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Diagnostics;

namespace libBAUtilCoreCS
{

   /// <summary>
   ///  General purpose string handling/formatting helpers
   ///  <remarks>
   ///  VB.NET / C# equivalents: https://sites.harding.edu/fmccown/vbnet_csharp_comparison.html
   /// </remarks>
   /// </summary>
   public class StringUtil
   {
      #region "Declares"
      /// Bytes to <unit> - Function Bytes2FormattedString()

      /// <summary>Data storage units</summary>
      /// <seealso cref="Bytes2FormattedString"/>
      public enum ESizeUnits : ulong
      {
         B = 1024,
         KB = B * B,
         MB = KB * B,
         GB = MB * B,
         TB = GB * B

      }
      #endregion

      /// <summary>
      /// Creates a formatted string representing the size in its proper 'spelled out' unit
      /// (Bytes, KB etc.)
      /// </summary>
      /// <param name="uintBytes">Number of bytes to transform</param>
      /// <returns>
      /// Spelled out size, e.g. 1030 -> '1KB'
      /// </returns>
      /// <remarks>
      /// Author: dbasnett<br />
      /// Source: http://www.vbforums.com/showthread.php?634675-RESOLVED-Bytes-to-MB-etc
      /// </remarks>
      public static string Bytes2FormattedString(ulong uintBytes)
      {
         string sUnits = string.Empty, szAsStr = string.Empty;
         float dblInUnits;

         if (uintBytes < (ulong)ESizeUnits.B)
         {
            szAsStr = uintBytes.ToString("n0");
            sUnits = "Bytes";
         }
         else if (uintBytes <= (ulong)ESizeUnits.KB)
         {
            dblInUnits = uintBytes / (ulong)ESizeUnits.B;
            szAsStr = dblInUnits.ToString("n1");
            sUnits = "KB";
         }
         else if (uintBytes <= (ulong)ESizeUnits.MB)
         {
            dblInUnits = uintBytes / (ulong)ESizeUnits.KB;
            szAsStr = dblInUnits.ToString("n1");
            sUnits = "MB";
         }
         else if (uintBytes <= (ulong)ESizeUnits.GB)
         {
            dblInUnits = uintBytes / (ulong)ESizeUnits.MB;
            szAsStr = dblInUnits.ToString("n1");
            sUnits = "GB";
         }
         else
         {
            dblInUnits = uintBytes / (ulong)ESizeUnits.GB;
            szAsStr = dblInUnits.ToString("n1");
            sUnits = "TB";
         }

         return System.String.Format("{0} {1}", szAsStr, sUnits);
      }

      /// <summary>
      /// Creates a formatted string representing the size in its proper 'spelled out' unit
      /// (Bytes, KB etc.)
      /// </summary>
      /// <param name="uintBytes">Number of bytes to transform</param>
      /// <returns>
      /// Spelled out size, e.g. 1030 -> '1KB'
      /// </returns>
      /// <remarks>
      /// Author: dbasnett<br />
      /// Source: http://www.vbforums.com/showthread.php?634675-RESOLVED-Bytes-to-MB-etc
      /// </remarks>
      public static string Bytes2FormattedString(ulong uintBytes, bool largestUnitOnly = true)
      {

         ulong uintDivisor;
         string sUnits = string.Empty, szAsStr = string.Empty;

         if (largestUnitOnly)
         {
            return Bytes2FormattedString(uintBytes);
         }

         while (uintBytes > 0)
         {
            if (uintBytes / (ulong)System.Math.Pow(1024, 4) > 0)
            {
               // TB
               uintDivisor = uintBytes / (ulong)System.Math.Pow(1024, 4);
               uintBytes = uintBytes - (uintDivisor * (ulong)System.Math.Pow(1024, 4));
               sUnits = "TB ";
               Debug.Print(System.String.Format("TB - uintDivisor: {0}, uintBytes: {1}", uintDivisor, uintBytes));   
            }
            else if (uintBytes / (ulong)System.Math.Pow(1024, 3) > 0)
            {
               // GB
               uintDivisor = uintBytes / (ulong)System.Math.Pow(1024, 3);
               uintBytes = uintBytes - (uintDivisor * (ulong)System.Math.Pow(1024, 3));
               sUnits = "GB ";
               Debug.Print(System.String.Format("GB - uintDivisor: {0}, uintBytes: {1}", uintDivisor, uintBytes));
            }
            else if (uintBytes / (ulong)System.Math.Pow(1024, 2) > 0)
            {
               // MB
               uintDivisor = uintBytes / (ulong)System.Math.Pow(1024, 2);
               uintBytes = uintBytes - (uintDivisor * (ulong)System.Math.Pow(1024, 2));
               sUnits = "MB ";
               Debug.Print(System.String.Format("MB - uintDivisor: {0}, uintBytes: {1}", uintDivisor, uintBytes));
            }
            else if (uintBytes / (ulong)System.Math.Pow(1024, 1) > 0)
            {
               // KB
               uintDivisor = uintBytes / (ulong)System.Math.Pow(1024, 1);
               uintBytes = uintBytes - (uintDivisor * (ulong)System.Math.Pow(1024, 1));
               sUnits = "KB ";
               Debug.Print(System.String.Format("KB - uintDivisor: {0}, uintBytes: {1}", uintDivisor, uintBytes));
            }
            else
            {
               // B
               uintDivisor = uintBytes / (ulong)System.Math.Pow(1024, 0);
               uintBytes = uintBytes - (uintDivisor * (ulong)System.Math.Pow(1024, 0));
               sUnits = "B ";
               Debug.Print(System.String.Format(" B - uintDivisor: {0}, uintBytes: {1}", uintDivisor, uintBytes));
            }

            szAsStr = szAsStr + uintDivisor.ToString("n0") + sUnits;

         }  // while

         return szAsStr.TrimEnd();
      }

      /// <summary>
      /// Replacement for VB6's Chr() function.
      /// </summary>
      /// <param name="ansiValue">ANSI value for which to return a string</param>
      /// <returns>
      /// ANSI String-Representation of <paramref name="ansiValue"/>
      /// </returns>
      /// <remarks>
      /// Source: https://stackoverflow.com/questions/36976240/c-sharp-char-from-int-used-as-string-the-real-equivalent-of-vb-chr?lq=1
      /// </remarks>
      public static string Chr(Int32 ansiValue)
      {
         return Char.ConvertFromUtf32(ansiValue);
      }

      /// <summary>
      /// Replacement for VB6's Chr() function.
      /// </summary>
      /// <param name="ansiValue">ANSI value for which to return a string</param>
      /// <returns>
      /// ANSI String-Representation of <paramref name="ansiValue"/>
      /// </returns>
      public static string Chr(UInt32 ansiValue)
      {
         // Return Char.ConvertFromUtf32(CType(ansiValue, Int32))
         return Convert.ToChar(ansiValue).ToString();
      }

      /// <summary>
      /// Capitalize the first letter of a string.
      /// </summary>
      /// <param name="sText">Source string</param>
      /// <param name="sCulture">Specific culture string e.g. "en-US"</param>
      /// <returns>
      /// <paramref name="sText"/> with the first letter capitalized.
      /// </returns>
      /// <remarks>
      /// Source: https://social.msdn.microsoft.com/Forums/vstudio/en-US/c0872f6d-2975-43e6-872a-d2ba7901ed0e/convert-first-letter-of-string-to-capital?forum=csharpgeneral
      /// </remarks>
      public static string MCase(string sText, string sCulture = "")
      {

         TextInfo ti;

         try
         {
            if (sCulture.Length > 0)
            {
               ti = new CultureInfo(sCulture, false).TextInfo;
               return ti.ToTitleCase(sText);
            }
            else
            {
               return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(sText);
            }
         }
         catch
         {
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(sText);
         }

      }

      /// <summary>
      /// Implements VB6's Left$() functionality.
      /// </summary>
      /// <param name="source">Source string</param>
      /// <param name="leftChars">Number of characters to return</param>
      /// <returns>
      /// For leftChars ...<br />
      ///    &gt; source.Length: source<br />
      ///    = 0: Empty string<br />
      ///    &lt; 0: Position from the end of source, e.g. Left("1234567890", -2) -> "12345678"
      /// </returns>
      /// <remarks>
      /// Source: Developed from https://stackoverflow.com/questions/844059/net-equivalent-of-the-old-vb-leftstring-length-function/12481156
      /// </remarks>
      public static string Left(string source, int leftChars)
      {

         if (System.String.IsNullOrEmpty(source) || leftChars == 0)
         {
            return System.String.Empty;
         }
         else if (leftChars > source.Length)
         {
            return source;
         }
         else if (leftChars < 0)
         {
            return source.Substring(0, source.Length + leftChars);
         }
         else
         {
            return source.Substring(0, Math.Min(leftChars, (int)(source.Length)));
         }

      }

      /// <summary>
      /// Implements VB's/PB's Right$() functionality.
      /// </summary>
      /// <param name="source">Source string</param>
      /// <param name="rightChars">Number of characters to return</param>
      /// <returns>
      /// For rightChars ...<br />
      ///    &gt; source.Length: source<br />
      ///    = 0: Empty string<br />
      ///    &lt; 0: Position from the start of source, e.g. Right("1234567890", -2) -&gt; "34567890"
      /// </returns>
      /// <remarks>
      /// Source: Developed from https://stackoverflow.com/questions/844059/net-equivalent-of-the-old-vb-leftstring-length-function/12481156
      /// </remarks>
      public static string Right(string source, int rightChars)
      {
         if (System.String.IsNullOrEmpty(source) || rightChars == 0)
         {
            return string.Empty;
         }
         else if (rightChars > source.Length)
         {
            return source;
         }
         else if (rightChars < 0)
         {
            return source.Substring(Math.Abs(rightChars));
         }
         else
         {
            return source.Substring(source.Length - rightChars, rightChars);
         }
      }

      /// <summary>
      /// Implements VB6's/PB's Mid$() functionality, as .NET's String.SubString()
      /// differs in its behavior that it raises an exception if startIndex > source.Length, 
      /// whereas Mid$() returns an empty string in such a case.
      /// </summary>
      /// <param name="source">Source string</param>
      /// <param name="startIndex">(0-based) start</param>
      /// <param name="length">Number of chars to return</param>
      /// <returns>
      /// For <paramref name="startIndex"/> &gt; <paramref name="source"/>.Length: <see cref="String.Empty"/>
      /// For <paramref name="length"/> &gt; <paramref name="startIndex"/> + <paramref name="source"/>.Length: all of <paramref name="source"/> from <paramref name="startIndex"/>
      /// </returns>
      /// <remarks>
      /// Source: Developed from https://stackoverflow.com/questions/844059/net-equivalent-of-the-old-vb-leftstring-length-function/12481156
      /// </remarks>
      public static string Mid(string source, int startIndex, int length = 0)
      {
         // Safe guards
         if (System.String.IsNullOrEmpty(source) || (startIndex > source.Length))
         {
            return System.String.Empty;
         }
         if (startIndex < 0)
         {
            throw new ArgumentOutOfRangeException("startIndex", "Must be 0 or greater.");
         }
         if (length < 0)
         {
            throw new ArgumentOutOfRangeException("length", "Must be 0 or greater.");
         }

         // Adjust length, if needed
         try
         {
            if (startIndex + length > source.Length || length == 0)
            {
               return source.Substring(startIndex - 1);
            }
            else
            {
               return source.Substring(startIndex - 1, length);
            }
         }
         catch
         {
            return System.String.Empty;
         }
      }

      /// <summary>
      /// Encloses <paramref name="text"/> with double quotation marks (").
      /// </summary>
      /// <param name="text">Wrap this string in quotation marks.</param>
      /// <returns><paramref name="text"/> enclosed in double quotation marks (")</returns>
      public static string EnQuote(string text)
      {
         return '"' + text + '"';
         /// Convert.ToChar(34).ToString + text + Convert.ToChar(34).ToString;
      }

      /// <summary>
      /// Mimics VB6's Space() function
      /// </summary>
      /// <param name="count">Number of space</param>
      /// <returns>String of <paramref name="count"/> spaces</returns>
      public static string Space(Int32 count)
      {
         return new System.String(' ', (int)count);
      }

      /// <summary>
      /// Mimics VB6's Space() function
      /// </summary>
      /// <param name="count">Number of space</param>
      /// <returns>String of <paramref name="count"/> spaces</returns>
      public static string Space(uint count)
      {
         return new System.String(' ', (int)count);
      }

      #region "Method StrRepeat()"
      /// <summary>
      /// Mimics VB6's String() Function
      /// </summary>
      /// <param name="character">Character to use</param>
      /// <param name="count">Number of characters</param>
      /// <returns>String of <paramref name="count"/> x <paramref name="character"/></returns>

      public static string StrRepeat(char character, Int32 count)
      {
         return new System.String(character, count);
      }

      public static string StrRepeat(string character, Int32 count)
      {

         // Safe guard
         if (character.Length != 1)
         {
            throw new ArgumentOutOfRangeException("Length must be 1.", character);
         }

         char temp = character[0];

         return new System.String(temp, count);
      }

      #endregion

      #region "Date formatting"
      /// <summary>
      /// Create a date string of format YYYYMMDD[[T]HHNNSS].
      /// </summary>
      /// <param name="dtmDate">Date/Time to format</param>
      /// <param name="appendTime"><see langref="true"/> = append time to date</param>
      /// <param name="dateSeparator">Character to separate date parts</param>
      /// <param name="dateTimeSeparator">Character to separate date part from time part</param>
      /// <returns>
      /// Date/time formatted as string.
      /// </returns>
      public static string DateYMD(DateTime dtmDate, Boolean appendTime = false, string dateSeparator = "", 
         string dateTimeSeparator = "T")
      {

         // Date part
         string sResult = dtmDate.Year.ToString("0000") + dateSeparator + dtmDate.Month.ToString("00") + dateSeparator +
            dtmDate.Day.ToString("00");


         // Time part
         if (appendTime == true)
         {
            sResult += dateTimeSeparator + dtmDate.Hour.ToString("00") + dtmDate.Minute.ToString("00") + dtmDate.Second.ToString("00");
         }

         return sResult;

      }
      #endregion

      #region "VB6 String constants"
      // ** Replacements for various handy VB6 string constants

      /// <summary>
      /// Mimics VB6's vbNewLine constant.
      /// </summary>
      /// <param name="n">Return this number of new lines</param>
      /// <returns>OS-specific new line character(s)</returns>
      public static string vbNewLine(Int32 n = 1)
      {
         string sResult = System.String.Empty;

         for (Int32 i = 1; i <= n; i++)
         {
            sResult += Environment.NewLine;
         }
         return sResult;
      }

      /// <summary>
      /// Mimics VB6's vbNullString constant
      /// </summary>
      /// <returns>String.Empty</returns>
      public static string vbNullString()
         {
            return System.String.Empty;
      }
      
      /// <summary>
      /// Constant for a double quotation mark (").
      /// </summary>
      /// <param name="n">Number of DQs to return</param>
      /// <returns><paramref name="n"/> "</returns>
      public static string vbQuote(Int32 n = 1)
      {
         string sResult = System.String.Empty;
         for (Int32 i = 1; i <= n; i++)
         {
            sResult += Convert.ToChar(34).ToString();
         }
         return sResult;
      }

   /// <summary>
   /// Mimics VB6's vbTab constant.
   /// </summary>
   /// <param name="n">Number of tabs to return</param>
   /// <returns><paramref name="n"/> tabs.</returns>
   public static string vbTab(Int32 n = 1)
      {
         string sResult = System.String.Empty;
         for (Int32 i = 1; i <= n; i++)
         {
            sResult += System.Convert.ToChar(9).ToString();
         }
         return sResult;
      }
      #endregion
   } // class StringUtil
}
