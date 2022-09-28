using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Cell : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public event Action destroy;

    private Transform draggingParent;
    private Transform inventory;
    private Transform equipment;
    private Transform cells;
    private StatsPanel statsPanel;
    private PlayerInventory playerInventory;
    public Item item;

    private Vector3 startPos;
    public bool isEquiped;
    private int indexEquipment;

    public void Init(Transform draggingParent, Transform inventory, Transform equipment, Transform cells, StatsPanel statsPanel, PlayerInventory playerInventory, Item item)
    {
        this.draggingParent = draggingParent;
        this.inventory = inventory;
        this.equipment = equipment;
        this.cells = cells;
        this.statsPanel = statsPanel;
        this.playerInventory = playerInventory;
        this.item = item;

        GetComponent<Image>().color = item.color;
        
        PlayerInventory.ClosedInventory += statsPanel.Exit;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        statsPanel.SetStats(transform.position, item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        statsPanel.Exit();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = transform.position;
        statsPanel.Exit();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!In((RectTransform)inventory))
            destroy?.Invoke();
        int closestIndex = 0;
        int closestIndexEquipment = 0;
        bool original = false;
        float distance = 0;

        for (int i = 0; i < cells.childCount; i++)
        {
            if (Vector2.Distance(transform.position, cells.GetChild(i).position) < Vector2.Distance(transform.position, cells.GetChild(closestIndex).position))
            {
                closestIndex = i;
                original = true;
                distance = Vector2.Distance(transform.position, cells.GetChild(i).position);
            }
        }

        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i).position == cells.GetChild(closestIndex).position)
            {
                transform.parent.GetChild(i).position = startPos;
                break;
            }
        }

        for (int i = 0; i < equipment.childCount; i++)
        {
            if (Vector2.Distance(transform.position, equipment.GetChild(i).position) < distance)
            {
                closestIndexEquipment = i;
                original = false;
                distance = Vector2.Distance(transform.position, equipment.GetChild(i).position);
            }
        }

        transform.position = cells.GetChild(closestIndex).position;

        if (isEquiped && original)
        {
            equipment.GetChild(indexEquipment).GetComponent<CellEquipment>().Equip(null);
            isEquiped = false;
        }

        if (!original)
        {
            if (item.tag == equipment.GetChild(closestIndexEquipment).tag)
            {
                for (int i = 0; i < transform.parent.childCount; i++)
                {
                    if (transform.parent.GetChild(i).position == equipment.GetChild(closestIndexEquipment).position)
                    {
                        transform.parent.GetChild(i).GetComponent<Cell>().isEquiped = false;
                        transform.parent.GetChild(i).position = startPos;
                        break;
                    }
                }

                transform.position = equipment.GetChild(closestIndexEquipment).position;
                if (!isEquiped)
                {
                    equipment.GetChild(closestIndexEquipment).GetComponent<CellEquipment>().Equip(item);
                    indexEquipment = closestIndexEquipment;
                    isEquiped = true;
                }
            }
        }

        playerInventory.Render();
    }

    private bool In(RectTransform originalParent)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(originalParent, transform.position);
    }

    private void OnDestroy()
    {
        PlayerInventory.ClosedInventory -= statsPanel.Exit;
    }
}
