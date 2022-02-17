using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject Canvas;
    public CharacterController controller;
    public Joystick joystick;
    private Transform playerTransform;


    public static int coins = 0;
    public static int currentLevel = 1;
    public int maxHealth = 3;
    public static int health = 3;

    public float speed = 3f;
    public float gravity = -9f;

    private Vector3 velocity;
    private float horizontalMove;

    public static bool isLevelStart = true;
    private bool levelEnd = false;
    public bool playerOnGround;
    public float rotationSpeed = 5f;



    void Start()
    {
        playerTransform = GetComponent<Transform>();
        isLevelStart = true;

        LoadPlayerPrefs();
    }

    void Update()
    {
        if (health == 0)
            Die();
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        if (!levelEnd && !isLevelStart)
        {
            playerOnGround = controller.isGrounded;
            if (playerOnGround && velocity.y < 0)
                velocity.y = 0f;

            horizontalMove = joystick.Horizontal;

            Vector3 move = new Vector3(-speed, 0, horizontalMove);
            controller.Move(move * Time.deltaTime * speed);


            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            transform.Rotate(0, horizontalMove, 0);


            if (Input.GetAxis("Horizontal") == 0)
            {
                Quaternion target = Quaternion.Euler(0f, -90f, 0f);
                playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, target, rotationSpeed * Time.deltaTime);
            }

        }
        else
        {
            if (joystick.Horizontal != 0f)
            {
                Debug.Log("Start Level");
                isLevelStart = false;
                GetComponent<Animator>().enabled = true;
            }
        }
    }



    private void Die()
    {
        speed = 0;
        Canvas.GetComponent<UIManagerScript>().gameover = true;
        GetComponent<Animator>().enabled = false;
    }

    public void HitObstacle()
    {
        health--;
    }

    public void LoadPlayerPrefs()
    {
        PlayerPrefs.SetInt("Health", 3);
        PlayerPrefs.SetInt("Coins", 0);
        PlayerPrefs.SetInt("CurrentLevel", 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            
            health--;


        }
        if (other.gameObject.tag == "Hearth")
        {
            if (health < maxHealth)
                health++;
            Destroy(other.gameObject);

        }

        if (other.gameObject.tag == "Coin")
        {
            coins++;
            Destroy(other.gameObject);

        }


        if (other.gameObject.tag == "FinishLine")
        {
            Canvas.GetComponent<UIManagerScript>().endgame = true;
            speed = 0;
            GetComponent<Animator>().enabled = false;
        }

        if (other.gameObject.tag == "GameOver")
        {
            Debug.Log("Gameover");
            speed = 0;
            Canvas.GetComponent<UIManagerScript>().gameover = true;

        }
    }
}
