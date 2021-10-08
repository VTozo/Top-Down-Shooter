using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Player player;
    public Enemy enemy;
    public PowerUp powerUp;
    public float enemySpawnDistance;
    public float powerUpSpawnDistance;
    int score;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerUp());
        score = 0;
    }

    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.8f);

            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            Vector3 delta = new Vector3(Mathf.Cos(angle) * enemySpawnDistance, 0, Mathf.Sin(angle) * enemySpawnDistance);
            Vector3 SpawnLocation = player.transform.position + delta;

            Instantiate<Enemy>(enemy, SpawnLocation, new Quaternion(0, 0, 0, 0));
        }
    }

    IEnumerator SpawnPowerUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);

            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            Vector3 delta = new Vector3(Mathf.Cos(angle) * powerUpSpawnDistance, 0, Mathf.Sin(angle) * powerUpSpawnDistance);
            Vector3 SpawnLocation = player.transform.position + delta;

            Instantiate<PowerUp>(powerUp, SpawnLocation, new Quaternion(0, 0, 0, 0));
        }
    }

    public void changeScore(int n)
    {
        score += n;
        scoreText.text = score.ToString();
    }
}
