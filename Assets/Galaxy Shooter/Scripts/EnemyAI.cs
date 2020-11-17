using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] 
    private GameObject explosionAnimation;
    
    [SerializeField] 
    private float speed = 5.0f;

    [SerializeField] 
    private AudioClip audioClip;

    private UIManager _uiManager;
    private GameManager _gameManager;

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    void Update()
    {
        if (_gameManager.GetGameOver())
        {
            Destroy(gameObject);
            return;
        }
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.down * (speed * Time.deltaTime));
        
        if (transform.position.y < -6f)
        {
            var xPos = Random.Range(-8, 8);
            transform.position = new Vector3(xPos, 4.2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (!player) return;
            player.Damage();
        }
        
        if (other.CompareTag("Laser"))
        {
            var laser = other.GetComponent<Laser>();
            if (!laser) return;
            Destroy(laser.gameObject);
            _uiManager.UpdateScore();
        }

        if (!(Camera.main is null)) AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
        Destroy(gameObject);
        Instantiate(explosionAnimation, transform.position, quaternion.identity);
    }
}
