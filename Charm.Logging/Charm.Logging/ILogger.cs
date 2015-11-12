/*
 * Copyright © 2015 Chris Armbrester.
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

using System;

namespace Charm.Logging
{
    public interface ILogger
    {
        bool IsDebugEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsWarnEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsFatalEnabled { get; }

        void Debug(object message);
        void Debug(object message, string category);
        void Debug(object message, dynamic data);
        void Debug(object message, string category, dynamic data);
        void Debug(object message, Exception exception);
        void Debug(object message, string category, Exception exception);
        void Debug(object message, dynamic data, Exception exception);
        void Debug(object message, string category, dynamic data, Exception exception);

        void Error(object message);
        void Error(object message, string category);
        void Error(object message, dynamic data);
        void Error(object message, string category, dynamic data);
        void Error(object message, Exception exception);
        void Error(object message, string category, Exception exception);
        void Error(object message, dynamic data, Exception exception);
        void Error(object message, string category, dynamic data, Exception exception);

        void Fatal(object message);
        void Fatal(object message, string category);
        void Fatal(object message, dynamic data);
        void Fatal(object message, string category, dynamic data);
        void Fatal(object message, Exception exception);
        void Fatal(object message, string category, Exception exception);
        void Fatal(object message, dynamic data, Exception exception);
        void Fatal(object message, string category, dynamic data, Exception exception);

        void Info(object message);
        void Info(object message, string category);
        void Info(object message, dynamic data);
        void Info(object message, string category, dynamic data);
        void Info(object message, Exception exception);
        void Info(object message, string category, Exception exception);
        void Info(object message, dynamic data, Exception exception);
        void Info(object message, string category, dynamic data, Exception exception);

        void Warn(object message);
        void Warn(object message, string category);
        void Warn(object message, dynamic data);
        void Warn(object message, string category, dynamic data);
        void Warn(object message, Exception exception);
        void Warn(object message, string category, Exception exception);
        void Warn(object message, dynamic data, Exception exception);
        void Warn(object message, string category, dynamic data, Exception exception);

        IVariablesContext GlobalContext { get; }
        IVariablesContext AmbientContext { get; }
    }
}
