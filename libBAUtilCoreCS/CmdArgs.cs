using System;
using System.Collections.Generic;
using System.Text;

using static libBAUtilCoreCS.StringHelper;

namespace libBAUtilCoreCS.Utils.Args
{

   /// <summary>
   /// Command line parameter handling
   /// See https://docs.microsoft.com/en-us/archive/msdn-magazine/2019/march/net-parse-the-command-line-with-system-commandline
   /// See https://github.com/commandlineparser/commandline
   /// </summary>
   public class CmdArgs
   {

      #region Declarations

      /// <summary>
      /// OS-dependent default parameter delimiter.
      /// </summary>
      public enum eArgumentDelimiterStyle
      {
         /// <summary>
         /// Windows style parameter delimiter '/'
         /// </summary>
         Windows,
         /// <summary>
         /// *nix style parameter delimiter '--'
         /// </summary>
         POSIX
      };

      // Default arguments and key/value delimiter
      /// <summary>
      /// Standard Windows parameter delimiter.
      /// </summary>
      private const string DELIMITER_ARGS_WIN = "/";
      /// <summary>
      /// Standard POSIX ("Linux") parameter delimiter.
      /// </summary>
      private const string DELIMITER_ARGS_POSIX = "--";
      /// <summary>
      /// Default key=value delimiter.
      /// </summary>
      private const string DELIMITER_VALUE = "=";

      private bool mbolCaseSensitive;  // Treat parameter names as case-sensitive?

      private string msDelimiterArgs = String.Empty;        // Arguments delimiter, typically "/"
      private string msDelimiterValue = String.Empty;       // Key/value delimiter, typically "="
      private string msOriginalParameters = String.Empty;   // Parameters as passed to the application
      private List<string> listValidParameters = null;      // Parameters as passed to the application

      private List<KeyValue> mcolKeyValues;

      #endregion

      #region Properties - Public

      /// <summary>
      /// Return the number of parameters (key/value)
      /// </summary>
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

      /// <summary>
      /// Treat parameter names as case-sensitive?
      /// </summary>
      public bool CaseSensitive
      {
         get { return mbolCaseSensitive; }
         set { mbolCaseSensitive = value; }
      }

      /// <summary>
      /// Parameter delimiter, e.g. '--' in '--param=value'
      /// </summary>
      public string DelimiterArgs
      {
         get { return msDelimiterArgs; }
         set { msDelimiterArgs = value; }
      }

      /// <summary>
      /// Name/value delimiter, e.g. '=' in  '--param=value'
      /// </summary>
      public string DelimiterValue
      {
         get { return msDelimiterValue; }
         set { msDelimiterValue = value; }
      }

      /// <summary>
      /// List of valid command line parameters.
      /// </summary>
      public List<string> ValidParameters
      {
         get { return listValidParameters; }
         set { listValidParameters = value; }
      }

      /// <summary>
      /// Unmodified parameter string as passed to the application
      /// </summary>
      public string OriginalParameters
      {
         get { return msOriginalParameters; }
         set { msOriginalParameters = value; }
      }

      /// <summary>
      /// The parsed command line parameters
      /// </summary>
      public List<KeyValue> KeyValues
      {
         get { return mcolKeyValues; }
         set { mcolKeyValues = value; }
      }

      #endregion

      #region Methods - Private

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

