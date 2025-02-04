﻿using RetSim.Items;

namespace RetSim.Data;
public static class Items
{
    public static readonly Dictionary<int, EquippableItem> AllItems = new();
    public static readonly Dictionary<int, EquippableWeapon> Weapons = new();
    public static readonly Dictionary<int, EquippableItem> Heads = new();
    public static readonly Dictionary<int, EquippableItem> Necks = new();
    public static readonly Dictionary<int, EquippableItem> Shoulders = new();
    public static readonly Dictionary<int, EquippableItem> Cloaks = new();
    public static readonly Dictionary<int, EquippableItem> Chests = new();
    public static readonly Dictionary<int, EquippableItem> Wrists = new();
    public static readonly Dictionary<int, EquippableItem> Hands = new();
    public static readonly Dictionary<int, EquippableItem> Waists = new();
    public static readonly Dictionary<int, EquippableItem> Legs = new();
    public static readonly Dictionary<int, EquippableItem> Feet = new();
    public static readonly Dictionary<int, EquippableItem> Fingers = new();
    public static readonly Dictionary<int, EquippableItem> Trinkets = new();
    public static readonly Dictionary<int, EquippableItem> Relics = new();
    public static readonly Dictionary<int, ItemSet> Sets = new();
    public static readonly Dictionary<int, Gem> Gems = new();
    public static readonly List<Gem> GemsSorted = new();
    public static readonly Dictionary<int, MetaGem> MetaGems = new();
    public static readonly Dictionary<int, Enchant> Enchants = new();

    private static readonly List<int> GemIDs = new()
    {
        33131,
        32193,
        24027,
        23095,
        28458,
        32217,
        24058,
        23098,
        30584,
        30559,
        30553,
        32211,
        24054,
        23111,
        30546,
        30574,
        31118,
        32197,
        28362,
        24031,
        28595,
        28462,
        28363,
        32220,
        30556,
        24061,
        23100,
        30604,
        32214,
        31865,
        31864,
        33142,
        32206,
        24051,
        23116,
        28468,
        33143,
        32205,
        38550,
        24048,
        28290,
        28467,
        30550,
        32226,
        30602,
        24067,
        27809,
        23104
    };

    public static void Initialize(List<EquippableWeapon> weapons, List<EquippableItem> armorPieces, List<ItemSet> sets, List<Gem> gems, List<MetaGem> metaGems, List<Enchant> enchants)
    {
        foreach (var weapon in weapons)
        {
            Weapons.Add(weapon.ID, weapon);
            AllItems.Add(weapon.ID, weapon);
        }

        foreach (var armor in armorPieces)
        {
            AllItems.Add(armor.ID, armor);

            switch (armor.Slot)
            {
                case Slot.Head:
                    Heads.Add(armor.ID, armor);
                    break;
                case Slot.Neck:
                    Necks.Add(armor.ID, armor);
                    break;
                case Slot.Shoulders:
                    Shoulders.Add(armor.ID, armor);
                    break;
                case Slot.Back:
                    Cloaks.Add(armor.ID, armor);
                    break;
                case Slot.Chest:
                    Chests.Add(armor.ID, armor);
                    break;
                case Slot.Wrists:
                    Wrists.Add(armor.ID, armor);
                    break;
                case Slot.Hands:
                    Hands.Add(armor.ID, armor);
                    break;
                case Slot.Waist:
                    Waists.Add(armor.ID, armor);
                    break;
                case Slot.Legs:
                    Legs.Add(armor.ID, armor);
                    break;
                case Slot.Feet:
                    Feet.Add(armor.ID, armor);
                    break;
                case Slot.Finger:
                    Fingers.Add(armor.ID, armor);
                    break;
                case Slot.Trinket:
                    Trinkets.Add(armor.ID, armor);
                    break;
                case Slot.Relic:
                    Relics.Add(armor.ID, armor);
                    break;
                default:
                    break;
            }
        }

        foreach (var set in sets)
        {
            Sets.Add(set.ID, set);
        }

        foreach (var gem in gems)
        {
            Gems.Add(gem.ID, gem);
        }

        foreach (int gem in GemIDs)
        {
            GemsSorted.Add(Gems[gem]);
        }

        foreach (var metaGem in metaGems)
        {
            MetaGems.Add(metaGem.ID, metaGem);
        }

        foreach (var enchant in enchants)
            Enchants.Add(enchant.ID, enchant);
    }
}

