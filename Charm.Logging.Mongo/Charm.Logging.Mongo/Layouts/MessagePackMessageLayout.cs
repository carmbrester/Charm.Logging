using System;
using log4net.Core;
using log4net.Layout;

namespace Charm.Logging.Mongo.Layouts
{
    public class MessagePackMessageLayout : IRawLayout
    {
        public object Format(LoggingEvent loggingEvent)
        {
            try
            {
                var messagePack = loggingEvent.MessageObject as MessagePack;
                if (messagePack == null)
                {
                    return loggingEvent.MessageObject;
                }

                return messagePack.Message;
            }
            catch (Exception ex)
            {
                return string.Format("Message Pack Layout Error: Message: {0}", ex);
            }
        }
    }
}