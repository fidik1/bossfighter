using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellEquipment : MonoBehaviour
{
    [SerializeField] private Player player;
    private Item item;

    public void Equip(Item item)
    {
        Dequip();
        this.item = item;
        if (item != null)
            player.AddEquipment(this.item);
    }

    public void Dequip()
    {
        if (item != null)
            player.RemoveEquipment(item);
    }
}
