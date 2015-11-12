using System;

namespace Charm.Logging
{
    internal class NoOpLogger : ILogger
    {
        public bool IsDebugEnabled
        {
            get { return false; }
        }

        public bool IsInfoEnabled
        {
            get { return false; }
        }

        public bool IsWarnEnabled
        {
            get { return false; }
        }

        public bool IsErrorEnabled
        {
            get { return false; }
        }

        public bool IsFatalEnabled
        {
            get { return false; }
        }

        public void Debug(object message)
        {
            // do nothing.
        }

        public void Debug(object message, string category)
        {
            // do nothing.
        }

        public void Debug(object message, dynamic data)
        {
            // do nothing.
        }

        public void Debug(object message, string category, dynamic data)
        {
            // do nothing.
        }

        public void Debug(object message, Exception exception)
        {
            // do nothing.
        }

        public void Debug(object message, string category, Exception exception)
        {
            // do nothing.
        }

        public void Debug(object message, dynamic data, Exception exception)
        {
            // do nothing.
        }

        public void Debug(object message, string category, dynamic data, Exception exception)
        {
            // do nothing.
        }

        public void Error(object message)
        {
            // do nothing.
        }

        public void Error(object message, string category)
        {
            // do nothing.
        }

        public void Error(object message, dynamic data)
        {
            // do nothing.
        }

        public void Error(object message, string category, dynamic data)
        {
            // do nothing.
        }

        public void Error(object message, Exception exception)
        {
            // do nothing.
        }

        public void Error(object message, string category, Exception exception)
        {
            // do nothing.
        }

        public void Error(object message, dynamic data, Exception exception)
        {
            // do nothing.
        }

        public void Error(object message, string category, dynamic data, Exception exception)
        {
            // do nothing.
        }

        public void Fatal(object message)
        {
            // do nothing.
        }

        public void Fatal(object message, string category)
        {
            // do nothing.
        }

        public void Fatal(object message, dynamic data)
        {
            // do nothing.
        }

        public void Fatal(object message, string category, dynamic data)
        {
            // do nothing.
        }

        public void Fatal(object message, Exception exception)
        {
            // do nothing.
        }

        public void Fatal(object message, string category, Exception exception)
        {
            // do nothing.
        }

        public void Fatal(object message, dynamic data, Exception exception)
        {
            // do nothing.
        }

        public void Fatal(object message, string category, dynamic data, Exception exception)
        {
            // do nothing.
        }

        public void Info(object message)
        {
            // do nothing.
        }

        public void Info(object message, string category)
        {
            // do nothing.
        }

        public void Info(object message, dynamic data)
        {
            // do nothing.
        }

        public void Info(object message, string category, dynamic data)
        {
            // do nothing.
        }

        public void Info(object message, Exception exception)
        {
            // do nothing.
        }

        public void Info(object message, string category, Exception exception)
        {
            // do nothing.
        }

        public void Info(object message, dynamic data, Exception exception)
        {
            // do nothing.
        }

        public void Info(object message, string category, dynamic data, Exception exception)
        {
            // do nothing.
        }

        public void Warn(object message)
        {
            // do nothing.
        }

        public void Warn(object message, string category)
        {
            // do nothing.
        }

        public void Warn(object message, dynamic data)
        {
            // do nothing.
        }

        public void Warn(object message, string category, dynamic data)
        {
            // do nothing.
        }

        public void Warn(object message, Exception exception)
        {
            // do nothing.
        }

        public void Warn(object message, string category, Exception exception)
        {
            // do nothing.
        }

        public void Warn(object message, dynamic data, Exception exception)
        {
            // do nothing.
        }

        public void Warn(object message, string category, dynamic data, Exception exception)
        {
            // do nothing.
        }

        public IVariablesContext GlobalContext
        {
            get { return new NoOpVariablesContext(); }
        }

        public IVariablesContext AmbientContext
        {
            get { return new NoOpVariablesContext(); }
        }
    }
}
