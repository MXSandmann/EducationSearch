using EducationSearchV3.Models.Dtos.Responses;
using EducationSearchV3.Repositories;
using Moq;

namespace EducationSearchV3Test.Repositories.Subjects
{
    internal static class MockSubjectRepository
    {
        public static Mock<ISubjectRepository> GetInstance()
        {
            var subjests = GetAllSubjects();
            var mockRepo = new Mock<ISubjectRepository>();
            mockRepo.Setup(x => x.GetAll())
                .ReturnsAsync(() =>
                {
                    return subjests;
                });
            return mockRepo;
        }

        private static List<GetSubjectDto> GetAllSubjects() => new()
        {
            new GetSubjectDto
            {
                Id = 1,
                Name = "Test1",
                Programs = null
            },
            new GetSubjectDto
            {
                Id = 2,
                Name = "Test2",
                Programs = null
            },
            new GetSubjectDto
            {
                Id = 3,
                Name = "Test3",
                Programs = null
            }
        };            
    }
}
