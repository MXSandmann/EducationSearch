using EducationSearchV3.Models;
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
        public async Task GetAll_ShouldReturnSubject()
        {
            // Arrange                       
            _subjectRepoMock.Setup(x => x.GetAllSubjects()).
                ReturnsAsync(() => GetAllSubjects());
            // Act
            var result = await _sut.GetAll();

            // Assert
            result!.ToList().Count.ShouldBe(3);
            result!.ShouldAllBe(x => x.Programs == Enumerable.Empty<string>());
            
        }

        [Fact]
        public async Task GetAll_ShouldReturnSubject_WithDependents()
        {
            // Arrange
            var subjects = GetAllSubjectsWithDependents();
            var name = subjects.First().Programs!.First().Name;
            _subjectRepoMock.Setup(x => x.GetAllSubjects()).
                ReturnsAsync(() => GetAllSubjectsWithDependents());
            // Act
            var result = await _sut.GetAll();

            // Assert
            result!.ToList().Count.ShouldBe(1);            
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
               

        private static List<Subject> GetAllSubjects() => new()
        {
            new Subject
            {
                Id = 1,
                Name = "Test1",
                Programs = null
            },
            new Subject
            {
                Id = 2,
                Name = "Test2",
                Programs = null
            },
            new Subject
            {
                Id = 3,
                Name = "Test3",
                Programs = null
            }
        };

        private static List<Subject> GetAllSubjectsWithDependents() => new()
        {            
            new Subject
            {
                Id = 99,
                Name = "Test4",
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
    }
}