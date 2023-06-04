using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NOTES_HDIP.Data;
using NOTES_HDIP.Models;
using X.PagedList.Mvc;
using X.PagedList.Mvc.Core;
using X.PagedList.Web;


namespace NOTES_HDIP.Controllers
{
    public class CommunityPostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommunityPostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        

        // GET: CommunityPosts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CommunityPosts.OrderByDescending(p => p.TimeCreated);
            return View(await applicationDbContext.ToListAsync());
        }


        //search function
        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {

            ViewData["GetCommunityPosts"] = id;

            if (_context.CommunityPosts == null)
            {
                return Problem("No Post of that description found");
            }

            var posts = from p in _context.CommunityPosts select p;
            

            if (!String.IsNullOrEmpty(id))
            {
                posts = posts.Where(p => p.Title!.Contains(id) || p.Content.Contains(id));
            }

            return View(await posts.AsNoTracking().ToListAsync());

        }

        

            // GET: CommunityPosts/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            

            if (id == null || _context.CommunityPosts == null)
            {
                return NotFound();
            }



            var communityPost = await _context.CommunityPosts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (communityPost == null)
            {
                return NotFound();
            }

            return View(communityPost);
        }

        

        // GET: CommunityPosts/Create
        public IActionResult Create()
        {
            //ViewData["UserID"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id");
            return View();
        }

        // POST: CommunityPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,TimeCreated")] CommunityPost communityPost)
        {
            var curretlyLoggedInUserId = HttpContext.User.Claims.ToList()[0].Value;
           
            if (ModelState.IsValid)
            {
                communityPost.UserID = curretlyLoggedInUserId;
                
                _context.Add(communityPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["UserID"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", communityPost.UserID);
            return View(communityPost);
        }

        // GET: CommunityPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var curretlyLoggedInUserId = HttpContext.User.Claims.ToList()[0].Value;
            if (id == null || _context.CommunityPosts == null)
            {
                return NotFound();
            }

            var communityPost = await _context.CommunityPosts.FindAsync(id);
            if (communityPost == null)
            {
                return NotFound();
            }
            if (communityPost.UserID != curretlyLoggedInUserId)
            {
                return RedirectToAction(nameof(Index));
            }
            //ViewData["UserID"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", communityPost.UserID);
            return View(communityPost);
        }

        // POST: CommunityPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,TimeCreated")] CommunityPost communityPost)
        {
            var curretlyLoggedInUserId = HttpContext.User.Claims.ToList()[0].Value;
            

            var time = communityPost.TimeCreated;
            //var OriginalTime = _context.CommunityPosts.FirstOrDefault
            if (id != communityPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    communityPost.UserID = curretlyLoggedInUserId;
                    communityPost.TimeCreated = time;
                    
                    _context.Update(communityPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommunityPostExists(communityPost.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["UserID"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", communityPost.UserID);
            return View(communityPost);
        }

        // GET: CommunityPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var curretlyLoggedInUserId = HttpContext.User.Claims.ToList()[0].Value;
            if (id == null || _context.CommunityPosts == null)
            {
                return NotFound();
            }

            //var applicationDbContext = _context.CommunityPosts;
            var communityPost = await _context.CommunityPosts
                //.Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (communityPost == null)
            {
                return NotFound();
            }
            if (communityPost.UserID != curretlyLoggedInUserId)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(communityPost);
        }

        // POST: CommunityPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CommunityPosts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CommunityPosts'  is null.");
            }
            var communityPost = await _context.CommunityPosts.FindAsync(id);
            if (communityPost != null)
            {
                _context.CommunityPosts.Remove(communityPost);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommunityPostExists(int id)
        {
          return (_context.CommunityPosts?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
