using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NOTES_HDIP.Controllers;
using NOTES_HDIP.Data;
using NOTES_HDIP.Models;
//using NUnit.Framework;
using System.Threading.Tasks;

namespace TestNotesProject
{

    public class NotespaceUnitTests
    {
        public ApplicationDbContext Create_database()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()


                .UseInMemoryDatabase(databaseName: "NotesDatabase")
                .Options;
            var database = new ApplicationDbContext(options);

            // seeding database

            database.NoteSpaces.Add(new NoteSpace
            {
                Id = 1,
                User = new ApplicationUser { Id = "asbsbs1", Email = "john@email.com" },
                UserID = "asbsbs1",
                Name = "TestNoteSpace",
                Description = "Description for testing",
                Priority = Priority.High,
                Notes = null

            }) ;
            
            database.SaveChanges();
            return database;
        }


        //testing in memory data is not null
        [Fact]
        public void NoteSpace_AssertNotNull()
        {
            //Arrange - data/object creation
            
            ApplicationDbContext database = Create_database();
            NoteSpacesController _notespacecontroller = new NoteSpacesController(database);

            //Act - simulate interaction/call/change
            var ns_ID = _notespacecontroller.Details(1);

            //Assert - assetion for test
            Assert.NotNull(ns_ID);

        }


        //testing index method returns object
        [Fact]
        public void NoteSpace_Index()
        {
            
            // Arrange
            ApplicationDbContext database = Create_database();
            NoteSpacesController _notespacecontroller = new NoteSpacesController(database);
            
            // Act
            var index = _notespacecontroller.Index();
            
            //Asert
            Assert.NotNull(index);
        }

        //testing details method returns object
        [Fact]
        public void NoteSpace_Details()
        {

            // Arrange
            ApplicationDbContext database = Create_database();
            NoteSpacesController _notespacecontroller = new NoteSpacesController(database);
            NoteSpace testNoteSpace = new NoteSpace();
            {
                testNoteSpace.Id = 200;
                testNoteSpace.Name = "Test 1";
                testNoteSpace.Priority = Priority.Low;
                testNoteSpace.Notes = null;
                testNoteSpace.Description = "Test for creation 100";
                testNoteSpace.User = new ApplicationUser { Id = "asbsbs2", Email = "john2@email.com" };
                testNoteSpace.UserID = "asbsbs2";
            };

            // Act
            var details = _notespacecontroller.Details(200);

            //Asert
            Assert.NotNull(details);
        }


        //test create Notespace
        [Fact]
        public void Notespace_Create()
        {
            //Arrange - data/object creation

            ApplicationDbContext database = Create_database();
            NoteSpacesController _notespacecontroller = new NoteSpacesController(database);

            NoteSpace testNoteSpace = new NoteSpace();
            {
                testNoteSpace.Id = 100;
                testNoteSpace.Name = "Test 1";
                testNoteSpace.Priority = Priority.Low;
                testNoteSpace.Notes = null;
                testNoteSpace.Description = "Test for creation 100";
                testNoteSpace.User = new ApplicationUser { Id = "asbsbs2", Email = "john2@email.com" };
                testNoteSpace.UserID = "asbsbs2";
            };
            //Act - simulate interaction/call/change
            var create = _notespacecontroller.Create(testNoteSpace);
            

            //Assert - assetion for test
            Assert.NotNull(create);
            Assert.IsType<NoteSpace>(testNoteSpace);
        }


        //Delete - Needs to be async otherwise will throw threading error
        [Fact]
        public async Task NoteSpace_DeleteAsync()
        {
            //arrange
            ApplicationDbContext database = Create_database();
            NoteSpacesController _notespacecontroller = new NoteSpacesController(database);
            //var NoteSpace = _notespacecontroller.Details(1);

            //act
            var deletion = await _notespacecontroller.Delete(2);

            //assert
            Assert.IsType<NotFoundResult>(deletion);
        }

        /*[Fact]
        public async Task NoteSpace_Edit()
        {
            ApplicationDbContext database = Create_database();
            var NewSpace = new NoteSpace()
            {
                Id= 5,
                User = new ApplicationUser { Id = "asbsbs1", Email = "john@email.com" },
                UserID = "asbsbs1",
                Name = "TestNoteSpace",
                Description = "Description for testing",
                Priority = Priority.High,
                Notes = null
            };

            //ApplicationDbContext database = Create_database();
            NoteSpacesController _notespacecontroller = new NoteSpacesController(database);

            
            //Act
            var create = _notespacecontroller.Create(NewSpace);

            
            
            var edit = await _notespacecontroller.Edit(5);
            //var edit1 = await _notespacecontroller.Edit(200);

            //Assert
            Assert.NotNull(edit);
            Assert.IsType<ViewResult>(edit);

            //Assert.NotNull(edit1);
            
        }
        */
    }
}