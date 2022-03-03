using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 0.6f;
    public float sensitivity = 0.2f;

    public Vector3 forwardMove;
    public float clampDistanceX;
    public float swerveValue;

    private float increaseSpeed = 0f;
    private int count = 0;

    public HealthBar healthBar;
    public float currentHealth = 3;
    
    private  Touch touch;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {
       
      
        if (ScoreSystem._points == 10 && count == 0)
        {
            increaseSpeed = 1.5f;
            SpeedUpgrade(increaseSpeed);
            count++;
        }
        if (ScoreSystem._points == 25 && count == 1)
        {
            increaseSpeed = 2f;
            SpeedUpgrade(increaseSpeed);
            count++;
        }
        if (ScoreSystem._points == 50 && count == 2)
        {
            increaseSpeed = 3f;
            SpeedUpgrade(increaseSpeed);
            count++;
        }
        if (ScoreSystem._points == 100 && count ==3)
        {
            increaseSpeed = 4f;
            SpeedUpgrade(increaseSpeed);
            count++;
        }
    }
    private void FixedUpdate()
    {
        PlayerMovements();
    }
   private void PlayerMovements()
    {
        
        if (Input.GetMouseButton(0))
        {
            forwardMove = transform.forward * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + forwardMove);
            
            Vector3 mousePosition = Input.mousePosition;
            if (mousePosition.x > Screen.width / 2)
            {
                transform.position += new Vector3(swerveValue, 0, 0);

            }
            else
            {
                transform.position += new Vector3(-swerveValue, 0, 0);

            }
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                Vector3 playerPosition = transform.position;
                playerPosition.x = Mathf.Clamp(playerPosition.x, -clampDistanceX, clampDistanceX);
                transform.position = playerPosition;
                transform.position += transform.forward * speed * Time.fixedDeltaTime;
            }


        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
 private void SpeedUpgrade(float increase)
    {
        speed +=increase;
     
    }
    public void Damage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            
           GameManager.gameManager.EndGame();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Check to see if the tag on the collider is equal to Enemy
        if (collision.gameObject.tag == "Obstacle")
        {
             Debug.Log("Triggered by Enemy");
           
            Damage(1);
        }
    }
}
