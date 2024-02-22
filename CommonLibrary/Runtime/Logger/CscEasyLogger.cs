using System;
using System.Runtime.ExceptionServices;
using System.Text;

namespace ChickStar.CommonLibrary.Runtime.Logger
{
    public static class CscEasyLogger
    {
        private static readonly StringBuilder LogBuilder = new();
        private const string Prefix = "[CSC]";
        private const string ArrayContentBracketStart = "[";
        private const string ArrayContentBracketEnd = "]";

        private static string BuildLog(object message, bool needPrefix = true)
        {
            if (needPrefix)
            {
                LogBuilder.Append(Prefix);
                LogBuilder.Append(" ");                
            }

            if (message is string str)
            {
                LogBuilder.Append(str);
                return ExportString(LogBuilder);
            }

            if (message is Array array)
            {
                var arrayLogBuilder = new StringBuilder();
                var count = 0;
                
                foreach (var content in array)
                {
                    count++;
                    arrayLogBuilder.Append(ArrayContentBracketStart);
                    arrayLogBuilder.Append(BuildLog(content, needPrefix: false));
                    arrayLogBuilder.Append(ArrayContentBracketEnd);
                    
                    if (count % 10 == 0)
                    {
                        arrayLogBuilder.Append(Environment.NewLine);
                    }
                }

                LogBuilder.AppendLine(ExportString(arrayLogBuilder));
                return ExportString(LogBuilder);
            }
            
            string ExportString(StringBuilder builder)
            {
                var result = builder.ToString();
                builder.Clear();
                return result;
            }

            LogBuilder.Append(message);
            return ExportString(LogBuilder);
        }
        
        

        public static void Log(object message)
        {
            UnityEngine.Debug.Log(BuildLog(message));
        }

        public static void LogWarning(object message)
        {
            UnityEngine.Debug.LogWarning(BuildLog(message));
        }

        public static void LogError(object message)
        {
            if (message is Exception exception)
            {
                LogException(exception);
                return;
            }
            
            UnityEngine.Debug.LogError(BuildLog(message));
        }

        public static void LogException(Exception exception)
        {
            ExceptionDispatchInfo.Capture(exception).Throw();            
        }
    }
}