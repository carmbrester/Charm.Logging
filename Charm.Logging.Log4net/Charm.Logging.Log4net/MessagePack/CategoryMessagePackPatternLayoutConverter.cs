using System;
using System.IO;
using log4net.Core;
using log4net.Layout.Pattern;

namespace Charm.Logging.Log4net.MessagePack
{
    public class CategoryMessagePackPatternLayoutConverter : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            try
            {
                var messagePack = loggingEvent.MessageObject as Charm.Logging.MessagePack;
                if (messagePack == null)
                {
                    return;
                }

                writer.Write(messagePack.Category);
            }
            catch (Exception ex)
            {
                writer.Write($"[Category Message Pack Pattern Layout Converter Error - Exception: {ex}]");
            }
        }
    }
}
