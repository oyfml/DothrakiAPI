using DothrakiAPI.Data;
using DothrakiAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DothrakiAPI.Repositories
{
    public class WordRepository : IWordRepository
    {
        private readonly WordContext _context;

        // Repository will query the database using the word context
        // We inject the context through the constructor
        public WordRepository(WordContext context)
        {
            _context = context;
        }

        // Create method
        public async Task<Word> Create(Word word)
        {
            // Use add method of DB set to add new instance of word class
            _context.Words.Add(word);
            // Save changes async method will insert data into database
            await _context.SaveChangesAsync();

            return word;
        }

        // Delete method
        public async Task Delete(string id)
        {
            // Use the remove method of DB set
            var wordToDelete = await _context.Words.FindAsync(id);
            _context.Words.Remove(wordToDelete);
            // Register changes to database
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Word>> Get()
        {
            // Fetches all the words from the database
            return await _context.Words.ToListAsync();
        }

        // The holder get method takes an id parameter
        public async Task<Word> Get(string id)
        {
            // Use find byidasync method to get a single word
            return await _context.Words.FindAsync(id);
        }

        // Update method
        public async Task Update(Word word)
        {
            // Update state of entity
            _context.Entry(word).State = EntityState.Modified;
            // Update entity in database
            await _context.SaveChangesAsync();
        }
    }
}
// Once repository is defined, we need to register it with the dependency injection system.
// Must add scope in update configure service method in Startup.cs
