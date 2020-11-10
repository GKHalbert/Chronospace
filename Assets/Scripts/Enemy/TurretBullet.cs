﻿using UnityEngine;

public class TurretBullet : MonoBehaviour
{

    public float movementSpeed = 36f;
    public float timer = 0f;
    GameManager gameManager;
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * (movementSpeed * gameManager.GetTimeWarpMultiplier()));
        timer += 1.0F * Time.deltaTime;
        if (timer >= 3)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.KillPlayer();
        }
        else {
            Destroy(this.gameObject);
        }
    }

}
