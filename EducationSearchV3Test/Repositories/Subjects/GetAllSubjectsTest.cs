using EducationSearchV3.Repositories;
using Moq;
using Shouldly;

namespace EducationSearchV3Test.Repositories.Subjects
{
    public class GetAllSubjectsTest
    {
        private readonly Mock<ISubjectRepository> _repositoryMock;

        public GetAllSubjectsTest()
        {
            _repositoryMock = MockSubjectRepository.GetInstance();
        }

        [Fact]
        public async Task GetAllSubjectsWithoutPrograms()
        {
            // Arrange


            // Act
            var results = await _repositoryMock.Object.GetAll();

            // Assert
            results!.ShouldAllBe(x => x.Programs == null);
        }
    }
}