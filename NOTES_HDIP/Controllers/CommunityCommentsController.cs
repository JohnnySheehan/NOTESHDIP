using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NOTES_HDIP.Data;
using NOTES_HDIP.Data.Migrations;
using NOTES_HDIP.Models;

namespace NOTES_HDIP.Controllers
{
    public class CommunityCommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommunityCommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CommunityComments
        public async Task<IActionResult> Index(int id)
        {
            
            var applicationDbContext = _context.CommunityComments.Where(p => p.PostId== id);
            return View(await applicationDbContext.ToListAsync());
        }

        //search
        [HttpGet]
        public async Task<IActionResult> Index(int id, string word)
        {
            var comments1 = _context.CommunityComments.Where(c => c.PostId == id);

            ViewData["GetComments"] = word;

            if (comments1 == null)
            {
                return Problem("No Notespace found");
            }

            if (!String.IsNullOrEmpty(word))
            {
                comments1 = comments1.Where(i => i.Content!.Contains(word));
            }

            return View(await comments1.AsNoTracking().ToListAsync());
        }

        // GET: CommunityComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CommunityComments == null)
            {
                return NotFound();
            }

            var communityComment = await _context.CommunityComments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (communityComment == null)
            {
                return NotFound();
            }

            return View(communityComment);
        }

        // GET: CommunityComments/Create
        public IActionResult Create()
        {

            ViewData["PostId"] = new SelectList(_context.CommunityPosts, "Id", "Title");
            //ViewData["UserID"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id");
            return View();

           
        }

        // POST: CommunityComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,Created,PostId")] CommunityComment communityComment)
        {
            var curretlyLoggedInUserId = HttpContext.User.Claims.ToList()[0].Value;
            
            if (ModelState.IsValid)
            {
                communityComment.UserID = curretlyLoggedInUserId;
                
                _context.Add(communityComment);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "CommunityComments", new { id = communityComment.PostId });
            }
            //ViewData["UserID"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", communityComment.UserID);
            return View(communityComment);
        }

        // GET: CommunityComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CommunityComments == null)
            {
                return NotFound();
            }

            var communityComment = await _context.CommunityComments.FindAsync(id);
            if (communityComment == null)
            {
                return NotFound();
            }
            ViewData["PostId"] = new SelectList(_context.CommunityPosts, "Id", "Title");
            //ViewData["UserID"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id");
            //return View();
            return View(communityComment);
        }

        // POST: CommunityComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,Created,PostId")] CommunityComment communityComment)
        {
            var curretlyLoggedInUserId = HttpContext.User.Claims.ToList()[0].Value;

            var time = communityComment.Created;
            if (id != communityComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    communityComment.UserID = curretlyLoggedInUserId;
                    communityComment.Created = time;

                    _context.Update(communityComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommunityCommentExists(communityComment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "CommunityComments", new { id = communityComment.PostId });
                
            }
           // ViewData["PostId"] = new SelectList(_context.CommunityPosts, "Id", "Title");
            return View(communityComment);
        }

        // GET: CommunityComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CommunityComments == null)
            {
                return NotFound();
            }

            var communityComment = await _context.CommunityComments
                
                .FirstOrDefaultAsync(m => m.Id == id);
            if (communityComment == null)
            {
                return NotFound();
            }

            return View(communityComment);
        }

        // POST: CommunityComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CommunityComments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CommunityComments'  is null.");
            }
            var communityComment = await _context.CommunityComments.FindAsync(id);
            if (communityComment != null)
            {
                _context.CommunityComments.Remove(communityComment);
            }
            
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "CommunityComments", new { id = communityComment.PostId });
        }

        private bool CommunityCommentExists(int id)
        {
          return (_context.CommunityComments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
