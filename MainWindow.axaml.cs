using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using JetBrains.Annotations;

namespace AvaloniaScrollToSelected
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new Context();
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }

    public class Context : INotifyPropertyChanged, ICommand
    {
        public ObservableCollection<int> List { get; } = new();

        public string CurrentText { get; set; } = "";

        public ICommand GoTo => this;
        
        public int selectedItem;
        public int SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        public Context()
        {
            for (int i = 0; i < 1000; ++i)
                List.Add(i);
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            if (int.TryParse(CurrentText, out var number))
                SelectedItem = number;
        }

        public event EventHandler? CanExecuteChanged;
    }
}