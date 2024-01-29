using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Study_Abroad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.Data
{
    public class StudyAbroadContext : IdentityDbContext
    { 
        public StudyAbroadContext(DbContextOptions<StudyAbroadContext> options) : base(options)
        {
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Campus> Campuses { get; set; }
        //public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ProgramLevel> ProgramLevels { get; set; }
        public DbSet<CourseIntake> CourseIntakes { get; set; }
        public DbSet<Disipline> Disiplines { get; set; }
        public DbSet<ContactUs> Contacts { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<StudentProfile> StudentProfiles { get; set; }
        public DbSet<EducationDetail> EducationDetails { get; set; }
        public DbSet<GreTest> GreTest { get; set; }
        public DbSet<GmatTest> GmatTests { get; set; }
        public DbSet<LanguageTest> LanguageTests { get; set; }
        public DbSet<ApplicationDetails> ApplicationDetails { get; set; }
        public DbSet<AppRequairement> Requairement { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<AssessmentEducationalBackground> AssessmentEducationalBackgrounds { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<State>()
        //        .HasOne(q => q.Countries)
        //.WithOne()
        //.IsRequired();
        //}
    }
}
