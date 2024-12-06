using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SendEmail([FromBody] ContactFormDto form)
    {
        if (form == null || string.IsNullOrWhiteSpace(form.Email) || string.IsNullOrWhiteSpace(form.Message))
        {
            return BadRequest("Ongeldige invoer.");
        }

        try
        {
            var smtpClient = new SmtpClient("smtp.google.com")
            {
                Port = 25,
                Credentials = new NetworkCredential("informatie.rentit@gmail.com", "InfoRentIt"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("informatie.rentit@gmail.com"),
                Subject = "Nieuw Contactformulier Bericht",
                Body = $"Naam: {form.Name}\nEmail: {form.Email}\nBericht: {form.Message}",
                IsBodyHtml = false,
            };

            mailMessage.To.Add("informatie.rentit@gmail.com");

            await smtpClient.SendMailAsync(mailMessage);

            return Ok("Bericht verzonden!");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Er is een fout opgetreden: {ex.Message}");
        }
    }
}

public class ContactFormDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
}
