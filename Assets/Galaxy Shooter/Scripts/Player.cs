using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    [SerializeField]
    private GameObject laser;
    
    [SerializeField]
    private GameObject tripleLaser;

    [SerializeField] 
    private GameObject explosion;
    
    [SerializeField] 
    private GameObject shield;

    [SerializeField]
    private float fireRate = .25f;
    
    [SerializeField]
    private int life = 3;

    [SerializeField]
    private GameObject[] enginesFailure; 

    private float _canFire = 0f;
    private bool _canTripleShot = false;
    private bool _canMoveFaster = false;
    private bool _hasShield = false;
    private UIManager _uiManager;
    private GameManager _gameManager;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        transform.position = new Vector3(0, 0, 0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager)
        {
            _uiManager.UpdateLives(life);
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        LimitatePlayer();
        ShootLaser();
    }

    private void MovePlayer() {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        var nextSpeed = _canMoveFaster ? speed * 2f : speed;

        transform.Translate(Vector3.right * (nextSpeed * horizontalInput * Time.deltaTime)); 
        transform.Translate(Vector3.up * (nextSpeed * verticalInput * Time.deltaTime)); 
    }

    private void LimitatePlayer() 
    {
        if (transform.position.y > 0) 
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }

        if (transform.position.y < -4.2f) 
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        if (transform.position.x > 8) 
        {
            transform.position = new Vector3(-8, transform.position.y, 0);
        }

        if (transform.position.x < -8) 
        {
            transform.position = new Vector3(8, transform.position.y, 0);
        }
    }

    private void ShootLaser() 
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)) {
            if(Time.time < _canFire) 
            {
                return;
            }

            if (_canTripleShot) 
            {
                Instantiate(tripleLaser, transform.position, Quaternion.identity);
            }
            else
            {
                var position = transform.position + new Vector3(0, .70f);
                Instantiate(laser, position, Quaternion.identity);
            }
            
            _canFire = Time.time + fireRate;
            _audioSource.Play();
        }
    }

    public void StartTripleShot()
    {
        _canTripleShot = true;
        StartCoroutine(TripleLaserDownRoutine());
    }

    private IEnumerator TripleLaserDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _canTripleShot = false;
    }

    public void StartToMoveFaster()
    {
        _canMoveFaster = true;
        StartCoroutine(this.MoveFasterDownRoutine());
    }
    
    private IEnumerator MoveFasterDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _canMoveFaster = false;
    }

    public void Damage()
    {
        if (_hasShield)
        {
            _hasShield = false;
            shield.gameObject.SetActive(false);
            return;
        }
        
        life--;
        _uiManager.UpdateLives(life);

        if (life == 2)
        {
            enginesFailure[0].SetActive(true);
        } 
        
        if (life == 1)
        {
            enginesFailure[1].SetActive(true);
        }

        if (life == 0)
        {
            Instantiate(explosion, transform.position, quaternion.identity);
            Destroy(gameObject);
            _gameManager.GameOver();
        }
    }

    public void ActivateShield()
    {
        _hasShield = true;
        shield.gameObject.SetActive(true);
    }
}
