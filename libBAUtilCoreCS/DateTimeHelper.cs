using System;
using System.ComponentModel;
// using static System.String;

using static libBAUtilCoreCS.StringHelper;

namespace libBAUtilCoreCS
{

  /// <summary>
  /// Enhanced with mostly tourism-related methods/properties <see cref="System.DateTime"/> object.
  /// </summary>
  /// <remarks>
  /// Inherits <see cref="DateTime"/>
  /// </remarks>
  [DefaultProperty("Date")]
  public class DateTimeHelper
  {
    #region Declares
    /// <summary>
    /// Format of IATA date, ddMMM or ddDMMMyy
    /// </summary>
    public enum IATADateType : int
    {
      /// <summary>
      /// Long date format, i.e. ddMMMyy
      /// </summary>
      DateLong,
      /// <summary>
      /// Short date format, i.e. ddMMM
      /// </summary>
      DateShort
    }

    /// <summary>
    /// Casing of month abbreviations
    /// </summary>
    public enum IATADateCasing : int
    {
      /// <summary>
      /// Lower case, e.g. 01jan
      /// </summary>
      ToLower,
      /// <summary>
      /// Mixed case, e.g. 01Jan
      /// </summary>
      ToMixed,
      /// <summary>
      /// Uper case, e.g. 01JAN
      /// </summary>
      ToUpper
    }

    private DateTime mdtmDate;
    #endregion

    #region Properties - Public

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

    /// <summary>
    /// Gets the current date represented by <see cref="Date"/> in IATA long date format, e.g. 01JAN21.
    /// </summary>
    public string IATADateLong => this.ToDateIATA(IATADateType.DateLong, IATADateCasing.ToUpper);

    /// <summary>
    /// Gets the current date represented by <see cref="Date"/> in IATA short date format, e.g. 01JAN.
    /// </summary>
    public string IATADateShort => this.ToDateIATA(IATADateType.DateShort, IATADateCasing.ToUpper);

    /// <summary>
    /// Gets a value that indicates whether the time represented by this instance is based on local time, Coordinated Universal Time (UTC), or neither.
    /// </summary>
    public DateTimeKind Kind => this.Date.Kind;

    /// <summary>
    /// Gets the milliseconds component of the date represented by this instance.
    /// </summary>
    public int Millisecond => this.Date.Millisecond;

    /// <summary>
    /// Gets the minute component of the date represented by this instance.
    /// </summary>
    public int Minute => this.Date.Minute;

    /// <summary>
    /// Gets the month component of the date represented by this instance.
    /// </summary>
    public int Month => this.Date.Month;

    /// <summary>
    /// Gets a DateTime object that is set to the current date and time on this computer, expressed as the local time.
    /// </summary>
    public DateTime Now => DateTime.Now;

    /// <summary>
    /// Gets the seconds component of the date represented by this instance.
    /// </summary>
    public int Second => this.Date.Second;

    /// <summary>
    /// Gets the number of ticks that represent the date and time of this instance.
    /// </summary>
    public long Ticks => this.Date.Ticks;

    /// <summary>
    /// Gets the time of day for this instance.
    /// </summary>
    public System.TimeSpan TimeOfDay => this.Date.TimeOfDay;

    /// <summary>
    /// Gets the current date.
    /// </summary>
    public DateTime Today => DateTime.Today;

    /// <summary>
    /// Gets a DateTime object that is set to the current date and time on this computer, expressed as the Coordinated Universal Time (UTC).
    /// </summary>
    public DateTime UtcNow => DateTime.UtcNow;

    /// <summary>
    /// Gets the year component of the date represented by this instance.
    /// </summary>
    public int Year => this.Date.Year;

    /// <summary>
    /// Represents the largest possible value of DateTime. This field is read-only.
    /// </summary>
    public DateTimeOffset MaxValue => DateTime.MaxValue;

    /// <summary>
    /// Represents the smallest possible value of DateTime. This field is read-only.
    /// </summary>
    public DateTimeOffset MinValue => DateTime.MinValue;
    #endregion

