using System;
using log4net;

namespace Charm.Logging.Log4net
{
    public class LoggerAdapter : ILogger
    {
        private readonly ILog _log;
        private readonly IProvideExecutionIds _executionIdProvider;
        private readonly bool _circumventLogicalThreadContext;

        internal LoggerAdapter(ILog log)
            : this(log, null, false)
        {
            _log = log;
        }

        internal LoggerAdapter(ILog log, IProvideExecutionIds executionIdProvider, bool circumventLogicalThreadContext)
        {
            _log = log;
            _executionIdProvider = executionIdProvider;
            _circumventLogicalThreadContext = circumventLogicalThreadContext;
        }

        internal log4net.Core.ILogger Logger
        {
            get { return _log.Logger; }
        }

        public bool IsDebugEnabled
        {
            get { return _log.IsDebugEnabled; }
        }

        public bool IsInfoEnabled
        {
            get { return _log.IsInfoEnabled; }
        }

        public bool IsWarnEnabled
        {
            get { return _log.IsWarnEnabled; }
        }

        public bool IsErrorEnabled
        {
            get { return _log.IsErrorEnabled; }
        }

        public bool IsFatalEnabled
        {
            get { return _log.IsFatalEnabled; }
        }

        public void Debug(object message)
        {
            var messagePack = CreateMessagePack(message);
            _log.Debug(messagePack);
        }

        public void Debug(object message, string category)
        {
            var messagePack = CreateMessagePack(message, category);
            _log.Debug(messagePack);
        }

        public void Debug(object message, dynamic data)
        {
            var messagePack = CreateMessagePack(message, data: data);
            _log.Debug(messagePack);
        }

        public void Debug(object message, string category, dynamic data)
        {
            var messagePack = CreateMessagePack(message, category, data);
            _log.Debug(messagePack);
        }

        public void Debug(object message, Exception exception)
        {
            var messagePack = CreateMessagePack(message);
            _log.Debug(messagePack, exception);
        }

        public void Debug(object message, string category, Exception exception)
        {
            var messagePack = CreateMessagePack(message, category);
            _log.Debug(messagePack, exception);
        }

        public void Debug(object message, dynamic data, Exception exception)
        {
            var messagePack = CreateMessagePack(message, data: data);
            _log.Debug(messagePack, exception);
        }

        public void Debug(object message, string category, dynamic data, Exception exception)
        {
            var messagePack = CreateMessagePack(message, category, data);
            _log.Debug(messagePack, exception);
        }

        public void Error(object message)
        {
            var messagePack = CreateMessagePack(message);
            _log.Error(messagePack);
        }

        public void Error(object message, string category)
        {
            var messagePack = CreateMessagePack(message, category);
            _log.Error(messagePack);
        }

        public void Error(object message, dynamic data)
        {
            var messagePack = CreateMessagePack(message, data: data);
            _log.Error(messagePack);
        }

        public void Error(object message, string category, dynamic data)
        {
            var messagePack = CreateMessagePack(message, category, data);
            _log.Error(messagePack);
        }

        public void Error(object message, Exception exception)
        {
            var messagePack = CreateMessagePack(message);
            _log.Error(messagePack, exception);
        }

        public void Error(object message, string category, Exception exception)
        {
            var messagePack = CreateMessagePack(message, category);
            _log.Error(messagePack, exception);
        }

        public void Error(object message, dynamic data, Exception exception)
        {
            var messagePack = CreateMessagePack(message, data: data);
            _log.Error(messagePack, exception);
        }

        public void Error(object message, string category, dynamic data, Exception exception)
        {
            var messagePack = CreateMessagePack(message, category, data);
            _log.Error(messagePack, exception);
        }

        public void Fatal(object message)
        {
            var messagePack = CreateMessagePack(message);
            _log.Fatal(messagePack);
        }

        public void Fatal(object message, string category)
        {
            var messagePack = CreateMessagePack(message, category);
            _log.Fatal(messagePack);
        }

        public void Fatal(object message, dynamic data)
        {
            var messagePack = CreateMessagePack(message, data: data);
            _log.Fatal(messagePack);
        }

        public void Fatal(object message, string category, dynamic data)
        {
            var messagePack = CreateMessagePack(message, category, data);
            _log.Fatal(messagePack);
        }

