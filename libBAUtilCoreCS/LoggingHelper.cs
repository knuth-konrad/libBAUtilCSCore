using System;
using System.Linq;
using System.Reflection;

using NLog;

namespace libBAUtilCS
{
  /// <summary>
  /// (NLog) logging helper methods
  /// </summary>
  public class LoggingHelper
  {

    /// <summary>
    /// Return a list of parameters info passed to a method.
    /// </summary>
    /// <param name="mb">The current method.</param>
    /// <param name="param">Parameters passed to the above method.</param>
    /// <returns>Parameters</returns>
    /// <remarks>
    /// When passing the actual parameters in <paramref name="param"/>, ideally these 
    /// should be passed in the order they appear in the method prototype.
    /// </remarks>
    /// <example>
    /// Console.WriteLine(GetMethodParameters(System.Reflection.MethodBase.GetCurrentMethod()));
    /// </example>
    public static string GetMethodParameters(MethodBase mb, params object[] param)
    {
      ParameterInfo[] pis = mb.GetParameters();
      string result = "Parameters:\n";

      try
      {
        foreach (ParameterInfo pi in pis)
        {
          if (pi.IsOut)
          {
            result += String.Format(" - {0} (out)\n", pi.Name);
          }
          else
          {
            result += String.Format(" - {0}\n", pi.Name);
          }
          result += String.Format("  Type    : {0}\n", pi.ParameterType);
          result += String.Format("  Position: {0}\n", pi.Position.ToString());
          if (pi.HasDefaultValue)
          {
            result += String.Format("  Default : {0}\n", pi.DefaultValue.ToString());
          }
        }
        if (param != null && param.GetLength(0) > 0)
        {
          result += "Values:\n";
          foreach (object o in param)
          {
            result += String.Format("  {0}\n", o.ToString());
          }
        }
      }
      catch { }

      return result;
    } // GetMethodParameters()

    /// <summary>
    /// Retrieve the current (text) log file's name and path
    /// </summary>
    /// <returns>NLog's log file incl. full path</returns>
    /// <remarks>
    /// Source: https://stackoverflow.com/questions/7332393/how-can-i-query-the-path-to-an-nlog-log-file
    /// </remarks>
    public static string GetNLogCurrentLogFile()
    {
      NLog.Targets.FileTarget fileTarget = LogManager.Configuration.AllTargets.FirstOrDefault(t => t is NLog.Targets.FileTarget) as NLog.Targets.FileTarget;
      return fileTarget == null ? string.Empty : fileTarget.FileName.Render(new LogEventInfo { Level = LogLevel.Info });
    } // GetNLogCurrentLogFile()

  } // class LoggingHelper
}
