﻿using RetSim.Events;
using System;
using System.Collections.Generic;

namespace RetSim
{
    public partial class Player
    {
        private AutoAttackEvent nextAutoAttack;
        private Dictionary<int, CooldownEndEvent> spellIdToCooldownEndEvent = new();
        private Dictionary<int, AuraEndEvent> auraIdToAuraEndEvent = new();

        private Dictionary<int, Func<int, List<Event>, int>> spellIdToSpellCast = new();

        public Player()
        {
            spellIdToSpellCast.Add(Spellbook.crusaderStrike.ID, (time, resultingEvents) => CastCrusaderStrike(time, resultingEvents));
            spellIdToSpellCast.Add(Spellbook.sealOfTheCrusader.ID, (time, resultingEvents) => CastSealOfTheCrusader(time, resultingEvents));

        }

        public int CastSpell(int spellId, int time, List<Event> resultingEvents)
        {
            int cooldown = Spellbook.ByID[spellId].Cooldown;
            if (cooldown > 0)
            {
                CooldownEndEvent cooldownEnd = new CooldownEndEvent(time + cooldown, this, spellId);
                resultingEvents.Add(cooldownEnd);
                spellIdToCooldownEndEvent[spellId] = cooldownEnd;
            }
            return spellIdToSpellCast[spellId](time, resultingEvents);
        }

        public void ApplyAura(int auraId, int time, List<Event> resultingEvents)
        {
            AuraEndEvent auraEndEvent = new(time + Auras.ByID[auraId].Duration, this, auraId);
            auraIdToAuraEndEvent[Auras.ByID[auraId].ID] = auraEndEvent;
            resultingEvents.Add(auraEndEvent);
        }

        public int CastCrusaderStrike(int time, List<Event> resultingEvents)
        {
            return 1212;
        }

        public int CastSealOfTheCrusader(int time, List<Event> resultingEvents)
        {
            ApplyAura(Auras.sealOfTheCrusader.ID, time, resultingEvents);
            return 0;
        }

        public int TimeOfNextSwing()
        {
            return nextAutoAttack.ExpirationTime;
        }

        public int MeleeAttack(int time, List<Event> resultingEvents)
        {
            nextAutoAttack = new AutoAttackEvent(time + 3500, this);
            resultingEvents.Add(nextAutoAttack);
            return 1234;
        }

        public void RemoveCooldownOf(int id)
        {
            spellIdToCooldownEndEvent.Remove(id);
        }

        public bool IsSpellOnCooldown(int id)
        {
            return spellIdToCooldownEndEvent.ContainsKey(id);
        }

        public int GetEndOfCooldown(int id)
        {
            return spellIdToCooldownEndEvent.TryGetValue(id, out CooldownEndEvent cooldownEvent) ? cooldownEvent.ExpirationTime : 0;
        }

        public void RemoveAura(int id)
        {
            spellIdToCooldownEndEvent.Remove(id);
        }

        public bool HasAura(int id)
        {
            return auraIdToAuraEndEvent.ContainsKey(id);
        }
        public int GetEndOfAura(int id)
        {
            return auraIdToAuraEndEvent.TryGetValue(id, out AuraEndEvent auraEvent) ? auraEvent.ExpirationTime : 0;
        }
    }
}

