using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy_AI : MonoBehaviour
{
    

    [SerializeField]
    private float _speed= 5.0f;
    [SerializeField]
    GameObject _enemyExplosionPrefab;

    [SerializeField]
    private AudioClip _clip;

    private UIManager _uiManager;
    
    void Start()
    {

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        realocar();
    }

    private void realocar()
    {
        if (transform.position.y <= -5.39f)
        {
            float randomX = Random.Range(7.7f, -7.7f);
            transform.position = new Vector2(randomX, 6.34f);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }
            Destroy(other.gameObject);
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject);
            _uiManager.UpdateScore();
        }
        else if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }         
                     
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject); 
        }
      
    }

   
    //collidir com o player faz explodir, sendo que o player perde uma vida apenas
    // quando o laser bater no enemy ele some e o enemy também

}
