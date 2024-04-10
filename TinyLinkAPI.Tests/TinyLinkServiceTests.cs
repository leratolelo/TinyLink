using Microsoft.AspNetCore.Http;
using Moq;
using System.Linq.Expressions;
using TinyLink.API.Commands;
using TinyLink.API.Infrastructure;
using TinyLink.API.Models.DTOs;
using TinyLink.API.Queries;
using TinyLink.API.Services;

namespace TinyLinkAPI.Tests
{
    public class TinyLinkServiceTests
    {

        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private Mock<IGenericRepository<TinyLink.API.Models.TinyLink>> _genericRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _genericRepositoryMock = new Mock<IGenericRepository<TinyLink.API.Models.TinyLink>>();
        }

        [Test]
        public void CreateTinyLink_ValidCommand_ReturnsNewTinyLink()
        {
            //Arrange
            var baseUrl = "https://tinylink.com/";
            var userId = Guid.NewGuid();
            var longLink = "https://www.tinylink.com/long-link";
            var command = new CreateTinyLinkCommand
            {
                UserId = userId,
                LongLink = longLink
            };

            _httpContextAccessorMock.SetupGet(x => x.HttpContext.Request.Scheme).Returns("https");
            _httpContextAccessorMock.SetupGet(x => x.HttpContext.Request.Host).Returns(new HostString("example.com"));
            _genericRepositoryMock.Setup(x => x.Add(It.IsAny<TinyLink.API.Models.TinyLink>()));
            _genericRepositoryMock.Setup(x => x.Save());

            var service = new TinyLinkService( _genericRepositoryMock.Object , _httpContextAccessorMock.Object);

            // Act
            var tinyLink  = service.CreateTinyLink(command);

            // Assert
            Assert.NotNull(tinyLink);
            Assert.AreEqual(userId, tinyLink.UserId);
            Assert.AreEqual(longLink, tinyLink.LongLink);
            Assert.IsFalse(string.IsNullOrEmpty(tinyLink.ShortLink));
            Assert.IsFalse(string.IsNullOrEmpty(tinyLink.Hash));
            Assert.IsFalse(tinyLink.Deleted);
            Assert.NotNull(tinyLink.ShortLink);

            _genericRepositoryMock.Verify(x => x.Add(It.IsAny<TinyLink.API.Models.TinyLink>()), Times.Once);
            _genericRepositoryMock.Verify(x => x.Save(), Times.Once);
           
        }

      [Test]
        public void UpdateTinyLink_ExistingDto_UpdatesTinyLink()
        {
            var idString = "73668668-63C8-481E-9E02-B9EFA2A4F9AC";
            var id = Guid.Parse(idString);

            var dto = new TinyLinkDto
            {
                Id = id, 
                LongLink = "https://example.com/updated", 
                Deactivated = true
            };

            var existingTinyLink = new TinyLink.API.Models.TinyLink { Id = dto.Id }; 

            _genericRepositoryMock.Setup(x => x.GetById(dto.Id)).Returns(existingTinyLink);

            ITinyLinkService service = new TinyLinkService(_genericRepositoryMock.Object, _httpContextAccessorMock.Object);

            service.UpdateTinyLink(dto); 

            _genericRepositoryMock.Verify(x => x.Update(existingTinyLink), Times.Once);
            _genericRepositoryMock.Verify(x => x.Save(), Times.Once);
            Assert.AreEqual(dto.LongLink, existingTinyLink.LongLink);
            Assert.IsTrue(existingTinyLink.Deleted);
        }
        
        [Test]
        public void GetTinyLink_ExistingHash_ReturnsTinyLink()
        {
            //Arrange
            var hash = "8583B0A9"; 
            var expectedTinyLink = new TinyLink.API.Models.TinyLink { Hash = hash };
            var query = new ConnectToTinyLinkQuery { TinyLink = "https://example.com/" + hash };

            var expectedTinyLinks = new[] { expectedTinyLink };
            _genericRepositoryMock.Setup(x => x.GetByCondition(It.IsAny<Expression<Func<TinyLink.API.Models.TinyLink, bool>>>()))
                                  .Returns(expectedTinyLinks.AsQueryable());

            ITinyLinkService service = new TinyLinkService(_genericRepositoryMock.Object, _httpContextAccessorMock.Object);

            //Act
            var result = service.GetTinyLinkByHash(hash).FirstOrDefault();

            //Assert
            Assert.That(result.Hash, Is.EqualTo(expectedTinyLink.Hash)); 
        }






    }
}