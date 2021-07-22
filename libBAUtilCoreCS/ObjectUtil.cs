using System;
// using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
// using System.Text;
// using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace libBAUtilCoreCS
{
   /// <summary>
   /// General object helpers
   /// </summary>
   class ObjectUtil
   {

      #region "Serialization"

      /// <summary>
      /// Returns the enumeration member's name for the specific enumeration value.
      /// </summary>
      /// <param name="enumType">.NET type of enum as retrieved by <see cref="Type.GetType"/>.</param>
      /// <param name="enumMemberValue">Return the member name for this value</param>
      /// <returns>
      /// The enumeration's member name matching <paramref name = "enumMemberValue" />
      /// </returns>
      /// <remarks>
      /// <paramref name="enumType"/> MUST be an Enumeration's* type*, e.g.GetType(MyEnum)
      /// </remarks>
      public static string GetEnumNameFromValue(Type enumType, Int32 enumMemberValue)
      {
         /*------------------------------------------------------------------------------
         'Prereq.  : 
         '
         '   Author: Knuth Konrad
         '     Date: 30.08.2018
         '   Source: Modified from https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/statements/enum-statement
         '  Changed: 2019-01-12
         '           - Enum member names naturally starting with a number should be prefixed 
         '           with a "_", e.g. _12_Passenger_Van. The underscore is then removed
         '------------------------------------------------------------------------------*/
         var names = Enum.GetNames(enumType);
         var values = Enum.GetValues(enumType);
         string sTemp = String.Empty;

         for (Int32 i = 0; i <= names.Length - 1; i++)
         {
            if ((Int32)values.GetValue(i) == enumMemberValue)
            {
               sTemp = names[i];
               if (sTemp.StartsWith("_") == true)
               {
                  sTemp = sTemp.Substring(1);
               }
               return sTemp;
            }
         }
         return String.Empty;
      }  // GetEnumNameFromValue

      /// <summary>
      /// Returns the enumeration member's name for the specific enumeration value.
      /// </summary>
      /// <param name="enumType">.NET type of enum as retrieved by <see cref="Type.GetType"/>.</param>
      /// <param name="enumMemberValue">Return the member name for this value.</param>
      /// <param name="alternativeNames">
      /// Array with alternative names to return.
      /// Empty array elements of alternativeNames() will cause the Enum's member name to be returned. E.g.
      /// Enum MyEnum
      ///    One
      ///    Two
      ///    Three
      /// End Enum
      /// alternativeNames() = "", "", "more than two"
      /// will return "One" for enumMemberValue = MyEnum.One, "Two" for MyEnum.Two, but "more than two" for MyEnum.Three
      /// </param>
      /// <returns>
      /// The enumeration's member name matching <paramref name = "enumMemberValue" />.
      /// </returns>
      /// <remarks>
      /// <paramref name="enumType"/> MUST be an Enumeration's* type*, e.g.GetType(MyEnum)
      /// </remarks>
      public static string GetEnumNameFromValue(Type enumType, Int32 enumMemberValue, params string[] alternativeNames)
      {
         /*'------------------------------------------------------------------------------
         'Prereq.  : enumType MUST be an Enumeration's* type*, e.g.GetType(MyEnum)
         '
         '   Author: Knuth Konrad
         '     Date: 24.04.2019
         '   Source: Modified from https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/statements/enum-statement
         '  Changed: -
         '------------------------------------------------------------------------------*/
         var names = Enum.GetNames(enumType);
         var values = Enum.GetValues(enumType);
         string sTemp = String.Empty;

         // Safe guard
         if (alternativeNames.Length != names.Length)
         {
            throw new ArgumentOutOfRangeException("alternativeNames[]. The number of array elements must match the number of Enumeration members.");
         }

         for (Int32 i = 0; i <= names.Length - 1; i++)
         {
            if ((Int32)values.GetValue(i) == enumMemberValue)
            {
               if (String.IsNullOrEmpty(alternativeNames[i]) == false)
               {
                  sTemp = alternativeNames[i];
               }
               else
               {
                  sTemp = names[i];
               }
               if (sTemp.StartsWith("_") == true)
               {
                  sTemp = sTemp.Substring(1);
               }
               return sTemp;
            }
         }
         return String.Empty;
      }  // GetEnumNameFromValue

      /// <summary>
      /// Determine if an object is serializable.
      /// </summary>
      /// <param name="obj">Check this object</param>
      public static Boolean IsSerializable(Object obj)
      {
         /*'------------------------------------------------------------------------------
         'Prereq.  : -
         '
         '   Author: Knuth Konrad
         '     Date: 28.05.2019
         '   Source: https://docs.microsoft.com/en-us/dotnet/standard/serialization/how-to-determine-if-netstandard-object-is-serializable
         '  Changed: -
         '------------------------------------------------------------------------------*/

         // Safe guard
         if (obj == null)
         {
            return false;
         }
         else
         {
            Type t = obj.GetType();
            return t.IsSerializable;
         }

      }  // IsSerializable()

      /// <summary>
      /// Serialize an object to (a) XML (string).
      /// </summary>
      /// <param name="obj">Serialize this object</param>
      /// <param name="omitXmlDeclaration">True = Serialize without XML declaration (&lt;?xml version="1.0"?&gt;)</param>
      /// <param name="omitXmlNamespace">
      /// True = Serialize without XML namespace declaration
      /// (xmlnsxsi = "http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd = "http://www.w3.org/2001/XMLSchema")
      /// </param>
      /// <returns></returns>
      public static string Serialize(object obj, Boolean omitXmlDeclaration = false, Boolean omitXmlNamespace = false)
      {
         /*
          * ------------------------------------------------------------------------------
          *Prereq.  : -
          *
          *   Author: Knuth Konrad
          *     Date: 28.05.2019
          *   Source: https://www.it-visions.de/lserver/CodeSampleDetails.aspx?c=2831
          *           https://stackoverflow.com/questions/258960/how-to-serialize-an-object-to-xml-without-getting-xmlns
          *  Changed: -
          *------------------------------------------------------------------------------
          */
         XmlSerializer serializer = new XmlSerializer(obj.GetType());
         XmlWriterSettings settings = new XmlWriterSettings();
         XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
         string s = String.Empty;

         if (omitXmlNamespace == true)
         {
            // Create an empty namespace
            ns.Add("", "");
         }

         omitXmlDeclaration = settings.OmitXmlDeclaration = true;

         MemoryStream ms = new MemoryStream();
         XmlWriter sw = XmlWriter.Create(ms, settings);
         StreamReader sr = new StreamReader(ms);

         serializer.Serialize(sw, obj, ns);
         ms.Position = 0;

         return sr.ReadToEnd();

      }  // Serialize()

      /// <summary>
      /// Deserialize a XML (string) to an object.
      /// </summary>
      /// <param name="xmlString">XML as String compatible with .NET's Deserialize method.</param>
      /// <param name="objType">Deserialize to this object type</param>
      public static Object Deserialize(string xmlString, Type objType)
      {
         /*
         '------------------------------------------------------------------------------
         'Prereq.  : -
         'Note     : -
         '
         '   Author: Knuth Konrad
         '     Date: 29.05.2019
         '   Source: https://stackoverflow.com/questions/27235951/deserialize-xml-string-to-object-vb-net
         '  Changed: -
         '------------------------------------------------------------------------------
         */
         XmlSerializer xser = new XmlSerializer(objType);
         TextReader sr = new StringReader(xmlString);

         return xser.Deserialize(sr);
      }

      /// <summary>
      /// Deserialize a XML (string) to a specific class.
      /// </summary>
      /// <typeparam name="T">Return an object of this class.</typeparam>
      /// <param name="xmlString">XML as String compatible with .NET's Deserialize method.</param>
      /// <returns></returns>
      public static T DeserializeAsClass<T>(string xmlString) where T: class
         {
            /*
            '------------------------------------------------------------------------------
            'Prereq.  : -
            'Note     : -
            '
            '   Author: Knuth Konrad
            '     Date: 29.05.2019
            '   Source: https://www.codeguru.com/csharp/.net/net_data/serializing-and-deserializing-xml-in-.net.html
            '  Changed: -
            '------------------------------------------------------------------------------
            */

            /*
            XmlSerializer xser = new XmlSerializer(GetType(T));
            StringReader sr = new StringReader(xmlString);

            return (T)xser.Deserialize(sr);
            */

            XmlSerializer ser = new XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(xmlString))
            {
               return (T)ser.Deserialize(sr);
            }

      }  // DeserializeAsClass()
      #endregion

      /// <summary>
      /// Create a deep copy of an object
      /// </summary>
      /// <param name="obj">Object to be cloned</param>
      /// <returns>
      /// Deep copy aka "a copy of all data and objects and their data" of <paramref name="obj"/>.
      /// </returns>
      /// <remarks>Source: https://www.rectanglered.com/deep-copying-object-vb-net </remarks>
      public static Object Clone(Object obj)
      {
         MemoryStream m = new MemoryStream();
         BinaryFormatter f = new BinaryFormatter();

         f.Serialize(m, obj);
         m.Seek(0, SeekOrigin.Begin);

         return f.Deserialize(m);
      }

      /// <summary>
      /// Simple wrapper to <see cref="Clone(Object)"/>
      /// </summary>
      /// <param name="o"></param>
      /// <returns>
      /// Deep copy aka "a copy of all data and objects and their data" of <paramref name="o"/>.
      /// </returns>
      public static Object DeepClone(Object o)
      {
         return Clone(o);
      }

   }  // class ObjectUtil
}
