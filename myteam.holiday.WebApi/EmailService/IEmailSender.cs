namespace myteam.holiday.WebApi.EmailService
{
    public interface IEmailSender
    {
        Task SendVerifyTokenAsync(string token, string emailAddress);
    }
}
