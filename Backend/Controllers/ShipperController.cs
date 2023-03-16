using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShipperController : ControllerBase
    {
        private readonly IShipperService _shipperService;

        public ShipperController(IShipperService shipperService)
        {
            _shipperService = shipperService;
        }


        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Shipper>> Get()
        {
            return Ok(
                _shipperService.GetAll()
            );
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Shipper>> Get(int id)
        {
            var shipper = _shipperService.Get(id);

            if (shipper.ShipperId == 0)
            {
                return NotFound();
            }

            return Ok(shipper);
        }
    }
}
