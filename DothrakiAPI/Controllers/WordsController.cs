using DothrakiAPI.Repositories;
using DothrakiAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DothrakiAPI.Controllers
{
    // Route attribute: defines path that controller will handle
    // e.g. api/words
    [Route("api/[controller]")]
    // API controller attribute provide behaviour such as automatic model validation and more.
    [ApiController]
    // Controller inherits from controller base
    // which provides many property and method that are useful for handling http requests
    public class WordsController : ControllerBase
    {
        private readonly IWordRepository _wordRepository;
        // Controller needs an instance of word repository to interact with the database
        // Let's inject word repository in the constructor
        public WordsController(IWordRepository wordRepository)
        {
            this._wordRepository = wordRepository;
        }

        // Action method: method to handle specific http requests
        // create one for each http verb you want to handle

        // http get attribute decorated will tell asp.net that method will handle http get requests
        [HttpGet]
        public async Task<IEnumerable<Word>> GetWords()
        {
            // when this method is invoked, asp.net will convert word object to json
            // before returning it to the caller
            return await _wordRepository.Get();
        }

        // This method return a task of action result of a word object
        // Why can't we just return a word object?
        // Task is because the caller will be able to await this method
        // Action result provide the flexibility to return other types like Not Found / Bad Request

        // Put id parameter in endpoint subpath
        [HttpGet("{id}")]
        public async Task<ActionResult<Word>> GetWords(string id)
        {
            return await _wordRepository.Get(id);
        }

        // Because of model binding; asp.net converts json in request payload to a word object
        [HttpPost]
        public async Task<ActionResult<Word>> PostWords([FromBody] Word word)
        {
            // Use create method on repository to insert word in the database
            var newWord = await _wordRepository.Create(word);
            // Returns a create action result which will generate 201 http status code
            return CreatedAtAction(nameof(GetWords), new { id = newWord.Id }, newWord);
        }

        // Update existing word
        [HttpPut]
        public async Task<ActionResult> PutWords(string id, [FromBody] Word word)
        {
            // Make sure id provided in url and payload is the same
            // else return bad request
            if (id != word.Id)
            {
                // 400 - server cannot understand request
                return BadRequest();
            }

            // Invoke update method of repository
            await _wordRepository.Update(word);

            // 201 - request has been processed but has no data
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete (string id)
        {
            var wordToDelete = await _wordRepository.Get(id);
            if (wordToDelete == null)
                return NotFound();

            await _wordRepository.Delete(wordToDelete.Id);
            return NoContent();
        }
    } 
}
