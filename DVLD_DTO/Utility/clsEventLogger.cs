using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace DVLD_Shared.Utility
{
    public static class clsEventLogger
    {
        private const string SourceName = "DVLD_App";
        private const string LogName = "Application";
        private const int MaxEntryLength = 30000;

        public static void LogEvent(
            Exception ex,
            string contextData = "",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (ex == null) return;

            string className = string.IsNullOrEmpty(sourceFilePath)
                ? "UnknownClass"
                : Path.GetFileNameWithoutExtension(sourceFilePath);

            string innerExDetails = ex.InnerException != null
                ? $"\nInner Exception: {ex.InnerException.GetType().FullName} - {ex.InnerException.Message}"
                : string.Empty;

            string contextLine = string.IsNullOrWhiteSpace(contextData)
                ? string.Empty
                : $"Context Data: {contextData}\n";

            string fullDetails = $"Location: {className}.{memberName}() [Line {sourceLineNumber}]\n" +
                                 contextLine +
                                 $"Exception Type: {ex.GetType().FullName}\n" +
                                 $"Message: {ex.Message}{innerExDetails}\n" +
                                 $"Stack Trace:\n{ex.StackTrace}";

            if (fullDetails.Length > MaxEntryLength)
            {
                fullDetails = fullDetails.Substring(0, MaxEntryLength) + "\n...[TRUNCATED]";
            }

            WriteToEventLog(fullDetails, EventLogEntryType.Error);
        }

        public static void LogSecurityEvent(
            string actionDetails,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "")
        {
            string className = string.IsNullOrEmpty(sourceFilePath)
                ? "UnknownClass"
                : Path.GetFileNameWithoutExtension(sourceFilePath);

            string fullDetails = $"Security Audit | Location: {className}.{memberName}()\n" +
                                 $"Details: {actionDetails}";

            WriteToEventLog(fullDetails, EventLogEntryType.Warning);
        }

        private static void WriteToEventLog(string entry, EventLogEntryType type)
        {
            try
            {
                EventLog.WriteEntry(SourceName, entry, type);
            }
            catch (ArgumentException)
            {
                try
                {
                    EventLog.CreateEventSource(SourceName, LogName);
                    EventLog.WriteEntry(SourceName, entry, type);
                }
                catch { }
            }
            catch
            {
            }
        }
    }
}