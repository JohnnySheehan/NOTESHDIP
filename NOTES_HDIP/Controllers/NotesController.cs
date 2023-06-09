﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NOTES_HDIP.Data;
using NOTES_HDIP.Models;

namespace NOTES_HDIP.Controllers
{
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Notes
        public async Task<IActionResult> Index(int id)
        {
            var applicationDbContext = _context.Notes.Where(n => n.NoteSpaceID == id);
            return View(await applicationDbContext.ToListAsync());
        }


        //search
        [HttpGet]
        public async Task<IActionResult> Index(int id, string word)
        {
            var notes1 = _context.Notes.Where(n => n.NoteSpaceID == id);

            ViewData["GetNoteSpaces"] = word;

            if (notes1 == null)
            {
                return Problem("No Notespace found");
            }

            if (!String.IsNullOrEmpty(word))
            {
                notes1 = notes1.Where(i => i.Name!.Contains(word));
            }

            return View(await notes1.AsNoTracking().ToListAsync());
        }


        // GET: Notes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Notes == null)
            {
                return NotFound();
            }

            var note = await _context.Notes
                .Include(n => n.NoteSpaces)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // GET: Notes/Create
        public IActionResult Create()
        {
            var curretlyLoggedInUserId = HttpContext.User.Claims.ToList()[0].Value;
            var notespaces = from p in _context.NoteSpaces.Where(n => n.UserID == curretlyLoggedInUserId)
                         select p;

            ViewData["NoteSpaceID"] =
                new SelectList(_context.NoteSpaces.Where(n => n.UserID == curretlyLoggedInUserId), "Id", "Name");
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NoteField,NoteSpaceID")] Note note)
        {
            


            var curretlyLoggedInUserId = HttpContext.User.Claims.ToList()[0].Value;
            var NoteSpaces = _context.Notes.Where(i => i.NoteSpaces.User.Id == curretlyLoggedInUserId);

            if (ModelState.IsValid)
            {
                
                _context.Add(note);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index(note.NoteSpaceID)));
                return RedirectToAction("Index", "Notes", new { id = note.NoteSpaceID });
            }
            ViewData["NoteSpaceID"] = new SelectList(_context.NoteSpaces, "Id", "Id", note.NoteSpaceID);
            return View(note);

            
        }

        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Notes == null)
            {
                return NotFound();
            }

            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            ViewData["NoteSpaceID"] = new SelectList(_context.NoteSpaces, "Id", "Id", note.NoteSpaceID);
            return View(note);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,NoteField,NoteSpaceID")] Note note)
        {
            if (id != note.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(note);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(note.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Notes", new { id = note.NoteSpaceID });
            }
            ViewData["NoteSpaceID"] = new SelectList(_context.NoteSpaces, "Id", "Id", note.NoteSpaceID);
            return View(note);
        }

        // GET: Notes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Notes == null)
            {
                return NotFound();
            }

            var note = await _context.Notes
                .Include(n => n.NoteSpaces)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Notes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Notes'  is null.");
            }
            var note = await _context.Notes.FindAsync(id);
            if (note != null)
            {
                _context.Notes.Remove(note);
            }
            
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "Notes", new { id = note.NoteSpaceID });
        }

        private bool NoteExists(int id)
        {
          return _context.Notes.Any(e => e.Id == id);
        }
    }
}
