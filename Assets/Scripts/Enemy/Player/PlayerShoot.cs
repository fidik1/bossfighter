using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private Transform shootPoint;
    [SerializeField] private List<GameObject> bulletPool;

    [SerializeField] private float bulletSpeed;

    private int currentBullet = 0;
    private bool isShooted;

    private void Update()
    {
        float rotationAngle = Mathf.Atan2(Camera.main.ScreenToWorldPoint(Input.mousePosition).y - shootPoint.transform.position.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).x - shootPoint.transform.position.x) * Mathf.Rad2Deg;
        shootPoint.rotation = Quaternion.Euler(0, 0, rotationAngle);

        if (Input.GetMouseButton(0) && !isShooted)
        {
            Shoot(rotationAngle);
        }
    }

    private void Shoot(float rotationAngle)
    {
        GameObject bullet = bulletPool[currentBullet];
        bullet.transform.position = shootPoint.transform.position;
        bullet.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
        bullet.GetComponent<Rigidbody2D>().velocity = shootPoint.right * bulletSpeed;
        bullet.GetComponent<Bullet>().damage = player.damage;

        _ = currentBullet < bulletPool.Count - 1 ? currentBullet++ : currentBullet = 0;

        isShooted = true;
        StartCoroutine(CooldownShoot());
    }

    private IEnumerator CooldownShoot()
    {
        yield return new WaitForSeconds(player.attackSpeed);
        isShooted = false;
    }
}
