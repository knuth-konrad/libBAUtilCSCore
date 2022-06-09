
namespace libBAUtilCoreCS
{

  /// <summary>
  /// Separate code and (this) info data from ConsoleHelper,
  /// allowing for easier editing this info.
  /// </summary>
  public class ConHelperData
  {

    //  Copyright notice values

    /// <summary>
    /// Default application developer/author name.
    /// </summary>
    public const string COPY_AUTHOR = "Knuth Konrad";
    /// <summary>
    /// Default company name.
    /// </summary>
    public const string COPY_COMPANYNAME = "BasicAware";

    // Console defaults

    /// <summary>
    /// Default line separation characters.
    /// </summary>
    public const string CON_SEPARATOR = "---";

    private string msAuthor = COPY_AUTHOR;
    private string msCompany = COPY_COMPANYNAME;
    private string msLineSeparator = CON_SEPARATOR;

    #region Properties - Public

    /// <summary>
    /// Application developer name
    /// </summary>
    public string Author
    {
      get { return msAuthor; }
      set { msAuthor = value; }
    }

    /// <summary>
    /// Company / Copyright holder name
    /// </summary>
    public string Company
    {
      get { return msCompany; }
      set { msCompany = value; }
    }

    /// <summary>
    /// Line separator
    /// </summary>
    public string LineSeparator
    {
      get { return msLineSeparator; }
      set { msLineSeparator = value; }
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Class constructor
    /// </summary>
    /// <param name="authorName">Application developer</param>
    /// <param name="companyName">Copyright holder</param>
    /// <param name="lineSeparator">Line separator</param>
    public ConHelperData(string authorName = COPY_AUTHOR, string companyName = COPY_COMPANYNAME,
      string lineSeparator = CON_SEPARATOR)
    {
      Author = authorName;
      Company = companyName;
      LineSeparator = lineSeparator;
    }

    #endregion
  }

}
