using EducationSearchV3.Models.Dtos.Responses;
using EducationSearchV3.Repositories.Contracts;
using EducationSearchV3.Services;
using EducationSearchV3.Services.Contracts;
using Moq;
using Shouldly;
using TestData = EducationSearchV3Test.Languages;

namespace EducationSearchV3Test.Services.Languages
{
    public class LanguageServiceTest
    {
        private readonly Mock<ILanguageRepository> _languageRepoMock;
        private readonly ILanguageService _sut;

        public LanguageServiceTest()
        {
            _languageRepoMock = new Mock<ILanguageRepository>();
            _sut = new LanguageService(_languageRepoMock.Object);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1806:Do not ignore method results", Justification = "<Pending>")]
        public async Task GetAll_ShouldReturnLanguages_WithDepenents()
        {
            // Arrange
            _languageRepoMock.Setup(x => x.GetAllLanguages())
                .ReturnsAsync(TestData.GetLanguages());

            // Act
            var results = await _sut.GetAll();

            // Assert
            results.ShouldNotBeNull();
            results.Count().ShouldBe(2);
            results.Select(x => x.ShouldBeOfType<GetLanguageDto>());
            results.SelectMany(x => x.Countries).ShouldNotBeEmpty();
            results.SelectMany(x => x.EducationPrograms).ShouldNotBeEmpty();
        }

        [Fact]
        public async Task GetOne_ShouldReturnLanguage_WhenFound()
        {
            // Arrange
            _languageRepoMock.Setup(x => x.GetLanguageById(It.IsAny<int>()))
                .ReturnsAsync(TestData.GetLanguages().First());

            // Act
            var result = await _sut.GetById(1);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<GetLanguageDto>();
        }

        [Fact]
        public async Task GetOne_ShouldReturnNull_WhenNotFound()
        {
            // Arrange
            _languageRepoMock.Setup(x => x.GetLanguageById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetById(1);

            // Assert
            result.ShouldBeNull();            
        }
    }
}
