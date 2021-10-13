using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
    Rigidbody rb;
    AudioSource laserSound;
    public float movementSpeed;
    public GameObject objectToClone;
    public Transform shotSpawnPoint;
    public float shotSpeed;
    public Camera cam;
    private bool powerUp;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        laserSound = GetComponent<AudioSource>();
        powerUp = false;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            laserSound.Play();
        }

        // Atirar sem power up
        if (Input.GetKeyDown(KeyCode.Mouse0) && !powerUp)
        {
            GameObject newBullet = Instantiate(objectToClone, shotSpawnPoint.position, shotSpawnPoint.rotation);
            newBullet.GetComponent<Rigidbody>().AddForce(shotSpawnPoint.forward * shotSpeed);
        }

        // Atirar com power up
        if (Input.GetKey(KeyCode.Mouse0) && powerUp)
        {
            GameObject newBullet = Instantiate(objectToClone, shotSpawnPoint.position, shotSpawnPoint.rotation);
            newBullet.GetComponent<Rigidbody>().AddForce(shotSpawnPoint.forward * shotSpeed);
        }

        // Convertendo a posição do mouse em um ponto no espaço 3D
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        
        // Rotacionando o jogador para a posição do mouse
        float t = cam.transform.position.y / (cam.transform.position.y - point.y);
        Vector3 finalPoint = new Vector3(t * (point.x - cam.transform.position.x) + cam.transform.position.x, 0, t * (point.z - cam.transform.position.z) + cam.transform.position.z);
        transform.LookAt(finalPoint, Vector3.up);
    }

    void FixedUpdate()
    {
        // Mover
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");
        Vector3 movementDir = new Vector3(hAxis, 0, vAxis);
        rb.AddForce(movementDir * movementSpeed);
    }

    public void PowerUp()
    {
        powerUp = true;
        StartCoroutine(PowerUpTimer());
    }

    IEnumerator PowerUpTimer()
    {
        yield return new WaitForSeconds(2);
        powerUp = false;
    }
}
