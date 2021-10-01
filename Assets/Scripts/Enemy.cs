using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float maxLife;
    private float life;
    private Transform target;
    public float movementSpeed;
    public GameObject explosion;

    void Start()
    {
        life = maxLife;
        target = GameObject.Find("Player").transform;

        AudioSource audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.LookAt(target.transform);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * movementSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("GameOver");
        }
        else if (collision.gameObject.tag != "Enemy")
        {
            life--;
        }

        if (life <= 0)
        {
            // Se o audio não estiver tocando ainda
            // Verificação previne que toque mais de uma vez enquanto o objeto é destruído
            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();

            // Instancia o efeito de explosão
            Instantiate(explosion, transform.position, transform.rotation);

            // Delay para destruir o objeto, para tocar o audio até o fim
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            FindObjectOfType<GameManager>().changeScore(100);
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
        
        
        

    }
}
