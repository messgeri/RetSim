﻿using RetSim.Items;
using RetSimDesktop.Model;
using RetSimDesktop.ViewModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Linq;

namespace RetSimDesktop
{
    /// <summary>
    /// Interaction logic for GearSelect.xaml
    /// </summary>
    /// 
    public partial class GearSelect : UserControl
    {
        private Dictionary<Slot, List<GearSlotSelect>> SelectorBySlot = new(); //Does it really have to be a list? 
        public Dictionary<Slot, List<DisplayGear>> ShownGear { get; set; }

        public GearSelect()
        {
            InitializeComponent();
            this.DataContextChanged += (o, e) =>
            {
                if (DataContext is RetSimUIModel retSimUIModel)
                {
                    retSimUIModel.SelectedPhases.PropertyChanged += Model_PropertyChanged;
                    Model_PropertyChanged(this, new PropertyChangedEventArgs(""));
                }
            };
            ShownGear = new();
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

            foreach(var slot in SelectorBySlot.Values)
            {
                foreach (var selector in slot)
                {
                    selector.GearSearched += SearchResult;
                }
            }
        }

        private void SearchResult(int slotID, string pattern)
        {
            Slot slot = (Slot)slotID; 
            if(DataContext is RetSimUIModel retSimUIModel)
            {
                List<DisplayGear> list = new List<DisplayGear>();
                if (retSimUIModel.SelectedPhases.Phase1Selected && retSimUIModel.GearByPhases[slot].ContainsKey(1))
                {
                    list.AddRange(retSimUIModel.GearByPhases[slot][1].Where(d => d.Item.Name.Contains(pattern, System.StringComparison.InvariantCultureIgnoreCase)));
                }
                if (retSimUIModel.SelectedPhases.Phase2Selected && retSimUIModel.GearByPhases[slot].ContainsKey(2))
                {
                    list.AddRange(retSimUIModel.GearByPhases[slot][2].Where(d => d.Item.Name.Contains(pattern, System.StringComparison.InvariantCultureIgnoreCase)));
                }
                if (retSimUIModel.SelectedPhases.Phase3Selected && retSimUIModel.GearByPhases[slot].ContainsKey(3))
                {
                    list.AddRange(retSimUIModel.GearByPhases[slot][3].Where(d => d.Item.Name.Contains(pattern, System.StringComparison.InvariantCultureIgnoreCase)));
                }
                if (retSimUIModel.SelectedPhases.Phase4Selected && retSimUIModel.GearByPhases[slot].ContainsKey(4))
                {
                    list.AddRange(retSimUIModel.GearByPhases[slot][4].Where(d => d.Item.Name.Contains(pattern, System.StringComparison.InvariantCultureIgnoreCase)));
                }
                if (retSimUIModel.SelectedPhases.Phase5Selected && retSimUIModel.GearByPhases[slot].ContainsKey(5))
                {
                    list.AddRange(retSimUIModel.GearByPhases[slot][5].Where(d => d.Item.Name.Contains(pattern, System.StringComparison.InvariantCultureIgnoreCase)));
                }
                ShownGear[slot] = list;
                SelectorBySlot[slot][0].SetBinding(GearSlotSelect.SlotListProperty, new Binding("ShownGear[" + slot + "]")
                {
                    Source = this,
                    Mode = BindingMode.OneWay
                });
            }
        }

        private void Model_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                foreach (var slot in SelectorBySlot.Keys)
                {
                    ShownGear[slot] = new();
                    if (retSimUIModel.SelectedPhases.Phase1Selected && retSimUIModel.GearByPhases[slot].ContainsKey(1))
                    {
                        ShownGear[slot].AddRange(retSimUIModel.GearByPhases[slot][1]);
                    }
                    if (retSimUIModel.SelectedPhases.Phase2Selected && retSimUIModel.GearByPhases[slot].ContainsKey(2))
                    {
                        ShownGear[slot].AddRange(retSimUIModel.GearByPhases[slot][2]);
                    }
                    if (retSimUIModel.SelectedPhases.Phase3Selected && retSimUIModel.GearByPhases[slot].ContainsKey(3))
                    {
                        ShownGear[slot].AddRange(retSimUIModel.GearByPhases[slot][3]);
                    }
                    if (retSimUIModel.SelectedPhases.Phase4Selected && retSimUIModel.GearByPhases[slot].ContainsKey(4))
                    {
                        ShownGear[slot].AddRange(retSimUIModel.GearByPhases[slot][4]);
                    }
                    if (retSimUIModel.SelectedPhases.Phase5Selected && retSimUIModel.GearByPhases[slot].ContainsKey(5))
                    {
                        ShownGear[slot].AddRange(retSimUIModel.GearByPhases[slot][5]);
                    }
                    foreach (var itemSelector in SelectorBySlot[slot])
                    {
                        itemSelector.SetBinding(GearSlotSelect.SlotListProperty, new Binding("ShownGear[" + slot + "]")
                        {
                            Source = this,
                            Mode = BindingMode.OneWay
                        });

                        itemSelector.LevelColumn.SortDirection = ListSortDirection.Descending;
                        itemSelector.gearSlot.Items.SortDescriptions.Add(new SortDescription(itemSelector.LevelColumn.SortMemberPath, ListSortDirection.Descending));


                        if (retSimUIModel.EnchantsBySlot.ContainsKey(slot))
                        {
                            itemSelector.SetBinding(GearSlotSelect.EnchantListProperty, new Binding("EnchantsBySlot[" + slot + "]")
                            {
                                Source = DataContext,
                                Mode = BindingMode.OneWay
                            });
                        }
                    }
                }
            }
        }
    }
}
