using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using ZATCAMAUI;

namespace ZATCAMAUI
{
    public class AgentConfiguration : IAgentConfiguration
    {
        public string AppKey { get; set; }

        public string ApplicationName { get; set; }

        public string CollectorURL { get; set; }

        public string ScreenshotURL { get; set; }

        public LoggingLevel LoggingLevel { get; set; }

        public string ReachabilityHostname { get; set; }

        public ISet<string> ExcludedURLPatterns { get; set; }

        public bool ScreenshotsEnabled { get; set; }

        public bool EnableAggregateExceptionReporting { get; set; }

        private AgentConfiguration(string appKey)
        {
            throw new Exception("No Valid Assembly");
        }

        public static IAgentConfiguration Create(string appKey)
        {
            return new AgentConfiguration(appKey);
        }
    }
    public interface IAgentConfiguration
    {
        string AppKey { get; set; }

        string CollectorURL { get; set; }

        string ScreenshotURL { get; set; }

        string ReachabilityHostname { get; set; }

        LoggingLevel LoggingLevel { get; set; }

        string ApplicationName { get; set; }

        ISet<string> ExcludedURLPatterns { get; set; }

        bool ScreenshotsEnabled { get; }

        bool EnableAggregateExceptionReporting { get; set; }
    }
    public enum LoggingLevel
    {
        Off,
        Error,
        Warn,
        Info,
        Debug,
        Verbose,
        All
    }

    public class Instrumentation : IInstrumentation
    {
        internal static AppInfo appInfo;

        public static bool enableAggregateExceptionReporting;

        private Instrumentation()
        {
        }

        public static void InitWithConfiguration(IAgentConfiguration iConfig)
        {
            Assembly asm = null;
            appInfo = new AppInfo(asm);
            enableAggregateExceptionReporting = iConfig.EnableAggregateExceptionReporting;
            string hybridAgentType = "Xamarin";
            string agentVersion = appInfo.AgentVersion;
            AppDynamics.Agent.Other.Instrumentation.InitWithConfiguration(iConfig, hybridAgentType, agentVersion);
        }

        public static void ChangeAppKey(string appKey)
        {
            AgentUtilities.ValidateAppKey(appKey);
            ChangeAppKey(appKey);
        }

        public static ICallTracker BeginCall(string className, string methodName, params object[] arguments)
        {
            return BeginCall(className, methodName, arguments);
        }

        public static void EndCall(ICallTracker call, object returnValue = null)
        {
            AppDynamics.Agent.Other.Instrumentation.EndCall(call, returnValue);
        }

        public static void EndCall(ICallTracker call, Exception e)
        {
            AppDynamics.Agent.Other.Instrumentation.EndCall(call, e);
        }

        public static void StartTimerWithName(string name)
        {
            AppDynamics.Agent.Other.Instrumentation.StartTimerWithName(name);
        }

        public static void StopTimerWithName(string name)
        {
            AppDynamics.Agent.Other.Instrumentation.StopTimerWithName(name);
        }

        public static void ReportMetricWithName(string name, long value)
        {
            AppDynamics.Agent.Other.Instrumentation.ReportMetricWithName(name, value);
        }

        public static void LeaveBreadcrumb(string breadcrumb, BreadcrumbVisibility mode = BreadcrumbVisibility.CrashesOnly)
        {
            AppDynamics.Agent.Other.Instrumentation.LeaveBreadcrumb(breadcrumb, mode);
        }

        public static void SetUserData(string key, string value)
        {
            AppDynamics.Agent.Other.Instrumentation.SetUserData(key, value);
        }

        public static void RemoveUserData(string key)
        {
            AppDynamics.Agent.Other.Instrumentation.RemoveUserData(key);
        }

        public static void SetUserDataLong(string key, long value)
        {
            AppDynamics.Agent.Other.Instrumentation.SetUserDataLong(key, value);
        }

        public static void RemoveUserDataLong(string key)
        {
            AppDynamics.Agent.Other.Instrumentation.RemoveUserDataLong(key);
        }

        public static void SetUserDataBoolean(string key, bool value)
        {
            AppDynamics.Agent.Other.Instrumentation.SetUserDataBoolean(key, value);
        }

        public static void RemoveUserDataBoolean(string key)
        {
            AppDynamics.Agent.Other.Instrumentation.RemoveUserDataBoolean(key);
        }

        public static void SetUserDataDouble(string key, double value)
        {
            AppDynamics.Agent.Other.Instrumentation.SetUserDataDouble(key, value);
        }

        public static void RemoveUserDataDouble(string key)
        {
            AppDynamics.Agent.Other.Instrumentation.RemoveUserDataDouble(key);
        }

