using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    [SerializeField] private new Transform camera;
    [SerializeField] private List<GameObject> listOfEnemy;
    [SerializeField] private List<GameObject> enemy;
    [SerializeField] private List<GameObject> enemyInGame;

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
        int i = 0;
        foreach (GameObject enemy in enemy)
        {
            GameObject enemyGame = Instantiate(enemy);
            enemyInGame.Add(enemyGame);
            if (i % 2 == 0)
                enemyGame.transform.position = new Vector3(camera.transform.position.x - 80, -22);
            else
                enemyGame.transform.position = new Vector3(camera.transform.position.x + 80, -22);
            i++;
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
