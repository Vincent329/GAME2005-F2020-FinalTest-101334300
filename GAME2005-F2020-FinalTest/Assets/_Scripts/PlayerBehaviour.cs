using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bullet;
    public int fireRate;

    public bool isPaused;
    public Vector3 forward;

    public BulletManager bulletManager;

    [Header("Movement")]
    public float speed;
    public bool isGrounded;


    public RigidBody3D body;
    public CubeBehaviour cube;
    public Camera playerCam;

    // all game objects in the scene
    public RigidBody3D[] rigidBodies;

    void Start()
    {
        rigidBodies = FindObjectsOfType<RigidBody3D>();
        isPaused = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            Time.timeScale = 1;
            _Fire();
            _Move();
            forward.x = playerCam.transform.forward.x;
            forward.z = playerCam.transform.forward.z;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
         else {
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        //Debug.Log("Forward Vector: " + forward);
        if (Input.GetKeyDown("p"))
        {
            isPaused = !isPaused;
            foreach(var bodies in rigidBodies)
            {
                bodies.isPaused = isPaused;
            }
        }
        //Take Q button for going back to Main Menu
        if (Input.GetKeyDown("q"))
        {
            Debug.Log("Q Button Pressed");
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
            }
        }
    }

    private void _Move()
    {
        if (isGrounded)
        {
            if (Input.GetAxisRaw("Horizontal") > 0.0f)
            {
                // move right
                body.velocity = playerCam.transform.right * speed * Time.deltaTime;
            }

            if (Input.GetAxisRaw("Horizontal") < 0.0f)
            {
                // move left
                body.velocity = -playerCam.transform.right * speed * Time.deltaTime;
            }

            if (Input.GetAxisRaw("Vertical") > 0.0f)
            {
                // move forward
                body.velocity = playerCam.transform.forward * speed * Time.deltaTime;
                if (Input.GetAxisRaw("Horizontal") > 0.0f)
                {
                    // move right

                    body.velocity += playerCam.transform.right * speed * Time.deltaTime;
                    body.velocity.x *= 0.8f;
                    body.velocity.z *= 0.8f;
                }

                else if (Input.GetAxisRaw("Horizontal") < 0.0f)
                {
                    // move left
                    body.velocity += -playerCam.transform.right * speed * Time.deltaTime;
                    body.velocity.x *= 0.8f;
                    body.velocity.z *= 0.8f;
                }
                
            }

            if (Input.GetAxisRaw("Vertical") < 0.0f) 
            {
                // move Back
                body.velocity = -playerCam.transform.forward * speed * Time.deltaTime;
                if (Input.GetAxisRaw("Horizontal") > 0.0f)
                {
                    // move right
                    body.velocity += playerCam.transform.right * speed * Time.deltaTime;
                    body.velocity.x *= 0.8f;
                    body.velocity.z *= 0.8f;
                }

                else if (Input.GetAxisRaw("Horizontal") < 0.0f)
                {
                    // move left
                    body.velocity += -playerCam.transform.right * speed * Time.deltaTime;
                    body.velocity.x *= 0.8f;
                    body.velocity.z *= 0.8f;
                }
               
            }

            body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, 0.9f);
            body.velocity = new Vector3(body.velocity.x, 0.0f, body.velocity.z); // remove y
            

            if (Input.GetAxisRaw("Jump") > 0.0f)
            {
               // body.velocity.x /= 1.5f;
               // body.velocity.z /= 1.5f;
                body.velocity.y = 1.0f * speed * 0.1f * Time.deltaTime;
            }

            transform.position += body.velocity;
        }
    }


    private void _Fire()
    {
        if (Input.GetAxisRaw("Fire1") > 0.0f)
        {
            // delays firing
            if (Time.frameCount % fireRate == 0)
            {

                var tempBullet = bulletManager.GetBullet(bulletSpawn.position, bulletSpawn.forward);
                tempBullet.transform.SetParent(bulletManager.gameObject.transform);
                //tempBullet.SetActive(true);
            }
        }
    }

    void FixedUpdate()
    {
        GroundCheck();
    }

    private void GroundCheck()
    {
        isGrounded = cube.isGrounded;
    }

}
