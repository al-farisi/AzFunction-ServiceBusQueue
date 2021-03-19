using System;

namespace ServiceBusFunctionApp
{
    public class MyMessageModel
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public int Score { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
