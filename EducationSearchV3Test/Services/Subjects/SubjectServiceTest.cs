using EducationSearchV3.Models;
using EducationSearchV3.Models.Dtos.Requests;
using EducationSearchV3.Models.Enums;
using EducationSearchV3.Repositories;
using EducationSearchV3.Services;
using Moq;
using Shouldly;

namespace EducationSearchV3Test.Repositories.Subjects
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
            var subjects = GetTestDataSubjects();
            var name = subjects.First().Programs!.First().Name;
            _subjectRepoMock.Setup(x => x.GetAllSubjects()).
                ReturnsAsync(() => GetTestDataSubjects());
            // Act
            var result = await _sut.GetAll();

            // Assert
            result!.ToList().Count.ShouldBe(3);            
            result!.SelectMany(x => x.Programs!).ShouldAllBe(x => x == name);       
        }

        [Fact]
        public async Task GetSubjectByID_ShouldReturnSubject_WhenFound()
        {
            // Arrange
            var id = new Random().Next();
            _subjectRepoMock.Setup(x => x.GetSubjectById(It.IsAny<int>()))
                .ReturnsAsync(() => new Subject { Id = id, Name = "Test1", Programs = CreateEducationPrograms()});

            // Act
            var result = await _sut.GetById(id);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(id);
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
            var pr = CreateEducationPrograms().First();

            _subjectRepoMock.Setup(x => x.HasSubjectWithName(It.IsAny<string>()))
                .ReturnsAsync(false);
            _subjectRepoMock.Setup(x => x.AddSubject(It.IsAny<Subject>())).Verifiable();
            _subjectRepoMock.Setup(x => x.GetAllSubjects()).
                ReturnsAsync(() => GetTestDataSubjects());
            _programRepoMock.Setup(x => x.GetProgramById(It.IsAny<int>())).ReturnsAsync(pr);
            // Act
            var results = await _sut.Create(GetTestDataSubjectDtos().First());

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
                ReturnsAsync(() => GetTestDataSubjects());
            _programRepoMock.Setup(x => x.GetProgramById(It.IsAny<int>())).ReturnsAsync(() => null);
            // Act
            // Assert
            Should.Throw<ArgumentException>(async () => await _sut.Create(GetTestDataSubjectDtos().First()));            
        }

        [Fact]
        public async Task DeleteSubject_ShouldReturnData_WhenFound()
        {
            // Arrange
            var subject = GetTestDataSubjects().First();
            _subjectRepoMock.Setup(x => x.GetSubjectById(It.IsAny<int>()))
                .ReturnsAsync(subject);
            _subjectRepoMock.Setup(x => x.RemoveSubject(It.IsAny<Subject>())).Verifiable();
            _subjectRepoMock.Setup(x => x.GetAllSubjects()).
                ReturnsAsync(() => GetTestDataSubjects());

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
                ReturnsAsync(() => GetTestDataSubjects());

            // Act
            var results = await _sut.Delete(new Random().Next());

            // Assert
            results.ShouldBeNull();
        }

        [Fact]
        public async Task UpdateSubject_ShouldReturnData_WhenFound()
        {
            // Arrange
            var pr = CreateEducationPrograms().First();
            var dto = GetTestDataSubjectDtos().Last();
            var subject = GetTestDataSubjects().First();
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
            var pr = CreateEducationPrograms().First();
            var dto = GetTestDataSubjectDtos().First();
            var subject = GetTestDataSubjects().First();
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
            var pr = CreateEducationPrograms().First();
            var dto = GetTestDataSubjectDtos().Last();            
            _subjectRepoMock.Setup(x => x.SaveChangesAsync()).Verifiable();
            _subjectRepoMock.Setup(x => x.GetSubjectById(It.IsAny<int>()))
                .ReturnsAsync(() => null);
            _programRepoMock.Setup(x => x.GetProgramById(It.IsAny<int>())).ReturnsAsync(pr);
            // Act
            var results = await _sut.Update(dto);

            // Assert
            results.ShouldBeNull();
        }



        private static List<Subject> GetTestDataSubjects() => new()
        {
            new Subject
            {
                Id = 1,
                Name = "Test1",
                Programs = CreateEducationPrograms()
            },
            new Subject
            {
                Id = 2,
                Name = "Test2",
                Programs = CreateEducationPrograms()
            },
            new Subject
            {
                Id = 3,
                Name = "Test3",
                Programs = CreateEducationPrograms()
            }
        };

        private static ICollection<EducationProgram> CreateEducationPrograms()
        {
            var programs = new List<EducationProgram>(1)
            {
                new EducationProgram
                {
                    Id = 99,
                    Name = "Grreat",
                    StudyLevel = StudyLevels.Bachelor,
                    Requirements = "blabla",
                    EducationForm = EducationsForms.OnCampus
                }
            };
            return programs;
        }

        private static IEnumerable<CreateUpdateSubjectDto> GetTestDataSubjectDtos()
        {
            return new List<CreateUpdateSubjectDto>(2)
            {
                new CreateUpdateSubjectDto
                {
                    Name = "Test1",
                    ProgramIds = new[] { 99 }
                },
                new CreateUpdateSubjectDto
                {
                    Id = 123,
                    Name = "Test1",
                    ProgramIds = new[] { 99 }
                }
            };
        }
    }
}