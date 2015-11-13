using System;
using log4net.Core;
using log4net.Layout;

namespace Charm.Logging.Mongo.Layouts
{
    public class MessagePackCategoryLayout : IRawLayout
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

                return messagePack.Category;
            }
            catch (Exception ex)
            {
                return string.Format("Message Pack Layout Error: Category: {0}", ex);
            }
        }
    }
}