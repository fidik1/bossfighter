using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private Entity boss = null;

    [SerializeField] private Player player;

    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private Text playerHealthText;

    [SerializeField] private Slider bossHealthBar;

    [SerializeField] private Text currentRound;

    [SerializeField] private List<Text> cardsText;
    [SerializeField] private List<int> cardsValue;

    private void Start()
    {
        RoundController.OnNewRound += OnNewRound;
        player.ChangedHP += ChangeHealthBar;
        player.OnCardAdded += OnCardAdded;

        if (boss != null)
            ChangeHealthBar();
    }

    private void ChangeHealthBar()
    {
        playerHealthBar.maxValue = player.maxHealthPoint;
        playerHealthBar.value = player.healthPoint;
        playerHealthText.text = player.healthPoint + " / " + player.maxHealthPoint;

        if (boss == null)
            return;

        bossHealthBar.value = boss.healthPoint;
    }

    private void OnCardAdded()
    {
        switch (player.lastCard.tag)
        {
            case "Health":
                cardsValue[0] += 1;
                cardsText[0].text = "x" + cardsValue[0];
                break;

            case "Damage":
                cardsValue[1] += 1;
                cardsText[1].text = "x" + cardsValue[1];
                break;

            case "AttackSpeed":
                cardsValue[2] += 1;
                cardsText[2].text = "x" + cardsValue[2];
                break;

            case "Speed":
                cardsValue[3] += 1;
                cardsText[3].text = "x" + cardsValue[3];
                break;

            case "Jump":
                cardsValue[4] += 1;
                cardsText[4].text = "x" + cardsValue[4];
                break;
        }
    }

    private void OnNewRound()
    {
        currentRound.text = "ROUND " + RoundController.currentRound.ToString();
        StartCoroutine(AnimationTextRound());

        if (boss != null)
            boss.ChangedHP -= ChangeHealthBar;
        
        if (RoundController.bossInGame != null)
        {
            bossHealthBar.gameObject.SetActive(true);
            boss = RoundController.bossInGame;
            bossHealthBar.maxValue = boss.maxHealthPoint;
            bossHealthBar.value = boss.healthPoint;
            boss.ChangedHP += ChangeHealthBar;
        }
        else
            bossHealthBar.gameObject.SetActive(false);
    }

    private IEnumerator AnimationTextRound()
    {
        currentRound.GetComponent<Animator>().SetBool("isNewRound", true);
        yield return new WaitForSeconds(1.5f);
        currentRound.GetComponent<Animator>().SetBool("isNewRound", false);
    }

    private void OnDestroy()
    {
        player.ChangedHP -= ChangeHealthBar;
        player.OnCardAdded -= OnCardAdded;

        if (boss != null)
            boss.ChangedHP -= ChangeHealthBar;
    }
}
