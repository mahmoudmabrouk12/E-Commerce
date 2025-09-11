
namespace E_Commerce.Core.DTOs
{
    public class EmailDTO
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public EmailDTO(string to, string from, string subject, string content)
        {
            To = to;
            From = from;
            Subject = subject;
            Content = content;
        }
    }
}
