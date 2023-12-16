using JsonDb;
using System;
using System.Linq;

namespace JsonDbTest
{
    class Program
    {
        static void Main()
        {
            // Read table (6 MB of English words). Reads file if exists and serializes to objects.
            var words = new JsonDb<Word>($"Database/Words.json");

            // Search through 370 099 words. Find "*awkward*".
            foreach (var awkward in words.Where(n => n.W.Contains("awkward")).OrderByDescending(n => n.W).Reverse())
                Console.WriteLine($"Awkward: {awkward.W} ");

            // Find "lose*".
            var losers = from word in words
                         where word.W.StartsWith("lose")
                         orderby word.W ascending
                         select word;

            // Show "lose*" search results.
            foreach (var lose in losers)
                Console.WriteLine($"Lose: {lose.W} ");

            // Reads file if exists and serializes to objects.
            var items = new JsonDb<Item>($"Database/Items.json");

            // Clear previous items.
            items.Clear();

            // Generate 10 000 items.
            for (int i = 1; i < 10001; i++)
            {
                items.Add(new Item()
                {
                    Id = i,
                    Name = "Item " + i,
                    Content = new string[] { "C 1", "C 2", "C 3" }
                });
            }

            // Access database item directly and update.
            items.Where(i => i.Name.Equals("Item 666")).FirstOrDefault().Content = new string[] { "C 1", "C 2", "C 3", "C 4" };

            // Write to disk.
            items.Save();
        }
    }
}
