using Microsoft.AspNetCore.Mvc;
using Moq;
using SelfieAWookie.API.UI.Application.DTOs;
using SelfieAWookie.API.UI.Controllers;
using SelfieAWookie.Core.Selfies.Domain;
using SelfiesAWookie.Core.Framework;
using System.Collections.Generic;
using Xunit;

namespace TestWebApi
{
    public class SelfieControllerUnitTest
    {
        #region Public methods
        [Fact]
        public void ShouldAddOneSelfie()
        {
            SelfieDto selfieDto = new SelfieDto();
            var repositoryMock = new Mock<ISelfieRepository>();
            var unit = new Mock<IUnitOfWork>();
            repositoryMock.Setup(x => x.UnitOfWork).Returns(unit.Object);
            repositoryMock.Setup(x => x.AddOne(It.IsAny<Selfie>())).Returns(new Selfie() { Id = 4});

            var controller = new SelfiesController(repositoryMock.Object);
            var result = controller.AddOne(selfieDto);

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            OkObjectResult okResult = (OkObjectResult)result;
            var addedSelfie = okResult.Value as SelfieDto;
            Assert.NotNull(addedSelfie);
            Assert.True(addedSelfie.Id > 0);
        }


        [Fact]
        public void ShouldReturnListOfSelfies()
        {
            var expectedList = new List<Selfie>() {
                new Selfie() {Wookie = new Wookie()},
                new Selfie() {Wookie = new Wookie()}
            };
            var repositoryMock = new Mock<ISelfieRepository>();
            repositoryMock.Setup(item => item.GetAll(It.IsAny<int>())).Returns(expectedList);
            var controller = new SelfiesController(repositoryMock.Object);

            var result = controller.GetAll();

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            OkObjectResult okResult = (OkObjectResult)result;

            Assert.IsType<List<SelfieResumeDto>>(okResult.Value);
            Assert.NotNull(okResult);
            List<SelfieResumeDto>? list = okResult.Value as List<SelfieResumeDto>;
            Assert.True(list.Count == expectedList.Count);

        }
        #endregion
    }
}