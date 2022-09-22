using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private List<DashShadow> shadow;

    private float time;
    private int currentShadow = 0;

    private void Awake()
    {
        PlayerMovement.isDashed += Dash;
    }

    private void Dash()
    {
        StartCoroutine(DashEnd());
    }

    private IEnumerator DashEnd()
    {
        time = 0.05f;
        while (time < playerMovement.dashTime)
        {
            time += Time.deltaTime * 5;
            shadow[currentShadow].ResetDash();
            shadow[currentShadow].transform.position = player.transform.position;
            _ = currentShadow < shadow.Count - 1 ? currentShadow++ : currentShadow = 0;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void OnDestroy()
    {
        PlayerMovement.isDashed -= Dash;
    }
}
