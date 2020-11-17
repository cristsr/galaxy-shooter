using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image playerLive;
    public Text scoreText;
    public Image mainMenu;
    public GameObject player;
    private int _score;

    public void UpdateLives(int currentLife)
    {
        Debug.Log("Lives: " + currentLife);
        playerLive.sprite = lives[currentLife];
    }

    public void UpdateScore()
    {
        _score += 10;
        scoreText.text = "Score: " + _score;
    }

    public void ShowMainMenu()
    {
        mainMenu.gameObject.SetActive(true);
        ResetScore();
    }
    
    public void HideMainMenu()
    {
        mainMenu.gameObject.SetActive(false);
    }

    private void ResetScore()
    {
        _score = 0;
        scoreText.text = "Score: " + _score;
    }
}
