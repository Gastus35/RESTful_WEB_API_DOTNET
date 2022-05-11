using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelfieAWookie.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructures.Data;
using Microsoft.EntityFrameworkCore;
using SelfieAWookie.API.UI.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SelfieAWookie.API.UI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SelfiesController : ControllerBase
    {
        #region Fields
        private readonly ISelfieRepository _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        #endregion

        #region Constructors
        public SelfiesController(ISelfieRepository repository, IWebHostEnvironment webHostEnvironment)
        {
            this._repository = repository;
            this._webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Public methods 
        [HttpGet]
        public IActionResult GetAll([FromQuery] int wookieId = 0)
        {
            var selfiesList = _repository.GetAll(wookieId);
            var model = selfiesList.Select(item => new SelfieResumeDto() {Title = item.Title, WookieId = item.WookieId, NbSelfiesFromWookie = (item.Wookie?.Selfies?.Count).GetValueOrDefault(0)}).ToList();
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
        public IActionResult AddOne(SelfieDto selfieDto)
        {
            IActionResult result = BadRequest();
            Selfie addSelfie = this._repository.AddOne(new Selfie()
            {
                Title = selfieDto.Title,
                ImagePath = selfieDto.ImagePath,
            }); ;
            this._repository.UnitOfWork.SaveChanges();

            if (addSelfie != null)
            {
                selfieDto.Id = addSelfie.Id;
                result = this.Ok(selfieDto);
            }

            return result;
        }
        #endregion
    }
}
