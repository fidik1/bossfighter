using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float FollowSpeed = 2f;
    [SerializeField] private float yOffset = 1f;

    void Update()
    {
        Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}
