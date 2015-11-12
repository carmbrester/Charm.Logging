#region License

/*
 * Copyright © 2002-2009 the original author or authors.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *      http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

/*
 * Copyright © 2015 Chris Armbrester
 *
 * Tweaked to match new name changes and namespace.
 */

#endregion

using System;
using System.Configuration;
using System.Diagnostics;

namespace Charm.Logging
{
    public class LogManager : ILogManager
    {
        public static string CHARM_LOGGING_SECTION { get { return "charm/logging"; } }
        public static IProvideExecutionIds ExecutionIdProvider { get; private set; }
        private static bool _initialized = false;
        private static ILoggerFactoryAdapter _adapter;
        private static readonly object _loadLock = new object();

        static LogManager()
        {
        }

        public static void Init(IProvideExecutionIds executionIdProvider)
        {
            ExecutionIdProvider = executionIdProvider;
            _initialized = true;
        }

        public static ILoggerFactoryAdapter Adapter
        {
            get
            {
                if (_adapter == null)
                {
                    lock (_loadLock)
                    {
                        if (_adapter == null)
                        {
                            _adapter = BuildLoggerFactoryAdapter();
                        }
                    }
                }
                return _adapter;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Adapter");
                }

                lock (_loadLock)
                {
                    _adapter = value;
                }
            }
        }

        private static ILoggerFactoryAdapter BuildLoggerFactoryAdapter()
        {
            object sectionResult = null;

            try
            {
                sectionResult = ConfigurationManager.GetSection(CHARM_LOGGING_SECTION);
            }
            catch (ConfigurationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException("Failed obtaining configuration for Charm.Logging from configuration section 'charm/logging'.", ex);
            }

            // configuration reader returned <null>
            if (sectionResult == null)
            {
                Trace.WriteLine(string.Format("no configuration section <{0}> found - suppressing logging output", CHARM_LOGGING_SECTION));

                ILoggerFactoryAdapter defaultFactory = new NoOpLoggerFactoryAdapter();
                return defaultFactory;
            }

            // ready to use ILoggerFactoryAdapter?
            if (sectionResult is ILoggerFactoryAdapter)
            {
                return (ILoggerFactoryAdapter)sectionResult;
            }

            // ensure what's left is a LogSetting instance
            ArgUtils.Guard(delegate
            {
                ArgUtils.AssertIsAssignable<LogSetting>("sectionResult", sectionResult.GetType());
            }
                           , "ConfigurationManager returned unknown settings instance of type {0}"
                           , sectionResult.GetType().FullName);

            ILoggerFactoryAdapter adapter = null;
            ArgUtils.Guard(delegate
            {
                adapter = BuildLoggerFactoryAdapterFromLogSettings((LogSetting)sectionResult);
            }
                , "Failed creating LoggerFactoryAdapter from settings");

            return adapter;
        }

        private static ILoggerFactoryAdapter BuildLoggerFactoryAdapterFromLogSettings(LogSetting setting)
        {
            ArgUtils.AssertNotNull("setting", setting);
            // already ensured by LogSetting
            //            AssertArgIsAssignable<ILoggerFactoryAdapter>("setting.FactoryAdapterType", setting.FactoryAdapterType
            //                                , "Specified FactoryAdapter does not implement {0}.  Check implementation of class {1}"
            //                                , typeof(ILoggerFactoryAdapter).FullName
            //                                , setting.FactoryAdapterType.AssemblyQualifiedName);

            ILoggerFactoryAdapter adapter = null;

            ArgUtils.Guard(delegate
            {
                if (setting.Properties != null
                    && setting.Properties.Count > 0)
                {
                    object[] args = { setting.Properties };

                    adapter = (ILoggerFactoryAdapter)Activator.CreateInstance(setting.FactoryAdapterType, args);
                }
                else
                {
                    adapter = (ILoggerFactoryAdapter)Activator.CreateInstance(setting.FactoryAdapterType);
                }
            }
                    , "Unable to create instance of type {0}. Possible explanation is lack of zero arg and single arg Charm.Logging.Configuration.NameValueCollection constructors"
                    , setting.FactoryAdapterType.FullName
            );

            // make sure
            ArgUtils.AssertNotNull("adapter", adapter, "Activator.CreateInstance() returned <null>");
            return adapter;
        }

        public static ILogger GetLogger<T>()
        {
            return Adapter.GetLogger(typeof(T));
        }

        ILogger ILogManager.GetLogger<T>()
        {
            return GetLogger<T>();
        }
        
        public static ILogger GetLogger(Type type)
        {
            return Adapter.GetLogger(type);
        }

        ILogger ILogManager.GetLogger(Type type)
        {
            return GetLogger(type);
        }

        public static ILogger GetLogger(string key)
        {
            return Adapter.GetLogger(key);
        }

        ILogger ILogManager.GetLogger(string key)
        {
            return GetLogger(key);
        }
    }
}
