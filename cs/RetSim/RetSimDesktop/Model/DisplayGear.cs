using RetSim.Items;
using RetSim.Units.UnitStats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private string str;
        private string ap;
        private string agi;
        private string crit;
        private string hit;
        private string haste;
        private string exp;
        private string arpen;
        private string stam;
        private string intellect;
        private string mp5;
        private string sp;

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
                InitializeItem();
                OnPropertyChanged(nameof(Item));
            }
        }
        #region Display
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
        #endregion

        #region Gems and Sockets
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
        #endregion

        #region Stats
        public string Str
        {
            get => str;
            set => SetProperty(ref str, value);
        }
        public string AP
        {
            get => ap;
            set => SetProperty(ref ap, value);
        }
        public string Agi
        {
            get => agi;
            set => SetProperty(ref agi, value);
        }
        public string Crit
        {
            get => crit;
            set => SetProperty(ref crit, value);
        }
        public string Hit
        {
            get => hit;
            set => SetProperty(ref hit, value);
        }
        public string Haste
        {
            get => haste;
            set => SetProperty(ref haste, value);
        }
        public string Exp
        {
            get => exp;
            set => SetProperty(ref exp, value);
        }
        public string ArPen
        {
            get => arpen;
            set => SetProperty(ref arpen, value);
        }
        public string Stam
        {
            get => stam;
            set => SetProperty(ref stam, value);
        }
        public string Intellect
        {
            get => intellect;
            set => SetProperty(ref intellect, value);
        }
        public string MP5
        {
            get => mp5;
            set => SetProperty(ref mp5, value);
        }
        public string SP
        {
            get => sp;
            set => SetProperty(ref sp, value);
        }
        #endregion

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

            Str = StatConverter(Item.Stats[StatName.Strength]);
            AP = StatConverter(Item.Stats[StatName.AttackPower]);
            Agi = StatConverter(Item.Stats[StatName.Agility]);
            Crit = StatConverter(Item.Stats[StatName.CritRating]);
            Hit = StatConverter(Item.Stats[StatName.HitRating]);
            Haste = StatConverter(Item.Stats[StatName.HasteRating]);
            Exp = StatConverter(Item.Stats[StatName.ExpertiseRating]);
            ArPen = StatConverter(Item.Stats[StatName.ArmorPenetration]);
            Stam = StatConverter(Item.Stats[StatName.Stamina]);
            Intellect = StatConverter(Item.Stats[StatName.Intellect]);
            MP5 = StatConverter(Item.Stats[StatName.ManaPer5]);
            SP = StatConverter(Item.Stats[StatName.SpellPower]);

        }

        private string StatConverter(float value)
        {
            return value.ToString("0;;' '");
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
                    return ($"+{Item.SocketBonus[stat]} MP5");
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
