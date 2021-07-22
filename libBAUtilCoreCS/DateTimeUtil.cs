using System;
using System.ComponentModel;
// using static System.String;

using static libBAUtilCoreCS.StringUtil;

namespace libBAUtilCoreCS
{
   /// <summary>
   /// Enhanced with mostly tourism-related methods/properties <see cref="System.DateTime"/> object.
   /// </summary>
   /// <remarks>
   /// Inherits <see cref="DateTime"/>
   /// </remarks>
   [DefaultProperty("Date")]
   public class DateTimeUtil
   {
      #region "Declares"
      /// <summary>
      /// Format of IATA date, ddMMM or ddDMMMyy
      /// </summary>
      public enum eIATADateType : int
      {
         DateLong,
         DateShort
      }

      /// <summary>
      /// Casing of month abbreviations
      /// </summary>
      public enum eIATADateCasing : int
      {
         ToLower, // 01jan
         ToMixed, // 01Jan
         ToUpper, // 01JAN
      }

      private DateTime mdtmDate;
      #endregion

      #region "Properties - Public"

      /// <summary>
      /// Gets a DateTime value that represents the date component of the current <see cref="DateTimeOffset"/> object.
      /// </summary>
      /// <returns><see cref="Date"/></returns>
      /// <remarks>Default property</remarks>
      public DateTime Date
      {
         get
         {
            return mdtmDate;
         }
         set
         {
            mdtmDate = value;
         }
      }

      /// <summary>
      /// Gets a DateTime value that represents the date component of the current <see cref="DateTimeOffset"/> object.
      /// </summary>
      /// <returns><see cref="System.DateTime"/></returns>
      public System.DateTime DateTime
         { 
            get
            {
               return mdtmDate;
            }
            set
            {
               mdtmDate = value;
            }

         }

      /// <summary>
      /// Gets the day of the month represented by the current <see cref="DateTimeOffset"/> object.
      /// </summary>
      /// <returns>
      /// The day component of the current DateTimeOffset object, expressed as a value between 1 and 31.
      /// </returns>
      public int Day => this.Date.Day;
      /// <summary>
      /// Gets the DayOfWeek of the week represented by the current <see cref="DateTimeOffset"/> object.
      /// </summary>
      /// <returns>
      /// One of the enumeration values that indicates the day of the week of the current <see cref="DateTimeOffset"/> object.
      /// </returns>
      public System.DayOfWeek DayOfWeek => this.Date.DayOfWeek;

      /// <summary>
      /// Gets the DayOfYear of the year represented by the current <see cref="DateTimeOffset"/> object.
      /// </summary>
      /// <returns>
      /// The day of the year of the current <see cref="DateTimeOffset"/> object, expressed as a value between 1 and 366.
      /// </returns>
      public int DayOfYear => this.Date.DayOfYear;

      /// <summary>
      /// Gets the hour component of the time represented by the current <see cref="DateTimeOffset"/> object.
      /// </summary>
      /// <returns>
      /// The hour component of the current <see cref="DateTimeOffset"/> object. This property uses a 24-hour clock; the value ranges from 0 to 23.
      /// </returns>
      public int Hour => this.Date.Hour;

      public string IATADateLong => this.ToDateIATA(eIATADateType.DateLong, eIATADateCasing.ToUpper);

      public string IATADateShort => this.ToDateIATA(eIATADateType.DateShort, eIATADateCasing.ToUpper);

      public int Millisecond => this.Date.Millisecond;

      public int Minute => this.Date.Minute;

      public int Month => this.Date.Month;
         
      public DateTime Now => DateTime.Now;

      public int Second => this.Date.Second;

      public long Ticks => this.Date.Ticks;

      public System.TimeSpan TimeOfDay => this.Date.TimeOfDay;

      public DateTime Today => DateTime.Today;

      public DateTime UtcNow => DateTime.UtcNow;

      public int Year => this.Date.Year;

      public DateTimeOffset MaxValue => DateTime.MaxValue;

      public DateTimeOffset MinValue => DateTime.MinValue;
      #endregion

      #region "Methods - Public"
      public DateTime Add(System.TimeSpan timeSpan)
      {
         return this.Date.Add(timeSpan);
      }

