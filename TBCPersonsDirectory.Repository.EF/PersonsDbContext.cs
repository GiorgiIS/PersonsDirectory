using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TBCPersonsDirectory.Core;

namespace TBCPersonsDirectory.Repository.EF
{
    public class PersonsDbContext : DbContext
    {
        public PersonsDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<City> Citys { get; set; }
        public DbSet<PersonConnection> PersonConnections { get; set; }
        public DbSet<PhoneNumberType> PhoneNumberTypes { get; set; }
        public DbSet<ConnectionType> ConnectionType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new List<City> {
                        new City(1, "Tbilisi"),
                        new City(2, "Batumi"),
                        new City(3, "Kutaisi"),
                        new City(4, "Foti"),
                });

            modelBuilder.Entity<ConnectionType>().HasData(
                  new List<ConnectionType> {
                    new ConnectionType(1, "College"),
                    new ConnectionType(2, "Familiar"),
                    new ConnectionType(3, "Relative"),
                    new ConnectionType(4, "Other"),
                  });

            modelBuilder.Entity<PhoneNumberType>().HasData(
               new List<PhoneNumberType> {
                    new PhoneNumberType(1, "Mobile"),
                    new PhoneNumberType(2, "Office"),
                    new PhoneNumberType(3, "Home"),
               });

            modelBuilder.Entity<Gender>().HasData(
               new List<Gender> {
                    new Gender(1, "Male"),
                    new Gender(2, "Female"),
               });

            modelBuilder.Entity<Person>().HasData(
                new List<Person> {
                 new Person(){
                     Id = 1,
                     CityId = 1,
                     BirthDate = DateTime.Now.AddYears(-25),
                     FirstName = "Kaka",
                     LastName = "Kuku",
                     ImageUrl = "NO_IMAGE",
                     GenderId = 1,
                     PrivateNumber = "12345678911",
                 },
                   new Person(){
                     Id = 2,
                     CityId = 1,
                     BirthDate = DateTime.Now.AddYears(-25),
                     FirstName = "Paolo",
                     LastName = "Maldini",
                     ImageUrl = "NO_IMAGE",
                     GenderId = 1,
                     PrivateNumber = "06345678911"
                 },
                    new Person(){
                     Id = 3,
                     CityId = 1,
                     BirthDate = DateTime.Now.AddYears(-28),
                     FirstName = "არაგორნ",
                     LastName = "ელენდიელი",
                     ImageUrl = "NO_IMAGE",
                     GenderId = 1,
                     PrivateNumber = "01457876911"
                 },
                     new Person(){
                     Id = 4,
                     CityId = 1,
                     BirthDate = DateTime.Now.AddYears(-26),
                     FirstName = "ვიქტორ",
                     LastName = "ჰიუგო",
                     ImageUrl = "NO_IMAGE",
                     GenderId = 1,
                     PrivateNumber = "11566378911"
                 },
                      new Person(){
                     Id = 5,
                     CityId = 1,
                     BirthDate = DateTime.Now.AddYears(-23),
                     FirstName = "ვარდან",
                     LastName = "მამეკონიანი",
                     ImageUrl = "NO_IMAGE",
                     GenderId = 1,
                     PrivateNumber = "12366278911"
                 }
                });

            modelBuilder.Entity<PersonPhoneNumber>().HasData(
                new List<PersonPhoneNumber> {
                         new PersonPhoneNumber(1, 1, "111111111", 1),
                         new PersonPhoneNumber(2, 1, "222222222", 2),
                          new PersonPhoneNumber(3, 2, "333333333", 2),
                         new PersonPhoneNumber(4, 2, "55555555", 1)
                     });

            modelBuilder.Entity<PersonConnection>().HasData(
                new List<PersonConnection>
                {
                    new PersonConnection(1,1,2,1),
                     new PersonConnection(2,1,3,1),
                     new PersonConnection(3,1,4,2),
                     new PersonConnection(4,2,1,2),
                     new PersonConnection(5,2,3,1),
                     new PersonConnection(6,3,1,1)
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
