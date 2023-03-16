using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using WebApi.Data;
using WebApi.DTOs;
using WebApi.Models;
using WebApi.Services.Interfaces;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }


        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {

            var ordenes = _orderService.GetAll();
            var ordenesDTO = _mapper.Map<List<OrderDTO>>(ordenes);    

            return Ok(
                ordenesDTO
            );
        }

        [HttpGet]
        //[Route("GetPrediction")]
        public ActionResult<IEnumerable<OrderPrediction>> GetPrediction()
        {

            var ordenes = _orderService.GetPrediction();

            return Ok(
                ordenes
            );
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Order>> Get(int id)
        {
            var orden = _orderService.Get(id);

            if (orden.OrderId == 0)
            {
                return NotFound();
            }

            return Ok(orden);
        }

        [HttpGet("{custid}")]
        //[Route("GetByCustId")]
        public ActionResult<IEnumerable<Order>> GetByCustId(int custid)
        {

            return Ok(
                _orderService.GetByCustid(custid)
            );
        }

        [HttpPost]
        public ActionResult Post([FromBody] OrderCreateDTO orderCreateDTO)
        {
            try {          
                
                var saleOrder = _mapper.Map<Order>(orderCreateDTO);

                 _orderService.Create(saleOrder);

                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }            
        }
    }
}