        public void Fatal(object message, Exception exception)
        {
            var messagePack = CreateMessagePack(message);
            _log.Fatal(messagePack, exception);
        }

        public void Fatal(object message, string category, Exception exception)
        {
            var messagePack = CreateMessagePack(message, category);
            _log.Fatal(messagePack, exception);
        }

        public void Fatal(object message, dynamic data, Exception exception)
        {
            var messagePack = CreateMessagePack(message, data: data);
            _log.Fatal(messagePack, exception);
        }

        public void Fatal(object message, string category, dynamic data, Exception exception)
        {
            var messagePack = CreateMessagePack(message, category, data);
            _log.Fatal(messagePack, exception);
        }

        public void Info(object message)
        {
            var messagePack = CreateMessagePack(message);
            _log.Info(messagePack);
        }

        public void Info(object message, string category)
        {
            var messagePack = CreateMessagePack(message, category);
            _log.Info(messagePack);
        }

        public void Info(object message, dynamic data)
        {
            var messagePack = CreateMessagePack(message, data: data);
            _log.Info(messagePack);
        }

        public void Info(object message, string category, dynamic data)
        {
            var messagePack = CreateMessagePack(message, category, data);
            _log.Info(messagePack);
        }

        public void Info(object message, Exception exception)
        {
            var messagePack = CreateMessagePack(message);
            _log.Info(messagePack, exception);
        }

        public void Info(object message, string category, Exception exception)
        {
            var messagePack = CreateMessagePack(message, category);
            _log.Info(messagePack, exception);
        }

        public void Info(object message, dynamic data, Exception exception)
        {
            var messagePack = CreateMessagePack(message, data: data);
            _log.Info(messagePack, exception);
        }

        public void Info(object message, string category, dynamic data, Exception exception)
        {
            var messagePack = CreateMessagePack(message, category, data);
            _log.Info(messagePack, exception);
        }

        public void Warn(object message)
        {
            var messagePack = CreateMessagePack(message);
            _log.Warn(messagePack);
        }

        public void Warn(object message, string category)
        {
            var messagePack = CreateMessagePack(message, category);
            _log.Warn(messagePack);
        }

        public void Warn(object message, dynamic data)
        {
            var messagePack = CreateMessagePack(message, data: data);
            _log.Warn(messagePack);
        }

        public void Warn(object message, string category, dynamic data)
        {
            var messagePack = CreateMessagePack(message, category, data);
            _log.Warn(messagePack);
        }

        public void Warn(object message, Exception exception)
        {
            var messagePack = CreateMessagePack(message);
            _log.Warn(messagePack, exception);
        }

        public void Warn(object message, string category, Exception exception)
        {
            var messagePack = CreateMessagePack(message, category);
            _log.Warn(messagePack, exception);
        }

        public void Warn(object message, dynamic data, Exception exception)
        {
            var messagePack = CreateMessagePack(message, data: data);
            _log.Warn(messagePack, exception);
        }

        public void Warn(object message, string category, dynamic data, Exception exception)
        {
            var messagePack = CreateMessagePack(message, category, data);
            _log.Warn(messagePack, exception);
        }

        private MessagePack CreateMessagePack(object message, string category = null,
            dynamic data = null)
        {
            string executionId = null;
            if (_executionIdProvider != null)
            {
                executionId = _executionIdProvider.GetExecutionId;
            }
            var messagePack = new MessagePack(message, executionId, category, data);
            return messagePack;
        }

        /// <summary>
        /// Returns the global context for variables
        /// </summary>
        public IVariablesContext GlobalContext
        {
            get { return new Log4NetGlobalVariablesContext(); }
        }

        public IVariablesContext AmbientContext
        {
            get
            {
                if (_circumventLogicalThreadContext)
                {
                    return ThreadContext;
                }

                return LogicalThreadContext;
            }
        }

        /// <summary>
        /// Returns the thread-specific context for variables
        /// </summary>
        protected IVariablesContext ThreadContext
        {
            get { return new Log4NetThreadVariablesContext(); }
        }

        /// <summary>
        /// Returns the logical thread-specific context for variables
        /// </summary>
        protected IVariablesContext LogicalThreadContext
        {
            get { return new Log4NetLogicalThreadVariablesContext(); }
        }
    }
}
