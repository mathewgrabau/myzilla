using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.ExtraInformation;

namespace Tremend.Logging
{
    public class MyLogger
    {
        private static Dictionary<string,object> BuildDebugInfo(string methodName){
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("Method", methodName);

            return dictionary;
        }

        private static Dictionary<string, object> BuildDebugInfoWithStack(string methodName)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("Method", methodName);
            DebugInformationProvider informationHelper = new DebugInformationProvider();
            informationHelper.PopulateDictionary(dictionary);

            return dictionary;
        }

        public static void WriteWithStack(string message, string methodName, string category){

            Logger.Write(message, category, BuildDebugInfoWithStack(methodName));
        }

        public static void Write(Exception exception, string methodName, string category)
        {
            Logger.Write(exception.Message, category, BuildDebugInfoWithStack(methodName));
        }

        public static void Write(string message, string methodName, string category)
        {
            Logger.Write(message, category, BuildDebugInfo(methodName));
        }



        public struct LoggingCategory
        {
            public const string General = "General";
            public const string Exception = "Exception";
            public const string Warning = "Warning";
            public const string Debug = "Debug";
        }
    }
}
