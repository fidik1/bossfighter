using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroom : Entity
{
    [SerializeField] private GameObject player;

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Bullet")
        {
            SpendHP(collider.GetComponent<Bullet>().damage);
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Bullet")
            StartCoroutine(ChangeColor());
            
    }

    IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
