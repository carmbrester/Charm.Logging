using System;
using System.IO;
using log4net.Core;
using log4net.Layout.Pattern;
using Newtonsoft.Json;

namespace Charm.Logging.Log4net.MessagePack
{
    public class MsgDataMessagePackPatternLayoutConverter : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            try
            {
                var messagePack = loggingEvent.MessageObject as Charm.Logging.MessagePack;
                if (messagePack == null || messagePack.Data == null)
                {
                    return;
                }

                var serializedMessagePackData = JsonConvert.SerializeObject(messagePack.Data, Formatting.Indented);
                writer.Write(serializedMessagePackData);
            }
            catch (Exception ex)
            {
                writer.Write($"[Msg Data Message Pack Pattern Layout Converter Error - Exception: {ex}]");
            }
        }
    }
}
