using System;
using System.IO;

using static System.IO.File;
using static System.IO.Path;

namespace libBAUtilCoreCS
{

   /// <summary>
   /// General file system helper methods
   /// </summary>
   public class FilesystemHelper
   {
      /// <summary>
      /// Standard Windows path delimiter.
      /// </summary>
      private const string DELIMITER_PATH_WIN = "\\";
      /// <summary>
      /// Standard POSIX ("Linux") path delimiter.
      /// </summary>
      private const string DELIMITER_PATH_POSIX = "/";

      /// <summary>
      /// Ensure a path does NOT end with a path delimiter
      /// </summary>
      /// <param name="sPath">
      /// Path (drive or network share), C:\Windows\ or \\myserver\myshare\
      /// </param>
      /// <param name="sDelim">
      /// Character to be treated as the folder delimiter.
      /// </param>
      /// <param name="bolCheckTail">
      /// Check the start or end (default) of <paramref name="sPath"/>.
      /// </param>
      /// <returns>
      /// <paramref name="sPath"/> with <paramref name="sDelim"/> stripped off, if present.
      /// </returns>
      public static string DenormalizePath(string sPath, string sDelim = "\\", bool bolCheckTail = true)
      {
         if (bolCheckTail == true)
         {
            if (StringHelper.Right(sPath, sDelim.Length) != sDelim)
            {
               return sPath;
            }
            else
            {
               return sPath.Substring(1, sPath.Length - sDelim.Length);
            }
         }
         else
         {
            if (StringHelper.Left(sPath, sDelim.Length) != sDelim)
            {
               return sPath;
            }
            else
            {
               return sPath.Substring(sDelim.Length + 1);
            }
         }
      }  // DenormalizePath

      /// <summary>
      /// Ensure a path does end with a path delimiter
      /// </summary>
      /// <param name="sPath">
      /// Path (drive or network share), C:\Windows\ or \\myserver\myshare\
      /// </param>
      /// <param name="sDelim">
      /// Character to be treated as the folder delimiter.
      /// </param>
      /// <param name="bolCheckTail">
      /// Check the start or end (default) of <paramref name="sPath"/>.
      /// </param>
      /// <returns>
      /// <paramref name="sPath"/> with <paramref name="sDelim"/> add, if not present.
      /// </returns>
      public static string NormalizePath(string sPath, string sDelim = "\\", bool bolCheckTail = true)
      {
         if (bolCheckTail == true)
         {
            if (StringHelper.Right(sPath, sDelim.Length) != sDelim)
            {
               return sPath + sDelim;
            }
            else
            {
               return sPath;
            }
         }
         else
         {
            if (StringHelper.Left(sPath, sDelim.Length) != sDelim)
               return sDelim + sPath;
            else
            {
               return sPath;
            }
         }
      }  // NormalizePath

      /// <summary>
      /// Alias for <see cref="System.IO.File.Exists(String)"/>
      /// </summary>
      /// <param name="file">The file to check.</param>
      /// <returns><see langword="true"/> if the caller has the required permissions and path contains the name of an existing file; otherwise, <see langword="false"/>. 
      /// This method also returns <see langword="false"/> if path is <see langword="null"/>, an invalid path, or a zero-length string. 
      /// If the caller does not have sufficient permissions to read the specified file, no exception is thrown and the method returns 
      /// <see langword="false"/> regardless of the existence of path.
      /// </returns>
      public static bool FileExists(string file)
      {
         return Exists(file);
      } // FileExists


      /// <summary>
      /// Alias for <see cref="System.IO.Directory.Exists(String)"/>
      /// </summary>
      /// <param name="folder">The file to check.</param>
      /// <returns><see langword="true"/> if the caller has the required permissions and path contains the name of an existing file; otherwise, <see langword="false"/>. 
      /// This method also returns <see langword="false"/> if path is <see langword="null"/>, an invalid path, or a zero-length string. 
      /// If the caller does not have sufficient permissions to read the specified file, no exception is thrown and the method returns 
      /// <see langword="false"/> regardless of the existence of path.
      /// </returns>
      public static Boolean FolderExists(String folder)
      {
         return Directory.Exists(folder);
      } // FolderExists


      /// <summary>
      /// Retrieve the parameter delimiter according to the OS' typical flavor
      /// </summary>
      /// <returns>OS typical parameter delimiter</returns>
      public static string GetDefaultPathDelimiterForOS()
      {
         if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
         {
            return DELIMITER_PATH_WIN;
         }
         else
         {
            return DELIMITER_PATH_POSIX;
         }
      }

      /// <summary>
      /// Creates a backup of a file by copying/moving it from the source 
      /// to the target folder.
      /// </summary>
      /// <param name="fileSource">
      /// Fully qualified source file name.
      /// </param>
      /// <param name="fileDest">
      /// Fully qualified destination file name.
      /// </param>
      /// <param name="copyOnly">
      /// Copy only (True) or move (False)?
      /// </param>
      /// <param name="incrementTarget">
      /// If <paramref name="fileDest"/> exists, create a new file named 
      /// (file).(nnnn).(ext) instead?
      /// Please note: duplicate creation is limited to 10,000 files.
      /// </param>
      /// <param name="newFile">
      /// (ByRef!) Returns the (newly created) fully qualified destination file name.
      /// </param>
      /// <returns>
      /// Success: <see langword="true"/>, failure: <see langword="false"/>.
      /// </returns>
      /// <remarks>
      /// If the destination file already exists, but <paramref name="incrementTarget"/> = <see langword="false"/>, 
      /// the already existing file will be overwritten.
      /// </remarks>
      public static bool BackupFile(string fileSource, string fileDest, out string newFile, bool copyOnly = false,
         bool incrementTarget = true)
      {
         string tempFile = System.String.Empty;

         string destPath = System.String.Empty, destFile = System.String.Empty, destExt = System.String.Empty;

         // Safe guard
         if (FileExists(fileSource) == false)
         {
            newFile = System.String.Empty;
            return false;
         }

         // Check if the destination file already exists
         // Split the file name into its components path, file name, extension
         // Create a new backup file name (incrementTarget = True)
         if (FileExists(fileDest) == true)
         {
            // Split the file name into its components
            destPath = GetDirectoryName(fileDest);
            destFile = GetFileNameWithoutExtension(fileDest);
            destExt = GetExtension(fileDest);

            // Target already exists, create a copy instead?
            Int32 i;
            if (incrementTarget == true)
            {
               i = 0;
               // Generate a new file name that eventually doesn't exist in the target folder.Stop at 9999!
               tempFile = NormalizePath(destPath) + destFile + "." + System.String.Format("{0:0000}", i) + destExt;
               while (Exists(tempFile) == true & i < 9999)
               {
                  i += 1;
                  tempFile = NormalizePath(destPath) + destFile + "." + System.String.Format("{0:0000}", i) + destExt;
               }
            }
            else
            {
               tempFile = fileDest;
            }
         }
         else
         {
            tempFile = fileDest;
         }

         if (copyOnly == true)
         {
            File.Copy(fileSource, tempFile);
         }
         else
         {
            File.Move(fileSource, tempFile);
         }

         newFile = tempFile;
         return true;

      } // BackupFile

   } // class FilesystemHelper

}
