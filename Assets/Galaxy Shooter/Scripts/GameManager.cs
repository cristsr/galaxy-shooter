using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject player;
    
    private UIManager _uiManager;
    private SpawnManager _spawnManager;
    private bool _gameOver;
    
    void Start()
    {
        _gameOver = true;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
    
    void Update()
    {
        if (_gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!_uiManager) return;
                
                _uiManager.HideMainMenu();
                Instantiate(player, Vector3.zero, Quaternion.identity);
                _spawnManager.StartSpawning();
                
                _gameOver = false;
            }
        }
    }

    public void GameOver()
    {
        _gameOver = true;
        _uiManager.ShowMainMenu();
        _spawnManager.StopSpawning();
    }

    public bool GetGameOver()
    {
        return _gameOver;
    }
}
