using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Cell : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public event Action destroy;

    private Transform inventory;
    private Transform equipment;
    private Transform cells;
    private StatsPanel statsPanel;
    private PlayerInventory playerInventory;
    public Item item;

    private Vector3 startPos;
    public bool isEquiped;
    private int indexEquipment;

    int closestIndex = 0;
    int closestIndexEquipment = 0;
    bool original = false;
    float distance = 0;

    public void Init(Transform inventory, Transform equipment, Transform cells, StatsPanel statsPanel, PlayerInventory playerInventory, Item item)
    {
        this.inventory = inventory;
        this.equipment = equipment;
        this.cells = cells;
        this.statsPanel = statsPanel;
        this.playerInventory = playerInventory;
        this.item = item;

        GetComponent<Image>().color = item.color;

        PlayerInventory.ClosedInventory += statsPanel.Exit;
        destroy += Dequip;
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
        startPos = transform.localPosition;
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
        closestIndex = 0;
        closestIndexEquipment = 0;
        original = false;
        distance = 0;
        bool eq = false;

        for (int i = 0; i < cells.childCount; i++)
        {
            if (Vector2.Distance(transform.position, cells.GetChild(i).position) < Vector2.Distance(transform.position, cells.GetChild(closestIndex).position))
            {
                closestIndex = i;
                original = true;
                eq = true;
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

        if (distance == 0)
            distance = 10000;

        for (int i = 0; i < equipment.childCount; i++)
        {
            if (Vector2.Distance(transform.position, equipment.GetChild(i).position) < distance)
            {
                closestIndexEquipment = i;
                original = false;
                eq = true;
                distance = Vector2.Distance(transform.position, equipment.GetChild(i).position);
            }
        }

        Dequip();

        if (eq)
            transform.position = cells.GetChild(closestIndex).position;
        else
            transform.localPosition = startPos;

        if (!original && eq)
            Equip();

        playerInventory.Render();
    }

    private void Equip()
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

    private void Dequip()
    {
        if (isEquiped && original)
        {
            equipment.GetChild(indexEquipment).GetComponent<CellEquipment>().Equip(null);
            isEquiped = false;
        }
    }

    private bool In(RectTransform originalParent)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(originalParent, transform.position);
    }

    private void OnDestroy()
    {
        PlayerInventory.ClosedInventory -= statsPanel.Exit;
        destroy -= Dequip;
    }
}
