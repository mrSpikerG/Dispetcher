using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Dispetcher
{
    public class ProcessAdvanced : ViewModelBase, INotifyPropertyChanged
    {
        public ProcessAdvanced(Process process)
        {
            this.Process = process;
            PerformanceCounter cpuCounter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
            double cpu = cpuCounter.NextValue();
            cpu = cpuCounter.NextValue();
            this.CPU = $"{Math.Round(cpu)}%";

            PerformanceCounter ramCounter = new PerformanceCounter("Process", "Working Set", process.ProcessName);
            double ram = ramCounter.NextValue() / (1000 * 1000);
            this.RAM = $"{Math.Round(ram, 1)} Mb";

            this.getCPU();
            this.getRAM();
        }


        private Process process;
        public Process Process
        {
            get => process;
            set => SetProperty<Process>(ref process, value);
        }

        private string cpu;
        private string ram;
        public string CPU
        {
            get => cpu;
            set => SetProperty<string>(ref cpu, value);
        }
        public string RAM
        {
            get => ram;
            set => SetProperty<string>(ref ram, value);
        }

        public void getCPU()
        {
            DispatcherTimer ProcTimerCPU = new DispatcherTimer();
            ProcTimerCPU.Tick += CPUgetCustomEvent;
            ProcTimerCPU.Interval = new TimeSpan(0, 0, 1);
            ProcTimerCPU.Start();
        }

        private void CPUgetCustomEvent(object sender, EventArgs e)
        {
            PerformanceCounter cpuCounter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
            double cpu = cpuCounter.NextValue();
            cpu = cpuCounter.NextValue();
            this.CPU = $"{Math.Round(cpu)}Мб/c";
        }

        public void getRAM()
        {
            DispatcherTimer ProcTimerRAM = new DispatcherTimer();
            ProcTimerRAM.Tick += RAMgetCustomEvent;
            ProcTimerRAM.Interval = new TimeSpan(0, 0, 1);
            ProcTimerRAM.Start();
        }

        private void RAMgetCustomEvent(object sender, EventArgs e)
        {
            PerformanceCounter ramCounter = new PerformanceCounter("Process", "Working Set", process.ProcessName);
            double ram = ramCounter.NextValue() / (1000 * 1000);
            this.RAM = $"{Math.Round(ram, 1)} Mb";
        }

       

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
