using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;

    public void GenerateItem(Vector3 position, bool boss = false)
    {
        GameObject item = Instantiate(itemPrefab, gameObject.transform);
        item.transform.position = position;
        GenerateItemStats(item.GetComponent<Item>(), boss);
    }

    private void GenerateItemStats(Item itemObject, bool boss)
    {
        GenerateItemType(itemObject);
        GenerateItemRarity(itemObject, boss);

        switch (itemObject.tag)
        {
            case "Helm":
                if (itemObject.rare == "Common")
                {
                    itemObject.addMaxHealthPoint = Random.Range(5, 16);
                    itemObject.addSpeed = Random.Range(5, 16);
                    itemObject.addJumps = Random.Range(0, 2);
                }
                else if(itemObject.rare == "Rare")
                {
                    itemObject.addMaxHealthPoint = Random.Range(10, 26);
                    itemObject.addSpeed = Random.Range(5, 16);
                    itemObject.addJumps = Random.Range(1, 2);
                }
                else if(itemObject.rare == "Epic")
                {
                    itemObject.addMaxHealthPoint = Random.Range(15, 31);
                    itemObject.addSpeed = Random.Range(5, 15);
                    itemObject.addJumps = Random.Range(0, 2);
                }
                else if(itemObject.rare == "Legendary")
                {
                    itemObject.addMaxHealthPoint = Random.Range(20, 41);
                    itemObject.addDamage = Random.Range(5, 15);
                    itemObject.addAttackSpeed = Random.Range(0, 5) / 100;
                    itemObject.addSpeed = Random.Range(5, 15);
                    itemObject.addJumps = Random.Range(0, 3);
                }
                break;

            case "Chestplate":
                if (itemObject.rare == "Common")
                {
                    itemObject.addMaxHealthPoint = Random.Range(12, 21);
                    itemObject.addSpeed = Random.Range(5, 16);
                    itemObject.addJumps = Random.Range(0, 2);
                }
                else if(itemObject.rare == "Rare")
                {
                    itemObject.addMaxHealthPoint = Random.Range(15, 31);
                    itemObject.addSpeed = Random.Range(5, 16);
                    itemObject.addJumps = Random.Range(1, 2);
                }
                else if(itemObject.rare == "Epic")
                {
                    itemObject.addMaxHealthPoint = Random.Range(23, 41);
                    itemObject.addSpeed = Random.Range(5, 15);
                    itemObject.addJumps = Random.Range(0, 2);
                }
                else if(itemObject.rare == "Legendary")
                {
                    itemObject.addMaxHealthPoint = Random.Range(33, 50);
                    itemObject.addDamage = Random.Range(5, 15);
                    itemObject.addAttackSpeed = Random.Range(0, 5) / 100;
                    itemObject.addSpeed = Random.Range(5, 15);
                    itemObject.addJumps = Random.Range(0, 3);
                }
                break;

            case "Gun":
                if (itemObject.rare == "Common")
                {
                    itemObject.addDamage = Random.Range(5, 15);
                    itemObject.addAttackSpeed = Random.Range(0, 5) / 100;
                }
                else if(itemObject.rare == "Rare")
                {
                    itemObject.addDamage = Random.Range(7, 21);
                    itemObject.addAttackSpeed = Random.Range(0, 5) / 100;
                }
                else if(itemObject.rare == "Epic")
                {
                    itemObject.addDamage = Random.Range(12, 28);
                    itemObject.addAttackSpeed = Random.Range(0, 5) / 100;
                }
                else if(itemObject.rare == "Legendary")
                {
                    itemObject.addMaxHealthPoint = Random.Range(5, 15);
                    itemObject.addDamage = Random.Range(15, 30);
                    itemObject.addAttackSpeed = Random.Range(0, 5) / 100;
                    itemObject.addSpeed = Random.Range(5, 15);
                    itemObject.addJumps = Random.Range(0, 3);
                }
                break;

            case "Leggings":
                if (itemObject.rare == "Common")
                {
                    itemObject.addMaxHealthPoint = Random.Range(5, 16);
                    itemObject.addSpeed = Random.Range(5, 16);
                    itemObject.addJumps = Random.Range(0, 2);
                }
                else if(itemObject.rare == "Rare")
                {
                    itemObject.addMaxHealthPoint = Random.Range(10, 26);
                    itemObject.addSpeed = Random.Range(5, 16);
                    itemObject.addJumps = Random.Range(1, 4);
                }
                else if(itemObject.rare == "Epic")
                {
                    itemObject.addMaxHealthPoint = Random.Range(15, 31);
                    itemObject.addSpeed = Random.Range(15, 31);
                    itemObject.addJumps = Random.Range(2, 5);
                }
                else if(itemObject.rare == "Legendary")
                {
                    itemObject.addMaxHealthPoint = Random.Range(30, 45);
                    itemObject.addDamage = Random.Range(5, 15);
                    itemObject.addAttackSpeed = Random.Range(0, 5) / 100;
                    itemObject.addSpeed = Random.Range(5, 15);
                    itemObject.addJumps = Random.Range(0, 3);
                }
                break;

            case "Boots":
                if (itemObject.rare == "Common")
                {
                    itemObject.addSpeed = Random.Range(10, 21);
                    itemObject.addJumps = Random.Range(0, 3);
                }
                else if (itemObject.rare == "Rare")
                {
                    itemObject.addSpeed = Random.Range(15, 26);
                    itemObject.addJumps = Random.Range(1, 5);
                }
                else if(itemObject.rare == "Epic")
                {
                    itemObject.addSpeed = Random.Range(20, 31);
                    itemObject.addJumps = Random.Range(2, 6);
                }
                else if(itemObject.rare == "Legendary")
                {
                    itemObject.addMaxHealthPoint = Random.Range(5, 15);
                    itemObject.addDamage = Random.Range(5, 15);
                    itemObject.addAttackSpeed = Random.Range(0, 5) / 100;
                    itemObject.addSpeed = Random.Range(15, 30);
                    itemObject.addJumps = Random.Range(3, 8);
                }
                break;
        }
    }

    private void GenerateItemRarity(Item itemObject, bool boss)
    {
        int rare = Random.Range(0, 101);
        if (rare <= 50 && !boss)
        {
            itemObject.color = new Color32(207, 207, 207, 255);
            itemObject.rare = "Common";
        }
        else if (rare > 50 && rare <= 75 && !boss)
        {
            itemObject.color = new Color32(97, 202, 255, 255);
            itemObject.rare = "Rare";
        }
        else if (rare > 75 && rare <= 94 && !boss || rare < 50 && boss)
        {
            itemObject.color = new Color32(251, 107, 255, 255);
            itemObject.rare = "Epic";
        }
        else if (rare > 94 && !boss || rare >= 50 && boss)
        {
            itemObject.color = new Color32(255, 130, 37, 255);
            itemObject.rare = "Legendary";
        }
    }

    private void GenerateItemType(Item itemObject)
    {
        int rare = Random.Range(0, 5);
        switch (rare)
        {
            case 0:
                itemObject.tag = "Helm";
                break;

            case 1:
                itemObject.tag = "Chestplate";
                break;

            case 2:
                itemObject.tag = "Gun";
                break;

            case 3:
                itemObject.tag = "Leggings";
                break;

            case 4:
                itemObject.tag = "Boots";
                break;
        }
    }
}
