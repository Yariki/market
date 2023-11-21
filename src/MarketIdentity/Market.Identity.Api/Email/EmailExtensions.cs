using Microsoft.AspNetCore.Identity.UI.Services;

namespace Market.Identity.Api.Email;

public static class EmailExtensions
{
    public static void AddEmail(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailOptions>(configuration.GetSection("Email"));
        services.AddTransient<IEmailSender, EmailSender>();
    }
}
