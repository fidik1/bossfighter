using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    [SerializeField] private List<GameObject> listOfEnemy;
    [SerializeField] private List<GameObject> enemy = new List<GameObject>();
    [SerializeField] private List<GameObject> enemyInGame = new List<GameObject>();

    [SerializeField] private List<GameObject> listOfBoss;
    public static Entity bossInGame = null;

    public delegate void RoundEvent();
    public static RoundEvent OnNewRound;
    public static RoundEvent OnRoundEnd;

    [SerializeField] private int countOfEnemy;
    public static int currentRound = 0;

    private void Start()
    {
        NewRound();
    }

    private void NewRound()
    {
        currentRound++;
        GenerateEnemy();

        foreach (GameObject enemy in enemy)
        {
            GameObject enemyGame = Instantiate(enemy);
            enemyInGame.Add(enemyGame);
            enemyGame.transform.position = new Vector3(-30, 20);
        }
        if (enemy.Count > countOfEnemy)
        {
            bossInGame = enemyInGame[enemyInGame.Count - 1].GetComponent<Entity>();
        }
        OnNewRound?.Invoke();
    }

    private void GenerateEnemy()
    {
        bossInGame = null;
        enemy.Clear();

        if (currentRound % 5 != 0)
        {
            for (int i = 0; i < countOfEnemy; i++)
            {
                enemy.Add(listOfEnemy[Random.Range(0, listOfEnemy.Count)]);
            }
        }
        else
        {
            for (int i = 0; i < countOfEnemy; i++)
            {
                enemy.Add(listOfEnemy[Random.Range(0, listOfEnemy.Count)]);
            }
            enemy.Add(listOfBoss[Random.Range(0, listOfBoss.Count)]);
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemyInGame.Remove(enemy);
        CheckEnemy();
    }

    private void CheckEnemy()
    {
        if (enemyInGame.Count == 0)
        {
            OnRoundEnd?.Invoke();
            NewRound();
        }
    }
}
