using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    public GameObject player;
    private UIManager _uiManager;
    
    void Start()
    {

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
       
    }
    // Update is called once per frame
    void Update()
    {

        if (gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameOver = false;
                Instantiate(player,new Vector3(0, -2.5f, 0), Quaternion.identity);
                _uiManager.HideTitleScreen();

             }
        }

    }
}
