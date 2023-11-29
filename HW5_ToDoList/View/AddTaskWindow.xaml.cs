using HW5_ToDoList.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HW5_ToDoList.View
{
    public partial class AddTaskWindow : Window
    {
        private MainViewModel _mainViewModel;

        public AddTaskWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            statusComboBox.SelectedIndex = 0;
            statusComboBox.ItemsSource = new List<string> { "in progress", "completed" };
            _mainViewModel = mainViewModel;
            DataContext = _mainViewModel;
            this.Closed += AddTaskWindow_Closed;
        }
        private void AddTaskWindow_Closed(object sender, EventArgs e)
        {
            _mainViewModel.SelectedStatusIndex = statusComboBox.SelectedIndex;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _mainViewModel.AddTask();
            Close();
        }
    }
}
