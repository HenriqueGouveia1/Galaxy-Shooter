using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

   
public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image livesImageDisplay;
    public GameObject titleScreen;
    public GameObject titleText;
    public Text scoreText;
    public int score;

        
  public void UpdateLives(int currentLives)
    {
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
       titleText.SetActive(true);
    }

    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
        titleText.SetActive(false);
        scoreText.text = "Score: ";
    }
   
}
