using BanklyTest.Interface;
using BanklyTest.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BanklyTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        public UsersController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult PostEmail([FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                //get formatted email body
                var getFormattedMessage = _emailSender.FormatEmailBody(user);

                _emailSender.SendEmail(user.Email, "Welcome onbaord", getFormattedMessage);

                return Ok();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


    }
}
