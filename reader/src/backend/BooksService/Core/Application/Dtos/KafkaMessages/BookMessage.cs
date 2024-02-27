namespace Application.Dtos.KafkaMessages;

public class BookMessage
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string AuthorFirstName { get; set; }
    public string AuthorLastName { get; set; }
}