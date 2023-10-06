using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.RequesetResponseMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.ESB.MassTransit.RequestResponsePattern.Consumer.Cunsomers
{
    public class RequestMessageConsumer : IConsumer<RequestMessage>
    {
        public async Task Consume(ConsumeContext<RequestMessage> context)
        {
            Console.WriteLine(context.Message.Text);
            await context.RespondAsync<ResponseMessage>(new()
            {
                Text = $"{context.Message.MessageNo}. response to request",
            });
        }
    }
}
