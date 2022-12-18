using EducationSearchV3.Models.Dtos.Responses;
using EducationSearchV3.Repositories.Contracts;
using EducationSearchV3.Services;
using EducationSearchV3.Services.Contracts;
using Moq;
using Shouldly;
using TestData = EducationSearchV3Test.EducationPrograms;

namespace EducationSearchV3Test.Services.EducationPrograms
{
    public class EducationProgramTest
    {
        private readonly Mock<IEducationProgramRepository> _programRepoMock;
        private readonly Mock<IHighSchoolRepository> _schoolRepoMock;
        private readonly Mock<ILanguageRepository> _languageRepoMock;
        private readonly Mock<ISubjectRepository> _subjectRepoMock;
        private readonly IEducationProgramService _sut;
        public EducationProgramTest()
        {
            _programRepoMock = new Mock<IEducationProgramRepository>();
            _schoolRepoMock = new Mock<IHighSchoolRepository>();
            _languageRepoMock = new Mock<ILanguageRepository>();
            _subjectRepoMock = new Mock<ISubjectRepository>();
            _sut = new EducationProgramService(_programRepoMock.Object,
                _schoolRepoMock.Object,
                _languageRepoMock.Object,
                _subjectRepoMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnNull_WhenNotFound()
        {
            // Arrange
            _programRepoMock.Setup(x => x.GetAllPrograms())
                .ReturnsAsync(() => null!);            

            // Act
            var results = await _sut.GetAll();

            // Assert
            results.ShouldBeNull();
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1806:Do not ignore method results", Justification = "<Pending>")]
        public async Task GetAll_ShouldReturnPrograms_WhenFound()
        {
            // Arrange
            _programRepoMock.Setup(x => x.GetAllPrograms())
                .ReturnsAsync(TestData.GetEducationPrograms());

            // Act
            var results = await _sut.GetAll();

            // Assert
            results.ShouldNotBeNull();
            results.Count().ShouldBe(2);
            results.Select(x => x.ShouldBeOfType<GetEPDto>());
            results.SelectMany(x => x.Languages).ShouldNotBeEmpty();            
        }

        [Fact]
        public async Task GetOne_ShouldReturnProgram_WhenFound()
        {
            // Arrange
            _programRepoMock.Setup(x => x.GetProgramById(It.IsAny<int>()))
                .ReturnsAsync(TestData.GetEducationPrograms().First());

            // Act
            var result = await _sut.GetById(1212);

            // Assert
            result.ShouldNotBeNull();
        }
    }
}
