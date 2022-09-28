using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public Card lastCard;

    public static List<Item> equipment = new List<Item>(5);

    public delegate void CardEvent();
    public CardEvent OnCardAdded;

    public void AddCard(Card card)
    {
        maxHealthPoint += card.addMaxHealthPoint;
        healthPoint = maxHealthPoint;
        SpendHP(0);
        damage += card.addDamage;
        attackSpeed -= card.addAttackSpeed;
        speed += card.addSpeed;
        maxJumps += card.addJumps;

        Cards(card);
    }

    private void Cards(Card card)
    {
        lastCard = card;
        OnCardAdded?.Invoke();
    }

    public void AddEquipment(Item item)
    {
        maxHealthPoint += item.addMaxHealthPoint;
        SpendHP(0);
        damage += item.addDamage;
        attackSpeed -= item.addAttackSpeed;
        speed += item.addSpeed;
        maxJumps += item.addJumps;
    }

    public void RemoveEquipment(Item item)
    { 
        maxHealthPoint -= item.addMaxHealthPoint;
        if (healthPoint > maxHealthPoint)
            healthPoint = maxHealthPoint;
        SpendHP(0);
        damage -= item.addDamage;
        attackSpeed += item.addAttackSpeed;
        speed -= item.addSpeed;
        maxJumps -= item.addJumps;
    }
}
