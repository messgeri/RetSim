using RetSim.Items;
using RetSimDesktop.Model;
using RetSimDesktop.ViewModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace RetSimDesktop
{
    /// <summary>
    /// Interaction logic for GearSelect.xaml
    /// </summary>
    /// 
    public partial class GearSelect : UserControl
    {
        private Dictionary<Slot, List<GearSlotSelect>> SelectorBySlot = new();

        public GearSelect()
        {
            InitializeComponent();
            this.DataContextChanged += (o, e) =>
            {
                if (DataContext is RetSimUIModel retSimUIModel)
                {
                    retSimUIModel.SelectedPhases.PropertyChanged += Model_PropertyChanged;
                }
            };
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                SelectorBySlot.Add(Slot.Head, new() { HeadSelect });
                SelectorBySlot.Add(Slot.Neck, new() { NeckSelect });
                SelectorBySlot.Add(Slot.Shoulders, new() { ShouldersSelect });
                SelectorBySlot.Add(Slot.Back, new() { BackSelect });
                SelectorBySlot.Add(Slot.Chest, new() { ChestSelect });
                SelectorBySlot.Add(Slot.Wrists, new() { WristSelect });
                SelectorBySlot.Add(Slot.Hands, new() { HandsSelect });
                SelectorBySlot.Add(Slot.Waist, new() { WaistSelect });
                SelectorBySlot.Add(Slot.Legs, new() { LegsSelect });
                SelectorBySlot.Add(Slot.Feet, new() { FeetSelect });
                SelectorBySlot.Add(Slot.Finger, new() { Finger1Select, Finger2Select });
                SelectorBySlot.Add(Slot.Trinket, new() { Trinket1Select, Trinket2Select });
                SelectorBySlot.Add(Slot.Relic, new() { RelicSelect });
            }
        }

        public void SwitchToSlotSelection(int slot)
        {
            GearTabs.SelectedIndex = (slot + 1) % 16;
        }

        private void Model_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                foreach (var slot in SelectorBySlot.Keys)
                {
                    var shownGear = new List<DisplayGear>();
                    if (retSimUIModel.SelectedPhases.Phase1Selected && retSimUIModel.GearByPhases[slot].ContainsKey(1))
                    {
                        shownGear.AddRange(retSimUIModel.GearByPhases[slot][1]);
                    }
                    if (retSimUIModel.SelectedPhases.Phase2Selected && retSimUIModel.GearByPhases[slot].ContainsKey(2))
                    {
                        shownGear.AddRange(retSimUIModel.GearByPhases[slot][2]);
                    }
                    if (retSimUIModel.SelectedPhases.Phase3Selected && retSimUIModel.GearByPhases[slot].ContainsKey(3))
                    {
                        shownGear.AddRange(retSimUIModel.GearByPhases[slot][3]);
                    }
                    if (retSimUIModel.SelectedPhases.Phase4Selected && retSimUIModel.GearByPhases[slot].ContainsKey(4))
                    {
                        shownGear.AddRange(retSimUIModel.GearByPhases[slot][4]);
                    }
                    if (retSimUIModel.SelectedPhases.Phase5Selected && retSimUIModel.GearByPhases[slot].ContainsKey(5))
                    {
                        shownGear.AddRange(retSimUIModel.GearByPhases[slot][5]);
                    }
                    shownGear.Reverse();
                    retSimUIModel.GearSlots[slot].AllItems = shownGear;
                }
            }
        }
    }
}
