using Cdb.Domain.Dto;
using Cdb.Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace investment_calculator.Controllers
{
    [Route("api/cdb")]
    [ApiController]
    public class CdbController : ControllerBase
    {
        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate([FromServices] ICalculateCDB calculateCDB, CdbRequest request)
        {
            try
            {
                var result = await calculateCDB.Execute(request);

                return Ok(result);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Errors);
            }
        }
    }
}
