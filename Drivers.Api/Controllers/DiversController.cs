using Drivers.Api.Models;
using Drivers.Api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Drivers.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiversController : ControllerBase
{
   
    private readonly ILogger<DiversController> _logger;

    private readonly DriverServices _diversServices;



    public DiversController(ILogger<DiversController>
     logger, DriverServices driversServices)
    {
        _logger = logger;
        _diversServices =driversServices;
    }
[HttpGet]
public async Task<IActionResult> GetDrivers()
{
    var drivers = await _diversServices.GetAsync();
    return Ok(drivers);
}

[HttpGet("{id}")]
public async Task <IActionResult> GetDriverById(string id)
{
    return Ok(await _diversServices.GetDriverById(id));
}



[HttpPost]
public async Task <IActionResult> CreateDriver([FromBody] Divers drive)
{
    if (drive==null)
    return BadRequest();
    if (drive.Name == string.Empty)
    ModelState.AddModelError("Name,", "El driver no debe estar vacio");

    await _diversServices.InsertDriver(drive);
    return Created("Created", true);
}
[HttpPut("{id}")]
  public async Task <IActionResult> UpdateDriver([FromBody] Divers drive, string id)
  {
    if (drive==null)
    return BadRequest();
    if (drive.Name==string.Empty)
    ModelState.AddModelError("Name", "EL  driver no deberia estar vacio");
    //Obtiene el valor de
    drive.Id =id;
    await _diversServices.UpdateDriver(drive);
    return Created("Created", true);
  }

  [HttpDelete("{id}")]
    public async Task <IActionResult> DeleteDriver(string id)
    {
        await _diversServices.DeleteDriver(id);
        return NoContent(); //Succes (Que todo salio bien)
    }


}
