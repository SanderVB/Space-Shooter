using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //config params
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float playerXPadding = 1f;
    [SerializeField] float playerYPadding = 1f;
    [SerializeField] int health = 500;
    [SerializeField] float explosionDuration = 1f;
    [SerializeField] GameObject deathExplosion;
    [SerializeField] float maxMoveY = .3f;

    [Header("Projectile")]
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] float reloadSpeed = .1f;
    [SerializeField] GameObject laserPrefab;
    Coroutine firingCoroutine;

    [Header("Sounds")]
    [SerializeField] AudioClip hitSound;
    [SerializeField] [Range(0, 1)] float hitSoundVolume = .75f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0,1)] float deathSoundVolume = .75f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = .25f;


    float xMin;
    float xMax;
    float yMin;
    float yMax;

    //Level level;
	// Use this for initialization
	void Start ()
    {
        //level=FindObjectOfType<Level>();
        SetUpMoveBoundaries();
        FindObjectOfType<GameSession>().UpdateHealth(health);
    }


    // Update is called once per frame
    void Update ()
    {
        Move();
        Fire();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        FindObjectOfType<GameSession>().UpdateHealth(health);

        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
        else
        {
            AudioSource.PlayClipAtPoint(hitSound,
                Camera.main.transform.position,
                hitSoundVolume);

        }
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, 
            Camera.main.transform.position, 
            deathSoundVolume);
        Destroy(gameObject);
        GameObject gO = Instantiate(
            deathExplosion,
            transform.position,
            Quaternion.identity);
        Destroy(gO, explosionDuration);
        FindObjectOfType<Level>().LoadGameOver();
        //level.LoadGameOver();
    }

    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContiniously());
        }
        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContiniously()
    {
        while (true)
        {
            GameObject playerLaser = Instantiate(
                    laserPrefab,
                    transform.position,
                    Quaternion.identity);
            playerLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(reloadSpeed);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + playerXPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - playerXPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + playerYPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, maxMoveY, 0)).y - playerYPadding;
    }

}
