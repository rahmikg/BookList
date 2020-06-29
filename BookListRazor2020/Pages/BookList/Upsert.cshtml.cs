using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor2020.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor2020.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private ApplicationDBContext _db;

        public UpsertModel(ApplicationDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }


        public async Task<IActionResult> OnGet(int? id)
        {
            Book = new Book();
            if(id == null)
            {   
                //create 
                return Page();
            }
            //update 
            Book = await _db.Book.FirstOrDefaultAsync(u => u.Id == id);
            if(Book == null)
            {
                return NotFound();

            }
            return Page();
           // Book = await _db.Book.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if(Book.Id == 0)
                {
                    _db.Book.Add(Book);
                }
                else
                {
                    _db.Book.Update(Book);
                }

               // var BookFromDb = await _db.Book.FindAsync(Book.Id);
               // BookFromDb.Name = Book.Name;
               // BookFromDb.Author = Book.Author;
               // BookFromDb.ISBN = Book.ISBN;

                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}