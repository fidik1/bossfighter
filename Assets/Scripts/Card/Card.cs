using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Card", menuName = "Cards")]
public class Card : ScriptableObject
{
    public Sprite sprite;
    public string text;

    public string tag;

    public float addMaxHealthPoint;
    public float addDamage;
    public float addAttackSpeed;
    public float addSpeed;
    public float addJumps;

    // here must be the buffs like vampiric...
}
