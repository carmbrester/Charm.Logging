﻿#region License

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
 * Removed NET20 constructor.
 */

#endregion

using System;
using System.Collections.Specialized;

namespace Charm.Logging
{
    /// <summary>
    /// Factory for creating <see cref="ILogger" /> instances that silently ignores
    /// logging requests.
    /// </summary>
    /// <remarks>
    /// This logger adapter is the default used by Common.Logging if unconfigured. Using this logger adapter is the most efficient
    /// way to suppress any logging output.
    /// <example>
    /// Below is an example how to configure this adapter:
    /// <code>
    /// &lt;configuration&gt;
    /// 
    ///   &lt;configSections&gt;
    ///     &lt;sectionGroup key=&quot;common&quot;&gt;
    ///       &lt;section key=&quot;logging&quot;
    ///                type=&quot;Common.Logging.ConfigurationSectionHandler, Common.Logging&quot;
    ///                requirePermission=&quot;false&quot; /&gt;
    ///     &lt;/sectionGroup&gt;
    ///   &lt;/configSections&gt;
    /// 
    ///   &lt;common&gt;
    ///     &lt;logging&gt;
    ///       &lt;factoryAdapter type=&quot;Common.Logging.Simple.NoOpLoggerFactoryAdapter, Common.Logging&quot;&gt;
    ///         &lt;arg key=&quot;level&quot; value=&quot;ALL&quot; /&gt;
    ///       &lt;/factoryAdapter&gt;
    ///     &lt;/logging&gt;
    ///   &lt;/common&gt;
    /// 
    /// &lt;/configuration&gt;
    /// </code>
    /// </example>
    /// </remarks>
    /// <author>Gilles Bayon</author>
    internal class NoOpLoggerFactoryAdapter : ILoggerFactoryAdapter
    {
        private static readonly ILogger s_nopLogger = new NoOpLogger();

        /// <summary>
        /// Constructor
        /// </summary>
        public NoOpLoggerFactoryAdapter()
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        public NoOpLoggerFactoryAdapter(NameValueCollection properties)
        { }

        #region ILoggerFactoryAdapter Members

        /// <summary>
        /// Get a ILog instance by type 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ILogger GetLogger(Type type)
        {
            return s_nopLogger;
        }

        /// <summary>
        /// Get a ILog instance by type key 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        ILogger ILoggerFactoryAdapter.GetLogger(string key)
        {
            return s_nopLogger;

        }

        #endregion
    }
}
