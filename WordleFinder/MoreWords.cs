using System.Text.RegularExpressions;

namespace WordleFinder
{
    internal class MoreWords
    {
        /*
        reply bound fight smack 
        */
        static async Task Main(string[] args)
        {
            List<char> Letters = new List<char>();
            List<string> Words = await LoadWords();

            MoreWords p = new MoreWords();

            Console.Write($"Length:{Words.Count}\n Enter The characters:");
            string i = Console.ReadLine();

            await p.GetAllLetters(Letters, i);

            // Create the regex pattern
            string pattern = $"^[^{string.Join("", Letters)}]*$";

            // Compile the regex
            Regex regex = new Regex(pattern);

            // Check each word
            bool FoundWord = false;
            List<string> FoundWords = new List<string>();

            foreach (var word in Words)
            {
                if (regex.IsMatch(word) && !HasDuplicateCharacters(word))
                {
                    FoundWords.Add(word);
                    Console.WriteLine($"The word '{word}' does not contain any of the specified characters and has no duplicates.");
                }
            }

            if (!FoundWord)
            {
                Console.WriteLine("\nDID Not Find Any Word");
            }
            else
            {
                Console.WriteLine("All Found Words:");
                foreach (var word in FoundWords)
                {
                    Console.WriteLine($"{word}");
                }
            }
               Console.ReadKey();
        }

        // Helper method to check if a word has duplicate characters
        static bool HasDuplicateCharacters(string word)
        {
            var characterSet = new HashSet<char>();
            foreach (char c in word)
            {
                if (!characterSet.Add(c)) // Add returns false if the item already exists
                {
                    return true; // Duplicate found
                }
            }
            return false; // No duplicates
        }

        public async Task GetAllLetters(List<char> Letters, string i)
        {
            foreach (char char_ in i)
            {

                Letters.Add(char_);

                Console.Write(char_);
                await Task.Delay(50);
            }
        }


        public static async Task<List<string>> LoadWords()
        {
            using (HttpClient client = new HttpClient())
            {
                Console.WriteLine("Connecting");
                string content = await client.GetStringAsync("https://cheaderthecoder.github.io/5-Letter-words/words.txt");
                string[] lines = content.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                return lines.ToList();
            }
        }


    }
}
