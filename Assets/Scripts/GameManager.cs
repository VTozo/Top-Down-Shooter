using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Player player;
    public Enemy enemy;
    public float distance;
    int score;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
        score = 0;
    }

    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            Vector3 delta = new Vector3(Mathf.Cos(angle) * distance, 0, Mathf.Sin(angle) * distance);
            Vector3 enemySpawnLocation = player.transform.position + delta;

            Instantiate<Enemy>(enemy, enemySpawnLocation, new Quaternion(0, 0, 0, 0));
        }
    }

    public void changeScore(int n)
    {
        score += n;
        scoreText.text = score.ToString();
    }
}
