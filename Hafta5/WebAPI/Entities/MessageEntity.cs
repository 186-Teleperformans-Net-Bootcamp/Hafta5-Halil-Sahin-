namespace WebAPI.Entities
{
    public class MessageEntity
    {
        public int MessageId { get; set; }
        public int MessageType { get; set; }
        public string MessageBody { get; set; }
        public bool IsPublicMessage { get; set; }
        public int GroupId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }


    }
}
