using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpRegister {
    TripleLaser,
    Velocity,
    Shield
};

public class PowerUp : MonoBehaviour
{
    [SerializeField] 
    private PowerUpRegister powerUp;
    
    [SerializeField] 
    private float speed = 5f;

    [SerializeField] 
    private AudioClip audioClip;
    
    void Update()
    {
        transform.Translate(Vector3.down * (speed * Time.deltaTime));

        if (transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (!player) return;
            
            if (!(Camera.main is null))
                AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);

            if (powerUp == PowerUpRegister.TripleLaser) 
            {
                player.StartTripleShot();
            }

            if (powerUp == PowerUpRegister.Velocity)
            {
                player.StartToMoveFaster();
            }
            
            if (powerUp == PowerUpRegister.Shield)
            {
                player.ActivateShield();
            }
            
            Destroy(this.gameObject);
        }
    }
}
