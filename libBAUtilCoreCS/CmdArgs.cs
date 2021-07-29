using System;
using System.Collections.Generic;
using System.Text;

using static libBAUtilCS.StringUtil;

namespace libBAUtilCS.Utils.Args
{

   /// <summary>
   /// Commandline parameter handling
   /// See https://docs.microsoft.com/en-us/archive/msdn-magazine/2019/march/net-parse-the-command-line-with-system-commandline
   /// See https://github.com/commandlineparser/commandline
   /// </summary>
   public class CmdArgs
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

      private Boolean ParseCmd(string sArgs)
      {
         string[] asArgs = sArgs.Split(Convert.ToChar(DelimiterArgs));
         return ParseCmd(asArgs);
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
            o.KeyLong = Left(sParam, sParam.IndexOf(DelimiterValue)).Trim();
            // Remove the leading delimiter, results in 'file'
            if (o.KeyLong.IndexOf(DelimiterArgs) > -1)
            {
               o.KeyLong = Mid(o.KeyLong, DelimiterArgs.Length);
            }
            // Since we parse this from the command line, set both
            o.KeyShort = o.KeyLong;
            o.Value = Mid(sParam, sParam.IndexOf(DelimiterValue) + 1);

            KeyValues.Add(o);
         }
         else
         {

            // Parameter of the form /Value.
            // These are considered to be boolean parameters. If present, their value is 'true'

            KeyValue o = new KeyValue(sParam);

            o.KeyLong = sParam.Trim();
            if (o.KeyLong.IndexOf(DelimiterArgs) > -1)
            {
               // o.KeyLong = Mid(o.KeyLong, DelimiterArgs.Length + 1);
               o.KeyLong = Mid(o.KeyLong, DelimiterArgs.Length);
            }
            // Since we parse this from the command line, set both
            o.KeyShort = o.KeyLong;
            o.Value = true;

            KeyValues.Add(o);
         }
         return true;
      }  // ParseParam


      /// <summary>
      /// Retrieve the parameter delimiter according to the OS' typical flavor
      /// </summary>
      /// <returns>OS typical parameter delimiter</returns>
      private string GetDefaultDelimiterForOS()
      {
         if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
         {
            return DELIMITER_ARGS_WIN;
         }
         else
         {
            return DELIMITER_ARGS_POSIX;
         }
      }

      #endregion

      #region "Methods - Public"

      /// <summary>
      /// Initializes the object by parsing System.Environment.GetCommandLineArgs()
      /// </summary>
      /// <returns></returns>
      public Boolean Initialize(string cmdLineArgs = "")
      {

         // Clear everything, as we're parsing anew.
         KeyValues = new List<KeyValue>();

         if (cmdLineArgs.Length< 1)
         {
            string[] asArgs = System.Environment.GetCommandLineArgs();
            // When using System.Environment.GetCommandLineArgs(), the 1st array element is the executable's name
            return ParseCmd(asArgs, 1);
         }
         else
         {
            return ParseCmd(cmdLineArgs);
         }
      }  // Initialize

      /// <summary>
      /// Validates all passed parameters
      /// </summary>
      public void Validate()
      {
         // Safe guard
         if (KeyValues.Count < 1)
         {
            return;
         }

         // Any duplicates?
         for (Int32 i = 0; i <= KeyValues.Count - 1; i++)
         {
            KeyValue o = KeyValues[i];
            if (HasDuplicate(o, i) == true)
            {
               throw new ArgumentException(String.Format("Duplicate parameter: {0}", o.Key));
            }
         }

      }  // Validate

      /// <summary>
      /// Determine if a certain parameter is present
      /// </summary>
      /// <param name="key">Parameter's name(<see cref = "KeyValue.Key" />)</param>
      /// <returns></returns>
      public Boolean HasParameter(string key)
      {
         foreach (KeyValue o in KeyValues)
         {

            if (CaseSensitive == true)
            {
               if (o.Key == key) { return true; }
            }
            else
            {
               if (o.Key.ToLower() == key.ToLower()) { return true; }
            }
         }

         return false;
      }  // HasParameter

      /// <summary>
      /// Determine if a certain parameter is present
      /// </summary>
      /// <param name="paramlist">List of parameter names (<see cref="KeyValue.Key"/>)</param>
      /// <returns>
      /// <see langword="true"/> if all parameters passed are present, otherwise <see langword="false"/>
      /// </returns>
      public Boolean HasParameter(List<string> paramList)
      {
         foreach (string s in paramList)
            {
               if (HasParameter(s) == false) { return false; }
            }
            // Reaching here, all parameters have been found
         return true;
      }  // HasParameter

      /// <summary>
      /// Return the corresponding KeyValue object
      /// </summary>
      /// <param name="key">The parameter's name(<see cref = "KeyValue.Key" /></ param >
      /// <param name="caseSensitive">Treat the name as case-sensitive?</param>
      /// <returns><see cref="KeyValue"/> whose <see cref="KeyValue.Key"/> equals <paramref name="key"/>.</returns>
      public KeyValue GetParameterByName(string key, Boolean caseSensitive = false)
      {

         // Safe guard
         if (HasParameter(key) == false) { throw new ArgumentException("Parameter doesn't exist: " + key); }

         foreach (KeyValue o in KeyValues)
         {
            if (caseSensitive == false)
            {
               if (o.Key.ToLower() == key.ToLower()) { return o; }
            }
            else
            {
               if (o.Key == key) { return o; }
            }
         }

         // We should never reach this point
         return null;

      }  // GetParameterByName

      /// <summary>
      /// Return the value of a parameter
      /// </summary>
      /// <param name="key">The parameter's name(<see cref = "KeyValue.Key" /></ param >
      /// <param name="caseSensitive">Treat the name as case-sensitive?</param>
      /// <returns></returns>
      public Object GetValueByName(string key, Boolean caseSensitive = false)
      {

         // Safe guard
         if (HasParameter(key) == false) { throw new ArgumentException("Parameter doesn't exist: " + key); }

         foreach (KeyValue o in KeyValues)
         {
            if (caseSensitive == false)
            {
               if (o.Key.ToLower() == key) { return o.Value; }
            }
            else
            {
               if (o.Key == key) { return o.Value; }
            }
         }
         // We should never reach this point
         return null;
      }  // GetValueByName

      #endregion

      #region "Constructor/Dispose"
      
      public CmdArgs()
      {
         DelimiterArgs = GetDefaultDelimiterForOS();
         DelimiterValue = DELIMITER_VALUE;
         ValidParameters = new List<String>();
      }

      public CmdArgs(List<string> validParams = null)
      {
         DelimiterArgs = GetDefaultDelimiterForOS();
         DelimiterValue = DELIMITER_VALUE;
         if (validParams != null)
         {
            ValidParameters = validParams;
         }
         else
         {
            ValidParameters = new List<string>();
         }
      }

      public CmdArgs(string delimiterArgs = DELIMITER_ARGS_WIN, string delimiterValue = DELIMITER_VALUE,
                     List<string> validParams  = null)
      {
         // Safe guard
         if (delimiterArgs.Length < 1 || delimiterValue.Length< 1) { throw new ArgumentOutOfRangeException("Empty argument or key/value delimiter are not allowed."); }

         DelimiterArgs = delimiterArgs;
         DelimiterValue = delimiterValue;
         KeyValues = new List<KeyValue>();
         if (validParams != null)
         {
            ValidParameters = validParams;
         }
         else
         {
            ValidParameters = new List<string>(); ;
         }
      }

      public CmdArgs(eArgumentDelimiterStyle delimiterArgsType = eArgumentDelimiterStyle.Windows,
                     string delimiterValue = DELIMITER_VALUE,
                     List<string> validParams = null)
      {

         // Safe guard
         if (delimiterValue.Length < 1) { throw new ArgumentOutOfRangeException("Empty argument or key/value delimiter are not allowed."); }

         if (delimiterArgsType == eArgumentDelimiterStyle.Windows)
         {
            DelimiterArgs = DELIMITER_ARGS_WIN;
         }
         else
         {
            DelimiterArgs = DELIMITER_ARGS_POSIX;
         }
         DelimiterValue = delimiterValue;
         KeyValues = new List<KeyValue>();
         if (validParams != null)
         {
            ValidParameters = validParams;
         }
         else
         {
            ValidParameters = new List<string>();
         }
      }

      public CmdArgs(List<KeyValue> keyValueList, string delimiterArgs = DELIMITER_ARGS_WIN,
                     string delimiterValue  = DELIMITER_VALUE)
      {

         // Safe guard
         if (delimiterArgs.Length < 1 || delimiterValue.Length < 1) { throw new ArgumentOutOfRangeException("Empty argument or key/value delimiter are not allowed."); }

         DelimiterArgs = delimiterArgs;
         DelimiterValue = delimiterValue;
         KeyValues = keyValueList;
         // Create the list of valid parameter names from the passed collection
         ValidParameters = new List<string>();
         foreach (KeyValue o in KeyValues)
         {
            ValidParameters.Add(o.Key);
         }
      }


      public CmdArgs(List<KeyValue> keyValueList, eArgumentDelimiterStyle delimiterArgsType  = eArgumentDelimiterStyle.Windows,
                  string delimiterValue = DELIMITER_VALUE)
      {

         // Safe guard
         if (delimiterValue.Length < 1) { throw new ArgumentOutOfRangeException("Empty argument or key/value delimiter are not allowed."); }

         if (delimiterArgsType == eArgumentDelimiterStyle.Windows)
         {
            DelimiterArgs = DELIMITER_ARGS_WIN;
         }
         else
         {
            DelimiterArgs = DELIMITER_ARGS_POSIX;
         }
         DelimiterValue = delimiterValue;
         KeyValues = keyValueList;
         // Create the list of valid parameter names from the passed collection
         ValidParameters = new List<string>();
         foreach (KeyValue o in KeyValues)
         {
            ValidParameters.Add(o.Key);
         }
      }


      #endregion

   }  // CmdArgs

   public class KeyValue
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
         set { msOriginalParameter = value; }
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
