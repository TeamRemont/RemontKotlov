using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RemontKotlov.Entities;
using RemontKotlov.Services.Servisces.Commands;
using RemontKotlov.Services.Servisces.Queries;
using RemontKotlov.ViewModels;

namespace RemontKotlov.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ResponseModel> PostAsync(CreateServiceCommand command, CancellationToken cancellation)
        {
            var respone = await _mediator.Send(command, cancellation);

            return respone;
        }

        [HttpPut]
        public async Task<ResponseModel> PutAsync(UpdateServiceCommand command, CancellationToken cancellation)
        {
            var respone = await _mediator.Send(command, cancellation);

            return respone;
        }

        [HttpDelete("{id}")]
        public async Task<ResponseModel> RemoveAsync(int id, CancellationToken cancellation)
        {
            var command = new DeleteServiceCommand { Id = id };

            var respone = await _mediator.Send(command, cancellation);

            return respone;
        }

        [HttpGet("{id}")]
        public async Task<Service> GetByIdAsync(int id, CancellationToken cancellation)
        {
            var query = new GetServiceByIdQuery { Id = id };

            var respone = await _mediator.Send(query, cancellation);

            return respone;
        }

        [HttpGet]
        public async Task<IEnumerable<Service>> GetAsync(CancellationToken cancellation)
        {
            var query = new GetAllServicesQuery();

            var respone = await _mediator.Send(query, cancellation);

            return respone;
        }
    }
}