      public DateTime AddDays(double days)
      {
         return this.Date.AddDays(days);
      }

      public DateTime AddHours(double hours)
      {
         return this.Date.AddHours(hours);
       }

      public DateTime AddMilliseconds(double milliseconds)
      {
         return this.Date.AddMilliseconds(milliseconds);
      }

      public DateTime AddMinutes(double minutes)
      {
         return this.Date.AddMinutes(minutes);
      }

      public DateTime AddMonths(int months)
      {
         return this.Date.AddMonths(months);
      }

      public DateTime AddSeconds(double seconds)
      { 
            return this.Date.AddSeconds(seconds);
      }

      public DateTime AddTicks(long ticks)
      {
         return this.Date.AddTicks(ticks);
      }
      
      public DateTime AddYears(int years)
      {
         return this.Date.AddYears(years);
      }

      public int CompareTo(DateTime value)
      {
         return this.Date.CompareTo(value);
      }

      public int CompareTo(Object value)
      {
         return this.Date.CompareTo(value);
       }

      public int DaysInMonth(int year, int month)
      {
         return DateTime.DaysInMonth(year, month);
      }

      public Boolean Equals(DateTime value)
      {
         return this.Date.Equals(value);
      }

      public DateTime FromBinary(long dateData)
      {
         return DateTime.FromBinary(dateData);
      }

      public DateTime FromFileTime(long dateData)
      {
         return DateTime.FromFileTime(dateData);
      }

      public DateTime FromFileTimeUtc(long dateData)
      {
         return DateTime.FromFileTimeUtc(dateData);
      }

      public DateTime FromOADate(double d)
      {
         return DateTime.FromOADate(d);
      }
      
      public string[] GetDateTimeFormats()
      {
         return this.Date.GetDateTimeFormats();
      }

      public string[] GetDateTimeFormats(Char format)
      {
         return this.Date.GetDateTimeFormats(format);
      }

      public string[] GetDateTimeFormats(Char format, System.IFormatProvider provider)
      {
         return this.Date.GetDateTimeFormats(format, provider);
      }

      public string[] GetDateTimeFormats(System.IFormatProvider provider)
      {
         return this.Date.GetDateTimeFormats(provider);
      }

      public Boolean IsDaylightSavingTime()
      {
         return this.Date.IsDaylightSavingTime();
      }

      public Boolean IsLeapYear(int year)
      {
         return DateTime.IsLeapYear(year);
      }

      public System.TimeSpan Subtract(DateTime value)
      {
         return this.Date.Subtract(value);
      }

      public DateTime Subtract(System.TimeSpan value)
      {
         return this.Date.Subtract(value);
      }

      /// <summary>
      /// return the last day in a month
      /// </summary>
      /// <param name="month">Last day of this month</param>
      /// <param name="year">Month in this year</param>
      /// <returns>
      /// Last day of given <paramref name="month"/> in <paramref name="year"/> as <see cref="System.DateTime" />
      /// </returns>
      public static DateTime GetLastDayInMonth(Int32 month, Int32 year)
      { 
         if (month == 12)
         {
            month = 1;
            year += 1;
         }
         else
         {
            month += 1;
         }

         DateTime dtm = new DateTime(year, month, 1);
         TimeSpan tsp = new TimeSpan(1, 0, 0, 0);

         return dtm.Subtract(tsp);
      }

