using EducationSearchV3.Repositories.Contracts;
using EducationSearchV3.Services;
using EducationSearchV3.Services.Contracts;
using Moq;
using Shouldly;
using TestData = EducationSearchV3Test.Countries;

namespace EducationSearchV3Test.Services.Countries
{
    public class CountryServiceTest
    {
        private readonly Mock<ICountryRepository> _countryRepoMock;
        private readonly Mock<ILanguageRepository> _languageRepoMock;
        private readonly Mock<IHighSchoolRepository> _highSchoolRepoMock;
        private readonly ICountryService _sut;

        public CountryServiceTest()
        {
            _countryRepoMock = new Mock<ICountryRepository>();
            _languageRepoMock = new Mock<ILanguageRepository>();
            _highSchoolRepoMock = new Mock<IHighSchoolRepository>();
            _sut = new CountryService(_countryRepoMock.Object, _languageRepoMock.Object, _highSchoolRepoMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnNull_WhenNoCountres()
        {
            // Arrange
            _countryRepoMock.Setup(x => x.GetAllCountries()).ReturnsAsync(() => null!);

            // Act
            var result = await _sut.GetAll();

            // Assert
            result.ShouldBeNull();
        }

        [Fact]
        public async Task GetAll_ShouldReturnCountry_WithDependents()
        {
            // Arrange
            var countries = TestData.GetCountries();
            var name = countries.First().HighSchools!.First().Name;
            _countryRepoMock.Setup(x => x.GetAllCountries()).
                ReturnsAsync(() => TestData.GetCountries());

            // Act
            var result = await _sut.GetAll();

            // Assert
            result!.ToList().Count.ShouldBe(2);
            result!.SelectMany(x => x.HighSchools!).ShouldAllBe(x => x == name);
        }

        [Fact]
        public async Task GetCountryById_ShouldReturnCountry_WhenFound()
        {
            // Arrange            
            _countryRepoMock.Setup(x => x.GetCountryById(It.IsAny<int>()))
                .ReturnsAsync(TestData.GetCountries().First());

            // Act
            var result = await _sut.GetById(new Random().Next());

            // Assert
            result.ShouldNotBeNull();
            result.Languages.ShouldNotBeEmpty();
            result.HighSchools.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task GetCountryById_ShouldReturnNull_WhenNotFound()
        {
            // Arrange            
            _countryRepoMock.Setup(x => x.GetCountryById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetById(new Random().Next());

            // Assert
            result.ShouldBeNull();
        }        
    }
}
