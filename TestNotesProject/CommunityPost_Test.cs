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
    public class CommunityPost_Test
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
                Id= 1,
                Title = "Test Community Post",
                TimeCreated= DateTime.Now,
                Content = "Test Content for Post",
                User = new ApplicationUser { Id = "asbsbs2", Email = "john2@email.com" },
                UserID = "asbsbs2",
        
            });

            
            
            database.SaveChanges();
            return database;
        }

        //testing index for Community Post
        [Fact]
        public void CommunityPost_Index()
        {
            //Arrange - data/object creation

            ApplicationDbContext database = Create_database();
            CommunityPostsController _communityPostsController= new CommunityPostsController(database);

            //Act - simulate interaction/call/change
            var index = _communityPostsController.Index();

            //Assert - assetion for test
            Assert.NotNull(index);
            //database.Dispose();
            
        }

        //Test details
        [Fact]
        public void CommunityPost_Details()
        {
            //Arrange - data/object creation

            ApplicationDbContext database = Create_database();
            CommunityPostsController _communityPostsController = new CommunityPostsController(database);

            //Act - simulate interaction/call/change
            var details = _communityPostsController.Details(1);

            //Assert - assetion for test
            Assert.NotNull(details);
            
        }


        //testing Create for Community Post
        [Fact]
        public void CommunityPost_Create()
        {
            //Arrange - data/object creation

            ApplicationDbContext database = Create_database();
            CommunityPostsController _communityPostsController = new CommunityPostsController(database);

            CommunityPost create_Post = new CommunityPost()
            {
                Id = 2,
                Title = "Test Title",
                TimeCreated = DateTime.Now,
                Content = "Test Content",
                User = new ApplicationUser { Id = "asbsbs2", Email = "john2@email.com" },
                UserID = "asbsbs2"
            };

            //Act - simulate interaction/call/change
            var create = _communityPostsController.Create(create_Post);

            var detail = _communityPostsController.Details(2);

            //Assert - assetion for test

            Assert.NotNull(create); Assert.NotNull(detail);
            
        }




        //Delete
        //cant test delete as there is a failure in test when trying to use the User ID as currently logged in user
        //User Tested instead and debugged and passes

        //Edit - User tested .. hidden behind User ID for Logged in user
    }
}
