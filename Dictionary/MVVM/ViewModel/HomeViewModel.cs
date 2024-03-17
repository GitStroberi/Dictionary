using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Dictionary.Models;
using System.Collections.ObjectModel;
using Dictionary.Core;

namespace Dictionary.MVVM.ViewModel
{
    public class HomeViewModel : Core.ViewModel
    {
        public RelayCommand SearchCommand { get; set; }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    UpdateSuggestions(_searchText);
                }
            }
        }

        private ObservableCollection<string> _suggestions;
        public ObservableCollection<string> Suggestions
        {
            get { return _suggestions; }
            set
            {
                _suggestions = value;
                OnPropertyChanged();
            }
        }

        private string _selectedWord;
        public string SelectedWord
        {
            get { return _selectedWord; }
            set
            {
                _selectedWord = value;
                OnPropertyChanged();
            }
        }

        DictionaryManager manager;
        public HomeViewModel()
        {
            ///try to read the json placed in the current folder named "dictionary.json" by initializing a DictionaryModel
            manager = new DictionaryManager("../../dictionary.json");

            //Initialize commands
            SearchCommand = new RelayCommand(Search, o => true);

            //Initialize suggestions
            Suggestions = new ObservableCollection<string>();
        }

        private void Search(object obj)
        {
            //Get the word from the textbox
            string word = obj as string;
            //Get the meaning of the word
            string definition = manager.GetDefinition(word);
            //If the meaning is not found, show a message box
            if (definition == null)
            {
                MessageBox.Show("Meaning not found");
            }
            else
            {
                //Show the meaning in a message box
                MessageBox.Show(definition);
            }
        }

        private void UpdateSuggestions(string word)
        {
            //Clear the suggestions
            Suggestions.Clear();
            //Get the suggestions from the dictionary manager
            List<string> suggestions = manager.GetSuggestions(word);
            //Add the suggestions to the observable collection
            foreach (string s in suggestions)
            {
                Suggestions.Add(s);
            }
        }
    }
}
