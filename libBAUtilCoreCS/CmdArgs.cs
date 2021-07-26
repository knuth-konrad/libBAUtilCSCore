using System;
using System.Collections.Generic;
using System.Text;

using static libBAUtilCoreCS.StringUtil;

namespace libBAUtilCoreCS.Utils.Args
{

   /// <summary>
   /// Commandline parameter handling
   /// See https://docs.microsoft.com/en-us/archive/msdn-magazine/2019/march/net-parse-the-command-line-with-system-commandline
   /// See https://github.com/commandlineparser/commandline
   /// </summary>
   class CmdArgs
   {

      #region "Declarations

      public enum eArgumentDelimiterStyle { Windows, POSIX };


      // Default arguments and key/value delimiter
      private const string DELIMITER_ARGS_WIN = "/";
      private const string DELIMITER_ARGS_POSIX = "--";
      private const string DELIMITER_VALUE = "=";

      Boolean mbolCaseSensitive;  // Treat parameter names as case-sensitive?

      private string msDelimiterArgs = String.Empty;  // Arguments delimiter, typically "/"
      private string msDelimiterValue = String.Empty; // Key/value delimiter, typically "="
      private List<string> listValidParameters;       // List of all valid parameter

      private List<KeyValue> mcolKeyValues;

      #endregion

      #region "Properties - Public"
      
      public Int32 ParametersCount
      {
         get
         {
            if (KeyValues != null)
            {
               return KeyValues.Count;
            }
            else
            {
               return 0;
            }
         }
      }

      public Boolean CaseSensitive
      {
         get { return mbolCaseSensitive; }
         set { mbolCaseSensitive = value; }
      }

      public string DelimiterArgs
      {
         get { return msDelimiterArgs; }
         set { msDelimiterArgs = value; }
      }

      public string DelimiterValue
      {
         get { return msDelimiterValue; }
         set { msDelimiterValue = value; }
      }

      public List<string> ValidParameters
      {
         get { return listValidParameters; }
         set { listValidParameters = value; }
      }

      public List<KeyValue> KeyValues
      {
         get { return mcolKeyValues; }
         set { mcolKeyValues = value; }
      }

      #endregion

      #region "Methods - Private"
      
      /// <summary>
      /// Is a parameter present more than once?
      /// </summary>
      private Boolean HasDuplicate(KeyValue currentKey, Int32 currentIndex)
      {

         // Safe guard
         if (KeyValues.Count < 1) { return false; }

         // Compare the passed parameter to the list and see if there's a duplicate entry
         for (Int32 i = 0;  i<= KeyValues.Count - 1; i++)
         {
            KeyValue o = KeyValues[i];

            if (CaseSensitive == false)
            {
               if ((o.Key.ToLower() == currentKey.Key.ToLower()) && (i != currentIndex))
               {
                  return true;
               }
            }
            else
            {
               if ((o.Key == currentKey.Key) && (i != currentIndex))
               {
                  return true;
               }
            }

         }  // for (Int32 i = 0;  i<= KeyValues.Count - 1; i++)

            // Reaching here, we've found no duplicates
            return false;
      }

      private Boolean ParseCmd(string[] asArgs, Int32 startIndex = 0)
      {

         string sKey = String.Empty;
         string sValue = String.Empty;
         Boolean bolResult = true;

         for (Int32 i = startIndex; i<=  asArgs.Length - 1; i++)
         {
            bolResult = bolResult && ParseParam(asArgs[i]);
         }

         return bolResult;

      }



      /// <summary>
      /// Parses a single key/pair combo into a matching <see cref="KeyValue"/> object
      /// </summary>
      /// <param name="sParam"></param>
      /// <returns></returns>
      private Boolean ParseParam(string sParam)
      {


         if (sParam.Length < 1)
         {
            return true;
         }

         if (sParam.Contains(DelimiterValue))
         {

            // Parameter of the form /key=value

            KeyValue o = new KeyValue(sParam);

            // '/file' for /file=MyFile.txt
            KeyLong = Left(sParam, InStr(sParam, DelimiterValue) - 1).Trim
            // Remove the leading delimiter, results in 'file'
            If.KeyLong.IndexOf(Me.DelimiterArgs) > -1 Then
               .KeyLong = Mid(.KeyLong, Me.DelimiterArgs.Length + 1)
            End If
            // Since we parse this from the command line, set both
            .KeyShort = .KeyLong
            .Value = Mid(sParam, InStr(sParam, DelimiterValue) + 1)

            .KeyValues.Add(o)

         Else
            ' Parameter of the form /Value.
            ' These are considered to be boolean parameters. If present, their value is 'True'

            Dim o As New KeyValue(sParam)

            With o
               .KeyLong = sParam.Trim
               If .KeyLong.IndexOf(Me.DelimiterArgs) > -1 Then
                  .KeyLong = Mid(.KeyLong, Me.DelimiterArgs.Length + 1)
               End If
               ' Since we parse this from the command line, set both
               .KeyShort = .KeyLong
               .Value = True
            End With

            .KeyValues.Add(o)

         End If

               return true;

      }


      #endregion

   }  // CmdArgs

   class KeyValue
   {

      private string msHelpText = "";
      private string msKeyLong = "";
      private string msKeyShort = "";
      private string msOriginalParameter = "";

      private Object moValue;

      /// <summary>
      /// Help text for this parameter
      /// </summary>
      public string HelpText
      {
         get { return msHelpText; }
         set { msHelpText = value; }
      }
 
      /// <summary>
      /// 'Outspoken' parameter name, e.g. /file
      /// </summary>
      public string KeyLong
      {
         get { return msKeyLong; }
         set { msKeyLong = value; }
      }

      /// <summary>
      /// Short parameter name, e.g. /f
      /// </summary>
      public string KeyShort
      {
         get { return msKeyShort; }
         set { msKeyShort = value; }
      }

      /// <summary>
      /// Parameter name, e.g. 'file' or 'f' from /file=MyFile.txt  or /f=MyFile.txt
      /// </summary>
      public string Key
      {
         get
         { // KeyLOng tales precedence
            if (this.KeyLong.Length > 0)
            {
               return KeyLong;
            }
            else
            {
               return KeyShort;
            }
         }
      }

      /// <summary>
      /// The full original parameter, e.g. /file=MyFile.txt
      /// </summary>
      /// <returns></returns>
     public string OriginalParameter
      {
         get { return msOriginalParameter; }
         set { msOriginalParameter = value};
      }

      /// <summary>
      /// Parameter value, e.g. 'MyFile.txt' from /file=MyFile.txt
      /// </summary>
      public Object Value
      {
         get { return moValue; }
         set { moValue = value; }
      }

   #region "Methods - Public"

      public override string ToString()
      {
         string sText = this.Key;
         if (this.HelpText.Length<1)
         {
            return sText;
         }
         else
         {
            return sText + ": " + this.HelpText;
         }
      }

         #endregion


      public KeyValue(string originalParam, string keyShort = "",
                        string keyLong = "", Object value = null,
                        string helpText = "")
      {
         // ToDo: base bzw.MyBase needed in C# constructor

         HelpText = helpText;
         KeyLong = keyLong;
         KeyShort = keyShort;
         OriginalParameter = originalParam;
         Value = value;
      }


      }
   }
