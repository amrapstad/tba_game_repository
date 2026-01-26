using UnityEngine;
using System.Collections;
using System.Collections.Generic;



namespace CardGame

{
    [CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
    public class Card : ScriptableObject
    {
        // Card Name
        public string cardName;

        // A card can have multiple effects
        public List<CardEffect> effects;

        // Card Description
        [TextArea(3, 6)]
        public string description;


        // Definition of a card effect
        [System.Serializable]
        public class CardEffect
        {
            public EffectType effectType;

            public int effectValue;
            public DamageType damageType;
            public HealType healType;
            public BuffType buffType;
        }

        // Enums for effect types
        public enum EffectType
        {
            Attack,
            Heal,
            Buff
        }

        // Enums for damage  NOTE: Use "None" for everything other than 'Attack' EffectType
        public enum DamageType
        {
            Physical,
            Magic,
            None,
        }

        // Enums for heal  NOTE: Use "None" for everything other than 'Heal' EffectType
        public enum HealType
        {
            Temporary,
            Permanent,
            None
        }

        // Enums for heal  NOTE: Use "None" for everything other than 'Heal' EffectType
        public enum BuffType
        {
            Speed,
            DamageBoost,
            None
        }

    }
}