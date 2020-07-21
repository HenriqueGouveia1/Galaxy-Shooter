using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField]
    private float _speed = 1.0f;
    [SerializeField]
    private int powerUpID;//0 = triple shot / 1 = speed boost / 2 = shield 
    [SerializeField]
    private AudioClip _clip;

    
    void Start()
    {

    }
   
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -7) { Destroy(this.gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //_audioSource.Play();
        if (other.tag == "Player")
        {
            Debug.Log("Collided with" + other.name);
            Player player = other.GetComponent<Player>();
          
            if (player != null)
            {
                if (powerUpID == 0)
                {
                    //enable tripleshot
                    player.TripleShotPowerUpOn();
                }
                else if (powerUpID == 1)
                {
                    //enable speed
                    player.speedBoostPowerUpOn();

                }
                else if (powerUpID == 2)
                {
                    player.ShieldPowerUpOn();

                }
                AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            }

      
            Destroy(this.gameObject);
        }
        

    }

  }