      /// <summary>
      /// Converts a IATA date string to a <see cref="System.DateTime"/> type
      /// </summary>
      /// <param name="iataDate">IATA date string, e.g. 01JAN or 01JAN19</param>
      /// <returns><see cref="System.DateTime"/></returns>
      /// <exception cref="ArgumentOutOfRangeException"></exception>
      public DateTime ToDate(string iataDate)
      { 

         // Safe guards
         if (iataDate.Length != 5 && iataDate.Length != 7)
         {
            throw new ArgumentOutOfRangeException("iataDate", "Invalid IATA date. Date must be either 5 or 7 characters, e.g. 01JAN or 01JAN19.");
         }

         string sDay = System.String.Empty;
         string sMonth = System.String.Empty;
         Int32 lMonth;
         string sYear = System.String.Empty;

         // Short IATA date
         if (iataDate.Length == 5)
         {
            sDay = Left(iataDate, 2);
            sMonth = Right(iataDate, 3);
            sYear = DateTime.Now.Year.ToString();
         }
         else if (iataDate.Length == 7)
         {
            sDay = Left(iataDate, 2);
            sMonth = iataDate.Substring(2, 3);
            sYear = Right(iataDate, 2);
         }

         switch (sMonth.ToLower())
         {
            case "jan":
               lMonth = 1;
               break;
            case "feb":
               lMonth = 2;
               break;
            case "mar":
               lMonth = 3;
               break;
            case "apr":
               lMonth = 4;
               break;
            case "may":
               lMonth = 5;
               break;
            case "jun":
               lMonth = 6;
               break;
            case "jul":
               lMonth = 7;
               break;
            case "aug":
               lMonth = 8;
               break;
            case "sep":
               lMonth = 9;
               break;
            case "oct":
               lMonth = 10;
               break;
            case "nov":
               lMonth = 11;
               break;
            case "dec":
               lMonth = 12;
               break;
            default:
               throw new ArgumentOutOfRangeException("iataDate (month)", "Month must be notated as 3-letter English month abbreviations, e.g. OCT for October");
         }

         // Should handle all other exceptions
         return new DateTime(int.Parse(sYear), lMonth, int.Parse(sDay));

      }  // ToDate

      public long ToBinary()
      {
         return this.Date.ToBinary();
      }

      /// <summary>
      /// Converts a <see cref="System.DateTime"/> to a IATA date string
      /// </summary>
      /// <returns><see cref="System.DateTime"/>Current <see cref="DateTime"/> as a string format as (ddMMM)</returns>
      public string ToDateIATA()
      {
         // Upper case is default
         return this.Date.ToString("ddMMM", System.Globalization.CultureInfo.InvariantCulture).ToUpper();
      }

      /// <summary>
      /// Converts a <see cref="System.DateTime"/> to a IATA date string
      /// </summary>
      /// <param name="dateType">
      /// Format of IATA date <see cref="eIATADateType"/>
      /// </param>
      /// <returns><see cref="System.DateTime"/>Current <see cref="DateTime"/> as a string formatted as (ddMMM)</returns>
      public string ToDateIATA(eIATADateType dateType)
      {
         // Upper case is default
         if (dateType == eIATADateType.DateLong)
         {
            return this.Date.ToString("ddMMMyy", System.Globalization.CultureInfo.InvariantCulture).ToUpper();
         }
         else
         {
            return this.Date.ToString("ddMMM", System.Globalization.CultureInfo.InvariantCulture).ToUpper();
         }
      }

      /// <summary>
      /// Converts a <see cref="System.DateTime"/> to a IATA date string
      /// </summary>
      /// <param name="nameCasing">
      /// Casing of IATA date <see cref="eIATADateCasing"/>
      /// </param>
      /// <returns><see cref="System.DateTime"/>Current <see cref="DateTime"/> as a string formatted as (ddMMM)</returns>
      public string ToDateIATA(eIATADateCasing nameCasing)
      { 
         switch (nameCasing)
         {
            case eIATADateCasing.ToLower:
               return this.Date.ToString("ddMMM", System.Globalization.CultureInfo.InvariantCulture).ToLower();
            case eIATADateCasing.ToMixed:
               return this.Date.ToString("ddMMM", System.Globalization.CultureInfo.InvariantCulture);
            case eIATADateCasing.ToUpper:
               return this.Date.ToString("ddMMM", System.Globalization.CultureInfo.InvariantCulture).ToUpper();
            default:
               return this.Date.ToString("ddMMM", System.Globalization.CultureInfo.InvariantCulture).ToUpper();
         }
      }

      /// <summary>
      /// Converts a <see cref="System.DateTime"/> to a IATA date string
      /// </summary>
      /// <param name="dateType">
      /// Format of IATA date <see cref="eIATADateType"/>
      /// </param>
      /// <param name="nameCasing">
      /// Casing of IATA date <see cref="eIATADateCasing"/>
      /// </param>
      /// <returns><see cref="System.DateTime"/>Current <see cref="Date"/> as a string format (ddMMM)</returns>
      public string ToDateIATA(eIATADateType dateType, eIATADateCasing nameCasing)
      {

         string sResult = this.ToDateIATA(dateType);

         switch (nameCasing)
         {
            case eIATADateCasing.ToLower:
               return sResult.ToLower();
            case eIATADateCasing.ToMixed:
               return sResult;
            case eIATADateCasing.ToUpper:
               return sResult.ToUpper();
            default:
               return sResult.ToUpper();
         }

      }

