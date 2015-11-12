using System;

namespace Charm.Logging
{
    [Serializable]
    public class MessagePack
    {
        public object Message { get; set; }
        public string Category { get; set; }
        public dynamic Data { get; set; }
        public string ExecutionId { get; set; }

        protected MessagePack() {}

        public MessagePack(object message, string executionId = null, string category = null, dynamic data = null)
        {
            Message = message;
            Category = category;
            Data = data;
            ExecutionId = executionId;
        }
    }
}
