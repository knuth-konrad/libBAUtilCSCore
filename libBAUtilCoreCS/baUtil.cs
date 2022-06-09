using System;

namespace libBAUtilCoreCS
{
  /// <summary>
  /// General multipurpose utility library.
  /// </summary>
  /// <remarks>
  /// Generates an XML comments for namespaces for SandCastle.
  /// See https://stackoverflow.com/questions/793210/xml-documentation-for-a-namespace
  /// </remarks>
  [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
  class NamespaceDoc
  {
  }
 
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
      /// <summary>
      /// VB False
      /// </summary>
      False = 0,
      /// <summary>
      /// VB True
      /// </summary>
      True = -1,
      /// <summary>
      /// VB UseDefault
      /// </summary>
      UseDefault = -2
    };

    #region "Formatting (Strings / Numbers)"

    #endregion

  } // class baUtil

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


    // ToDo: test if the below really matches VB.NET's \ operation

    #region IntDiv

    /// <summary>
    ///  Mimics VB.NET's \ (=integer division) arithmetical operator.
    ///  Divides two numbers and returns an integer result.
    ///  The result is the integer quotient of expression1 divided by expression2, 
    ///  which discards any remainder and retains only the integer portion. This is known as truncation.
    ///  Before performing the division, Visual Basic attempts to convert any floating-point numeric expression to Long.
    ///  The conversion to Long is also subject to banker's rounding.
    ///  If expression1 or expression2 evaluates to Nothing, it is treated as zero.
    /// </summary>
    /// <param name="exp1">Divide <paramref name="exp1"/> by <paramref name="exp2"/></param>
    /// <param name="exp2">Divide <paramref name="exp1"/> by <paramref name="exp2"/></param>
    /// <returns>Returns the integer quotient of <paramref name="exp1"/> divided <paramref name="exp2"/></returns>
    /// <remarks>
    /// VB.NET's \: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/operators/integer-division-operator
    /// </remarks>
    public static Int64 IntDiv(double exp1, double exp2)
    {
      // Round-to-nearest-even aka "Banker's Rounding" both parameters
      // e.g. 23.5 becomes +24, as does +24.5; while −23.5 becomes −24, as does −24.
      Int64 int1 = Convert.ToInt64(Math.Round(exp1, MidpointRounding.ToEven));
      Int64 int2 = Convert.ToInt64(Math.Round(exp2, MidpointRounding.ToEven));

      if (int2 == 0)
      {
        throw new DivideByZeroException("exp2 can't be zero.");
      }

      double result = int1 / int2;

      return Convert.ToInt64(Math.Truncate(result));
    }  // IntDiv

    /*
    /// <summary>
    ///  Mimics VB.NET's \ (=integer division) arithmetical operator.
    ///  Divides two numbers and returns an integer result.
    ///  The result is the integer quotient of expression1 divided by expression2, 
    ///  which discards any remainder and retains only the integer portion. This is known as truncation.
    ///  Before performing the division, Visual Basic attempts to convert any floating-point numeric expression to Long.
    ///  The conversion to Long is also subject to banker's rounding.
    ///  If expression1 or expression2 evaluates to Nothing, it is treated as zero.
    /// </summary>
    /// <param name="exp1">Divide <paramref name="exp1"/> by <paramref name="exp2"/></param>
    /// <param name="exp2">Divide <paramref name="exp1"/> by <paramref name="exp2"/></param>
    /// <returns>Returns the integer quotient of <paramref name="exp1"/> divided <paramref name="exp2"/></returns>
    /// <remarks>
    /// VB.NET's \: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/operators/integer-division-operator
    /// </remarks>
    public Int64 IntDiv(float exp1, float exp2)
    {
      // Round-to-nearest-even aka "Banker's Rounding" both parameters
      // e.g. 23.5 becomes +24, as does +24.5; while −23.5 becomes −24, as does −24.
      Int64 int1 = Convert.ToInt64(Math.Round(exp1, MidpointRounding.ToEven));
      Int64 int2 = Convert.ToInt64(Math.Round(exp2, MidpointRounding.ToEven));

      if (int2 == 0)
      {
        throw new DivideByZeroException("exp2 can't be zero.");
      }

      double result = int1 / int2;

      return Convert.ToInt64(Math.Truncate(result));
    }  // IntDiv

    /// <summary>
    ///  Mimics VB.NET's \ (=integer division) arithmetical operator.
    ///  Divides two numbers and returns an integer result.
    ///  The result is the integer quotient of expression1 divided by expression2, 
    ///  which discards any remainder and retains only the integer portion. This is known as truncation.
    ///  Before performing the division, Visual Basic attempts to convert any floating-point numeric expression to Long.
    ///  The conversion to Long is also subject to banker's rounding.
    ///  If expression1 or expression2 evaluates to Nothing, it is treated as zero.
    /// </summary>
    /// <param name="exp1">Divide <paramref name="exp1"/> by <paramref name="exp2"/></param>
    /// <param name="exp2">Divide <paramref name="exp1"/> by <paramref name="exp2"/></param>
    /// <returns>Returns the integer quotient of <paramref name="exp1"/> divided <paramref name="exp2"/></returns>
    /// <remarks>
    /// VB.NET's \: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/operators/integer-division-operator
    /// </remarks>
    public long IntDiv(decimal exp1, decimal exp2)
    {
      // Round-to-nearest-even aka "Banker's Rounding" both parameters
      // e.g. 23.5 becomes +24, as does +24.5; while −23.5 becomes −24, as does −24.
      long int1 = Convert.ToInt64(Math.Round(exp1, MidpointRounding.ToEven));
      long int2 = Convert.ToInt64(Math.Round(exp2, MidpointRounding.ToEven));

      if (int2 == 0)
      {
        throw new DivideByZeroException("exp2 can't be zero.");
      }

      double result = int1 / int2;

      return Convert.ToInt64(Math.Truncate(result));
    }  // IntDiv
    */

    #endregion

  } // class MathUtil

}
