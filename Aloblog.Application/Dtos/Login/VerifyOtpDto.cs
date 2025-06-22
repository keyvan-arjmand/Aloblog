namespace Aloblog.Application.Dtos.Login;

public class VerifyOtpDto
{
    public string PhoneNumber { get; set; } = string.Empty;
    public string Otp { get; set; } = string.Empty;
}