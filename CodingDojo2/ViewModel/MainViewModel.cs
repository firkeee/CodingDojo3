using GalaSoft.MvvmLight;
using Shared.BaseModels;
using Shared.Interfaces;
using Simulation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CodingDojo2.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private Simulator sim;
        private List<ItemVm> modelItems = new List<ItemVm>();
        public ObservableCollection<ItemVm> SensorList { get; set; }
        public ObservableCollection<ItemVm> ActorList { get; set; }
        public ObservableCollection<string> ModeSelectionList { get; private set; }

        private string currentTime = DateTime.Now.ToLocalTime().ToShortTimeString();
        public string  CurrentTime
        {
            get { return currentTime; }
            set { currentTime = value; RaisePropertyChanged(); }
        }

        private string currentDate = DateTime.Now.ToLocalTime().ToShortDateString();

        public string CurrentDate
        {
            get { return currentDate; }
            set { currentDate = value; RaisePropertyChanged(); }
        }

        public MainViewModel()
        {
            ActorList = new ObservableCollection<ItemVm>();
            SensorList = new ObservableCollection<ItemVm>();

            ModeSelectionList = new ObservableCollection<string>();
            //ModeSelectionList.Add("Enabled");
            //ModeSelectionList.Add("Disabled");
            //ModeSelectionList.Add("Auto");
            //ModeSelectionList.Add("Manual");

            foreach (var item in Enum.GetNames(typeof(SensorModeType)))
            {
                ModeSelectionList.Add(item);
            }
            foreach (var item in Enum.GetNames(typeof(ModeType)))
            {
                ModeSelectionList.Add(item);

            }

            if (!IsInDesignMode)
            {
                LoadData();
                //timer.Start();
            }
        }

        private void LoadData()
        {
            sim = new Simulator(modelItems);

            foreach(var item in modelItems)
            {
                if (item.ItemType.Equals(typeof(ISensor)))
                    SensorList.Add(item);
                else if (item.ItemType.Equals(typeof(IActuator)))
                    ActorList.Add(item);
            }
        }
    }
}