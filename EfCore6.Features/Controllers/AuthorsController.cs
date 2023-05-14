using EFCore6.Core.Entities;
using EFCore6.Core.Models;
using EFCore6.Core.Servics.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace EfCore6.Features.Controllers
{

    // One To Many
    //[ApiController]
    [Route("[controller]")]
    public class AuthorsController : ControllerBase
    {
        private IAuthorService AuthorService { get; }

        public AuthorsController(IAuthorService authorService)
        {
            AuthorService = authorService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await AuthorService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ExplicitLoading")]
        public async Task<IActionResult> ExplicitLoading([FromBody] Author author)
        {
            try
            {
                return Ok(await AuthorService.ExplicitLoading(author));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("EnsureDbCreated")]
        public IActionResult GetWeatherForecast()
        {
            try
            {
                AuthorService.EnsureCreated();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddAuthor")]
        public async Task<IActionResult> AddAuthor([FromBody] Author author)
        {
            try
            {
               await AuthorService.Add(author);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateAuthor")]
        public async Task<IActionResult> UpdateAuthor([FromBody] Author author)
        {
            try
            {
                await AuthorService.Update(author);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteAuthorBook")]
        public async Task<IActionResult> DeleteBook([FromBody] Author author)
        {
            try
            {
                await AuthorService.DeleteBook(author);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("AddNewBookUpdateAuthor")]
        public async Task<IActionResult> AddNewBookUpdateAuthor([FromBody] BookAuthorModel bookAuthorModel)
        {
            try
            {
                await AuthorService.AddBookUpdateAuthor(bookAuthorModel.Author, bookAuthorModel.Book);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}