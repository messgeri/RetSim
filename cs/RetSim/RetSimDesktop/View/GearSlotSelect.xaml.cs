using RetSim.Items;
using RetSim.Units.UnitStats;
using RetSimDesktop.Model;
using RetSimDesktop.View;
using RetSimDesktop.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Linq;
using System.ComponentModel;

namespace RetSimDesktop
{
    /// <summary>
    /// Interaction logic for GearSlotSelect.xaml
    /// </summary>
    public partial class GearSlotSelect : UserControl
    {
        private static GearSim gearSimWorker = new();
        public delegate void GearSearchEventHandler(GearSlotSelect vm, int slotID, string pattern);
        public event GearSearchEventHandler GearSearched;

        public int SlotID { get; set; }
        public IEnumerable<DisplayGear> SlotList
        {
            get => (IEnumerable<DisplayGear>)GetValue(SlotListProperty);
            set => SetValue(SlotListProperty, value);
        }

        private void SearchBar_ConfirmChange(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GearSearched?.Invoke(this, SlotID, SearchBar.Text);
            }
        }
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*if (EnableRTS.IsChecked != null && EnableRTS.IsChecked.Value == true)
            {
                GearSearched?.Invoke(this, SlotID, SearchBar.Text);
            }*/
            if(DataContext is RetSimUIModel viewmodel)
            {
                Slot slot = (Slot)SlotID;
                viewmodel.GearSlots[slot].FilterItems(SearchBar.Text);
            }
        }

        public static readonly DependencyProperty SlotListProperty = DependencyProperty.Register(
            "SlotList",
            typeof(IEnumerable<DisplayGear>),
            typeof(GearSlotSelect));
        
        public List<Enchant> EnchantList
        {
            get => (List<Enchant>)GetValue(EnchantListProperty);
            set => SetValue(EnchantListProperty, value);
        }

        public static readonly DependencyProperty EnchantListProperty = DependencyProperty.Register(
            "EnchantList",
            typeof(List<Enchant>),
            typeof(GearSlotSelect));

        public DisplayGear SelectedItem
        {
            get => (DisplayGear)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem",
            typeof(DisplayGear),
            typeof(GearSlotSelect));

        public Enchant SelectedEnchant
        {
            get => (Enchant)GetValue(SelectedEnchantProperty);
            set => SetValue(SelectedEnchantProperty, value);
        }

        public static readonly DependencyProperty SelectedEnchantProperty = DependencyProperty.Register(
            "SelectedEnchant",
            typeof(Enchant),
            typeof(GearSlotSelect));


        public GearSlotSelect()
        {
            InitializeComponent();
            this.DataContextChanged += (o, e) =>
            {
                if (DataContext is RetSimUIModel retSimUIModel)
                {
                    if (EnchantList == null)
                    {
                        EnchantComboBox.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        EnchantComboBox.Visibility = Visibility.Visible;
                    }
                    Binding binding = new Binding()
                    {
                        Source = DataContext,
                        Path = new PropertyPath("GearSlots["+(Slot)SlotID+"].ShownItems"),
                        Mode = BindingMode.TwoWay,
                        IsAsync = true
                    };
                    gearSlot.SetBinding(DataGrid.ItemsSourceProperty, binding);

                    Binding text = new Binding()
                    {
                        Source = DataContext,
                        Path = new PropertyPath("GearSlots[" + (Slot)SlotID + "].SearchWord"),
                        Mode = BindingMode.TwoWay,
                    };
                    SearchBar.SetBinding(TextBlock.TextProperty, text);
                }
            };
            
            /*gearSlot.SetBinding(DataGrid.ItemsSourceProperty, new Binding("SlotList")
            {
                Source = this,
                Mode = BindingMode.TwoWay,
                IsAsync = true
            });*/

            gearSlot.SetBinding(DataGrid.SelectedItemProperty, new Binding("SelectedItem")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            });

            EnchantComboBox.SetBinding(ComboBox.ItemsSourceProperty, new Binding("EnchantList")
            {
                Source = this,
                Mode = BindingMode.OneWay,
            });

            EnchantComboBox.SetBinding(ComboBox.SelectedItemProperty, new Binding("SelectedEnchant")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            });

            Binding strBinding = new("Str");
            StrColumn.Binding = strBinding;
            Binding apBinding = new("AP");
            APColumn.Binding = apBinding;
            Binding agiBinding = new("Agi");
            AgiColumn.Binding = agiBinding;
            Binding critBinding = new("Crit");
            CritColumn.Binding = critBinding;
            Binding hitBinding = new("Hit");
            HitColumn.Binding = hitBinding;
            Binding hasteBinding = new("Haste");
            HasteColumn.Binding = hasteBinding;
            Binding expBinding = new("Exp");
            ExpColumn.Binding = expBinding;
            Binding apenBinding = new("ArPen");
            APenColumn.Binding = apenBinding;
            Binding staBinding = new("Stam");
            StaColumn.Binding = staBinding;
            Binding intBinding = new("Intellect");
            IntColumn.Binding = intBinding;
            Binding mp5Binding = new("MP5");
            MP5Column.Binding = mp5Binding;
            Binding spBinding = new("SP");
            SPColumn.Binding = spBinding;
        }

        private void GearSim_Click(object sender, RoutedEventArgs e)
        {
            if (!gearSimWorker.IsBusy && DataContext is RetSimUIModel retSimUIModel)
            {
                retSimUIModel.SimButtonStatus.IsSimButtonEnabled = false;
                gearSimWorker.RunWorkerAsync(new Tuple<RetSimUIModel, IEnumerable<DisplayGear>, int>(retSimUIModel, retSimUIModel.GearSlots[(Slot)SlotID].AllItems, SlotID));
            }
        }

        private void DataGridCell_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGridCell cell)
            {
                if (DataContext is RetSimUIModel retSimUIModel && DataGridRow.GetRowContainingElement(cell).Item is DisplayGear displayGear)
                {
                    if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = "https://tbc.wowhead.com/item=" + displayGear.Item.ID,
                            UseShellExecute = true
                        });
                    }
                    else if (e.ChangedButton == MouseButton.Left && e.ButtonState == MouseButtonState.Pressed)
                    {
                        var column = cell.Column;
                        if (cell.Column.Header.GetType() == typeof(CheckBox) && cell.Content is CheckBox checkBox)
                        {
                            checkBox.IsChecked = !checkBox.IsChecked;
                            e.Handled = true;
                        }
                        else if (column != null && column == Socket1Column || column == Socket2Column || column == Socket3Column)
                        {
                            Socket? selectedSocket = null;

                            if (column == Socket1Column)
                            {
                                selectedSocket = displayGear.Item.Socket1;
                            }
                            else if (column == Socket2Column)
                            {
                                selectedSocket = displayGear.Item.Socket2;
                            }
                            else if (column == Socket3Column)
                            {
                                selectedSocket = displayGear.Item.Socket3;
                            }

                            if (selectedSocket != null)
                            {
                                GemPickerWindow gemPicker;
                                if (selectedSocket.Color == SocketColor.Meta)
                                {
                                    gemPicker = new(RetSim.Data.Items.MetaGems.Values, selectedSocket.SocketedGem, true);
                                }
                                else
                                {
                                    gemPicker = new(RetSim.Data.Items.GemsSorted, selectedSocket.SocketedGem);
                                }

                                retSimUIModel.TooltipSettings.HoverItemID = 0;
                                if (gemPicker.ShowDialog() == true)
                                {
                                    selectedSocket.SocketedGem = gemPicker.SelectedGem;

                                    retSimUIModel.SelectedGear.OnPropertyChanged("");
                                    displayGear.OnPropertyChanged("");
                                    displayGear.RefreshGems();
                                }
                                e.Handled = true;
                            }
                        }
                    }
                    else if (e.ChangedButton == MouseButton.Right && e.ButtonState == MouseButtonState.Pressed)
                    {
                        var column = cell.Column;
                        if (column != null && column == Socket1Column || column == Socket2Column || column == Socket3Column)
                        {
                            bool socketNotNull = false;
                            if (column == Socket1Column && displayGear.Item.Socket1 != null)
                            {
                                displayGear.Item.Socket1.SocketedGem = null;
                                socketNotNull = true;
                            }
                            else if (column == Socket2Column && displayGear.Item.Socket2 != null)
                            {
                                displayGear.Item.Socket2.SocketedGem = null;
                                socketNotNull = true;
                            }
                            else if (column == Socket3Column && displayGear.Item.Socket3 != null)
                            {
                                displayGear.Item.Socket3.SocketedGem = null;
                                socketNotNull = true;
                            }
                            if (socketNotNull)
                            {
                                displayGear.OnPropertyChanged("");
                                retSimUIModel.SelectedGear.OnPropertyChanged("");
                                DataGridCell_MouseEnter(cell, null);
                                return;
                            }
                        }
                        if (gearSlot.SelectedItem == displayGear)
                        {
                            gearSlot.SelectedItem = null;
                            retSimUIModel.SelectedGear.OnPropertyChanged("");
                        }
                    }
                }
            }
        }

        private void DataGridCell_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right && e.ButtonState == MouseButtonState.Released)
            {
                e.Handled = true;
            }
        }

        private void ChkSelectAll_Checked(object sender, RoutedEventArgs e)
        {
            if (DataContext is RetSimUIModel viewmodel)
            {
                Slot slot = (Slot)SlotID;
                foreach (var displayItem in viewmodel.GearSlots[slot].ShownItems)
                {
                    displayItem.EnabledForGearSim = true;
                }
            }
        }

        private void ChkSelectAll_Unchecked(object sender, RoutedEventArgs e)
        {
            if (DataContext is RetSimUIModel viewmodel)
            {
                Slot slot = (Slot)SlotID;
                foreach (var displayItem in viewmodel.GearSlots[slot].ShownItems)
                {
                    displayItem.EnabledForGearSim = false;
                }
            }
        }
        private void DataGridCell_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is DataGridCell cell)
            {
                if (DataContext is RetSimUIModel retSimUIModel && DataGridRow.GetRowContainingElement(cell).Item is DisplayGear displayItem)
                {
                    var column = cell.Column;

                    if (column == Socket1Column && displayItem.Item.Socket1 != null && displayItem.Item.Socket1.SocketedGem != null)
                    {
                        retSimUIModel.TooltipSettings.HoverItemID = displayItem.Item.Socket1.SocketedGem.ID;
                    }
                    else if (column == Socket2Column && displayItem.Item.Socket2 != null && displayItem.Item.Socket2.SocketedGem != null)
                    {
                        retSimUIModel.TooltipSettings.HoverItemID = displayItem.Item.Socket2.SocketedGem.ID;
                    }
                    else if (column == Socket3Column && displayItem.Item.Socket3 != null && displayItem.Item.Socket3.SocketedGem != null)
                    {
                        retSimUIModel.TooltipSettings.HoverItemID = displayItem.Item.Socket3.SocketedGem.ID;
                    }
                    else if (retSimUIModel.TooltipSettings.HoverItemID != displayItem.Item.ID)
                    {
                        retSimUIModel.TooltipSettings.HoverItemID = displayItem.Item.ID;
                    }
                    /*
                    foreach (var item in SlotList)
                    {
                        if (item.Item.Slot == Slot.Finger)
                        {
                            retSimUIModel.TooltipSettings.RingEnchant = SelectedEnchant;
                        }
                        break;
                    }*/
                }
            }
        }

        private void DataGridCell_MouseLeave(object sender, MouseEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                retSimUIModel.TooltipSettings.HoverItemID = 0;
            }
        }
    }
    public class QualityToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var input = (Quality)value;
            return input switch
            {
                Quality.Poor => new SolidColorBrush(Color.FromRgb(157, 157, 157)),
                Quality.Common => new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                Quality.Uncommon => new SolidColorBrush(Color.FromRgb(30, 255, 0)),
                Quality.Rare => new SolidColorBrush(Color.FromRgb(0, 112, 221)),
                Quality.Epic => new SolidColorBrush(Color.FromRgb(163, 53, 238)),
                Quality.Legendary => new SolidColorBrush(Color.FromRgb(255, 128, 0)),
                Quality.Artifact => new SolidColorBrush(Color.FromRgb(230, 204, 128)),
                _ => Brushes.Red,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class SocketBonusConverter : IValueConverter
    {
        private static readonly Dictionary<StatName, string> StatToShortString = new()
        {
            { StatName.Stamina, "Stam" },
            { StatName.Intellect, "Int" },
            { StatName.ManaPer5, "MP5" },
            { StatName.Strength, "Str" },
            { StatName.AttackPower, "AP" },
            { StatName.Agility, "Agi " },
            { StatName.CritRating, "Crit" },
            { StatName.HitRating, "Hit" },
            { StatName.Haste, "Haste" },
            { StatName.ArmorPenetration, "APen" },
            { StatName.SpellPower, "SP" },
            { StatName.Resilience, "Resi" }
        };
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is EquippableItem item && item.Socket1 != null && item.SocketBonus != null)
            {
                foreach (var stat in item.SocketBonus.Keys)
                {
                    if (StatToShortString.ContainsKey(stat))
                        return $"+{item.SocketBonus[stat]} {StatToShortString[stat]}";
                }
            }
            return "";
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
    public class SocketBonusActiveConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is EquippableItem item && item.Socket1 != null && item.IsSocketBonusActive())
            {
                return new SolidColorBrush(Colors.Black);
            }
            return new SolidColorBrush(Colors.LightGray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class StatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((float)value).ToString("0;;' '");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class SocketConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is not Socket socket)
            {
                return "-";
            }

            if (socket.SocketedGem != null)
            {
                return "[" + socket.Color.ToString() + "]";
            }

            return socket.Color.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class SocketToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Socket socket)
            {
                if (socket.SocketedGem != null && MediaMetaData.GemsToIconName.ContainsKey(socket.SocketedGem.ID))
                {
                    return new BitmapImage(new Uri($"pack://application:,,,/Properties/Icons/{MediaMetaData.GemsToIconName[socket.SocketedGem.ID]}"));
                }
                else if (socket.Color == SocketColor.Red)
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Properties/Icons/red_socket.jpg"));
                }
                else if (socket.Color == SocketColor.Blue)
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Properties/Icons/blue_socket.jpg"));
                }
                else if (socket.Color == SocketColor.Yellow)
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Properties/Icons/yellow_socket.jpg"));
                }
                else if (socket.Color == SocketColor.Meta)
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Properties/Icons/meta_socket.jpg"));
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class ItemToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int id)
            {
                if (MediaMetaData.ItemsMetaData.ContainsKey(id))
                {
                    return new BitmapImage(new Uri($"pack://application:,,,/Properties/Icons/{MediaMetaData.ItemsMetaData[id].IconFileName}"));
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class SocketToSocketColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Socket socket && socket.SocketedGem != null)
            {
                if (socket.Color == SocketColor.Meta)
                {
                    return new SolidColorBrush(Colors.DarkGray);
                }
                if (socket.Color == SocketColor.Red)
                {
                    return new SolidColorBrush(Colors.Red);
                }
                if (socket.Color == SocketColor.Blue)
                {
                    return new SolidColorBrush(Colors.Blue);
                }
                if (socket.Color == SocketColor.Yellow)
                {
                    return new SolidColorBrush(Color.FromRgb(181, 181, 0));
                }
            }
            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
