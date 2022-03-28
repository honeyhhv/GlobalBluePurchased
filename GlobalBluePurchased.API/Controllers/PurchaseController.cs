using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalBluePurchased.API.ViewModel;
using GlobalBluePurchased.Domain.Core.Models;
using GlobalBluePurchased.Domain.Core.Models.ValueObjects;
using GlobalBluePurchased.Domain.Request;
using MediatR;

namespace GlobalBluePurchased.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PurchaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<ResultDto>> Get([FromQuery] CalculatePurchaseQueries data)
        {
            var result = await _mediator.Send(data);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }
    }
}
