using System;
using System.Security.Cryptography;

namespace libBAUtilCoreCS
{
   /// <summary>
   /// En-/Decryption helper (3DES).
   /// </summary>
   /// <remarks>
   /// Source: https://msdn.microsoft.com/en-us/library/ms172831(v=vs.110).aspx
   /// </remarks>
   public sealed class baCrypto3DES
   {
      #region "Declarations"

      private TripleDESCryptoServiceProvider TripleDes = new TripleDESCryptoServiceProvider();

      #endregion

      #region "Methods - Public"
      
      /// <summary>
      /// Decode a string.
      /// </summary>
      /// <param name="encryptedText">Encoded string</param>
      /// <returns>Decoded <paramref name="encryptedText"/></returns>
      public string DecryptData(string encryptedText)
      {

         // Convert the encrypted text string to a byte array. 
         byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

         // Create the stream. 
         System.IO.MemoryStream ms = new System.IO.MemoryStream();
         // Create the decoder to write to the stream. 
         CryptoStream decStream = new CryptoStream(ms, TripleDes.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

         // Use the crypto stream to write the byte array to the stream.
         decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
         decStream.FlushFinalBlock();

         // Convert the plaintext stream to a string. 
         return System.Text.Encoding.Unicode.GetString(ms.ToArray());

      }  // DecryptData

      /// <summary>
      /// Encode a string.
      /// </summary>
      /// <param name="plainText">Plain string</param>
      /// <returns>Encoded <paramref name="plainText"/></returns>
      public string EncryptData(string plainText)
      {

         if (String.IsNullOrEmpty(plainText) == true)
         {
            plainText = String.Empty;
         }

         // Convert the plaintext string to a byte array. 
         byte[] plaintextBytes = System.Text.Encoding.Unicode.GetBytes(plainText);

         // Create the stream. 
         System.IO.MemoryStream ms = new System.IO.MemoryStream();
         // Create the encoder to write to the stream. 
         CryptoStream encStream = new CryptoStream(ms, TripleDes.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
         // Use the crypto stream to write the byte array to the stream.
         encStream.Write(plaintextBytes, 0, plaintextBytes.Length);
         encStream.FlushFinalBlock();

         // Convert the encrypted stream to a printable string. 
         return Convert.ToBase64String(ms.ToArray());

      }  // EncryptData

      /// Initializes a new instance of the CryptoUtil class
      /// </summary>
      /// <param name="key">De-/Encryption key ("password")</param>
      public baCrypto3DES(string key)
      {
         // Initialize the crypto provider.
         // VB: TripleDes.Key = TruncateHash(key, TripleDes.KeySize \ 8)
         // VB's \ (integer division) = C#'s ?
         // When both number are integers a \ b = a/ b
         TripleDes.Key = TruncateHash(key, TripleDes.KeySize / 8);
         TripleDes.IV = TruncateHash(String.Empty, TripleDes.BlockSize / 8);
      }


      #endregion

      private byte[] TruncateHash(string key, Int32 length)
      {
         SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

         // Hash the key. 
         byte[] keyBytes = System.Text.Encoding.Unicode.GetBytes(key);
         byte[] hash = sha1.ComputeHash(keyBytes);

         // Truncate or pad the hash. 
         Array.Resize(ref hash, length - 1);
         return hash;
      } // TruncateHash

   }  // baCrypto3DES

   /// <summary>
   /// En-/Decrypting helper (AES).
   /// </summary>
   /// <remarks>
   /// Source: https://msdn.microsoft.com/en-us/library/ms172831(v=vs.110).aspx
   /// </remarks>
   public sealed class baCryptoAES
   {

      #region "Declares"
         private RijndaelManaged AES = new RijndaelManaged();
      #endregion

      #region "Methods - Public"

      /// <summary>
      /// Decode a string.
      /// </summary>
      /// <param name="encryptedText">Encoded string</param>
      /// <returns>Decoded <paramref name="encryptedText"/></returns>
      public string DecryptData(string encryptedText)
         {

            // Convert the encrypted text string to a byte array. 
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

            // Create the stream. 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            // Create the decoder to write to the stream. 
            CryptoStream decStream = new CryptoStream(ms, AES.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

            // Use the crypto stream to write the byte array to the stream.
            decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
            decStream.FlushFinalBlock();

            // Convert the plain text stream to a string. 
            return System.Text.Encoding.Unicode.GetString(ms.ToArray());

         }

      /// <summary>
      /// Encode a string.
      /// </summary>
      /// <param name="plainText">Plain string</param>
      /// <returns>Encoded <paramref name="plainText"/></returns>
      public string EncryptData(string plainText)
      {

         if (String.IsNullOrEmpty(plainText) == true)
         {
            plainText = String.Empty;
         }

         // Convert the plaintext string to a byte array. 
         byte[] plaintextBytes = System.Text.Encoding.Unicode.GetBytes(plainText);

         // Create the stream.
         System.IO.MemoryStream ms = new System.IO.MemoryStream();
         // Create the encoder to write to the stream. 
         CryptoStream encStream = new CryptoStream(ms, AES.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

         // Use the crypto stream to write the byte array to the stream.
         encStream.Write(plaintextBytes, 0, plaintextBytes.Length);
         encStream.FlushFinalBlock();

         // Convert the encrypted stream to a printable string.
         return Convert.ToBase64String(ms.ToArray());

      }

      #endregion

      /// <summary>
      /// Initializes a new instance of the CryptoUtil class
      /// </summary>
      /// <param name="key">De-/Encryption key ("password")</param>
      public baCryptoAES(string key)
      {
         // Initialize the crypto provider.
         AES.Key = TruncateHash(key, AES.KeySize / 8);
         AES.IV = TruncateHash("", AES.BlockSize / 8);
      }

      private byte[] TruncateHash(string key, Int32 length)
      {

         SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

         // Hash the key. 
         byte[] keyBytes = System.Text.Encoding.Unicode.GetBytes(key);
         byte[] hash = sha1.ComputeHash(keyBytes);

         // Truncate or pad the hash. 
         Array.Resize(ref hash, length - 1);
         return hash;

      }

   }  // baCryptoAES

}
