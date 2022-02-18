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
    public partial class WeaponSelect : UserControl
    {
        private Dictionary<WeaponType, GearSlotSelect> SelectorByType = new();
        public Dictionary<WeaponType, List<DisplayGear>> ShownWeapons { get; set; }
        public List<DisplayGear> AllShownWeapons { get; set; }

        public WeaponSelect()
        {
            InitializeComponent();
            this.DataContextChanged += (o, e) =>
            {
                if (DataContext is RetSimUIModel retSimUIModel)
                {
                    retSimUIModel.SelectedPhases.PropertyChanged += Model_PropertyChanged;
                }
            };
            ShownWeapons = new();
            AllShownWeapons = new();
            SelectorByType.Add(WeaponType.Sword, SwordSelect);
            SelectorByType.Add(WeaponType.Mace, MaceSelect);
            SelectorByType.Add(WeaponType.Axe, AxeSelect);
            SelectorByType.Add(WeaponType.Polearm, PolearmSelect);

            foreach(var selector in SelectorByType.Keys)
            {
                SelectorByType[selector].SetBindingParameters((int)selector, true);
            }
        }

        private void Model_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                foreach (var slot in SelectorByType.Keys)
                {
                    var shownGear = new List<DisplayGear>();
                    if (retSimUIModel.SelectedPhases.Phase1Selected && retSimUIModel.WeaponsByPhases[slot].ContainsKey(1))
                    {
                        shownGear.AddRange(retSimUIModel.WeaponsByPhases[slot][1]);
                    }
                    if (retSimUIModel.SelectedPhases.Phase2Selected && retSimUIModel.WeaponsByPhases[slot].ContainsKey(2))
                    {
                        shownGear.AddRange(retSimUIModel.WeaponsByPhases[slot][2]);
                    }
                    if (retSimUIModel.SelectedPhases.Phase3Selected && retSimUIModel.WeaponsByPhases[slot].ContainsKey(3))
                    {
                        shownGear.AddRange(retSimUIModel.WeaponsByPhases[slot][3]);
                    }
                    if (retSimUIModel.SelectedPhases.Phase4Selected && retSimUIModel.WeaponsByPhases[slot].ContainsKey(4))
                    {
                        shownGear.AddRange(retSimUIModel.WeaponsByPhases[slot][4]);
                    }
                    if (retSimUIModel.SelectedPhases.Phase5Selected && retSimUIModel.WeaponsByPhases[slot].ContainsKey(5))
                    {
                        shownGear.AddRange(retSimUIModel.WeaponsByPhases[slot][5]);
                    }
                    shownGear.Reverse();
                    retSimUIModel.Weapons[slot].AllItems = shownGear;
                }
            }
        }
    }
}
