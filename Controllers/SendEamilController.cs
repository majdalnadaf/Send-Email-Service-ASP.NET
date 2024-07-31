using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendEmailService.DataTransferObjects;
using SendEmailService.Services;

namespace SendEmailService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendEamilController : ControllerBase
    {
        private readonly ISendEmail _sendEmail;
        public SendEamilController(ISendEmail sendEmail)
        {
            _sendEmail = sendEmail;                
        }

        [HttpPost("Send")]
        public async Task<IActionResult> SendEmail([FromForm] DTOEmailInfo model )
        {
            try
            {
                await _sendEmail.Send(model.EmailTo, model.Subject, model.Body, model.Attachments);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }


        }
    }
}
