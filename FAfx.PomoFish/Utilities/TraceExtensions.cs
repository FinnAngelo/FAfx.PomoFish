using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FAfx.Utilities
{
    [DebuggerStepThrough()]
    public static class TraceSourceExtensions
    {
        /// <summary>
        /// Logs all exceptions and starts/ends all TraceSource activities
        /// </summary>
        /// <param name="traceSource"></param>
        /// <param name="parameters"></param>
        /// <param name="action"></param>
        [DebuggerHidden()]
        [DebuggerStepThrough()]
        public static void LogEvents(this TraceSource traceSource, object[] parameters, Action action)
        {
            var stackFrame = new StackFrame(skipFrames: 1);
            LogEvents<object>(traceSource, stackFrame, parameters, action);
        }

        /// <summary>
        /// Logs all exceptions and starts/ends all TraceSource activities
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="traceSource"></param>
        /// <param name="parameters"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        [DebuggerHidden()]
        [DebuggerStepThrough()]
        public static TResult LogEvents<TResult>(this TraceSource traceSource, object[] parameters, Func<TResult> function)
        {
            var stackFrame = new StackFrame(skipFrames: 1);
            return LogEvents<TResult>(traceSource, stackFrame, parameters, function);
        }

        [DebuggerHidden()]
        [DebuggerStepThrough()]
        private static TResult LogEvents<TResult>(TraceSource traceSource, StackFrame stackFrame, object[] parameters, Delegate funcOrAction)
        {
            TResult result = default(TResult);

            var methodSignature = GetMethodSignature(stackFrame.GetMethod());

            var parameterList = new List<object> { methodSignature };
            parameterList.AddRange(parameters.Select(x => x.ToLogString()).ToArray());
            
            #region Start Activity

            Guid oldActivityId = Trace.CorrelationManager.ActivityId;
            Guid newActivityId = Guid.NewGuid();

            if (oldActivityId != Guid.Empty)
            {
                traceSource.TraceTransfer(0, "Transferring to new activity...", newActivityId);
            }
            Trace.CorrelationManager.ActivityId = newActivityId;
            traceSource.TraceEvent(TraceEventType.Start, 0, methodSignature);

            #endregion

            try
            {

                traceSource.TraceData(TraceEventType.Verbose, 0, parameterList.ToArray());
                result = (TResult)funcOrAction.DynamicInvoke();
            }
            catch (Exception Ex)
            {
                traceSource.TraceEvent(TraceEventType.Error, 0, "{0} > {1} ", methodSignature, Ex.Message);
                parameterList.Add(Ex);
                parameterList.Add(string.Format("Ex: {0}", Ex.ToLogString()));
                traceSource.TraceData(TraceEventType.Error, 0, parameterList.ToArray());
                throw;
            }
            finally
            {
                #region Stop Activity

                if (oldActivityId != Guid.Empty)
                {
                    traceSource.TraceTransfer(0, "Transferring back to old activity...", oldActivityId);
                }
                traceSource.TraceEvent(TraceEventType.Stop, 0, methodSignature);
                Trace.CorrelationManager.ActivityId = oldActivityId;

                #endregion
            }

            return result;
        }
 
        private static readonly Dictionary<MethodBase, string> _methodSignatures = new Dictionary<MethodBase, string>();
        
        [DebuggerHidden()]
        [DebuggerStepThrough()]
        private static string GetMethodSignature(MethodBase methodBase)
        {
            if (_methodSignatures.Keys.Contains(methodBase))
            {
                return _methodSignatures[methodBase];
            }

            var methodType = methodBase.ReflectedType.FullName;
            var methodName = methodBase.Name;
            var methodParameters = String.Join(", ",
                methodBase.GetParameters().Select(
                    p => String.Format("{0} {1}", p.Name, p.ParameterType.Name)
                    ).ToArray());

            var result = string.Format("{0}.{1} ({2})", methodType, methodName, methodParameters);
            _methodSignatures.Add(methodBase, result);

            return result;
        }

        #region ToLogString: Formatting types for Logging

        [DebuggerHidden()]
        [DebuggerStepThrough()]
        internal static string ToLogString<TSource>(this TSource obj)
        {
            if (obj == null) return "null";
            ////--------------------------------------------
            ////Serialize ISerializable
            //if (Attribute.IsDefined(typeof(TSource), typeof(SerializableAttribute)))
            //{
            //    var serialiser = new XmlSerializer(typeof(TSource));
            //    using (var writer = new StringWriter())
            //    {
            //        serialiser.Serialize(writer, obj);
            //        return writer.ToString();
            //    }
            //}
            //else
            //{
            return String.Format("{0}", obj);
            //}
        }



        [DebuggerHidden()]
        [DebuggerStepThrough()]
        internal static string ToLogString(this Exception obj)
        {
            if (obj == null) return "null";
            StringBuilder sb = new StringBuilder();

            const string format = @"
-----------------------------
ToString(): {0}
Message: {1}
Source: {2}
Target site: {3}
Stack trace: {4}
";

            while (obj != null)
            {
                sb.AppendFormat(
                    format,
                    obj.ToString(),
                    obj.Message,
                    obj.Source,
                    obj.TargetSite,
                    obj.StackTrace
                    );

                // Walk the InnerException tree
                obj = obj.InnerException;
            }

            return sb.ToString();
        }

        #endregion

    }

}


