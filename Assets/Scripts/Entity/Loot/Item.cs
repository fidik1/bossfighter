using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Color32 color;

    public new string tag;

    public string rare;

    public float addMaxHealthPoint;
    public float addDamage;
    public float addAttackSpeed;
    public float addSpeed;
    public float addJumps;

    private void Start()
    {
        GetComponent<SpriteRenderer>().color = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<PlayerInventory>().IsHaveSlots())
            {
                collision.GetComponent<PlayerInventory>().ControlItem(GetComponent<Item>());
                transform.position = new Vector3(0, -200);
            }
        }
    }
}
