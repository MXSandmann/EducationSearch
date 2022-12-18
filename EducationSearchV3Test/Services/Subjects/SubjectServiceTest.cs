using EducationSearchV3.Models;
using EducationSearchV3.Models.Dtos.Responses;
using EducationSearchV3.Repositories.Contracts;
using EducationSearchV3.Services;
using EducationSearchV3.Services.Contracts;
using Moq;
using Shouldly;
using TestData = EducationSearchV3Test.Subjects;
using TestDataPrograms = EducationSearchV3Test.EducationPrograms;

namespace EducationSearchV3Test.Services.Subjects
{
    public class SubjectServiceTest
    {
        private readonly Mock<ISubjectRepository> _subjectRepoMock;
        private readonly Mock<IEducationProgramRepository> _programRepoMock;
        private readonly ISubjectService _sut;

        public SubjectServiceTest()
        {
            _subjectRepoMock = new Mock<ISubjectRepository>();
            _programRepoMock = new Mock<IEducationProgramRepository>();
            _sut = new SubjectService(_subjectRepoMock.Object,
                _programRepoMock.Object);

        }

        [Fact]
        public async Task GetAll_ShouldReturnNull_WhenNoSubjects()
        {
            // Arrange            
            _subjectRepoMock.Setup(x => x.GetAllSubjects()).
                ReturnsAsync(() => null!);
            // Act
            var result = await _sut.GetAll();

            // Assert
            result.ShouldBeNull();
        }

        [Fact]
        public async Task GetAll_ShouldReturnSubject_WithDependents()
        {
            // Arrange
            var subjects = TestData.GetSubjects();
            var name = subjects.First().Programs!.First().Name;
            _subjectRepoMock.Setup(x => x.GetAllSubjects()).
                ReturnsAsync(subjects);
            // Act
            var result = await _sut.GetAll();

            // Assert
            result.ShouldNotBeNull();
            result.Count().ShouldBe(3);
            result.Select(x => x.ShouldBeOfType<GetSubjectDto>());
            result.SelectMany(x => x.Programs!).ShouldNotBeEmpty();
        }

