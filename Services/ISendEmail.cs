namespace SendEmailService.Services
{
    public interface ISendEmail
    {
        Task Send(string emailTo, string subject, string body, List<IFormFile> attachments);
    }
}
