﻿using log4net;

namespace Charm.Logging.Log4net
{
    /// <summary>
    /// A logical thread context for logger variables
    /// </summary>
    public class Log4NetLogicalThreadVariablesContext : IVariablesContext
    {
        /// <summary>
        /// Sets the value of a new or existing variable within the global context
        /// </summary>
        /// <param name="key">The key of the variable that is to be added</param>
        /// <param name="value">The value to add</param>
        public void Set(string key, object value)
        {
            LogicalThreadContext.Properties[key] = value;
        }

        /// <summary>
        /// Gets the value of a variable within the global context
        /// </summary>
        /// <param name="key">The key of the variable to get</param>
        /// <returns>The value or null if not found</returns>
        public object Get(string key)
        {
            return LogicalThreadContext.Properties[key];
        }

        /// <summary>
        /// Checks if a variable is set within the global context
        /// </summary>
        /// <param name="key">The key of the variable to check for</param>
        /// <returns>True if the variable is set</returns>
        public bool Contains(string key)
        {
            return LogicalThreadContext.Properties[key] != null;
        }

        /// <summary>
        /// Removes a variable from the global context by key
        /// </summary>
        /// <param name="key">The key of the variable to remove</param>
        public void Remove(string key)
        {
            LogicalThreadContext.Properties.Remove(key);
        }

        /// <summary>
        /// Clears the global context variables
        /// </summary>
        public void Clear()
        {
            LogicalThreadContext.Properties.Clear();
        }

        public object this[string key]
        {
            get { return Get(key); }
            set { Set(key, value); }
        }
    }
}