      /// <summary>
      /// Is a parameter present more than once?
      /// </summary>
      private bool HasDuplicate(KeyValue currentKey, Int32 currentIndex)
      {

         // Safe guard
         if (KeyValues.Count < 1) { return false; }

         // Compare the passed parameter to the list and see if there's a duplicate entry
         for (Int32 i = 0; i <= KeyValues.Count - 1; i++)
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

      private bool ParseCmd(string[] asArgs, Int32 startIndex = 0)
      {

         string sKey = String.Empty;
         string sValue = String.Empty;
         bool bolResult = true;

         for (Int32 i = startIndex; i <= asArgs.Length - 1; i++)
         {
            bolResult = bolResult && ParseParam(asArgs[i]);
         }

         return bolResult;

      }

      private bool ParseCmd(string sArgs)
      {
         string[] asArgs = sArgs.Split(Convert.ToChar(DelimiterArgs));
         return ParseCmd(asArgs);
      }

      /// <summary>
      /// Parses a single key/pair combo into a matching <see cref="KeyValue"/> object
      /// </summary>
      /// <param name="sParam"></param>
      /// <returns></returns>
      private bool ParseParam(string sParam)
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
            // Value as string
            o.ValueText = Mid(sParam, sParam.IndexOf(DelimiterValue) + 1);

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

      #endregion

      #region Methods - Public

      /// <summary>
      /// Initializes the object by parsing System.Environment.GetCommandLineArgs()
      /// </summary>
      /// <param name="cmdLineArgs">Command line parameter string, e.g. /abc=123 /def</param>
      /// <returns><see langword="true"/>/<see langword="false"/></returns>
      public bool Initialize(string cmdLineArgs = "")
      {
         // Clear everything, as we're parsing anew.
         KeyValues = new List<KeyValue>();

         if (cmdLineArgs.Length < 1)
         {
            string[] asArgs = System.Environment.GetCommandLineArgs();
            if (asArgs.Length < 2)
            {
               // No command line arguments passed
               return false;
            }
            // When using System.Environment.GetCommandLineArgs(), the 1st array element is the executable's name
            OriginalParameters = asArgs[1];
            return ParseCmd(asArgs, 1);
         }
         else
         {
            OriginalParameters = cmdLineArgs;
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
      /// Determine if any of these parameters are present
      /// </summary>
      /// <param name="paramList">List of parameter names (<see cref="KeyValue.Key"/>)</param>
      /// <returns>
      /// <see langword="true"/> if at least one of the parameters passed is present, otherwise <see langword="false"/>
      /// </returns>
      public bool HasAnyParameter(List<String> paramList)
      {
         foreach (String s in paramList)
         {
            if (HasParameter(s))
            {
               return true;
            }
         }

         // Reaching here, no parameter has been found
         return false;

      }

      /// <summary>
      /// Determine if a certain parameter is present
      /// </summary>
      /// <param name="key">Parameter's name(<see cref = "KeyValue.Key" />)</param>
      /// <returns><see langword="true"/>/<see langword="false"/></returns>
      public bool HasParameter(string key)
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
      /// Determine if a certain parameter is present referring to it either
      /// by its long or short form.
      /// </summary>
      /// <param name="keyShort">Parameter's name(<see cref = "KeyValueBase.KeyShort" />)</param>
      /// <param name="keyLong">Parameter's name(<see cref = "KeyValueBase.KeyLong" />)</param>
      /// <returns><see langword="true"/>/<see langword="false"/></returns>
      public bool HasParameter(string keyShort, string keyLong)
      {
         foreach (KeyValue o in KeyValues)
         {

            if (CaseSensitive == true)
            {
               if (o.Key == keyShort || o.Key == keyLong) { return true; }
            }
            else
            {
               if (o.Key.ToLower() == keyShort.ToLower() || o.Key.ToLower() == keyLong.ToLower()) { return true; }
            }
         }

         return false;
      }  // HasParameter

      /// <summary>
      /// Determine if a certain parameter is present
      /// </summary>
      /// <param name="paramList>">List of parameter names (<see cref="KeyValue.Key"/>)</param>
      /// <returns>
      /// <see langword="true"/> if all parameters passed are present, otherwise <see langword="false"/>
      /// </returns>
      public bool HasParameter(List<string> paramList)
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
      /// <param name="key">The parameter's name(<see cref = "KeyValue.Key" /></param >
      /// <param name="caseSensitive">Treat the name as case-sensitive?</param>
      /// <returns><see cref="KeyValue"/> whose <see cref="KeyValue.Key"/> equals <paramref name="key"/>.</returns>
      public KeyValue GetParameterByName(string key, bool caseSensitive = false)
      {

         // Safe guard
         if (HasParameter(key) == false) { throw new ArgumentException("Parameter doesn't exist: " + key, key); }

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
      /// Return the corresponding KeyValue object
      /// </summary>
      /// <param name="keyShort">Parameter's name(<see cref = "KeyValueBase.KeyShort" />)</param>
      /// <param name="keyLong">Parameter's name(<see cref = "KeyValueBase.KeyLong" />)</param>
      /// <param name="caseSensitive">Treat the name as case-sensitive?</param>
      /// <returns><see cref="KeyValue"/> whose <see cref="KeyValue.Key"/> equals <paramref name="keyShort"/> or <paramref name="keyLong"/>.</returns>
      public KeyValue GetParameterByName(string keyShort, string keyLong, bool caseSensitive = false)
      {

         // Safe guard
         if (HasParameter(keyShort, keyLong) == false) { throw new ArgumentException("Parameter doesn't exist: " + keyShort + ", " + keyLong); }

         foreach (KeyValue o in KeyValues)
         {
            if (caseSensitive == false)
            {
               if (o.Key.ToLower() == keyShort.ToLower() || o.Key.ToLower() == keyLong.ToLower()) { return o; }
            }
            else
            {
               if (o.Key == keyShort || o.Key == keyLong) { return o; }
            }
         }

         // We should never reach this point
         return null;

      }  // GetParameterByName

      /// <summary>
      /// Return the value of a parameter
      /// </summary>
      /// <param name="key">The parameter's name(<see cref = "KeyValue.Key" /></param >
      /// <param name="caseSensitive">Treat the name as case-sensitive?</param>
      /// <returns>Value of parameter or <see langword="null"/> if parameter doesn't exist</returns>
      public Object GetValueByName(string key, bool caseSensitive = false)
      {

         // Safe guard
         if (HasParameter(key) == false) { throw new ArgumentException("Parameter doesn't exist: " + key); }

         foreach (KeyValue o in KeyValues)
         {
            if (caseSensitive == false)
            {
               if (o.Key.ToLower() == key.ToLower()) { return o.Value; }
            }
            else
            {
               if (o.Key == key) { return o.Value; }
            }
         }
         // We should never reach this point
         return null;
      }  // GetValueByName

      /// <summary>
      /// Return the value of a parameter
      /// </summary>
      /// <param name="keyShort">Parameter's name(<see cref = "KeyValueBase.KeyShort" />)</param>
      /// <param name="keyLong">Parameter's name(<see cref = "KeyValueBase.KeyLong" />)</param>
      /// <param name="caseSensitive">Treat the name as case-sensitive?</param>
      /// <returns>Value of parameter or <see langword="null"/> if parameter doesn't exist</returns>
      public Object GetValueByName(string keyShort, string keyLong, bool caseSensitive = false)
      {

         // Safe guard
         if (HasParameter(keyShort, keyLong) == false) { throw new ArgumentException("Parameter doesn't exist: " + keyShort + ", " + keyLong); }

         foreach (KeyValue o in KeyValues)
         {
            if (caseSensitive == false)
            {
               if (o.Key.ToLower() == keyShort || o.Key.ToLower() == keyLong) { return o.Value; }
            }
            else
            {
               if (o.Key == keyShort || o.Key == keyLong) { return o.Value; }
            }
         }
         // We should never reach this point
         return null;
      }  // GetValueByName

      #endregion

      #region Constructor/Dispose

      /// <summary>
      /// Initializes a new instance of the command line parser object.
      /// </summary>
      public CmdArgs()
      {
         DelimiterArgs = GetDefaultDelimiterForOS();
         DelimiterValue = DELIMITER_VALUE;
      }

      /// <summary>
      /// Initializes a new instance of the command line parser object.
      /// </summary>
      /// <param name="delimiterArgs">Argument delimiter. Defaults to Windows-style '/'.</param>
      /// <param name="delimiterValue">Key/value delimiter. Defaults to '='.</param>
      public CmdArgs(string delimiterArgs = DELIMITER_ARGS_WIN, string delimiterValue = DELIMITER_VALUE)
      {
         // Safe guard
         if (delimiterArgs.Length < 1 || delimiterValue.Length < 1) { throw new ArgumentOutOfRangeException("Empty argument or key/value delimiter are not allowed."); }

         DelimiterArgs = delimiterArgs;
         DelimiterValue = delimiterValue;
         KeyValues = new List<KeyValue>();
      }

      /// <summary>
      /// Initializes a new instance of the command line parser object.
      /// </summary>
      /// <param name="delimiterArgsType">Argument delimiter. Defaults to Windows-style '/'.</param>
      /// <param name="delimiterValue">Key/value delimiter. Defaults to '='.</param>
      public CmdArgs(eArgumentDelimiterStyle delimiterArgsType = eArgumentDelimiterStyle.Windows,
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
         KeyValues = new List<KeyValue>();
      }

      /// <summary>
      /// Initializes a new instance of the command line parser object.
      /// </summary>
      /// <param name="keyValueList">Command line argument list of <see cref="KeyValue"/></param>
      /// <param name="delimiterArgs">Argument delimiter. Defaults to Windows-style '/'.</param>
      /// <param name="delimiterValue">Key/value delimiter. Defaults to '='.</param>
      public CmdArgs(List<KeyValue> keyValueList, string delimiterArgs = DELIMITER_ARGS_WIN,
                       string delimiterValue = DELIMITER_VALUE)
      {

         // Safe guard
         if (delimiterArgs.Length < 1 || delimiterValue.Length < 1) { throw new ArgumentOutOfRangeException("Empty argument or key/value delimiter are not allowed."); }

         DelimiterArgs = delimiterArgs;
         DelimiterValue = delimiterValue;
         KeyValues = keyValueList;
      }

      /// <summary>
      /// Initializes a new instance of the command line parser object.
      /// </summary>
      /// <param name="keyValueList">Command line argument list of <see cref="KeyValue"/></param>
      /// <param name="delimiterArgsType">Argument delimiter. Defaults to Windows-style '/'.</param>
      /// <param name="delimiterValue">Key/value delimiter. Defaults to '='.</param>
      public CmdArgs(List<KeyValue> keyValueList, eArgumentDelimiterStyle delimiterArgsType = eArgumentDelimiterStyle.Windows,
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
      }

      /// <summary>
      /// Initializes a new instance of the command line parser object.
      /// </summary>
      /// <param name="validParams">A list of valid command line parameters</param>
      public CmdArgs(List<string> validParams = null) : base()
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

      #endregion

   }  // class CmdArgs


   /// <summary>
   /// A single command line parameter + its value, i.e. /parameter=value
   /// </summary>
   public class KeyValue : KeyValueBase
   {

      private String msHelpText = String.Empty;
      private String msOriginalParameter = String.Empty;
      private String msValueText = String.Empty;

      private Object moValue;

      #region Properties - Public

      /// <summary>
      /// Help text for this parameter
      /// </summary>
      public string HelpText
      {
         get { return msHelpText; }
         set { msHelpText = value; }
      }

      /// <summary>
      /// Parameter name, e.g. 'file' or 'f' from /file=MyFile.txt  or /f=MyFile.txt
      /// </summary>
      public string Key
      {
         get
         { // KeyLong takes precedence
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
      public string OriginalParameter
      {
         get { return msOriginalParameter; }
         set { msOriginalParameter = value; }
      }

      /// <summary>
      /// Parameter value as string, e.g. 'MyFile.txt' from /file=MyFile.txt
      /// </summary>
      public String ValueText
      {
         get { return msValueText; }
         set { msValueText = value; }
      }
      
      /// <summary>
      /// Parameter value, e.g. 'MyFile.txt' from /file=MyFile.txt
      /// </summary>
      public Object Value
      {
         get { return moValue; }
         set { moValue = value; }
      }

      #endregion

      #region Methods - Public

      /// <summary>
      /// Overwrites .ToString()
      /// </summary>
      public override string ToString()
      {
         string sText = this.Key;
         if (this.HelpText.Length < 1)
         {
            return sText;
         }
         else
         {
            return sText + ": " + this.HelpText;
         }
      }

      #endregion

      /// <summary>
      /// Initializes a new instance of the command line parameter key/value pair object.
      /// </summary>
      /// <param name="originalParam">Key/value pair as originally passed via command line, e.g. /file=MyFile.txt</param>
      /// <param name="keyShort">Short notation of parameter name, i.e. '/f'</param>
      /// <param name="keyLong">Long notation of parameter name, i.e. /file</param>
      /// <param name="value">The actual value passed.</param>
      /// <param name="helpText">Short textual description of this parameter.</param>
      public KeyValue(string originalParam, string keyShort = "",
                        string keyLong = "", Object value = null,
                        string helpText = "")
      {

         HelpText = helpText;
         KeyLong = keyLong;
         KeyShort = keyShort;
         OriginalParameter = originalParam;
         Value = value;
         if (Value == null)
            ValueText = String.Empty;
         else
         {
            try { ValueText = value.ToString(); }
            catch { ValueText = String.Empty; }
         }
      }

   } // KeyValue

   /// <summary>
   /// Base/parent KeyValue class
   /// </summary>
   public class KeyValueBase
   {
      private string msKeyLong = String.Empty;
      private string msKeyShort = String.Empty;

      /// <summary>
      /// This parameter is mandatory
      /// </summary>
      public bool IsMandatory { get; set; }

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
      /// Short and long parameter name
      /// </summary>
      public KeyValueBase(string shortKey = "", string longKey = "")
      {
         KeyLong = longKey;
         KeyShort = shortKey;
      }

      /// <summary>
      /// Short and long parameter name
      /// </summary>
      public KeyValueBase(KeyValueBase o)
      {
         KeyLong = o.KeyLong;
         KeyShort = o.KeyShort;
      }

   } // class KeyValueBase

}
