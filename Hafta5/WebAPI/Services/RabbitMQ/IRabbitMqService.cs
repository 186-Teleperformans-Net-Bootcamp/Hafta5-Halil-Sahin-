namespace WebAPI.Services.RabbitMQ
{
    public interface IRabbitMqService
    {
         
        void CreateMessageQueue(string message);
        string ConsumeMessageQueue();

    }
}
