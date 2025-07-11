using Carbook.Application.Features.CQRS.Commands.BannerCommands;
using Carbook.Application.Features.CQRS.Handlers.BannerHandlers;
using Carbook.Application.Features.CQRS.Queries.BannerQueries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Carbook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannersController : ControllerBase
    {
        private readonly GetBannerQueryHandler _getBannerQueryHandler;
        private readonly GetBannerByIdQueryHandler _getBannerByIdQueryHandler;
        private readonly CreateBannerCommandHandler _createBannerCommandHandler;
        private readonly UpdateBannerCommandHandler _updateBannerCommandHandler;
        private readonly RemoveBannerCommandHandler _removeBannerCommandHandler;

        public BannersController(GetBannerQueryHandler getBannerQueryHandler, GetBannerByIdQueryHandler getBannerByIdQueryHandler, CreateBannerCommandHandler createBannerCommandHandler, UpdateBannerCommandHandler updateBannerCommandHandler, RemoveBannerCommandHandler removeBannerCommandHandler)
        {
            _getBannerQueryHandler = getBannerQueryHandler;
            _getBannerByIdQueryHandler = getBannerByIdQueryHandler;
            _createBannerCommandHandler = createBannerCommandHandler;
            _updateBannerCommandHandler = updateBannerCommandHandler;
            _removeBannerCommandHandler = removeBannerCommandHandler;
        }
        [HttpGet]
        public async Task<IActionResult> BannerList()
        {
            var values = await _getBannerQueryHandler.Handle();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBanner(int id)
        {
            var value=await _getBannerByIdQueryHandler.Handle(new GetBannerByIdQuery(id));
            return Ok(value);   
        }
        [HttpPost]
        public async Task<IActionResult> CreateBanner(CreateBannerCommand commad)
        {
            await _createBannerCommandHandler.Handle(commad);
            return Ok("bilgi eklendi");
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveBanner(int id)
        {
           await _removeBannerCommandHandler.Handle(new RemoveBannerCommand(id));
            return Ok("bilgi silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBanner(UpdateBannerCommand commad)
        {
            await _updateBannerCommandHandler.Handle(commad);
            return Ok("bilgi güncellendi");
        }
    }
}
