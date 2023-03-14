using Microsoft.EntityFrameworkCore;

using weatherforecast.Models;
using weatherforecast.Data;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Moq;
using weatherforecast.Services.CityService;
using AutoMapper;
using weatherforecast.Dto;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Org.BouncyCastle.Security;
using weatherforecast;

namespace watherforecast
{
    [TestFixture]
    public class CityServiceTests
    {    

        [Test]
        public void AddCity_Test()
        {
            IMapper _mapper = AutoMapperConfig.Initialize();
            var mockSet = new Mock<DbSet<City>>();
            
          
            var mockContext = new Mock<WeatherDataContext>();
            mockContext.Setup(m=>m.City).Returns(mockSet.Object);
            
            var service = new CityService(_mapper, mockContext.Object);
            
            //get the mapper from service. look at auto mapper, how to use it in unit test
            var cityToBeAdded = new AddCityDto() { CityName = "testCity" };
            var result = service.AddCity(cityToBeAdded);
            Assert.That(result.Result.Success, "true" );
           
        }
    }
}