    #region Methods - Public
    /// <summary>
    /// Returns a new DateTime that adds the value of the specified TimeSpan to the value of this instance.
    /// </summary>
    /// <param name="timeSpan">A positive or negative time interval.</param>
    /// <returns>An object whose value is the sum of the date and time represented by this instance and the time interval represented by value.</returns>
    public DateTime Add(System.TimeSpan timeSpan)
    {
      return this.Date.Add(timeSpan);
    }

    /// <summary>
    /// Returns a new DateTime that adds the specified number of days to the value of this instance.
    /// </summary>
    /// <param name="days">A number of whole and fractional days. The value parameter can be negative or positive.</param>
    /// <returns>An object whose value is the sum of the date and time represented by this instance and the number of days represented by value.</returns>
    public DateTime AddDays(double days)
    {
      return this.Date.AddDays(days);
    }

    /// <summary>
    /// Returns a new DateTime that adds the specified number of hours to the value of this instance.
    /// </summary>
    /// <param name="hours">A number of whole and fractional hours. The value parameter can be negative or positive.</param>
    /// <returns>Returns a new DateTime that adds the specified number of hours to the value of this instance.</returns>
    public DateTime AddHours(double hours)
    {
      return this.Date.AddHours(hours);
    }

    /// <summary>
    /// Returns a new DateTime that adds the specified number of milliseconds to the value of this instance.
    /// </summary>
    /// <param name="milliseconds">A number of whole and fractional milliseconds. The value parameter can be negative or positive. Note that this value is rounded to the nearest integer.</param>
    /// <returns>An object whose value is the sum of the date and time represented by this instance and the number of milliseconds represented by value.</returns>
    public DateTime AddMilliseconds(double milliseconds)
    {
      return this.Date.AddMilliseconds(milliseconds);
    }

    /// <summary>
    /// Returns a new DateTime that adds the specified number of minutes to the value of this instance.
    /// </summary>
    /// <param name="minutes">A number of whole and fractional minutes. The value parameter can be negative or positive.</param>
    /// <returns>An object whose value is the sum of the date and time represented by this instance and the number of minutes represented by value.</returns>
    public DateTime AddMinutes(double minutes)
    {
      return this.Date.AddMinutes(minutes);
    }

    /// <summary>
    /// Returns a new DateTime that adds the specified number of months to the value of this instance.
    /// </summary>
    /// <param name="months">A number of months. The months parameter can be negative or positive.</param>
    /// <returns>An object whose value is the sum of the date and time represented by this instance and months.</returns>
    public DateTime AddMonths(int months)
    {
      return this.Date.AddMonths(months);
    }

    /// <summary>
    /// Returns a new DateTime that adds the specified number of seconds to the value of this instance.
    /// </summary>
    /// <param name="seconds">A number of whole and fractional seconds. The value parameter can be negative or positive.</param>
    /// <returns>An object whose value is the sum of the date and time represented by this instance and the number of seconds represented by value.</returns>
    public DateTime AddSeconds(double seconds)
    {
      return this.Date.AddSeconds(seconds);
    }

    /// <summary>
    /// Returns a new DateTime that adds the specified number of ticks to the value of this instance.
    /// </summary>
    /// <param name="ticks">A number of 100-nanosecond ticks. The value parameter can be positive or negative.</param>
    /// <returns>An object whose value is the sum of the date and time represented by this instance and the time represented by value.</returns>
    public DateTime AddTicks(long ticks)
    {
      return this.Date.AddTicks(ticks);
    }

    /// <summary>
    /// Returns a new DateTime that adds the specified number of years to the value of this instance.
    /// </summary>
    /// <param name="years">A number of years. The value parameter can be negative or positive.</param>
    /// <returns>An object whose value is the sum of the date and time represented by this instance and the number of years represented by value.</returns>
    public DateTime AddYears(int years)
    {
      return this.Date.AddYears(years);
    }

