using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public Card lastCard;

    public delegate void CardEvent();
    public CardEvent OnCardAdded;

    public void ReloadBuffs(Card card)
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
}
