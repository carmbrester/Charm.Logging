using System;
using log4net.Core;
using log4net.Layout;

namespace Charm.Logging.Mongo.Layouts
{
    public class MessagePackExecutionIdLayout : IRawLayout
    {
        public object Format(LoggingEvent loggingEvent)
        {
            try
            {
                var messagePack = loggingEvent.MessageObject as MessagePack;
                if (messagePack == null)
                {
                    return null;
                }

                return messagePack.ExecutionId;
            }
            catch (Exception ex)
            {
                return string.Format("Message Pack Layout Error: ExecutionId: {0}", ex);
            }
        }
    }
}