    /// <summary>
    /// Compares the value of this instance to a specified DateTime value and returns an integer that indicates whether this instance is earlier than, the same as, or later than the specified DateTime value.
    /// </summary>
    /// <param name="value">The object to compare to the current instance.</param>
    /// <returns>The object to compare to the current instance.
    /// Less than zero: This instance is earlier than value. 
    /// Zero: This instance is the same as value. 
    /// Greater than zero This instance is later than value. 
    /// </returns>
    public int CompareTo(DateTime value)
    {
      return this.Date.CompareTo(value);
    }

    /// <summary>
    /// Compares the value of this instance to a specified object that contains a specified DateTime value, and returns an integer that indicates whether this instance is earlier than, the same as, or later than the specified DateTime value.
    /// </summary>
    /// <param name="value">A boxed object to compare, or null.</param>
    /// <returns>The object to compare to the current instance.
    /// Less than zero: This instance is earlier than value. 
    /// Zero: This instance is the same as value. 
    /// Greater than zero This instance is later than value. 
    /// </returns>
    public int CompareTo(Object value)
    {
      return this.Date.CompareTo(value);
    }

    /// <summary>
    /// Returns the number of days in the specified month and year.
    /// </summary>
    /// <param name="year">The year.</param>
    /// <param name="month">The month (a number ranging from 1 to 12).</param>
    /// <returns>The number of days in month for the specified year.
    /// For example, if month equals 2 for February, the return value is 28 or 29 depending upon whether year is a leap year.
    /// </returns>
    public int DaysInMonth(int year, int month)
    {
      return DateTime.DaysInMonth(year, month);
    }

    /// <summary>
    /// Returns a value indicating whether the value of this instance is equal to the value of the specified DateTime instance.
    /// </summary>
    /// <param name="value">The object to compare to this instance.</param>
    /// <returns>
    /// <see langword="true"/> if the value parameter equals the value of this instance; otherwise, <see langword="false"/>. 
    /// </returns>
    public bool Equals(DateTime value)
    {
      return this.Date.Equals(value);
    }

    /// <summary>
    /// Returns a value indicating whether two DateTime objects, or a DateTime instance and another object or DateTime, have the same value.
    /// </summary>
    /// <param name="t1">The first object to compare.</param>
    /// <param name="t2">The second object to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the two values are equal; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(DateTime t1, DateTime t2)
    {
      return DateTime.Equals(t1, t2);
    }

    /// <summary>
    /// Returns a value indicating whether this instance is equal to a specified object.
    /// </summary>
    /// <param name="value">The object to compare to this instance.</param>
    /// <returns>
    /// <see langword="true"/> if value is an instance of DateTime and equals the value of this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals(Object value)
    {
      return this.Date.Equals(value);
    }

    /// <summary>
    /// Deserializes a 64-bit binary value and recreates an original serialized DateTime object.
    /// </summary>
    /// <param name="dateData">A 64-bit signed integer that encodes the Kind property in a 2-bit field and the Ticks property in a 62-bit field.</param>
    /// <returns>An object that is equivalent to the DateTime object that was serialized by the <see cref="DateTime.ToBinary()"/> method.</returns>
    public DateTime FromBinary(long dateData)
    {
      return DateTime.FromBinary(dateData);
    }

    /// <summary>
    /// Converts the specified Windows file time to an equivalent local time.
    /// </summary>
    /// <param name="fileTime">A Windows file time expressed in ticks.</param>
    /// <returns>An object that represents the local time equivalent of the date and time represented by the fileTime parameter.</returns>
    public DateTime FromFileTime(long fileTime)
    {
      return DateTime.FromFileTime(fileTime);
    }

    /// <summary>
    /// Converts the specified Windows file time to an equivalent UTC time.
    /// </summary>
    /// <param name="fileTime">A Windows file time expressed in ticks.</param>
    /// <returns>An object that represents the UTC time equivalent of the date and time represented by the fileTime parameter.</returns>
    public DateTime FromFileTimeUtc(long fileTime)
    {
      return DateTime.FromFileTimeUtc(fileTime);
    }

