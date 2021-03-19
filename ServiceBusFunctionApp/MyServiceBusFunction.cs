using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;

namespace ServiceBusFunctionApp
{
    public class MyServiceBusFunction
    {
        [FunctionName("MyServiceBusFunction")]
        public void Run([ServiceBusTrigger("%MessageQueueName%", Connection = "ServiceBusConnectionString")]Message message, MessageReceiver messageReceiver, ILogger log)
        {
            try
            {
                string payload = Encoding.UTF8.GetString(message.Body);
                log.LogInformation($"C# ServiceBus queue trigger function processed message: {payload}");

                MyMessageModel model = JsonConvert.DeserializeObject<MyMessageModel>(payload);

                //Do you things here, such as Some Actions or Calls Some Service or Another Method


                //complete the message if there is no error
                messageReceiver.CompleteAsync(message.SystemProperties.LockToken);

            }
            catch (Exception)
            {
                // Do your error handling here


                // Send message to DeadLetter Queue
                messageReceiver.DeadLetterAsync(message.SystemProperties.LockToken);

            }
        }
    }
}
