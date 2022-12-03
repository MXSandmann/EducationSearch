﻿using EducationSearchV3.Models;
using EducationSearchV3.Models.Enums;
using EducationSearchV3.Repositories.Contracts;
using EducationSearchV3.Services;
using EducationSearchV3.Services.Contracts;
using Moq;
using Shouldly;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var countries = GetTestDataCountries();
            var name = countries.First().HighSchools!.First().Name;
            _countryRepoMock.Setup(x => x.GetAllCountries()).
                ReturnsAsync(() => GetTestDataCountries());
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
                .ReturnsAsync(GetTestDataCountries().First());

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

        //[Fact]
        //public async Task CreateCountry_ShouldSuccess_WhenNotFound()
        //{
        //    // Arrange            
        //    _countryRepoMock.Setup(x => x.HasCountryWithName(It.IsAny<string>()))
        //        .ReturnsAsync(false);
        //    _countryRepoMock.Setup(x => x.AddCountry(It.IsAny<Country>()))
        //        .Verifiable();
        //    _countryRepoMock.Setup(x => x.GetAllCountries())
        //        .ReturnsAsync(GetTestDataCountries());

        //    // Act
        //    var result = await _sut.Create()


        //    // Assert
        //}

        private static List<Country> GetTestDataCountries() => new(2)
        {
            new Country
            {
                Id = 1,
                Name = "Test1",
                Languages = CreateLanguages(),
                HighSchools = CreateHighSchools()
            },
            new Country
            {
                Id = 2,
                Name = "Test2",
                Languages = CreateLanguages(),
                HighSchools = CreateHighSchools()
            }
        };

        private static List<Language> CreateLanguages() => new(2)
        {
            new Language
            {
                Id = 1,
                Name = Languages.English
            },
            new Language
            {
                Id = 2,
                Name = Languages.Italian
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
