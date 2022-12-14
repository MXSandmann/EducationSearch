// <auto-generated />
using EducationSearchV3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EducationSearchV3.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221126161315_update entities 2")]
    partial class updateentities2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CountryLanguage", b =>
                {
                    b.Property<int>("CountriesId")
                        .HasColumnType("int");

                    b.Property<int>("LanguagesId")
                        .HasColumnType("int");

                    b.HasKey("CountriesId", "LanguagesId");

                    b.HasIndex("LanguagesId");

                    b.ToTable("CountryLanguage");
                });

            modelBuilder.Entity("EducationProgramLanguage", b =>
                {
                    b.Property<int>("EducationProgramsId")
                        .HasColumnType("int");

                    b.Property<int>("LanguagesId")
                        .HasColumnType("int");

                    b.HasKey("EducationProgramsId", "LanguagesId");

                    b.HasIndex("LanguagesId");

                    b.ToTable("EducationProgramLanguage");
                });

            modelBuilder.Entity("EducationProgramSubject", b =>
                {
                    b.Property<int>("ProgramsId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectsId")
                        .HasColumnType("int");

                    b.HasKey("ProgramsId", "SubjectsId");

                    b.HasIndex("SubjectsId");

                    b.ToTable("EducationProgramSubject");
                });

            modelBuilder.Entity("EducationSearchV3.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries", (string)null);
                });

            modelBuilder.Entity("EducationSearchV3.Models.EducationProgram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EducationForm")
                        .HasColumnType("int");

                    b.Property<int>("HighSchoolId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Requirements")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudyLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HighSchoolId");

                    b.ToTable("EducationPrograms", (string)null);
                });

            modelBuilder.Entity("EducationSearchV3.Models.HighSchool", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("HighSchools", (string)null);
                });

            modelBuilder.Entity("EducationSearchV3.Models.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Languages", (string)null);
                });

            modelBuilder.Entity("EducationSearchV3.Models.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Subjects", (string)null);
                });

            modelBuilder.Entity("CountryLanguage", b =>
                {
                    b.HasOne("EducationSearchV3.Models.Country", null)
                        .WithMany()
                        .HasForeignKey("CountriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EducationSearchV3.Models.Language", null)
                        .WithMany()
                        .HasForeignKey("LanguagesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EducationProgramLanguage", b =>
                {
                    b.HasOne("EducationSearchV3.Models.EducationProgram", null)
                        .WithMany()
                        .HasForeignKey("EducationProgramsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EducationSearchV3.Models.Language", null)
                        .WithMany()
                        .HasForeignKey("LanguagesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EducationProgramSubject", b =>
                {
                    b.HasOne("EducationSearchV3.Models.EducationProgram", null)
                        .WithMany()
                        .HasForeignKey("ProgramsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EducationSearchV3.Models.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EducationSearchV3.Models.EducationProgram", b =>
                {
                    b.HasOne("EducationSearchV3.Models.HighSchool", "HighSchool")
                        .WithMany("Programs")
                        .HasForeignKey("HighSchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HighSchool");
                });

            modelBuilder.Entity("EducationSearchV3.Models.HighSchool", b =>
                {
                    b.HasOne("EducationSearchV3.Models.Country", "Country")
                        .WithMany("HighSchools")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("EducationSearchV3.Models.Country", b =>
                {
                    b.Navigation("HighSchools");
                });

            modelBuilder.Entity("EducationSearchV3.Models.HighSchool", b =>
                {
                    b.Navigation("Programs");
                });
#pragma warning restore 612, 618
        }
    }
}
