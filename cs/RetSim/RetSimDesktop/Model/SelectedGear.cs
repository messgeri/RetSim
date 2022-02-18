﻿using RetSim.Items;
using RetSim.Units.Player.Static;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using static RetSim.Data.Items;

namespace RetSimDesktop.Model
{
    public class SelectedGear : INotifyPropertyChanged
    {
        private DisplayGear? selectedHead = new() { Item = AllItems[32235], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedNeck = new() { Item = AllItems[30022], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedShoulders = new() { Item = AllItems[30055], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedBack = new() { Item = AllItems[30098], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedChest = new() { Item = AllItems[30905], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedWrists = new() { Item = AllItems[32574], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedHands = new() { Item = AllItems[29947], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedWaist = new() { Item = AllItems[30106], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedLegs = new() { Item = AllItems[30900], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedFeet = new() { Item = AllItems[32366], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedFinger1 = new() { Item = AllItems[30834], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedFinger2 = new() { Item = AllItems[32526], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedTrinket1 = new() { Item = AllItems[29383], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedTrinket2 = new() { Item = AllItems[28830], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedRelic = new() { Item = AllItems[27484], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedWeapon = new() { Item = Weapons[32332], DPS = 0, EnabledForGearSim = true };

        private Enchant? headEnchant = Enchants[35452];
        private Enchant? shouldersEnchant = Enchants[35417];
        private Enchant? backEnchant = Enchants[34004];
        private Enchant? chestEnchant = Enchants[27960];
        private Enchant? wristsEnchant = Enchants[27899];
        private Enchant? handsEnchant = Enchants[33995];
        private Enchant? legsEnchant = Enchants[35490];
        private Enchant? feetEnchant = Enchants[27951];
        private Enchant? finger1Enchant = null;
        private Enchant? finger2Enchant = null;
        private Enchant? weaponEnchant = Enchants[27984];

        public DisplayGear? SelectedHead
        {
            get { return selectedHead; }
            set
            {
                selectedHead = value;
                OnPropertyChanged(nameof(SelectedHead));
            }
        }

        public DisplayGear? SelectedNeck
        {
            get { return selectedNeck; }
            set
            {
                selectedNeck = value;
                OnPropertyChanged(nameof(SelectedNeck));
            }
        }

        public DisplayGear? SelectedShoulders
        {
            get { return selectedShoulders; }
            set
            {
                selectedShoulders = value;
                OnPropertyChanged(nameof(SelectedShoulders));
            }
        }

        public DisplayGear? SelectedBack
        {
            get { return selectedBack; }
            set
            {
                selectedBack = value;
                OnPropertyChanged(nameof(SelectedBack));
            }
        }

        public DisplayGear? SelectedChest
        {
            get { return selectedChest; }
            set
            {
                selectedChest = value;
                OnPropertyChanged(nameof(SelectedChest));
            }
        }

        public DisplayGear? SelectedWrists
        {
            get { return selectedWrists; }
            set
            {
                selectedWrists = value;
                OnPropertyChanged(nameof(SelectedWrists));
            }
        }

        public DisplayGear? SelectedHands
        {
            get { return selectedHands; }
            set
            {
                selectedHands = value;
                OnPropertyChanged(nameof(SelectedHands));
            }
        }

        public DisplayGear? SelectedWaist
        {
            get { return selectedWaist; }
            set
            {
                selectedWaist = value;
                OnPropertyChanged(nameof(SelectedWaist));
            }
        }

        public DisplayGear? SelectedLegs
        {
            get { return selectedLegs; }
            set
            {
                selectedLegs = value;
                OnPropertyChanged(nameof(SelectedLegs));
            }
        }

        public DisplayGear? SelectedFeet
        {
            get { return selectedFeet; }
            set
            {
                selectedFeet = value;
                OnPropertyChanged(nameof(SelectedFeet));
            }
        }

        public DisplayGear? SelectedFinger1
        {
            get { return selectedFinger1; }
            set
            {
                selectedFinger1 = value;
                OnPropertyChanged(nameof(SelectedFinger1));
            }
        }

        public DisplayGear? SelectedFinger2
        {
            get { return selectedFinger2; }
            set
            {
                selectedFinger2 = value;
                OnPropertyChanged(nameof(SelectedFinger2));
            }
        }

        public DisplayGear? SelectedTrinket1
        {
            get { return selectedTrinket1; }
            set
            {
                selectedTrinket1 = value;
                OnPropertyChanged(nameof(SelectedTrinket1));
            }
        }

        public DisplayGear? SelectedTrinket2
        {
            get { return selectedTrinket2; }
            set
            {
                selectedTrinket2 = value;
                OnPropertyChanged(nameof(SelectedTrinket2));
            }
        }
        public DisplayGear? SelectedRelic
        {
            get { return selectedRelic; }
            set
            {
                selectedRelic = value;
                OnPropertyChanged(nameof(SelectedRelic));
            }
        }

        public DisplayGear? SelectedWeapon
        {
            get { return selectedWeapon; }
            set
            {
                selectedWeapon = value;
                OnPropertyChanged(nameof(SelectedWeapon));
            }
        }

        public Enchant? HeadEnchant { get { return headEnchant; } set { headEnchant = value; OnPropertyChanged(nameof(HeadEnchant)); } }
        public Enchant? ShouldersEnchant { get { return shouldersEnchant; } set { shouldersEnchant = value; OnPropertyChanged(nameof(ShouldersEnchant)); } }
        public Enchant? BackEnchant { get { return backEnchant; } set { backEnchant = value; OnPropertyChanged(nameof(BackEnchant)); } }
        public Enchant? ChestEnchant { get { return chestEnchant; } set { chestEnchant = value; OnPropertyChanged(nameof(ChestEnchant)); } }
        public Enchant? WristsEnchant { get { return wristsEnchant; } set { wristsEnchant = value; OnPropertyChanged(nameof(WristsEnchant)); } }
        public Enchant? HandsEnchant { get { return handsEnchant; } set { handsEnchant = value; OnPropertyChanged(nameof(HandsEnchant)); } }
        public Enchant? LegsEnchant { get { return legsEnchant; } set { legsEnchant = value; OnPropertyChanged(nameof(LegsEnchant)); } }
        public Enchant? FeetEnchant { get { return feetEnchant; } set { feetEnchant = value; OnPropertyChanged(nameof(FeetEnchant)); } }
        public Enchant? Finger1Enchant { get { return finger1Enchant; } set { finger1Enchant = value; OnPropertyChanged(nameof(Finger1Enchant)); } }
        public Enchant? Finger2Enchant { get { return finger2Enchant; } set { finger2Enchant = value; OnPropertyChanged(nameof(Finger2Enchant)); } }
        public Enchant? WeaponEnchant { get { return weaponEnchant; } set { weaponEnchant = value; OnPropertyChanged(nameof(WeaponEnchant)); } }


        public Equipment GetEquipment()
        {
            return new()
            {
                Head = SelectedHead?.Item,
                Neck = SelectedNeck?.Item,
                Shoulders = SelectedShoulders?.Item,
                Back = SelectedBack?.Item,
                Chest = SelectedChest?.Item,
                Wrists = SelectedWrists?.Item,
                Hands = SelectedHands?.Item,
                Waist = SelectedWaist?.Item,
                Legs = SelectedLegs?.Item,
                Feet = SelectedFeet?.Item,
                Finger1 = SelectedFinger1?.Item,
                Finger2 = SelectedFinger2?.Item,
                Trinket1 = SelectedTrinket1?.Item,
                Trinket2 = SelectedTrinket2?.Item,
                Relic = SelectedRelic?.Item,
                Weapon = (SelectedWeapon?.Item) as EquippableWeapon,

                HeadEnchant = HeadEnchant,
                ShouldersEnchant = ShouldersEnchant,
                BackEnchant = BackEnchant,
                ChestEnchant = ChestEnchant,
                WristsEnchant = WristsEnchant,
                HandsEnchant = HandsEnchant,
                LegsEnchant = LegsEnchant,
                FeetEnchant = FeetEnchant,
                Finger1Enchant = Finger1Enchant,
                Finger2Enchant = Finger2Enchant,
                WeaponEnchant = WeaponEnchant,
            };
        }

        public void ClearSelectedGear()
        {
            selectedHead = null;
            selectedNeck = null;
            selectedShoulders = null;
            selectedBack = null;
            selectedChest = null;
            selectedWrists = null;
            selectedHands = null;
            selectedWaist = null;
            selectedLegs = null;
            selectedFeet = null;
            selectedFinger1 = null;
            selectedFinger2 = null;
            selectedTrinket1 = null;
            selectedTrinket2 = null;
            selectedRelic = null;
            selectedWeapon = null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class SelectedGearJsonConverter : JsonConverter<SelectedGear>
    {

        public override SelectedGear? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Dictionary<string, int> properties = new();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Not a PropertyName: " + reader.TokenType);
                }

                string propertyName = reader.GetString();

                reader.Read();

                properties[propertyName] = reader.GetInt32();
            }

            SelectedGear result = new();

            if (properties.ContainsKey("SelectedHead"))
            {
                result.SelectedHead = new() { Item = AllItems[properties["SelectedHead"]], DPS = 0, EnabledForGearSim = true };
            }
            else
            {
                result.SelectedHead = null;
            }
            if (properties.ContainsKey("SelectedNeck"))
            {
                result.SelectedNeck = new() { Item = AllItems[properties["SelectedNeck"]], DPS = 0, EnabledForGearSim = true };
            }
            else
            {
                result.SelectedNeck = null;
            }
            if (properties.ContainsKey("SelectedShoulders"))
            {
                result.SelectedShoulders = new() { Item = AllItems[properties["SelectedShoulders"]], DPS = 0, EnabledForGearSim = true };
            }
            else
            {
                result.SelectedShoulders = null;
            }
            if (properties.ContainsKey("SelectedBack"))
            {
                result.SelectedBack = new() { Item = AllItems[properties["SelectedBack"]], DPS = 0, EnabledForGearSim = true };
            }
            else
            {
                result.SelectedBack = null;
            }
            if (properties.ContainsKey("SelectedChest"))
            {
                result.SelectedChest = new() { Item = AllItems[properties["SelectedChest"]], DPS = 0, EnabledForGearSim = true };
            }
            else
            {
                result.SelectedChest = null;
            }
            if (properties.ContainsKey("SelectedWrists"))
            {
                result.SelectedWrists = new() { Item = AllItems[properties["SelectedWrists"]], DPS = 0, EnabledForGearSim = true };
            }
            else
            {
                result.SelectedWrists = null;
            }
            if (properties.ContainsKey("SelectedHands"))
            {
                result.SelectedHands = new() { Item = AllItems[properties["SelectedHands"]], DPS = 0, EnabledForGearSim = true };
            }
            else
            {
                result.SelectedHands = null;
            }
            if (properties.ContainsKey("SelectedWaist"))
            {
                result.SelectedWaist = new() { Item = AllItems[properties["SelectedWaist"]], DPS = 0, EnabledForGearSim = true };
            }
            else
            {
                result.SelectedWaist = null;
            }
            if (properties.ContainsKey("SelectedLegs"))
            {
                result.SelectedLegs = new() { Item = AllItems[properties["SelectedLegs"]], DPS = 0, EnabledForGearSim = true };
            }
            else
            {
                result.SelectedLegs = null;
            }
            if (properties.ContainsKey("SelectedFeet"))
            {
                result.SelectedFeet = new() { Item = AllItems[properties["SelectedFeet"]], DPS = 0, EnabledForGearSim = true };
            }
            else
            {
                result.SelectedFeet = null;
            }
            if (properties.ContainsKey("SelectedFinger1"))
            {
                result.SelectedFinger1 = new() { Item = AllItems[properties["SelectedFinger1"]], DPS = 0, EnabledForGearSim = true };
            }
            else
            {
                result.SelectedFinger1 = null;
            }
            if (properties.ContainsKey("SelectedFinger2"))
            {
                result.SelectedFinger2 = new() { Item = AllItems[properties["SelectedFinger2"]], DPS = 0, EnabledForGearSim = true };
            }
            else
            {
                result.SelectedFinger2 = null;
            }
            if (properties.ContainsKey("SelectedTrinket1"))
            {
                result.SelectedTrinket1 = new() { Item = AllItems[properties["SelectedTrinket1"]], DPS = 0, EnabledForGearSim = true };
            }
            else
            {
                result.SelectedTrinket1 = null;
            }
            if (properties.ContainsKey("SelectedTrinket2"))
            {
                result.SelectedTrinket2 = new() { Item = AllItems[properties["SelectedTrinket2"]], DPS = 0, EnabledForGearSim = true };
            }
            else
            {
                result.SelectedTrinket2 = null;
            }
            if (properties.ContainsKey("SelectedRelic"))
            {
                result.SelectedRelic = new() { Item = AllItems[properties["SelectedRelic"]], DPS = 0, EnabledForGearSim = true };
            }
            else
            {
                result.SelectedRelic = null;
            }
            if (properties.ContainsKey("SelectedWeapon"))
            {
                result.SelectedWeapon = new() { Item = Weapons[properties["SelectedWeapon"]], DPS = 0, EnabledForGearSim = true };
            }
            else
            {
                result.SelectedWeapon = null;
            }

            if (properties.ContainsKey("HeadEnchant") && properties["HeadEnchant"] != -1)
            {
                result.HeadEnchant = Enchants[properties["HeadEnchant"]];
            }
            else if (properties.ContainsKey("HeadEnchant") && properties["HeadEnchant"] == -1)
            {
                result.HeadEnchant = null;
            }
            if (properties.ContainsKey("ShouldersEnchant") && properties["ShouldersEnchant"] != -1)
            {
                result.ShouldersEnchant = Enchants[properties["ShouldersEnchant"]];
            }
            else if (properties.ContainsKey("ShouldersEnchant") && properties["ShouldersEnchant"] == -1)
            {
                result.ShouldersEnchant = null;
            }
            if (properties.ContainsKey("BackEnchant") && properties["BackEnchant"] != -1)
            {
                result.BackEnchant = Enchants[properties["BackEnchant"]];
            }
            else if (properties.ContainsKey("BackEnchant") && properties["BackEnchant"] == -1)
            {
                result.BackEnchant = null;
            }
            if (properties.ContainsKey("ChestEnchant") && properties["ChestEnchant"] != -1)
            {
                result.ChestEnchant = Enchants[properties["ChestEnchant"]];
            }
            else if (properties.ContainsKey("ChestEnchant") && properties["ChestEnchant"] == -1)
            {
                result.ChestEnchant = null;
            }
            if (properties.ContainsKey("WristsEnchant") && properties["WristsEnchant"] != -1)
            {
                result.WristsEnchant = Enchants[properties["WristsEnchant"]];
            }
            else if (properties.ContainsKey("WristsEnchant") && properties["WristsEnchant"] == -1)
            {
                result.WristsEnchant = null;
            }
            if (properties.ContainsKey("HandsEnchant") && properties["HandsEnchant"] != -1)
            {
                result.HandsEnchant = Enchants[properties["HandsEnchant"]];
            }
            else if (properties.ContainsKey("HandsEnchant") && properties["HandsEnchant"] == -1)
            {
                result.HandsEnchant = null;
            }
            if (properties.ContainsKey("LegsEnchant") && properties["LegsEnchant"] != -1)
            {
                result.LegsEnchant = Enchants[properties["LegsEnchant"]];
            }
            else if (properties.ContainsKey("LegsEnchant") && properties["LegsEnchant"] == -1)
            {
                result.LegsEnchant = null;
            }
            if (properties.ContainsKey("FeetEnchant") && properties["FeetEnchant"] != -1)
            {
                result.FeetEnchant = Enchants[properties["FeetEnchant"]];
            }
            else if (properties.ContainsKey("FeetEnchant") && properties["FeetEnchant"] == -1)
            {
                result.FeetEnchant = null;
            }
            if (properties.ContainsKey("Finger1Enchant") && properties["Finger1Enchant"] != -1)
            {
                result.Finger1Enchant = Enchants[properties["Finger1Enchant"]];
            }
            else if (properties.ContainsKey("Finger1Enchant") && properties["Finger1Enchant"] == -1)
            {
                result.Finger1Enchant = null;
            }
            if (properties.ContainsKey("Finger2Enchant") && properties["Finger2Enchant"] != -1)
            {
                result.Finger2Enchant = Enchants[properties["Finger2Enchant"]];
            }
            else if (properties.ContainsKey("Finger2Enchant") && properties["Finger2Enchant"] == -1)
            {
                result.Finger2Enchant = null;
            }
            if (properties.ContainsKey("WeaponEnchant") && properties["WeaponEnchant"] != -1)
            {
                result.WeaponEnchant = Enchants[properties["WeaponEnchant"]];
            }
            else if (properties.ContainsKey("WeaponEnchant") && properties["WeaponEnchant"] == -1)
            {
                result.WeaponEnchant = null;
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, SelectedGear value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            if (value.SelectedHead != null)
            {
                writer.WriteNumber("SelectedHead", value.SelectedHead.Item.ID);
            }
            if (value.SelectedNeck != null)
            {
                writer.WriteNumber("SelectedNeck", value.SelectedNeck.Item.ID);
            }
            if (value.SelectedShoulders != null)
            {
                writer.WriteNumber("SelectedShoulders", value.SelectedShoulders.Item.ID);
            }
            if (value.SelectedBack != null)
            {
                writer.WriteNumber("SelectedBack", value.SelectedBack.Item.ID);
            }
            if (value.SelectedChest != null)
            {
                writer.WriteNumber("SelectedChest", value.SelectedChest.Item.ID);
            }
            if (value.SelectedWrists != null)
            {
                writer.WriteNumber("SelectedWrists", value.SelectedWrists.Item.ID);
            }
            if (value.SelectedHands != null)
            {
                writer.WriteNumber("SelectedHands", value.SelectedHands.Item.ID);
            }
            if (value.SelectedWaist != null)
            {
                writer.WriteNumber("SelectedWaist", value.SelectedWaist.Item.ID);
            }
            if (value.SelectedLegs != null)
            {
                writer.WriteNumber("SelectedLegs", value.SelectedLegs.Item.ID);
            }
            if (value.SelectedFeet != null)
            {
                writer.WriteNumber("SelectedFeet", value.SelectedFeet.Item.ID);
            }
            if (value.SelectedFinger1 != null)
            {
                writer.WriteNumber("SelectedFinger1", value.SelectedFinger1.Item.ID);
            }
            if (value.SelectedFinger2 != null)
            {
                writer.WriteNumber("SelectedFinger2", value.SelectedFinger2.Item.ID);
            }
            if (value.SelectedTrinket1 != null)
            {
                writer.WriteNumber("SelectedTrinket1", value.SelectedTrinket1.Item.ID);
            }
            if (value.SelectedTrinket2 != null)
            {
                writer.WriteNumber("SelectedTrinket2", value.SelectedTrinket2.Item.ID);
            }
            if (value.SelectedRelic != null)
            {
                writer.WriteNumber("SelectedRelic", value.SelectedRelic.Item.ID);
            }
            if (value.SelectedWeapon != null)
            {
                writer.WriteNumber("SelectedWeapon", value.SelectedWeapon.Item.ID);
            }

            if (value.HeadEnchant != null)
            {
                writer.WriteNumber("HeadEnchant", value.HeadEnchant.ID);
            }
            if (value.ShouldersEnchant != null)
            {
                writer.WriteNumber("ShouldersEnchant", value.ShouldersEnchant.ID);
            }
            if (value.BackEnchant != null)
            {
                writer.WriteNumber("BackEnchant", value.BackEnchant.ID);
            }
            if (value.ChestEnchant != null)
            {
                writer.WriteNumber("ChestEnchant", value.ChestEnchant.ID);
            }
            if (value.WristsEnchant != null)
            {
                writer.WriteNumber("WristsEnchant", value.WristsEnchant.ID);
            }
            if (value.HandsEnchant != null)
            {
                writer.WriteNumber("HandsEnchant", value.HandsEnchant.ID);
            }
            if (value.LegsEnchant != null)
            {
                writer.WriteNumber("LegsEnchant", value.LegsEnchant.ID);
            }
            if (value.FeetEnchant != null)
            {
                writer.WriteNumber("FeetEnchant", value.FeetEnchant.ID);
            }
            if (value.Finger1Enchant != null)
            {
                writer.WriteNumber("Finger1Enchant", value.Finger1Enchant.ID);
            }
            if (value.Finger2Enchant != null)
            {
                writer.WriteNumber("Finger2Enchant", value.Finger2Enchant.ID);
            }
            if (value.WeaponEnchant != null)
            {
                writer.WriteNumber("WeaponEnchant", value.WeaponEnchant.ID);
            }

            writer.WriteEndObject();
        }
    }
}
