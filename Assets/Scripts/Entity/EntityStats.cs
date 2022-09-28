using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EntityStats : ScriptableObject
{
    public Sprite sprite;

    public float maxHealthPoint = 100;
    public float damage = 10;
    public float attackSpeed = 1;
    public float speed = 10;
    public float jumpForce = 1;
    public float maxJumps = 2;
}
