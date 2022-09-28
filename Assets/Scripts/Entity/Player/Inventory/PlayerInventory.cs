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
    [SerializeField] private Transform draggingParrent;
    [SerializeField] private Transform equipment;

    [SerializeField] private List<int> index = new List<int>(8);

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
            if (!index.Contains(i))
            {
                GameObject el = Instantiate(cell, cells);
                el.GetComponent<Cell>().Init(draggingParrent, inventory.transform, equipment, cellsBG, statsPanel, GetComponent<PlayerInventory>(), item[i]);
                el.transform.position = cellsBG.GetChild(i - indent).position;
                el.transform.SetSiblingIndex(i);
                el.GetComponent<Cell>().destroy += () =>
                {
                    Destroy(item[item.IndexOf(el.GetComponent<Cell>().item)].gameObject);
                    item.Remove(el.GetComponent<Cell>().item);
                    Destroy(el);
                };
            }
            else
                indent++;
        }
    }

    public void CheckEquip()
    {
        index.Clear();
        for (int i = 0; i < cells.childCount; i++)
        {
            if (cells.GetChild(i).GetComponent<Cell>().isEquiped)
                index.Add(i);
            else
                Destroy(cells.GetChild(i).gameObject);
        }
    }

    public void AddItem(Item itemObject)
    {
        item.Add(itemObject);
    }

    public bool IsHaveSlots()
    {
        if (item.Count - index.Count < 8)
            return true;
        else
            return false;
    }
}
