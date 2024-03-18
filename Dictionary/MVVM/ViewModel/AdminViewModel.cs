using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Dictionary.Core;
using Dictionary.Models;
using Dictionary.MVVM.View;

namespace Dictionary.MVVM.ViewModel
{
    public class AdminViewModel : Core.ViewModel
    {
        public RelayCommand AddWordCommand { get; private set; }
        public RelayCommand RemoveWordCommand { get; private set; }

        public RelayCommand SearchCommand { get; set; }
        public RelayCommand SuggestionSelectedCommand { get; set; }

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
                    UpdateSuggestions(_searchText, _selectedCategory);
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

        private string _definition;
        public string Definition
        {
            get { return _definition; }
            set
            {
                _definition = value;
                OnPropertyChanged();
            }
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                _imageUrl = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _categories;
        public ObservableCollection<string> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCategory;
        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
                UpdateSuggestions(_searchText, _selectedCategory);
            }
        }

        private string _selectedSuggestion;
        public string SelectedSuggestion
        {
            get { return _selectedSuggestion; }
            set
            {
                _selectedSuggestion = value;
                OnPropertyChanged();

                // Update the search text when a suggestion is selected
                SelectedWord = value;
            }
        }

        public DictionaryManager manager;

        // Method to execute when a suggestion is selected
        private void SuggestionSelected(object obj)
        {
            if (obj != null)
            {
                SelectedWord = obj.ToString();
            }
        }

        public void PopulateCategories()
        {
            //Clear the categories
            Categories.Clear();
            Categories.Add("ALL");
            //Get the categories from the dictionary manager
            List<string> categories = manager.GetCategories();
            //Add the categories to the observable collection
            foreach (string c in categories)
            {
                Categories.Add(c);
            }
        }

        private void Search(object obj)
        {
            //Get the word from the textbox
            string word = obj as string;
            //Get the meaning of the word
            if (SelectedCategory == "ALL" || SelectedCategory == null)
            {
                Definition = manager.GetDefinition(word);
            }
            else
            {
                Definition = manager.GetDefinition(word, SelectedCategory);
            }
            //Get the image url of the word
            ImageUrl = manager.GetImageUrl(word);
            if (!File.Exists(ImageUrl))
            {
                ImageUrl = "../../Images/missing.png";
            }
        }

        private void UpdateSuggestions(string word, string selectedCategory)
        {
            //Clear the suggestions
            Suggestions.Clear();
            //Get the suggestions from the dictionary manager
            if (word == null)
            {
                word = "";
            }
            List<string> suggestions = manager.GetSuggestions(word, selectedCategory);
            //Add the suggestions to the observable collection
            foreach (string s in suggestions)
            {
                Suggestions.Add(s);
            }
        }

        public AdminViewModel()
        {
            ///try to read the json placed in the current folder named "dictionary.json" by initializing a DictionaryModel
            manager = new DictionaryManager("../../dictionary.json");

            //Initialize commands
            SearchCommand = new RelayCommand(Search, o => true);
            AddWordCommand = new RelayCommand(AddWord, o => true);
            RemoveWordCommand = new RelayCommand(RemoveWord, o => true);

            SuggestionSelectedCommand = new RelayCommand(SuggestionSelected, o => true);

            //Initialize suggestions
            Suggestions = new ObservableCollection<string>();

            //Initialize categories
            Categories = new ObservableCollection<string>();
            PopulateCategories();
            
        }

        private void AddWord(object sender)
        {
            // Open the add word dialog
            AddWordDialog dialog = new AddWordDialog();
            dialog.DataContext = this;
            dialog.ShowDialog();

        }

        private void RemoveWord(object sender)
        {
            // Open the remove word dialog
            RemoveWordDialog dialog = new RemoveWordDialog();
            dialog.DataContext = this;
            dialog.ShowDialog();
        }
    }
}
