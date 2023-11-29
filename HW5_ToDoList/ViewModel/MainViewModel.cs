using HW5_ToDoList.Command;
using HW5_ToDoList.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace HW5_ToDoList.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TaskModel> _tasks;
        private TaskModel _selectedTask;
        private int _selectedStatusIndex;

        public int SelectedStatusIndex
        {
            get { return _selectedStatusIndex; }
            set
            {
                if (_selectedStatusIndex != value)
                {
                    _selectedStatusIndex = value;
                    OnPropertyChanged(nameof(SelectedStatusIndex));
                }
            }
        }
        public ObservableCollection<TaskModel> Tasks
        {
            get { return _tasks; }
            set
            {
                if (_tasks != value)
                {
                    _tasks = value;
                    OnPropertyChanged(nameof(Tasks));
                }
            }
        }
        public TaskModel SelectedTask
        {
            get
            {
                if (_selectedTask == null)
                {
                    _selectedTask = new TaskModel();
                }
                return _selectedTask;
            }
            set
            {
                if (_selectedTask != value)
                {
                    _selectedTask = value;
                    OnPropertyChanged(nameof(SelectedTask));
                }
            }
        }
        public ICommand AddTaskCommand { get; }
        public ICommand ChangeStatusCommand { get; }
        public ICommand DeleteTaskCommand { get; }

        public MainViewModel()
        {
            _tasks = new ObservableCollection<TaskModel>();
            AddTaskCommand = new RelayCommand(AddTask);
            ChangeStatusCommand = new RelayCommand(ChangeStatus);
            DeleteTaskCommand = new RelayCommand(DeleteTask);
        }
        public void AddTask()
        {
            TaskModel newTask = new TaskModel
            {
                Name = SelectedTask.Name,
                Status = SelectedStatusIndex == 0 ? "in progress" : "completed"
            };
            Tasks.Add(newTask);
            SelectedTask.Name = string.Empty;
        }
        public void ChangeStatus()
        {
            if (SelectedTask != null)
            {
                if (SelectedTask.Status == null || SelectedTask.Status.Equals("in progress"))
                {
                    SelectedTask.Status = "completed";
                }
                else
                {
                    SelectedTask.Status = "in progress";
                }
                CollectionViewSource.GetDefaultView(Tasks).Refresh();
            }
        }
        private void DeleteTask()
        {
            if (SelectedTask != null)
            {
                Tasks.Remove(SelectedTask);
                SelectedTask = new TaskModel();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
