using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Models;

namespace Dictionary.MVVM.ViewModel
{
    public class HomeViewModel : Core.ViewModel
    {
        DictionaryManager dictionaryManager = new DictionaryManager("../../dictionary.json");

        public HomeViewModel()
        {
            ///try to read the json placed in the current folder named "dictionary.json" by initializing a DictionaryModel
            DictionaryManager manager = new DictionaryManager("../../dictionary.json");

            ///add a word
            manager.AddWord(new WordModel { Category = "Noun", Word = "Dog apple 2", Definition = "A dog apple 2" });
        }
    }
}
