using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<Item> item;

    [SerializeField] private GameObject inventory;

    [SerializeField] private Transform cells;
    [SerializeField] private Transform cellsBG;
    [SerializeField] private GameObject cell;
    [SerializeField] private Transform equipment;

    [SerializeField] private StatsPanel statsPanel;

    public delegate void InventoryEvent();
    public static InventoryEvent ClosedInventory;
    private int v;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventory.activeInHierarchy)
            {
                Time.timeScale = 1;
                inventory.SetActive(false);
                ClosedInventory?.Invoke();
            }
            else
            {
                Time.timeScale = 0;
                inventory.SetActive(true);
                Render();
            }
        }
    }

    public void Render()
    {
        CheckEquip();
        
        int indent = 0;
        for (int i = 0; i < item.Count; i++)
        {
            GameObject el = Instantiate(cell, cells);
            el.GetComponent<Cell>().Init(inventory.transform, equipment, cellsBG, statsPanel, GetComponent<PlayerInventory>(), item[i]);
            el.transform.position = cellsBG.GetChild(i - indent).position;
            el.transform.SetSiblingIndex(i);
            el.GetComponent<Cell>().destroy += () =>
            {
                Destroy(item[item.IndexOf(el.GetComponent<Cell>().item)].gameObject);
                item.Remove(el.GetComponent<Cell>().item);
                Destroy(el);
            };
        }
    }

    public void CheckEquip()
    {
        for (int i = 0; i < cells.childCount; i++)
        {
            if (!cells.GetChild(i).GetComponent<Cell>().isEquiped)
                Destroy(cells.GetChild(i).gameObject);
        }
    }

    public void ControlItem(Item itemObject, bool isRemove = false)
    {
        if (!isRemove)
            item.Add(itemObject);
        else
            item.Remove(itemObject);
    }

    public bool IsHaveSlots()
    {
        if (item.Count < 8)
            return true;
        else
            return false;
    }
}
