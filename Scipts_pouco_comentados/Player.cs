using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //var. codição do tripleshot
    public bool canTriple = false;
    //var do powerup de speedboost
    public bool Boosted = false;

    public bool Shielded = false;

    public int lives = 3;

    [SerializeField]
    private float _fireRate = 0.25f;
    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    GameObject _explosionPlayerPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _ShieldPrefab;

    [SerializeField]
    public GameObject shieldGameObject;

    private float _canFire = 0.0f;
    [SerializeField]
    private float _speed = 5.0f;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private Spawn_Manager _spawnManager;
    private AudioSource _audioSource;

    [SerializeField]
    private GameObject[] _engines;

    private int hitCounts = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -2.5f, 0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        _audioSource = GetComponent<AudioSource>();

        if (_uiManager != null)
        {
            _uiManager.UpdateLives(lives);
        }

        if(_spawnManager != null)
        {
            _spawnManager.StartSpawn();
        }
    }

    void Update()
    {
        movimentação();
        if (Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.Joystick1Button2)))
        {
            tiro();
        }
        
    }
    private void movimentação()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        /*if o boost estiver aqtivo
         * move 1,5x mais ´rapido
         * else move normal
        */

        if (Boosted == false)
        {
            //_speed = 5.0f;
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }
        else
        {
            //_speed = 7.5f;
            transform.Translate(Vector3.right * _speed*1.5f * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed*1.5f * verticalInput * Time.deltaTime);
        }

        //limita aonde andar no mapa pelo eixo Y
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        //limita aonde anadar no mapa pelo eixo X
        if (transform.position.x >= 9.0f)
        {
            transform.position = new Vector3(-8.0f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.0f)
        {
            transform.position = new Vector3(8.0f, transform.position.y, 0);
        }

    }

    private void tiro()
    {
        if (Time.time > _canFire) 
        {
            _audioSource.Play();

            if (canTriple == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f), Quaternion.identity);
                
            }
            _canFire = Time.time + _fireRate;

             }
        
        }

    public void Damage()
    {
       

        if (Shielded == true)
        {
            Shielded = false;
            shieldGameObject.SetActive(false);
            return;
        }

        hitCounts++;

        if (hitCounts == 1)
        {
            _engines[0].SetActive(true);
        }
        else if (hitCounts == 2)
        {
            _engines[1].SetActive(true);
        }

        lives--;
        _uiManager.UpdateLives(lives);

        if (lives < 1)
        {
            Destroy(this.gameObject);
            Instantiate(_explosionPlayerPrefab, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uiManager.ShowTitleScreen();
        }
    }
    
    //metodo pra ativar o speedBoost
    public void speedBoostPowerUpOn()
    {
        StartCoroutine(speedBoostPowerDownRoutine());
    }

    public IEnumerator speedBoostPowerDownRoutine()
    {
        Boosted = true;

        yield return new WaitForSeconds(5.0f);
      
        Boosted = false;
    }
    //metodo pra ativar o triplotiro
    public void TripleShotPowerUpOn()
    {
        
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    public IEnumerator TripleShotPowerDownRoutine()
    {
        canTriple = true;
        yield return new WaitForSeconds(5.0f);

        canTriple = false;
    }

    public void ShieldPowerUpOn()
    {
        Shielded = true;
        shieldGameObject.SetActive(true);
    }
   
}

 
