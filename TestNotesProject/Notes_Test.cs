using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NOTES_HDIP.Controllers;
using NOTES_HDIP.Data;
using NOTES_HDIP.Models;
//using NUnit.Framework;


namespace TestNotesProject
{
    public class Notes_Test
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

            });

            database.Notes.Add(new Note
            {
                Id = 1,
                Name = "Test Note",
                NoteField = "Testing the note",
                NoteSpaceID = 1,
            });

            database.SaveChanges();
            return database;
        }

        //testing in memory data is not null / details method
        [Fact]
        public void Note_Details()
        {
            //Arrange - data/object creation

            ApplicationDbContext database = Create_database();
            NotesController _notescontroller = new NotesController(database);

            //Act - simulate interaction/call/change
            var Note_ID = _notescontroller.Details(1);

            //Assert - assetion for test
            Assert.NotNull(Note_ID);
            
        }

        //testing Note Create Method
        [Fact]
        public async Task Note_Create()
        {
            //Arrange - data/object creation

            ApplicationDbContext database = Create_database();
            NotesController _notescontroller = new NotesController(database);

            Note testNote = new Note()
            {
                Id = 2,
                Name = "Test Note",
                NoteField = "Testing the note",
                NoteSpaceID = 1,
            };

            //Act - simulate interaction/call/change
            database.Notes.Add(testNote);
            database.SaveChanges();
            

            //Assert - assetion for test

            Assert.IsType<Note>(testNote);
        }

        //Delete - Needs to be async otherwise will throw threading error
        [Fact]
        public async Task Note_DeleteAsync()
        {
            //arrange
            ApplicationDbContext database = Create_database();
            NotesController _notescontroller = new NotesController(database);

            //act
            var deletion = await _notescontroller.Delete(2);

            //assert
            Assert.IsType<NotFoundResult>(deletion);
        }

        //testing Index returns object
        [Fact]
        public void Notes_Index()
        {

            //arrange
            ApplicationDbContext database = Create_database();
            NotesController _notescontroller = new NotesController(database);

            //act .. id = 1 for Notespace
            var index = _notescontroller.Index(1);

            //assert
            Assert.NotNull(index);
        }


    }
}
