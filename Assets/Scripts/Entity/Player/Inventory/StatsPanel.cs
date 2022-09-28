using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] private List<Text> texts;
    [SerializeField] private List<GameObject> panel;

    public void SetStats(Vector3 position, Item item)
    {
        transform.position = position + new Vector3(150, 25);

        texts[0].text = item.tag;
        texts[1].color = item.color;
        texts[1].text = "Rarity: " + item.rare;

        if (item.addMaxHealthPoint != 0)
        {
            panel[2].gameObject.SetActive(true);
            texts[2].text = "Max Health + " + item.addMaxHealthPoint;
        }
        if (item.addDamage != 0)
        {
            panel[3].gameObject.SetActive(true);
            texts[3].text = "Damage + " + item.addDamage;
        }
        if (item.addAttackSpeed != 0)
        {
            panel[4].gameObject.SetActive(true);
            texts[4].text = "Attack Speed + " + item.addAttackSpeed;
        }
        if (item.addSpeed != 0)
        {
            panel[5].gameObject.SetActive(true);
            texts[5].text = "Speed + " + item.addSpeed;
        }
        if (item.addJumps != 0)
        {
            panel[6].gameObject.SetActive(true);
            texts[6].text = "Jump + " + item.addJumps;
        }
    }

    public void Exit()
    {
        transform.position = new Vector3(0, -1000);
        for (int i = 2; i < 7; i++)
            panel[i].gameObject.SetActive(false);
    }
}
