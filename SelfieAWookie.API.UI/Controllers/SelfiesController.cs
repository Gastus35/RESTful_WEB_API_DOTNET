using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelfieAWookie.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructures.Data;
using Microsoft.EntityFrameworkCore;
using SelfieAWookie.API.UI.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MediatR;
using SelfieAWookie.API.UI.Application.Queries;
using SelfieAWookie.API.UI.Application.Commands;

namespace SelfieAWookie.API.UI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SelfiesController : ControllerBase
    {
        #region Fields
        private readonly IMediator _mediator;
        private readonly ISelfieRepository _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        #endregion

        #region Constructors
        public SelfiesController(IMediator mediator, ISelfieRepository repository, IWebHostEnvironment webHostEnvironment)
        {
            this._mediator = mediator;
            this._repository = repository;
            this._webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Public methods 
        [HttpGet]
        public IActionResult GetAll([FromQuery] int wookieId = 0)
        {
            //var selfiesList = _repository.GetAll(wookieId);
            //var model = selfiesList.Select(item => new SelfieResumeDto() {Title = item.Title, WookieId = item.WookieId, NbSelfiesFromWookie = (item.Wookie?.Selfies?.Count).GetValueOrDefault(0)}).ToList();

            var model = this._mediator.Send(new SelectAllSelfiesQuery() { WookieId = wookieId});
            
            return this.Ok(model);
        }

        [Route("photos")]
        [HttpPost]
        public async Task<IActionResult> AddPicture(IFormFile picture)
        {
            string filePath = Path.Combine(this._webHostEnvironment.ContentRootPath, @"images\selfies");
            if (! Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            filePath = Path.Combine(filePath, picture.FileName);

            using var stream = new FileStream(filePath, FileMode.OpenOrCreate);
            await picture.CopyToAsync(stream);
            var itemFile = this._repository.AddOnePicture(filePath);
            
            this._repository.UnitOfWork.SaveChanges();
            

            return this.Ok(itemFile);
        }

        [HttpPost]
        public async Task<IActionResult> AddOne(SelfieDto selfieDto)
        {
            IActionResult result = BadRequest();

            var item = await this._mediator.Send(new AddSelfieCommand() { Item = selfieDto});
            if (item != null)
            {
                result = this.Ok(item);
            }
            return result;
        }
        #endregion
    }
}
