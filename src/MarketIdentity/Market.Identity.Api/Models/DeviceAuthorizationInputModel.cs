namespace Market.Identity.Api.Models;

public class DeviceAuthorizationInputModel : ConsentInputModel
{
    public string? UserCode { get; set; }
}
