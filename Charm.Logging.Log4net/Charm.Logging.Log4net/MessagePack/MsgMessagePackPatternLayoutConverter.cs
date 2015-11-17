using System;
using System.IO;
using log4net.Core;
using log4net.Layout.Pattern;

namespace Charm.Logging.Log4net.MessagePack
{
    public class MsgMessagePackPatternLayoutConverter : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            try
            {
                var messagePack = loggingEvent.MessageObject as Logging.MessagePack;
                if (messagePack == null)
                {
                    writer.Write(loggingEvent.MessageObject);
                }
                else
                {
                    writer.Write(messagePack.Message);
                }
            }
            catch (Exception ex)
            {
                writer.Write($"[Msg Message Pack Pattern Layout Converter Error - Exception: {ex}]");
            }
        }
    }
}
