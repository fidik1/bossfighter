using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField] private ItemGenerator itemGenerator;
    [SerializeField] private int chanceDrop;

    public void Drop(Vector3 position,bool boss = false)
    {
        if (Random.Range(0, 101) < chanceDrop && !boss)
        {
            itemGenerator.GenerateItem(position);
        }
        else if (boss)
        {
            itemGenerator.GenerateItem(position, true);
        }
    }
}
