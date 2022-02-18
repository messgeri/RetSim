﻿namespace RetSim.Misc;

public static class Constants
{
    public static class BaseStats
    {
        public const int Health = 3197;
        public const int Mana = 2673;

        public const int AttackPower = 190;
        public const float CritChance = 0.65f;

        public const float SpellCritChance = 3.32f;
    }

    public static class Stats
    {
        public const int HealthPerStamina = 10;
        public const int ManaPerIntellect = 15;
        public const float StaminaPerHealth = 1f / HealthPerStamina;
        public const float IntellectPerMana = 1f / ManaPerIntellect;

        public const int APPerDPS = 14;

        public const int APPerStrength = 2;
        public const float StrengthPerAP = 1f / APPerStrength;

        public const int AgilityPerCrit = 25;
        public const int ArmorPerAgility = 2;
        public const float AgilityPerArmor = 1f / ArmorPerAgility;

        public const int IntellectPerSpellCrit = 80;

        public const int HitPenalty = 1;

        public const int ExpertiseCap = 26;
        public const int ExpertisePerDodge = 4;

        public const float PhysicalCritBonus = 2f;
        public const float SpellCritBonus = 1.5f;

        public const int NormalizedWeaponSpeed = 3300;
    }

    public static class Ratings
    {
        public const float Crit = 22.08f;
        public const float Hit = 15.77f;
        public const float Haste = 15.77f;

        public const float SpellCrit = 22.08f;
        public const float SpellHit = 12.62f;
        public const float SpellHaste = 15.77f;

        public const float Expertise = 3.93f;

        public const float Resilience = 39.4f;
    }

    public static class Boss
    {
        public const int Level = 73;

        public const float ArmorMagicNumber = 10557.5f;

        public const int MissChance = 8;
        public const float DodgeChance = 6.5f;
        public const int GlancingChance = 24;
        public const int UpgradedGlancingChance = 2400;
        public const int GlancePenaltyMax = 35;
        public const int GlancePenaltyMin = 15;

        public const int CritSuppression = 3;
        public const float CritAuraSuppression = 1.8f;

        public const int SpellMissChance = 17;
        public const int MininumSpellMissChance = 1;

        public const int LevelResistance = 28;
        public const int ResistanceCap = Level * 5;

        public static readonly Dictionary<float, int> ResistanceProbabilities = new()
        {
            { 0.25f, 1 }, //75% resist, 1% chance
            { 0.5f, 5 }, //50% resist, 4% chance
            { 0.75f, 18 }, //25% resist, 13% chance
                           //{ 1f, 82 }
        };

    }

    public static class Numbers
    {
        public const int DefaultGCD = 1500;
        public const int MinimumGCD = 1000;
        public const int MillisecondsPerSec = 1000;

        public const string MillisecondFormatter = "{0:00:00:000}";
    }

    public static class EquipmentSlots //TODO: Turn into enum
    {
        public const int Head = 0;
        public const int Neck = 1;
        public const int Shoulders = 2;
        public const int Back = 3;
        public const int Chest = 4;
        public const int Wrists = 5;
        public const int Hands = 6;
        public const int Waist = 7;
        public const int Legs = 8;
        public const int Feet = 9;
        public const int Finger1 = 10;
        public const int Finger2 = 11;
        public const int Trinket1 = 12;
        public const int Trinket2 = 13;
        public const int Relic = 14;
        public const int Weapon = 15;

        public const int Total = 16;
    }
}