using RetSim.Items;
using RetSim.Units.UnitStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RetSimDesktop.Model
{
    public class DisplayGear : BindableBase
    {
        private float dps;
        private bool enabledForGearSim;
        private EquippableItem item;
        private SolidColorBrush quality;
        private BitmapImage image;
        private SolidColorBrush socket1;
        private SolidColorBrush socket2;
        private SolidColorBrush socket3;
        private BitmapImage gem1;
        private BitmapImage gem2;
        private BitmapImage gem3;
        private string socketBonus;

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
                InitializeItem();
            }
        }

        public SolidColorBrush Quality
        {
            get => quality;
            set => SetProperty(ref quality, value); 
        }

        public BitmapImage Image
        {
            get => image;
            set => SetProperty(ref image, value);    
        }

        public SolidColorBrush Socket1
        {
            get => socket1;
            set => SetProperty(ref socket1, value);
        }
        
        public SolidColorBrush Socket2
        {
            get => socket2;
            set => SetProperty(ref socket2, value);
        }

        public SolidColorBrush Socket3
        {
            get => socket3;
            set => SetProperty(ref socket3, value); 
        }

        public BitmapImage Gem1
        {
            get => gem1;
            set => SetProperty(ref gem1, value);
        }

        public BitmapImage Gem2
        {
            get => gem2;
            set => SetProperty(ref gem2, value);
        }

        public BitmapImage Gem3
        {
            get => gem3;
            set => SetProperty(ref gem3, value);
        }

        public string SocketBonus
        {
            get => socketBonus;
            set => SetProperty(ref socketBonus, value);
        }

        public void RefreshGems()
        {
            Gem1 = SocketToImageConverter(Item.Socket1);
            Gem2 = SocketToImageConverter(Item.Socket2);
            Gem3 = SocketToImageConverter(Item.Socket3);
        }

        private void InitializeItem()
        {
            switch (Item.Quality)
            {
                case RetSim.Items.Quality.Poor:
                    Quality = new SolidColorBrush(Color.FromRgb(157, 157, 157));
                    break;
                case RetSim.Items.Quality.Common:
                    Quality = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    break;
                case RetSim.Items.Quality.Uncommon:
                    Quality = new SolidColorBrush(Color.FromRgb(30, 255, 0));
                    break;
                case RetSim.Items.Quality.Rare:
                    Quality = new SolidColorBrush(Color.FromRgb(0, 112, 221));
                    break;
                case RetSim.Items.Quality.Epic:
                    Quality = new SolidColorBrush(Color.FromRgb(163, 53, 238));
                    break;
                case RetSim.Items.Quality.Legendary:
                    Quality = new SolidColorBrush(Color.FromRgb(255, 128, 0));
                    break;
                case RetSim.Items.Quality.Artifact:
                    Quality = new SolidColorBrush(Color.FromRgb(230, 204, 128));
                    break;
            }

            if (MediaMetaData.ItemsMetaData.ContainsKey(Item.ID))
                Image = new BitmapImage(new Uri($"pack://application:,,,/Properties/Icons/{MediaMetaData.ItemsMetaData[Item.ID].IconFileName}"));
            else
                Image = null;

            Socket1 = SocketColorBrushConverter(Item.Socket1);
            Socket2 = SocketColorBrushConverter(Item.Socket2);
            Socket3 = SocketColorBrushConverter(Item.Socket3);

            Gem1 = SocketToImageConverter(Item.Socket1);
            Gem2 = SocketToImageConverter(Item.Socket2);
            Gem3 = SocketToImageConverter(Item.Socket3);

            SocketBonus = SocketBonusConverter();
        }

        private SolidColorBrush SocketColorBrushConverter(Socket value)
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

        private BitmapImage SocketToImageConverter(Socket value)
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

        private string SocketBonusConverter()
        {           
            List<string> bonuses = new List<string>();
            if (Item.SocketBonus == null) return "";
            var stat = Item.SocketBonus.Keys.First();
            switch (stat)
            {
                    case StatName.Stamina:
                        return ($"+{Item.SocketBonus[stat]} Stam");
                    case StatName.Intellect:
                        return ($"+{Item.SocketBonus[stat]} Int");
                    case StatName.ManaPer5:
                        return($"+{Item.SocketBonus[stat]} MP5");
                    case StatName.Strength:
                        return ($"+{Item.SocketBonus[stat]} Str");
                    case StatName.AttackPower:
                        return ($"+{Item.SocketBonus[stat]} AP");
                    case StatName.Agility:
                        return ($"+{Item.SocketBonus[stat]} Agi");
                    case StatName.CritRating:
                        return ($"+{Item.SocketBonus[stat]} Crit");
                    case StatName.HitRating:
                        return ($"+{Item.SocketBonus[stat]} Hit");
                    case StatName.Haste:
                        return ($"+{Item.SocketBonus[stat]} Haste");
                    case StatName.ArmorPenetration:
                        return ($"+{Item.SocketBonus[stat]} APen");
                    case StatName.SpellPower:
                        return ($"+{Item.SocketBonus[stat]} SP");
                    case StatName.Resilience:
                        return ($"+{Item.SocketBonus[stat]} Resi");
                    default:
                    return ("");
            }
        }
    }
}
