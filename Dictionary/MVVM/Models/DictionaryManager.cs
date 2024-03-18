using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dictionary.Models
{
    public class DictionaryManager
    {
        List<WordModel> words { get; set;} = new List<WordModel>();

        public DictionaryManager(string jsonPath)
        {
            string jsonString = System.IO.File.ReadAllText(jsonPath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<WordModel> dictionaryData = JsonSerializer.Deserialize<List<WordModel>>(jsonString, options);
            if (dictionaryData != null)
            {
                words = dictionaryData;
                System.Console.WriteLine("Dictionary loaded successfully");
                System.Console.WriteLine("Number of words: " + words.Count);
            }
        }

        public WordModel GetWord(string word, string category)
        {
            word = word.ToLower();
            category = category.ToLower();
            foreach (WordModel w in words)
            {
                if (w.Word.ToLower() == word && w.Category.ToLower() == category)
                {
                    return w;
                }
            }
            System.Console.WriteLine("Word not found");
            return null;
        }

        public void AddWord(WordModel word)
        {
            word.Word = word.Word.ToLower();
            word.Category = word.Category.ToLower();
            foreach (WordModel w in words)
            {
                if (w.Word == word.Word && w.Category == word.Category)
                {
                    System.Console.WriteLine("Word already exists");
                    return;
                }
            }
            words.Add(word);
            string jsonString = JsonSerializer.Serialize(words);
            System.IO.File.WriteAllText("../../dictionary.json", jsonString);
        }

        public void AddWord(string word, string category, string definition, string imageurl)
        {
            word = word.ToLower();
            category = category.ToLower();
            foreach (WordModel w in words)
            {
                if (w.Word == word && w.Category == category)
                {
                    System.Console.WriteLine("Word already exists");
                    return;
                }
            }
            words.Add(new WordModel { Word = word, Category = category, Definition = definition, ImageUrl = imageurl });
            string jsonString = JsonSerializer.Serialize(words);
            System.IO.File.WriteAllText("../../dictionary.json", jsonString);
        }

        public void RemoveWord(WordModel word)
        {
            if (!words.Contains(word))
            {
                System.Console.WriteLine("Word not found");
                return;
            }
            words.Remove(word);
            string jsonString = JsonSerializer.Serialize(words);
            System.IO.File.WriteAllText("../../dictionary.json", jsonString);
        }

        public void RemoveWord(string word, string category)
        {
            WordModel wordModel = GetWord(word, category);
            if (wordModel == null)
            {
                System.Console.WriteLine("Word not found");
                return;
            }
            words.Remove(wordModel);
            string jsonString = JsonSerializer.Serialize(words);
            System.IO.File.WriteAllText("../../dictionary.json", jsonString);
        }

        public void ChangeWord(WordModel word, string newWord, string newCategory, string newDefinition)
        {
            if (!words.Contains(word))
            {
                System.Console.WriteLine("Word not found");
                return;
            }
            word.Word = newWord;
            word.Category = newCategory;
            word.Definition = newDefinition;
            string jsonString = JsonSerializer.Serialize(words);
            System.IO.File.WriteAllText("../../dictionary.json", jsonString);
        }

        public string GetDefinition(string word)
        {
            foreach (WordModel w in words)
            {
                if (w.Word == word)
                {
                    return w.Definition;
                }
            }
            return "Word not found";
        }

        public string GetDefinition(string word, string category)
        {
            if (category == "ALL")
            {
                return GetDefinition(word);
            }
            foreach (WordModel w in words)
            {
                if (w.Word == word && w.Category == category)
                {
                    return w.Definition;
                }
            }
            return "Word not found";
        }

        public string GetCategory(string word)
        {
            foreach (WordModel w in words)
            {
                if (w.Word == word)
                {
                    return w.Category;
                }
            }
            return "Word not found";
        }

        public string GetImageUrl(string word)
        {
            foreach (WordModel w in words)
            {
                if (w.Word == word)
                {
                    return w.ImageUrl;
                }
            }
            return "Word not found";
        }

        public List<string> GetSuggestions(string word)
        {
            List<string> suggestions = new List<string>();
            foreach (WordModel w in words)
            {
                if (w.Word.StartsWith(word))
                {
                    suggestions.Add(w.Word);
                }
            }
            return suggestions;
        }

        public List<string> GetSuggestions(string word, string category)
        {
            List<string> suggestions = new List<string>();
            if(category == "ALL" || category == null)
            {
                return GetSuggestions(word);
            }
            foreach (WordModel w in words)
            {
                if (w.Word.StartsWith(word) && w.Category == category)
                {
                    suggestions.Add(w.Word);
                }
            }
            return suggestions;
        }

        public List<string> GetCategories()
        {
            List<string> categories = new List<string>();
            foreach (WordModel w in words)
            {
                if (!categories.Contains(w.Category))
                {
                    categories.Add(w.Category);
                }
            }
            return categories;
        }

        public List<WordModel> GetRandomWords(int number)
        {
            List<WordModel> randomWords = new List<WordModel>();
            Random random = new Random();
            for (int i = 0; i < number; i++)
            {
                randomWords.Add(words[random.Next(words.Count)]);
            }
            return randomWords;
        }
    }
}
