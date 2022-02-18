﻿using RetSim.Items;
using System.ComponentModel;

namespace RetSimDesktop.Model
{
    public class DisplayGear : INotifyPropertyChanged
    {
        private float dps;
        private bool enabledForGearSim;
        private EquippableItem item;

        public float DPS
        {
            get { return dps; }
            set
            {
                dps = value;
                OnPropertyChanged(nameof(DPS));
            }
        }
        public bool EnabledForGearSim
        {
            get { return enabledForGearSim; }
            set
            {
                enabledForGearSim = value;
                OnPropertyChanged(nameof(EnabledForGearSim));
            }
        }

        public EquippableItem Item
        {
            get { return item; }
            set
            {
                item = value;
                OnPropertyChanged(nameof(Item));
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
