using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dictionary.Models
{
    internal class DictionaryManager
    {
        List<WordModel> words = new List<WordModel>();

        public DictionaryManager(string jsonPath)
        {
            string jsonString = System.IO.File.ReadAllText(jsonPath);
            words = JsonSerializer.Deserialize<List<WordModel>>(jsonString);
        }
    }
}
