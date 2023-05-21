using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NOTES_HDIP.Controllers;
using NOTES_HDIP.Data;
using NOTES_HDIP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNotesProject
{
    public class CommunityComment_Test
    {
        public ApplicationDbContext Create_database()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()


                .UseInMemoryDatabase(databaseName: "NotesDatabase")
                .Options;
            var database = new ApplicationDbContext(options);

            // seeding database

            database.CommunityPosts.Add(new CommunityPost
            {
                Id = 1,
                Title = "Test Community Post",
                TimeCreated = DateTime.Now,
                Content = "Test Content for Post",
                User = new ApplicationUser { Id = "asbsbs2", Email = "john2@email.com" },
                UserID = "asbsbs2",

            });

            database.CommunityComments.Add(new CommunityComment
            {
                Id = 1,
                Content = "Comment test",
                PostId= 1,
                Created= DateTime.Now,
                UserID = "asbsbs2"
            });

            database.SaveChanges();
            return database;
        }

        //testing index for Community Post
        [Fact]
        public void CommunityComment_Index_NotNull()
        {
            //Arrange - data/object creation

            ApplicationDbContext database = Create_database();
            CommunityCommentsController _communityCommentsController = new CommunityCommentsController(database);

            //Act - simulate interaction/call/change
            var index = _communityCommentsController.Index();

            //Assert - assetion for test
            Assert.NotNull(index);
            //database.Dispose();
        }

        //Test Community Post Details
        [Fact]
        public void CommunityComment_Details_AssertNotNull()
        {
            //Arrange - data/object creation

            ApplicationDbContext database = Create_database();
            CommunityCommentsController _communityCommentsController = new CommunityCommentsController(database);

            //Act - simulate interaction/call/change
            var details = _communityCommentsController.Details(1);

            //Assert - assetion for test
            Assert.NotNull(details);
        }

        //testing Create for Community Post
        [Fact]
        public void CommunityComment_AssertCreateisnotNull()
        {
            //Arrange - data/object creation

            ApplicationDbContext database = Create_database();
            CommunityCommentsController _communityCommentsController = new CommunityCommentsController(database);

            CommunityComment create_Post = new CommunityComment()
            {
                Id = 1,
                Content = "Comment test",
                PostId = 1,
                Created = DateTime.Now,
                UserID = "asbsbs2"
            };

            //Act - simulate interaction/call/change
            var create = _communityCommentsController.Create(create_Post);

            var detail = _communityCommentsController.Details(2);

            //Assert - assetion for test

            Assert.NotNull(create); Assert.NotNull(detail);
        }

    }
}
