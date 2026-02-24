using AutoMapper;
using BetterHealthCareAPI.Application;
using BetterHealthCareAPI.Application.Dto;
using BetterHealthCareAPI.Application.Interfaces;
using BetterHealthCareAPI.Domain.Models;
using BetterHealthCareAPI.Application.Mapper;
using BetterHealthCareAPI.Infrastructure;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BetterHealthCareAPI.Tests
{
    public class PatientServiceTests
    {
        private readonly IMapper _mapper;

        public PatientServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedDtos()
        {
            // arrange
            var patients = new List<Patient>
            {
                new Patient { Id = 1, Name = "John", HealthNumber = "123", DateOfBirth = System.DateTime.Today }
            };

            var repoMock = new Mock<IRepository<Patient>>();
            repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(patients);

            // we don't need a real DbContext for this unit test; pass null
            BetterHealthCareDbContext? context = null;
            var loggerMock = new Mock<Microsoft.Extensions.Logging.ILogger<PatientService>>();

            var service = new PatientService(context!, repoMock.Object, _mapper, loggerMock.Object);

            // act
            var result = await service.GetAllAsync();

            // assert
            Assert.Single(result);
            Assert.Equal("John", ((List<PatientDto>)result)[0].Name);
        }
    }
}