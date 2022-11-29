using EducationSearchV3.Models;
using EducationSearchV3.Repositories;
using Moq;

namespace EducationSearchV3Test.Repositories.Subjects
{
    internal static class MockSubjectRepository
    {
        public static Mock<ISubjectRepository> GetInstanceWithSubjects()
        {
            var subjests = GetAllSubjects();
            var mockRepo = new Mock<ISubjectRepository>();
            mockRepo.Setup(x => x.GetAllSubjects())
                .ReturnsAsync(() =>
                {
                    return subjests;
                });
            return mockRepo;
        }

        public static Mock<ISubjectRepository> GetInstanceWithoutSubjects()
        {            
            var mockRepo = new Mock<ISubjectRepository>();
            mockRepo.Setup(x => x.GetAllSubjects())
                .ReturnsAsync(() =>
                {
                    return null;
                });
            return mockRepo;
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
    }
}