      public long ToFileTime()
      {
         return this.Date.ToFileTime();
      }

      public long ToFileTimeUtc()
      {
         return this.Date.ToFileTimeUtc();
      }

      public DateTime ToLocalTime()
      {
         return this.Date.ToLocalTime();
      }

      public string ToLongDateString()
      {
         return this.Date.ToLongDateString();
      }

      public string ToLongTimeString()
      {
         return this.Date.ToLongTimeString();
      }

      public double ToOADate()
      {
         return this.Date.ToOADate();
      }

      public string ToShortDateString()
      {
         return this.Date.ToShortDateString();
      }

      public string ToShortTimeString()
      {
         return this.Date.ToShortTimeString();
      }

      public override string ToString()
      {
         return this.Date.ToString();
      }

      public string ToString(string format)
      {
         return this.Date.ToString(format);
      }

      public string ToString(string format, System.IFormatProvider provider)
      {
         return this.Date.ToString(format, provider);
      }
      #endregion

      #region "Constructor/Dispose"

      public DateTimeUtil()
      { 
         this.Date = new DateTime();
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="DateTimeOffset"/> structure using the specified DateTime value.
      /// </summary>
      /// <param name="newDate">A date and time.</param>
      public DateTimeUtil(DateTime newDate)
      {
         this.Date = newDate;
      }

      public DateTimeUtil(Int64 ticks)
      { 
         this.Date = new DateTime(ticks);
      }

      public DateTimeUtil(Int64 ticks, DateTimeKind kind)
      {
         this.Date = new DateTime(ticks, kind);
      }

      public DateTimeUtil(Int32 year, Int32 month, Int32 day)
      {
         this.Date = new DateTime(year, month, day);
      }

      public DateTimeUtil(Int32 year, Int32 month, Int32 day, System.Globalization.Calendar calendar)
      {
         this.Date = new DateTime(year, month, day, calendar);
      }

      public DateTimeUtil(Int32 year, Int32 month, Int32 day, Int32 hour, Int32 minute, Int32 second)
      {
         this.Date = new DateTime(year, month, day, hour, minute, second);
      }

      public DateTimeUtil(Int32 year, Int32 month, Int32 day, Int32 hour, Int32 minute, Int32 second, DateTimeKind kind)
      {
         this.Date = new DateTime(year, month, day, hour, minute, second, kind);
      }

      public DateTimeUtil(Int32 year, Int32 month, Int32 day, Int32 hour, Int32 minute, Int32 second, System.Globalization.Calendar calendar)
      {
         this.Date = new DateTime(year, month, day, hour, minute, second, calendar);
      }

      public DateTimeUtil(Int32 year, Int32 month, Int32 day, Int32 hour, Int32 minute, Int32 second, Int32 millisecond)
      {
         this.Date = new DateTime(year, month, day, hour, minute, second, millisecond);
      }

      public DateTimeUtil(Int32 year, Int32 month, Int32 day, Int32 hour, Int32 minute, Int32 second, Int32 millisecond, DateTimeKind kind)
      {
         this.Date = new DateTime(year, month, day, hour, minute, second, millisecond, kind);
      }

      public DateTimeUtil(Int32 year, Int32 month, Int32 day, Int32 hour, Int32 minute, Int32 second, Int32 millisecond, System.Globalization.Calendar calendar)
      {
         this.Date = new DateTime(year, month, day, hour, minute, second, millisecond, calendar);
      }

      public DateTimeUtil(Int32 year, Int32 month, Int32 day, Int32 hour, Int32 minute, Int32 second, Int32 millisecond, System.Globalization.Calendar calendar, DateTimeKind kind)
      {

         this.Date = new DateTime(year, month, day, hour, minute, second, millisecond, calendar, kind);
      }

      public DateTimeUtil(string iataDate)
      {
         this.DateTime = this.ToDate(iataDate);
      }
      #endregion

   }  // DateTimeUtil
}
