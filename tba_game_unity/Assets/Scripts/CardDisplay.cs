using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using CardGame;

public class CardDisplay : MonoBehaviour
{
    
    public Card cardData;


    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text damageText;
    public Image[] typeImages;
    public Image cardImage;

    public string damageType;
    public string healType;
    private List<CardEffectData> effects = new List<CardEffectData>();

    void Start()
    {
        UpdateCardDisplay();
    }

    public void UpdateCardDisplay()
    {
        if (cardData == null) return;

        // Basic info
        nameText.text = cardData.cardName;
        descriptionText.text = cardData.description;

        effects.Clear();

        // Build effect list
        foreach (var effect in cardData.effects)
        {
            CardEffectData data = new CardEffectData
            {
                effectType = effect.effectType,
                subType = GetSubType(effect),
                value = effect.effectValue
            };

            effects.Add(data);
        }

        // Disable all type images first
        foreach (var img in typeImages)
        {
            img.gameObject.SetActive(false);
        }

        // Activate images based on effect type
        foreach (var effect in effects)
        {
            switch (effect.effectType)
            {
                case Card.EffectType.Attack:
                    if (typeImages.Length > 0)
                        typeImages[0].gameObject.SetActive(true);
                    break;

                case Card.EffectType.Heal:
                    if (typeImages.Length > 1)
                        typeImages[1].gameObject.SetActive(true);
                    break;

                case Card.EffectType.Buff:
                    if (typeImages.Length > 2)
                        typeImages[2].gameObject.SetActive(true);
                    break;
            }
        }


        FetchDamageType();
    }

    private string GetSubType(Card.CardEffect effect)
    {
        switch (effect.effectType)
        {
            case Card.EffectType.Attack:
                return effect.damageType.ToString();

            case Card.EffectType.Heal:
                return effect.healType.ToString();

            default:
                return effect.buffType.ToString();
        }
    }

    private void FetchDamageType()
    {
        foreach (var effect in effects)
        {
            if (effect.effectType == Card.EffectType.Attack)
            {
                damageType = effect.subType;
                damageText.text = effect.value.ToString();
                return;
            }
        }

        // No attack found
        damageText.text = "0";
        damageType = "None";
    }
}

[System.Serializable]
public struct CardEffectData
{
    public Card.EffectType effectType;
    public string subType;
    public int value;
}