        public static void SetUserDataDate(string key, DateTime value)
        {
            AppDynamics.Agent.Other.Instrumentation.SetUserDataDate(key, value);
        }

        public static void RemoveUserDataDate(string key)
        {
            AppDynamics.Agent.Other.Instrumentation.RemoveUserDataDate(key);
        }

        public static void ReportError(Exception exception, ErrorSeverityLevel severity)
        {
            if (exception != null)
            {
                AppDynamics.Agent.Other.Instrumentation.ReportError(exception, (int)severity);
            }
        }

        public static void StartNextSession()
        {
            StartNextSession();
        }

        public static ISessionFrame StartSessionFrame(string sessionFrameName)
        {
            return StartSessionFrame(sessionFrameName);
        }
    }
    public enum BreadcrumbVisibility
    {
        CrashesOnly,
        CrashesAndSessions
    }
    public enum ErrorSeverityLevel
    {
        INFO,
        WARNING,
        CRITICAL
    }
    public interface ISessionFrame
    {
        string Name { get; set; }

        void End();
    }
    public interface IInstrumentation
    {
    }

    internal class AgentUtilities
    {
        private AgentUtilities()
        {
        }

        public static void ValidateAppKey(string appKey)
        {
            if (string.IsNullOrEmpty(appKey))
            {
                throw new Exception("AppKey cannot be null or empty.");
            }

            Regex regex = new Regex("^([a-zA-Z0-9]){1,}(-[A-Z]{3}){2,}$");
            if (!regex.IsMatch(appKey))
            {
                throw new Exception($"Application key malformed: {appKey}, it should look like: AD-AAA-BBB.");
            }
        }

        public static void ValidateAppName(string applicationName)
        {
            if (string.IsNullOrEmpty(applicationName))
            {
                throw new Exception("Application name cannot be the empty string.");
            }
        }

        public static void ValidateAndroidAppName(string applicationName)
        {
            string pattern = "^[a-zA-Z]{1}[a-zA-Z0-9_]{0,}$";
            string errorMsg = "Application name is a full Java-language-style package name. It must contain only alphanumeric characters, underscores and periods in reverse-DNS format. Individual package name parts may only start with letters.";
            ValidateAppNameHelper(applicationName, pattern, errorMsg);
        }

        public static void ValidateIosAppName(string applicationName)
        {
            string pattern = "^[a-zA-Z0-9-]+$";
            string errorMsg = "Application name must contain only alphanumeric characters, hyphens and periods in reverse-DNS format.";
            ValidateAppNameHelper(applicationName, pattern, errorMsg);
        }

        private static void ValidateAppNameHelper(string applicationName, string pattern, string errorMsg)
        {
            string[] array = applicationName.Split('.');
            if (array.Length < 2)
            {
                throw new Exception($"Application name malformed: {applicationName}. {errorMsg}");
            }

            Regex regex = new Regex(pattern);
            string[] array2 = array;
            foreach (string input in array2)
            {
                if (!regex.IsMatch(input))
                {
                    throw new Exception($"Application name malformed: {applicationName}. {errorMsg}");
                }
            }
        }

        public static string EscapeSpecialCharacter(string str)
        {
            if (str == null)
            {
                return null;
            }

            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in str)
            {
                switch (c)
                {
                    case '\\':
                        stringBuilder.Append("\\\\");
                        break;
                    case '"':
                        stringBuilder.Append("\\\"");
                        break;
                    default:
                        stringBuilder.Append(c);
                        break;
                    case '\b':
                    case '\t':
                    case '\n':
                    case '\f':
                    case '\r':
                        break;
                }
            }

