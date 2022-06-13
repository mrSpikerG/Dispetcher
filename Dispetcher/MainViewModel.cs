using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dispetcher
{
    public class MainViewModel : INotifyPropertyChanged
    {

        private ProcessAdvanced selectedModel;
        public ProcessAdvanced SelectedModel {
            get => this.selectedModel;
            set
            {
                this.selectedModel = value;
                OnPropertyChanged("SelectedModel");
            }
        }
        public ObservableCollection<ProcessAdvanced> ProcessCollection { get; set; }

        public MainViewModel()
        {
            //SelectedModel.ProcessName
            this.ProcessCollection = new ObservableCollection<ProcessAdvanced>();
            this.ProcessCollection = this.getProcesses();
        //    this.ProcessCollection.Add(new ProcessAdvanced("test"));
            //this.ProcessCollection = this.getProcesses();
        }

        //получение процессов
        private ObservableCollection<ProcessAdvanced> getProcesses()
        {
            ObservableCollection<ProcessAdvanced> TEMP = new ObservableCollection<ProcessAdvanced>();

            foreach (var item in Process.GetProcesses())
            {
                try {
                    TEMP.Add(new ProcessAdvanced(item));
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
            return TEMP;
        }

        private RelayCommand removeCommand;

        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ?? (removeCommand = new RelayCommand(() =>
                {
                        SelectedModel.Process.Kill();
                        ProcessCollection.Remove(SelectedModel);
                }));
            }
            set { }
        }

        public event PropertyChangedEventHandler PropertyChanged;
       
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


    }
}