    /// <summary>
    /// Returns a <see cref="DateTime"/> equivalent to the specified OLE Automation Date.
    /// </summary>
    /// <param name="d">An OLE Automation Date value.</param>
    /// <returns>An object that represents the same date and time as d.</returns>
    public DateTime FromOADate(double d)
    {
      return DateTime.FromOADate(d);
    }

    /// <summary>
    /// Converts the value of this instance to all the string representations supported by the standard date and time format specifiers.
    /// </summary>
    /// <returns>A string array where each element is the representation of the value of this instance formatted with one of the standard date and time format specifiers.</returns>
    public string[] GetDateTimeFormats()
    {
      return this.Date.GetDateTimeFormats();
    }

    /// <summary>
    /// Converts the value of this instance to all the string representations supported by the specified standard date and time format specifier.
    /// </summary>
    /// <param name="format">A standard date and time format string.</param>
    /// <returns>A string array where each element is the representation of the value of this instance formatted with one of the standard date and time format specifiers.</returns>
    public string[] GetDateTimeFormats(Char format)
    {
      return this.Date.GetDateTimeFormats(format);
    }

    /// <summary>
    /// Converts the value of this instance to all the string representations supported by the specified standard date and time format specifier.
    /// </summary>
    /// <param name="provider">An object that supplies culture-specific formatting information about this instance.</param>
    /// <returns>A string array where each element is the representation of the value of this instance formatted with one of the standard date and time format specifiers.</returns>
    public string[] GetDateTimeFormats(System.IFormatProvider provider)
    {
      return this.Date.GetDateTimeFormats(provider);
    }

    /// <summary>
    /// Converts the value of this instance to all the string representations supported by the specified standard date and time format specifier.
    /// </summary>
    /// <param name="format">A standard date and time format string.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information about this instance.</param>
    /// <returns>A string array where each element is the representation of the value of this instance formatted with one of the standard date and time format specifiers.</returns>
    public string[] GetDateTimeFormats(Char format, System.IFormatProvider provider)
    {
      return this.Date.GetDateTimeFormats(format, provider);
    }

    /// <summary>
    /// Indicates whether this instance of <see cref="System.DateTime"/> is within the daylight saving time range for the current time zone.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the value of the <see cref="DateTime.Kind"/> property is <see cref="DateTimeKind.Local"/> or 
    /// <see cref="DateTimeKind.Unspecified"/> and the value of this instance of <see cref="System.DateTime"/> is within the 
    /// daylight saving time range for the local time zone; false if <see cref="DateTime.Kind"/> is <see cref="DateTimeKind.Utc"/>.
    /// </returns>
    public bool IsDaylightSavingTime()
    {
      return this.Date.IsDaylightSavingTime();
    }

    /// <summary>
    /// Returns an indication whether the specified year is a leap year.
    /// </summary>
    /// <param name="year">A 4-digit year.</param>
    /// <returns><see langword="true"/> if year is a leap year; otherwise, <see langword="false"/> .</returns>
    public bool IsLeapYear(int year)
    {
      return DateTime.IsLeapYear(year);
    }

    /// <summary>
    /// Returns the value that results from subtracting the specified time or duration from the value of this instance
    /// </summary>
    /// <param name="value">The date and time value to subtract.</param>
    /// <returns>A time interval that is equal to the date and time represented by this instance minus the date and time represented by value.</returns>
    public System.TimeSpan Subtract(DateTime value)
    {
      return this.Date.Subtract(value);
    }

    /// <summary>
    /// Returns a new DateTime that subtracts the specified duration from the value of this instance.
    /// </summary>
    /// <param name="value">The time interval to subtract.</param>
    /// <returns>An object that is equal to the date and time represented by this instance minus the time interval represented by value.</returns>
    public DateTime Subtract(System.TimeSpan value)
    {
      return this.Date.Subtract(value);
    }

    /// <summary>
    /// Return the last day in a month.
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
        sYear = Left(DateTime.Now.Year.ToString(), 2) + Right(iataDate, 2);
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

