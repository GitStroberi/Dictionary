using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Dictionary.Core;
using Dictionary.Models;

namespace Dictionary.MVVM.ViewModel
{
    public class GameViewModel : Core.ViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DictionaryManager _manager;
        private Random _random;
        private List<WordModel> _words;
        private int _currentIndex;

        public GameViewModel()
        {
            _manager = new DictionaryManager("../../dictionary.json");
            _random = new Random();
            _words = _manager.GetRandomWords(5); // Get 5 random words from the dictionary
            _currentIndex = 0;
            IsRunning = true;

            LoadCurrentWord(); // Load the first word
        }

        private void LoadCurrentWord()
        {
            if (_currentIndex < _words.Count)
            {
                // Load the current word, picking randomly either the image or the definition
                if (_random.Next(2) == 0)
                {
                    CurrentDefinition = _words[_currentIndex].Definition;
                    CurrentImage = null;
                }
                else
                {
                    if (!File.Exists(_words[_currentIndex].ImageUrl))
                    {
                        CurrentImage = "../../Images/missing.png";
                    }
                    else
                    {
                        CurrentImage = _words[_currentIndex].ImageUrl;
                    }
                    CurrentDefinition = null;
                }
            }
            else
            {
                // Game Over
                CurrentDefinition = "Game Over";
                CurrentImage = null;
                GuessInput = "";
            }
        }

        private string _currentDefinition;
        public string CurrentDefinition
        {
            get => _currentDefinition;
            set
            {
                _currentDefinition = value;
                OnPropertyChanged(nameof(CurrentDefinition));
            }
        }

        private string _currentImage;
        public string CurrentImage
        {
            get => _currentImage;
            set
            {
                _currentImage = value;
                OnPropertyChanged(nameof(CurrentImage));
            }
        }

        private string _guessInput;
        public string GuessInput
        {
            get => _guessInput;
            set
            {
                _guessInput = value;
                OnPropertyChanged(nameof(GuessInput));
            }
        }

        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                OnPropertyChanged(nameof(Score));
            }
        }

        private string _result;
        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        public RelayCommand NextCommand => new RelayCommand(Next);

        public RelayCommand RestartCommand => new RelayCommand(Restart);

        private void Restart(object parameter)
        {
            // Reset game state
            _words = _manager.GetRandomWords(5);
            _currentIndex = 0;
            Score = 0;
            LoadCurrentWord();
            Result = "";
            IsGameOver = false;
            IsRunning = true;
        }

        private bool _isGameOver;
        public bool IsGameOver
        {
            get => _isGameOver;
            set
            {
                _isGameOver = value;
                OnPropertyChanged(nameof(IsGameOver));
            }
        }

        private bool isRunning;

        public bool IsRunning
        {
            get => isRunning;
            set
            {
                isRunning = value;
                OnPropertyChanged(nameof(IsRunning));
            }
        }

        private void Next(object parameter)
        {
            if (_currentIndex + 1 < _words.Count)
            {
                if (GuessInput.ToLower() == _words[_currentIndex].Word.ToLower())
                {
                    Score++; // Increment score if the guess is correct
                    Result = "Correct!";
                }
                else
                {
                    Result = "Incorrect! The correct answer is: " + _words[_currentIndex].Word;
                }

                _currentIndex++;
                LoadCurrentWord(); // Load the next word
            }
            else
            {
                // Game Over
                CurrentDefinition = "Game Over";
                CurrentImage = null;
                GuessInput = "";
                IsGameOver = true;
                IsRunning = false;
            }
        }
    }
}