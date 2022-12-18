using EducationSearchV3.Models;
using EducationSearchV3.Models.Dtos.Requests;
using EducationSearchV3.Models.Enums;
using LanguagesEnum = EducationSearchV3.Models.Enums.Languages;

namespace EducationSearchV3Test
{
    internal static class EducationPrograms
    {
        internal static List<EducationProgram> GetEducationPrograms()
        {
            var programs = new List<EducationProgram>(2)
            {
                new EducationProgram
                {
                    Id = 99,
                    Name = "Grreat",
                    StudyLevel = StudyLevels.Bachelor,
                    Requirements = "blabla",
                    EducationForm = EducationsForms.OnCampus,
                    Languages = GetLanguages(),
                    HighSchool = new HighSchool { Id = 1, Name = "TestSchool"},
                    Subjects = GetSubjects()

                },
                new EducationProgram
                {
                    Id = 2424,
                    Name = "Blalba",
                    StudyLevel = StudyLevels.PhD,
                    Requirements = "all free",
                    EducationForm = EducationsForms.Blended,
                    Languages = GetLanguages(),
                    HighSchool = new HighSchool { Id = 1, Name = "TestSchool"},
                    Subjects = GetSubjects()
                }
            };
            return programs;
        }

        private static List<Language> GetLanguages() => new(2)
        {
            new Language
            {
                Id = 1,
                Name = LanguagesEnum.English

            },
            new Language
            {
                Id = 2,
                Name = LanguagesEnum.Italian
            }
        };

        private static List<HighSchool> GetHighSchools() => new(1)
        {
            new HighSchool
            {
                Id = 1,
                Name = "TestSchool"
            }
        };

        private static List<Subject> GetSubjects() => new(3)
        {
            new Subject
            {
                Id = 1,
                Name = "Test1"                
            },
            new Subject
            {
                Id = 2,
                Name = "Test2"                
            },
            new Subject
            {
                Id = 3,
                Name = "Test3"                
            }
        };
    }

    internal static class Subjects
    {
        internal static List<Subject> GetSubjects() => new()
        {
            new Subject
            {
                Id = 1,
                Name = "Test1",
                Programs = GetEducationPrograms()
            },
            new Subject
            {
                Id = 2,
                Name = "Test2",
                Programs = GetEducationPrograms()
            },
            new Subject
            {
                Id = 3,
                Name = "Test3",
                Programs = GetEducationPrograms()
            }
        };

        private static List<EducationProgram> GetEducationPrograms()
        {
            var programs = new List<EducationProgram>(2)
            {
                new EducationProgram
                {
                    Id = 99,
                    Name = "Grreat",
                    StudyLevel = StudyLevels.Bachelor,
                    Requirements = "blabla",
                    EducationForm = EducationsForms.OnCampus                 
                },
                new EducationProgram
                {
                    Id = 2424,
                    Name = "Blalba",
                    StudyLevel = StudyLevels.PhD,
                    Requirements = "all free",
                    EducationForm = EducationsForms.Blended
                }
            };
            return programs;
        }

        internal static IEnumerable<CreateUpdateSubjectDto> GetSubjectDtos()
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

    internal static class Languages
    {
        internal static List<Language> GetLanguages() => new(2)
        {
            new Language
            {
                Id = 1,
                Name = LanguagesEnum.English,
                Countries = GetCountries(),
                EducationPrograms = GetEducationPrograms()
            },
            new Language
            {
                Id = 2,
                Name = LanguagesEnum.Italian,
                Countries = GetCountries(),
                EducationPrograms = GetEducationPrograms()
            }
        };

        private static List<Country> GetCountries() => new(2)
        {
            new Country
            {
                Id = 1,
                Name = "Test1"                
            },
            new Country
            {
                Id = 2,
                Name = "Test2"               
            }
        };

        private static List<EducationProgram> GetEducationPrograms()
        {
            var programs = new List<EducationProgram>(2)
            {
                new EducationProgram
                {
                    Id = 99,
                    Name = "Grreat",
                    StudyLevel = StudyLevels.Bachelor,
                    Requirements = "blabla",
                    EducationForm = EducationsForms.OnCampus
                },
                new EducationProgram
                {
                    Id = 2424,
                    Name = "Blalba",
                    StudyLevel = StudyLevels.PhD,
                    Requirements = "all free",
                    EducationForm = EducationsForms.Blended
                }
            };
            return programs;
        }
    }

    internal static class Countries
    {
        internal static List<Country> GetCountries() => new(2)
        {
            new Country
            {
                Id = 1,
                Name = "Test1",
                Languages = GetLanguages(),
                HighSchools = CreateHighSchools()
            },
            new Country
            {
                Id = 2,
                Name = "Test2",
                Languages = GetLanguages(),
                HighSchools = CreateHighSchools()
            }
        };

        private static List<Language> GetLanguages() => new(2)
        {
            new Language
            {
                Id = 1,
                Name = LanguagesEnum.English

            },
            new Language
            {
                Id = 2,
                Name = LanguagesEnum.Italian
            }
        };

        private static List<HighSchool> CreateHighSchools() => new(1)
        {
            new HighSchool
            {
                Id = 1,
                Name = "TestSchool"
            }
        };
    }
}