    /// <summary>
    /// Serializes the current DateTime object to a 64-bit binary value that subsequently can be used to recreate the DateTime object.
    /// </summary>
    /// <returns>A 64-bit signed integer that encodes the Kind and Ticks properties.</returns>
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
    /// Format of IATA date <see cref="IATADateType"/>
    /// </param>
    /// <returns><see cref="System.DateTime"/>Current <see cref="DateTime"/> as a string formatted as (ddMMM)</returns>
    public string ToDateIATA(IATADateType dateType)
    {
      // Upper case is default
      if (dateType == IATADateType.DateLong)
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
    /// Casing of IATA date <see cref="IATADateCasing"/>
    /// </param>
    /// <returns><see cref="System.DateTime"/>Current <see cref="DateTime"/> as a string formatted as (ddMMM)</returns>
    public string ToDateIATA(IATADateCasing nameCasing)
    {
      switch (nameCasing)
      {
        case IATADateCasing.ToLower:
          return this.Date.ToString("ddMMM", System.Globalization.CultureInfo.InvariantCulture).ToLower();
        case IATADateCasing.ToMixed:
          return this.Date.ToString("ddMMM", System.Globalization.CultureInfo.InvariantCulture);
        case IATADateCasing.ToUpper:
          return this.Date.ToString("ddMMM", System.Globalization.CultureInfo.InvariantCulture).ToUpper();
        default:
          return this.Date.ToString("ddMMM", System.Globalization.CultureInfo.InvariantCulture).ToUpper();
      }
    }

    /// <summary>
    /// Converts a <see cref="System.DateTime"/> to a IATA date string
    /// </summary>
    /// <param name="dateType">
    /// Format of IATA date <see cref="IATADateType"/>
    /// </param>
    /// <param name="nameCasing">
    /// Casing of IATA date <see cref="IATADateCasing"/>
    /// </param>
    /// <returns><see cref="System.DateTime"/>Current <see cref="Date"/> as a string format (ddMMM)</returns>
    public string ToDateIATA(IATADateType dateType, IATADateCasing nameCasing)
    {

      string sResult = this.ToDateIATA(dateType);

      switch (nameCasing)
      {
        case IATADateCasing.ToLower:
          return sResult.ToLower();
        case IATADateCasing.ToMixed:
          return sResult;
        case IATADateCasing.ToUpper:
          return sResult.ToUpper();
        default:
          return sResult.ToUpper();
      }

    }

    /// <summary>
    /// Converts the value of the current DateTime object to a Windows file time.
    /// </summary>
    /// <returns>The value of the current DateTime object expressed as a Windows file time.</returns>
    public long ToFileTime()
    {
      return this.Date.ToFileTime();
    }

    /// <summary>
    /// Converts the value of the current DateTime object to a Windows file time.
    /// </summary>
    /// <returns>The value of the current DateTime object expressed as a Windows file time.</returns>
    public long ToFileTimeUtc()
    {
      return this.Date.ToFileTimeUtc();
    }

    /// <summary>
    /// Converts the value of the current DateTime object to local time.
    /// </summary>
    /// <returns>
    /// An object whose Kind property is Local, and whose value is the local time equivalent to the value of the current DateTime object, 
    /// or MaxValue if the converted value is too large to be represented by a DateTime object, or MinValue if the converted value is 
    /// too small to be represented as a DateTime object.
    /// </returns>
    public DateTime ToLocalTime()
    {
      return this.Date.ToLocalTime();
    }

    /// <summary>
    /// Converts the value of the current DateTime object to its equivalent long date string representation.
    /// </summary>
    /// <returns>A string that contains the long date string representation of the current DateTime object.</returns>
    public string ToLongDateString()
    {
      return this.Date.ToLongDateString();
    }

    /// <summary>
    /// Converts the value of the current DateTime object to its equivalent long time string representation.
    /// </summary>
    /// <returns>A string that contains the long time string representation of the current DateTime object.</returns>
    public string ToLongTimeString()
    {
      return this.Date.ToLongTimeString();
    }

    /// <summary>
    /// Converts the value of this instance to the equivalent OLE Automation date.
    /// </summary>
    /// <returns>A double-precision floating-point number that contains an OLE Automation date equivalent to the value of this instance.</returns>
    public double ToOADate()
    {
      return this.Date.ToOADate();
    }

    /// <summary>
    /// Converts the value of the current DateTime object to its equivalent short date string representation.
    /// </summary>
    /// <returns>A string that contains the short date string representation of the current DateTime object.</returns>
    public string ToShortDateString()
    {
      return this.Date.ToShortDateString();
    }

    /// <summary>
    /// Converts the value of the current DateTime object to its equivalent short time string representation.
    /// </summary>
    /// <returns>A string that contains the short time string representation of the current DateTime object.</returns>
    public string ToShortTimeString()
    {
      return this.Date.ToShortTimeString();
    }

    /// <summary>
    /// Converts the value of the current DateTime object to its equivalent string representation using the formatting conventions of the current culture.
    /// </summary>
    /// <returns>A string representation of the value of the current DateTime object.</returns>
    public override string ToString()
    {
      return this.Date.ToString();
    }

    /// <summary>
    /// Converts the value of the current DateTime object to its equivalent string representation using the specified format and the formatting conventions of the current culture.
    /// </summary>
    /// <param name="format">A standard or custom date and time format string.</param>
    /// <returns>A string representation of value of the current DateTime object as specified by format.</returns>
    public string ToString(string format)
    {
      return this.Date.ToString(format);
    }

    /// <summary>
    /// Converts the value of the current DateTime object to its equivalent string representation.
    /// </summary>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <returns>A string representation of value of the current DateTime object as specified by provider.</returns>
    public string ToString(System.IFormatProvider provider)
    {
      return this.Date.ToString(provider);
    }

    /// <summary>
    /// Converts the value of the current DateTime object to its equivalent string representation.
    /// </summary>
    /// <param name="format">A standard or custom date and time format string.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <returns>A string representation of value of the current DateTime object as specified by format and provider.</returns>
    public string ToString(string format, System.IFormatProvider provider)
    {
      return this.Date.ToString(format, provider);
    }

    /// <summary>
    /// Implements <see cref="Object.GetHashCode()"/>
    /// </summary>
    /// <returns><see cref="Object.GetHashCode()"/></returns>
    public override Int32 GetHashCode()
    {
      var hashCode = -794484751;
      hashCode = hashCode * -1521134295 + this.Date.GetHashCode();
      hashCode = hashCode * -1521134295 + this.DateTime.GetHashCode();
      return hashCode;
    }
    #endregion

    #region Constructor/Dispose

    /// <summary>
    /// Initializes a new instance of the DateTime structure.
    /// </summary>
    public DateTimeHelper()
    {
      this.Date = new DateTime();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DateTimeOffset"/> structure using the specified DateTime value.
    /// </summary>
    /// <param name="newDate">A date and time.</param>
    public DateTimeHelper(DateTime newDate)
    {
      this.Date = newDate;
    }

    /// <summary>
    /// Initializes a new instance of the DateTime structure to a specified number of ticks.
    /// </summary>
    /// <param name="ticks">A date and time expressed in the number of 100-nanosecond intervals that have elapsed since January 1, 0001 at 00:00:00.000 in the Gregorian calendar.</param>
    public DateTimeHelper(Int64 ticks)
    {
      this.Date = new DateTime(ticks);
    }

    /// <summary>
    /// Initializes a new instance of the DateTime structure to a specified number of ticks and to Coordinated Universal Time (UTC) or local time.
    /// </summary>
    /// <param name="ticks">A date and time expressed in the number of 100-nanosecond intervals that have elapsed since January 1, 0001 at 00:00:00.000 in the Gregorian calendar.</param>
    /// <param name="kind">Coordinated Universal Time (UTC) or local time</param>
    public DateTimeHelper(Int64 ticks, DateTimeKind kind)
    {
      this.Date = new DateTime(ticks, kind);
    }

    /// <summary>
    /// Initializes a new instance of the DateTime structure to the specified year, month, and day.
    /// </summary>
    /// <param name="year">The year (1 through 9999).</param>
    /// <param name="month">The month (1 through 12).</param>
    /// <param name="day">The day (1 through the number of days in month).</param>
    public DateTimeHelper(Int32 year, Int32 month, Int32 day)
    {
      this.Date = new DateTime(year, month, day);
    }

    /// <summary>
    /// Initializes a new instance of the DateTime structure to the specified year, month, and day for the specified calendar.
    /// </summary>
    /// <param name="year">The year (1 through the number of years in calendar).</param>
    /// <param name="month">The month (1 through the number of months in calendar).</param>
    /// <param name="day">The day (1 through the number of days in month).</param>
    /// <param name="calendar">The calendar that is used to interpret year, month, and day.</param>
    public DateTimeHelper(Int32 year, Int32 month, Int32 day, System.Globalization.Calendar calendar)
    {
      this.Date = new DateTime(year, month, day, calendar);
    }

    /// <summary>
    /// Initializes a new instance of the DateTime structure to the specified year, month, day, hour, minute, and second.
    /// </summary>
    /// <param name="year">The year (1 through 9999).</param>
    /// <param name="month">The month (1 through 12).</param>
    /// <param name="day">The day (1 through the number of days in month).</param>
    /// <param name="hour">The hours (0 through 23).</param>
    /// <param name="minute">The minutes (0 through 59).</param>
    /// <param name="second">The seconds (0 through 59).</param>
    public DateTimeHelper(Int32 year, Int32 month, Int32 day, Int32 hour, Int32 minute, Int32 second)
    {
      this.Date = new DateTime(year, month, day, hour, minute, second);
    }

    /// <summary>
    /// Initializes a new instance of the DateTime structure to the specified year, month, day, hour, minute, second, and Coordinated Universal Time (UTC) or local time.
    /// </summary>
    /// <param name="year">The year (1 through 9999).</param>
    /// <param name="month">The month (1 through 12).</param>
    /// <param name="day">The day (1 through the number of days in month).</param>
    /// <param name="hour">The hours (0 through 23).</param>
    /// <param name="minute">The minutes (0 through 59).</param>
    /// <param name="second">The seconds (0 through 59).</param>
    /// <param name="kind">One of the enumeration values that indicates whether year, month, day, hour, minute and second specify a local time, Coordinated Universal Time (UTC), or neither.</param>
    public DateTimeHelper(Int32 year, Int32 month, Int32 day, Int32 hour, Int32 minute, Int32 second, DateTimeKind kind)
    {
      this.Date = new DateTime(year, month, day, hour, minute, second, kind);
    }

    /// <summary>
    /// Initializes a new instance of the DateTime structure to the specified year, month, day, hour, minute, and second for the specified calendar.
    /// </summary>
    /// <param name="year">The year (1 through the number of years in calendar).</param>
    /// <param name="month">The month (1 through the number of months in calendar).</param>
    /// <param name="day">The day (1 through the number of days in month).</param>
    /// <param name="hour">The hours (0 through 23).</param>
    /// <param name="minute">The minutes (0 through 59).</param>
    /// <param name="second">The seconds (0 through 59).</param>
    /// <param name="calendar">The calendar that is used to interpret year, month, and day.</param>
    public DateTimeHelper(Int32 year, Int32 month, Int32 day, Int32 hour, Int32 minute, Int32 second, System.Globalization.Calendar calendar)
    {
      this.Date = new DateTime(year, month, day, hour, minute, second, calendar);
    }

    /// <summary>
    /// Initializes a new instance of the DateTime structure to the specified year, month, day, hour, minute, second, and millisecond.
    /// </summary>
    /// <param name="year">The year (1 through 9999).</param>
    /// <param name="month">The month (1 through 12).</param>
    /// <param name="day">The day (1 through the number of days in month).</param>
    /// <param name="hour">The hours (0 through 23).</param>
    /// <param name="minute">The minutes (0 through 59).</param>
    /// <param name="second">The seconds (0 through 59).</param>
    /// <param name="millisecond">The milliseconds (0 through 999).</param>
    public DateTimeHelper(Int32 year, Int32 month, Int32 day, Int32 hour, Int32 minute, Int32 second, Int32 millisecond)
    {
      this.Date = new DateTime(year, month, day, hour, minute, second, millisecond);
    }

    /// <summary>
    /// Initializes a new instance of the DateTime structure to the specified year, month, day, hour, minute, second, millisecond, and Coordinated Universal Time (UTC) or local time.
    /// </summary>
    /// <param name="year">The year (1 through 9999).</param>
    /// <param name="month">The month (1 through 12).</param>
    /// <param name="day">The day (1 through the number of days in month).</param>
    /// <param name="hour">The hours (0 through 23).</param>
    /// <param name="minute">The minutes (0 through 59).</param>
    /// <param name="second">The seconds (0 through 59).</param>
    /// <param name="millisecond">The milliseconds (0 through 999).</param>
    /// <param name="kind">One of the enumeration values that indicates whether year, month, day, hour, minute, second, and millisecond specify a local time, Coordinated Universal Time (UTC), or neither.</param>
    public DateTimeHelper(Int32 year, Int32 month, Int32 day, Int32 hour, Int32 minute, Int32 second, Int32 millisecond, DateTimeKind kind)
    {
      this.Date = new DateTime(year, month, day, hour, minute, second, millisecond, kind);
    }

    /// <summary>
    /// One of the enumeration values that indicates whether year, month, day, hour, minute, second, and millisecond specify a local time, Coordinated Universal Time (UTC), or neither.
    /// </summary>
    /// <param name="year">The year (1 through the number of years in calendar).</param>
    /// <param name="month">The month (1 through the number of months in calendar).</param>
    /// <param name="day">The day (1 through the number of days in month).</param>
    /// <param name="hour">The hours (0 through 23).</param>
    /// <param name="minute">The minutes (0 through 59).</param>
    /// <param name="second">The seconds (0 through 59).</param>
    /// <param name="millisecond">The milliseconds (0 through 999).</param>
    /// <param name="calendar">The calendar that is used to interpret year, month, and day.</param>
    public DateTimeHelper(Int32 year, Int32 month, Int32 day, Int32 hour, Int32 minute, Int32 second, Int32 millisecond, System.Globalization.Calendar calendar)
    {
      this.Date = new DateTime(year, month, day, hour, minute, second, millisecond, calendar);
    }

    /// <summary>
    /// Initializes a new instance of the DateTime structure to the specified year, month, day, hour, minute, second, millisecond, and Coordinated Universal Time (UTC) or local time for the specified calendar.
    /// </summary>
    /// <param name="year">The year (1 through the number of years in calendar).</param>
    /// <param name="month">The month (1 through the number of months in calendar).</param>
    /// <param name="day">The day (1 through the number of days in month).</param>
    /// <param name="hour">The hours (0 through 23).</param>
    /// <param name="minute">The minutes (0 through 59).</param>
    /// <param name="second">The seconds (0 through 59).</param>
    /// <param name="millisecond">The milliseconds (0 through 999).</param>
    /// <param name="calendar">The calendar that is used to interpret year, month, and day.</param>
    /// <param name="kind">One of the enumeration values that indicates whether year, month, day, hour, minute, second, and millisecond specify a local time, Coordinated Universal Time (UTC), or neither.</param>
    public DateTimeHelper(Int32 year, Int32 month, Int32 day, Int32 hour, Int32 minute, Int32 second, Int32 millisecond, System.Globalization.Calendar calendar, DateTimeKind kind)
    {

      this.Date = new DateTime(year, month, day, hour, minute, second, millisecond, calendar, kind);
    }

    /// <summary>
    /// Initializes a new instance of the DateTime structure to the specified IATA date.
    /// </summary>
    /// <param name="iataDate">IATA date in the form e.g. 01JAN or 01JAN21</param>
    public DateTimeHelper(string iataDate)
    {
      this.DateTime = this.ToDate(iataDate);
    }
    #endregion

  }  // class DateTimeHelper

}