            return stringBuilder.ToString();
        }
    }
    public interface ICallTracker
    {
        void ReportCallEnded();

        void ReportCallEndedWithException(Exception e);

        void ReportCallEndedWithReturnValue(object ret);

        void WithArguments(params object[] args);
    }

    internal class AppInfo
    {
        public string GUID;

        public string CompiledPath;

        public string DevicePath;

        public string FullName;

        public string PDBPath;

        public string AgentVersion = "Unknown";

        private AppInfo()
        {
        }

        private void LogIt(char severity, string message)
        {
        }

        private static T GetAssemblyAttributeFirst<T>(Assembly asm) where T : Attribute
        {
            return null;
        }

        private void GetDebugInfoFromDevicePath()
        {
            _ = DevicePath;
        }

        private string getDebugInfoFromAssemblyInfo(Assembly asm)
        {
            return ((object)null)?.ToString().ToLower();
        }

        private void GetPathInfoFromAssemblyInfo(Assembly asm)
        {
        }

        private void GetVersionNumberFromAssemblyInfo(Assembly asm)
        {
            Version version = null;
            if (version != null)
            {
                AgentVersion = $"{version.Major}.{version.Minor}.{version.Build}";
                LogIt('I', "Agent Version: " + AgentVersion);
            }
            else
            {
                LogIt('W', "No Agent Version found");
            }
        }

        public AppInfo(Assembly asm)
        {
            if (asm == null)
            {
                return;
            }

            GetPathInfoFromAssemblyInfo(asm);
            GetDebugInfoFromDevicePath();
            GetVersionNumberFromAssemblyInfo(asm);
            if (GUID != null)
            {
                LogIt('I', "PDB GUID from file: " + GUID);
                return;
            }

            GUID = getDebugInfoFromAssemblyInfo(asm);
            if (GUID != null)
            {
                LogIt('I', "PDB GUID from attribute: " + GUID);
            }
            else
            {
                LogIt('W', "No PDB GUID found");
            }
        }

        public string ToJson()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (GUID != null)
            {
                stringBuilder.Append(JsonUtilities.GetJsonKeyValue("guid", GUID)).Append(",");
            }

            if (CompiledPath != null)
            {
                stringBuilder.Append(JsonUtilities.GetJsonKeyValue("compiledPath", CompiledPath)).Append(",");
            }

            if (DevicePath != null)
            {
                stringBuilder.Append(JsonUtilities.GetJsonKeyValue("devicePath", DevicePath)).Append(",");
            }

            if (FullName != null)
            {
                stringBuilder.Append(JsonUtilities.GetJsonKeyValue("fullName", FullName)).Append(",");
            }

            if (PDBPath != null)
            {
                stringBuilder.Append(JsonUtilities.GetJsonKeyValue("pdbName", PDBPath)).Append(",");
            }

            return stringBuilder.ToString();
        }
    }
    internal class JsonUtilities
    {
        internal static int maxCharactersCount = 2048;

        internal static string GetJsonKeyValue(string key, string value, bool quoteOnValue = true)
        {
            if (value == null)
            {
                value = "";
            }

            if (quoteOnValue)
            {
                if (value.Length > maxCharactersCount)
                {
                    value = value.Substring(0, maxCharactersCount - 3) + "...";
                }

                value = AgentUtilities.EscapeSpecialCharacter(value);
            }

            if (quoteOnValue)
            {
                return $"\"{key}\":\"{value}\"";
            }

            return $"\"{key}\":{value}";
        }

        internal static string GetJsonKeyValue(string key, long value)
        {
            return $"\"{key}\":{value}";
        }

        internal static string GetJsonKeyValue(string key, bool value)
        {
            return $"\"{key}\":{value.ToString().ToLower()}";
        }
    }
}
namespace AppDynamics.Agent.Other
{
    public class Instrumentation : IInstrumentation
    {
        public static bool enableExceptionReporting;

        private Instrumentation()
        {
        }

        public static void InitWithConfiguration(IAgentConfiguration iConfig)
        {
            throw new NotImplementedException("This functionality is only supported on Android and iOS.");
        }

        public static void InitWithConfiguration(IAgentConfiguration iConfig, string hybridAgentType, string hybridAgentVersion)
        {
            throw new NotImplementedException("This functionality is only supported on Android and iOS.");
        }

        public static void ChangeAppKey(string appKey)
        {
        }

        public static ICallTracker BeginCall(string className, string methodName, params object[] arguments)
        {
            throw new NotImplementedException("This functionality is only supported on Android and iOS.");
        }

        public static void EndCall(ICallTracker call, object returnValue = null)
        {
        }

        public static void EndCall(ICallTracker call, Exception e)
        {
        }

        public static void StartTimerWithName(string name)
        {
        }

        public static void StopTimerWithName(string name)
        {
        }

        public static void ReportMetricWithName(string name, long value)
        {
        }

        public static void LeaveBreadcrumb(string breadcrumb, BreadcrumbVisibility mode = BreadcrumbVisibility.CrashesOnly)
        {
        }

        public static void SetUserData(string key, string value)
        {
        }

        public static void RemoveUserData(string key)
        {
        }

        public static void SetUserDataLong(string key, long value)
        {
        }

        public static void RemoveUserDataLong(string key)
        {
        }

        public static void SetUserDataBoolean(string key, bool value)
        {
        }

        public static void RemoveUserDataBoolean(string key)
        {
        }

        public static void SetUserDataDouble(string key, double value)
        {
        }

        public static void RemoveUserDataDouble(string key)
        {
        }

        public static void SetUserDataDate(string key, DateTime value)
        {
        }

        public static void RemoveUserDataDate(string key)
        {
        }

        public static void ReportError(Exception exception, int severity)
        {
        }

        public static void StartNextSession()
        {
        }

        public static ISessionFrame StartSessionFrame(string sessionFrameName)
        {
            throw new NotImplementedException("This functionality is only supported on Android and iOS.");
        }
    }
}