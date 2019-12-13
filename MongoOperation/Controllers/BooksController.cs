using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoOperation.Business;
using MongoOperation.Models;

namespace MongoOperation.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController: ControllerBase
    {
        private readonly BookBusiness _bookBusiness;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookBusiness"></param>
        public BooksController(BookBusiness bookBusiness)
        {
            _bookBusiness = bookBusiness;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Book>> Get() =>
            _bookBusiness.Get();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Book> Get(string id)
        {
            var book = _bookBusiness.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            _bookBusiness.Create(book);

            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookIn"></param>
        /// <returns></returns>
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Book bookIn)
        {
            var book = _bookBusiness.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookBusiness.Update(id, bookIn);

            return NoContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _bookBusiness.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookBusiness.Delete(book.Id);

            return NoContent();
        }
    }
}