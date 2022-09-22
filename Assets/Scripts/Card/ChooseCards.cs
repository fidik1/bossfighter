using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCards : MonoBehaviour
{
    [SerializeField] private Player player;
    
    [SerializeField] private int countOfCards = 3;

    [SerializeField] private List<Card> cardBuff;

    [SerializeField] private GameObject cardPanel;

    [SerializeField] private List<Card> cardActiveBuff;
    [SerializeField] private List<Image> cardSprite;
    [SerializeField] private List<Text> cardText;

    private bool boss;

    private void Start()
    { 
        RoundController.OnNewRound += OnNewRound;
        RoundController.OnRoundEnd += OnRoundEnd;
    }

    private void OnNewRound()
    {
        if (RoundController.bossInGame != null)
            boss = true;
    }

    private void OnRoundEnd()
    {
        if (boss)
        {
            SetCards();
            cardPanel.SetActive(true);
        }
    }

    private void SetRandomBuff()
    {
        cardActiveBuff.Clear();

        for (int i = 0; i < countOfCards; i++)
        {
            Card randomBuff = cardBuff[Random.Range(0, cardBuff.Count)];
            cardActiveBuff.Add(randomBuff);
        }
    }

    private void SetCards()
    {
        SetRandomBuff();

        for (int i = 0; i < countOfCards; i++)
        {
            cardSprite[i].sprite = cardActiveBuff[i].sprite;
            cardText[i].text = cardActiveBuff[i].text;
        }
        Time.timeScale = 0;
    }

    public void ChooseCard(int numberOfCard)
    {
        Time.timeScale = 1;
        player.ReloadBuffs(cardActiveBuff[numberOfCard]);
        cardPanel.SetActive(false);

        boss = false;
    }

    private void OnDestroy()
    {
        RoundController.OnNewRound -= OnNewRound;
        RoundController.OnRoundEnd -= OnRoundEnd;
    }
}
