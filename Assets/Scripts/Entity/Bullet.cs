using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage { get; set; }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy" || collider.tag == "Ground" || collider.tag == "Wall")
        {
            transform.position = new Vector3(-1000, 2000);
        }
    }
}
