using System;
using System.Collections.Generic;
using System.Text;

namespace libBAUtilCoreCS.Utils.Args
{
   class CmdArgs
   {
   }

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

   public void CmdArgs(string originalParam, string keyShort = "",
                     string keyLong = "", Object value = null,
                     string helpText = "")
      {
         // ToDo: base bzw.MyBase needed in C# constructor

         /*
         MyBase.New

            With Me
               .HelpText = helpText
               .KeyLong = keyLong
               .KeyShort = keyShort
               .OriginalParameter = originalParam
               .Value = value
            End With

         End Sub
         */
      }



   }
}