        [Fact]
        public async Task GetSubjectByID_ShouldReturnSubject_WhenFound()
        {
            // Arrange
            var subject = TestData.GetSubjects().First();            
            _subjectRepoMock.Setup(x => x.GetSubjectById(It.IsAny<int>()))
                .ReturnsAsync(subject);

            // Act
            var result = await _sut.GetById(4545);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<GetSubjectDto>();            
            result.Programs.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task GetSubjectByID_ShouldReturnNull_WhenNotFound()
        {
            // Arrange            
            _subjectRepoMock.Setup(x => x.GetSubjectById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetById(new Random().Next());

            // Assert
            result.ShouldBeNull();            
        }

        [Fact]
        public async Task CreateSubject_ShouldSuccess_WhenNotFound()
        {
            // Arrange
            var pr = TestDataPrograms.GetEducationPrograms().First();

            _subjectRepoMock.Setup(x => x.HasSubjectWithName(It.IsAny<string>()))
                .ReturnsAsync(false);
            _subjectRepoMock.Setup(x => x.AddSubject(It.IsAny<Subject>())).Verifiable();
            _subjectRepoMock.Setup(x => x.GetAllSubjects()).
                ReturnsAsync(TestData.GetSubjects());
            _programRepoMock.Setup(x => x.GetProgramById(It.IsAny<int>())).ReturnsAsync(pr);
            // Act
            var results = await _sut.Create(TestData.GetSubjectDtos().First());

            // Assert
            results.ShouldNotBeNull();
        }

        [Fact]
        public void CreateSubject_ShouldThrowExeption_WhenNoDependentsFound()
        {
            // Arrange       
            _subjectRepoMock.Setup(x => x.HasSubjectWithName(It.IsAny<string>()))
                .ReturnsAsync(false);
            _subjectRepoMock.Setup(x => x.AddSubject(It.IsAny<Subject>())).Verifiable();
            _subjectRepoMock.Setup(x => x.GetAllSubjects()).
                ReturnsAsync(() => TestData.GetSubjects());
            _programRepoMock.Setup(x => x.GetProgramById(It.IsAny<int>())).ReturnsAsync(() => null);
            // Act
            // Assert
            Should.Throw<ArgumentException>(async () => await _sut.Create(TestData.GetSubjectDtos().First()));
        }

        [Fact]
        public async Task DeleteSubject_ShouldReturnData_WhenFound()
        {
            // Arrange
            var subject = TestData.GetSubjects().First();
            _subjectRepoMock.Setup(x => x.GetSubjectById(It.IsAny<int>()))
                .ReturnsAsync(subject);
            _subjectRepoMock.Setup(x => x.RemoveSubject(It.IsAny<Subject>())).Verifiable();
            _subjectRepoMock.Setup(x => x.GetAllSubjects()).
                ReturnsAsync(TestData.GetSubjects());

            // Act
            var results = await _sut.Delete(new Random().Next());

            // Assert
            results.ShouldNotBeNull();
        }

        [Fact]
        public async Task DeleteSubject_ShouldReturnNull_WhenNotFound()
        {
            // Arrange            
            _subjectRepoMock.Setup(x => x.GetSubjectById(It.IsAny<int>()))
                .ReturnsAsync(() => null);
            _subjectRepoMock.Setup(x => x.RemoveSubject(It.IsAny<Subject>())).Verifiable();
            _subjectRepoMock.Setup(x => x.GetAllSubjects()).
                ReturnsAsync(TestData.GetSubjects());

            // Act
            var results = await _sut.Delete(new Random().Next());

            // Assert
            results.ShouldBeNull();
        }

        [Fact]
        public async Task UpdateSubject_ShouldReturnData_WhenFound()
        {
            // Arrange
            var pr = TestDataPrograms.GetEducationPrograms().First();
            var dto = TestData.GetSubjectDtos().Last();
            var subject = TestData.GetSubjects().First();
            _subjectRepoMock.Setup(x => x.SaveChangesAsync()).Verifiable();
            _subjectRepoMock.Setup(x => x.GetSubjectById(It.IsAny<int>()))
                .ReturnsAsync(subject);
            _programRepoMock.Setup(x => x.GetProgramById(It.IsAny<int>())).ReturnsAsync(pr);
            // Act
            var results = await _sut.Update(dto);

            // Assert
            results.ShouldNotBeNull();
        }

        [Fact]
        public async Task UpdateSubject_ShouldReturnNull_WhenNoID()
        {
            // Arrange
            var pr = TestDataPrograms.GetEducationPrograms().First();
            var dto = TestData.GetSubjectDtos().First();
            var subject = TestData.GetSubjects().First();
            _subjectRepoMock.Setup(x => x.SaveChangesAsync()).Verifiable();
            _subjectRepoMock.Setup(x => x.GetSubjectById(It.IsAny<int>()))
                .ReturnsAsync(subject);
            _programRepoMock.Setup(x => x.GetProgramById(It.IsAny<int>())).ReturnsAsync(pr);
            // Act
            var results = await _sut.Update(dto);

            // Assert
            results.ShouldBeNull();
        }

        [Fact]
        public async Task UpdateSubject_ShouldReturnNull_WhenNotFound()
        {
            // Arrange
            var pr = TestDataPrograms.GetEducationPrograms().First();
            var dto = TestData.GetSubjectDtos().Last();
            _subjectRepoMock.Setup(x => x.SaveChangesAsync()).Verifiable();
            _subjectRepoMock.Setup(x => x.GetSubjectById(It.IsAny<int>()))
                .ReturnsAsync(() => null);
            _programRepoMock.Setup(x => x.GetProgramById(It.IsAny<int>())).ReturnsAsync(pr);
            // Act
            var results = await _sut.Update(dto);

            // Assert
            results.ShouldBeNull();
        }
    }
}