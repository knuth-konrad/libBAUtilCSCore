using System;
// using System.ComponentModel;

namespace libBAUtilCoreCS
{
   /// <summary>
   /// General purpose helper methods
   /// </summary>
   public class baUtil
{
   /// <summary>
   /// Mimic Microsoft.VisualBasic.TriState
   /// </summary> 
   public enum TriState
   {
      False = 0,
      True = -1,
      UseDefault = -2 };

   #region "Formatting (Strings / Numbers)"

   #endregion
}
   /// <summary>
   /// General math helpers
   /// </summary>
   public class MathUtil
   {
      /// <summary>
      /// Returns the % of Total given by Part, e.g. Total = 200, Part = 50 = 25(%)
      /// </summary>
      /// <param name="part">Part of <paramref name="total"/> to be expressed as a percent value.</param>
      /// <param name="total">Value considered to be 100%.</param>
      /// <returns><paramref name="part"/> percent of <paramref name="total"/></returns>
      public float Percent(float part, float total)
      {
         if (total == 0)
         {
            throw new ArgumentOutOfRangeException("total", "Value can't be 0 (zero).");
         }

         return (part / total) * 100;
      }
   }
}
