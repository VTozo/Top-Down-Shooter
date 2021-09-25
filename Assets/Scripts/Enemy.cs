using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float maxLife;
    private float life;

    void Start()
    {
        life = maxLife;
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        life--;

        if(life <= 0)
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            Destroy(gameObject, 0.5f);
        }
        else if ((life / maxLife) <= (1f / 3f))
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        }
        else if ((life / maxLife) <= (2f / 3f))
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.yellow;
        }
        
        
        if (collision.gameObject.tag != "Player")
        {
            Destroy(collision.gameObject);
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
        
    }
}
