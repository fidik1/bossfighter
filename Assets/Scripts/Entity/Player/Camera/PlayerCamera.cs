using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float FollowSpeed = 2f;
    [SerializeField] private float yOffset = 1f;

    private void Update()
    {
        Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y + yOffset);
            
        if (transform.position.x > 186.5f)
            newPos = new Vector3(186.5f, player.transform.position.y + yOffset);
        else if(transform.position.x < -186.5f)
            newPos = new Vector3(-186.5f, player.transform.position.y + yOffset);

        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}
