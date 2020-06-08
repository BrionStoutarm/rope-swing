using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private bool is_grounded;
    private bool is_climbing;
    private bool could_climb;
    private bool rope_shot;

    private const double max_rope_length = 10.0;
    private const float max_speed = 5.0f;

    private Rigidbody2D spriteRigidBody;
    private double cur_rope_length;

    public Rope m_rope; 

    // Start is called before the first frame update
    void Start()
    {
        is_grounded = true;
        is_climbing = false;
        could_climb = false;
        rope_shot = false;
        cur_rope_length = 0;
        spriteRigidBody = GetComponentInChildren<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
    }

    public void FeetEnter()
    {
        is_grounded = true;
        spriteRigidBody.velocity = new Vector2(spriteRigidBody.velocity.x, 0.0f);
    }

    public void FeetExit()
    {
        is_grounded = false;
    }

    public void ClimbEnter()
    {
        could_climb = true;
    }

    public void ClimbExit()
    {
        could_climb = false;
        is_climbing = false;
        spriteRigidBody.gravityScale = 1.0f;
    }
    public void ClampHorizontalVelocity()
    {
        if (spriteRigidBody.velocity.x > max_speed)
        {
            spriteRigidBody.velocity = new Vector2(max_speed, spriteRigidBody.velocity.y);
        }
        else if (spriteRigidBody.velocity.x < -max_speed)
        {
            spriteRigidBody.velocity = new Vector2(-max_speed, spriteRigidBody.velocity.y);
        }
    }

    public void ClampVerticalVelocity()
    {
        if (spriteRigidBody.velocity.y > max_speed)
        {
            spriteRigidBody.velocity = new Vector2(spriteRigidBody.velocity.x, max_speed);
        }
        else if (spriteRigidBody.velocity.x < -max_speed)
        {
            spriteRigidBody.velocity = new Vector2(spriteRigidBody.velocity.x, -max_speed);
        }
    }

    private void SlowVerticalVelocity()
    {
        if (Mathf.Abs(spriteRigidBody.velocity.x) < GameConstants.velocity_delta){
            spriteRigidBody.velocity = new Vector2(spriteRigidBody.velocity.x, 0f);
        }
        else if (spriteRigidBody.velocity.y < 0f)
        {
            spriteRigidBody.velocity = new Vector2(spriteRigidBody.velocity.x, spriteRigidBody.velocity.y + GameConstants.velocity_delta);
        }
        else if (spriteRigidBody.velocity.y > 0f)
        {
            spriteRigidBody.velocity = new Vector2(spriteRigidBody.velocity.x, spriteRigidBody.velocity.y - GameConstants.velocity_delta);

        }
    }

    private void SlowHorizontalVelocity()
    {
        if (Mathf.Abs(spriteRigidBody.velocity.x) < GameConstants.velocity_delta){
            spriteRigidBody.velocity = new Vector2(0f, spriteRigidBody.velocity.y);
        }
        else if (spriteRigidBody.velocity.x < 0f)
        {
            spriteRigidBody.velocity = new Vector2(spriteRigidBody.velocity.x + GameConstants.velocity_delta, spriteRigidBody.velocity.y);
        }
        else if (spriteRigidBody.velocity.x > 0f)
        {
            spriteRigidBody.velocity = new Vector2(spriteRigidBody.velocity.x - GameConstants.velocity_delta, spriteRigidBody.velocity.y);

        }
    }

    void GetPlayerInput()
    {

        //Climbing
        if (is_climbing)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.W))
            {
                spriteRigidBody.velocity = new Vector2(spriteRigidBody.velocity.x, spriteRigidBody.velocity.y + GameConstants.velocity_delta);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.S))
            {
                spriteRigidBody.velocity = new Vector2(spriteRigidBody.velocity.x, spriteRigidBody.velocity.y - GameConstants.velocity_delta);
            }
            else
            {
                SlowVerticalVelocity();
            }
        }
        else if (could_climb)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.S))
            {
                is_climbing = true;
                spriteRigidBody.gravityScale = 0;
                spriteRigidBody.velocity = new Vector2(0.0f, 0.0f);
            }
        }
        
        //shoot rope
        if(Input.GetMouseButtonDown(0))
        {
            rope_shot = true;
            Debug.Log("Shoot rope");
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mp = new Vector2(mousePos.x, mousePos.y);

            Debug.DrawLine(spriteRigidBody.position, mousePos, Color.red, 2.0f);

            m_rope.Shoot(mp);
        }
        //rope is shot and extending
        if(rope_shot && Input.GetMouseButton(0) && cur_rope_length < max_rope_length)
        {
            Debug.Log("Rope is shooting");
        }
        //cancel rope shot
        else if( (rope_shot && Input.GetMouseButtonUp(0)) || cur_rope_length > max_rope_length)
        {
            Debug.Log("Rope canceled");
            rope_shot = false;
        }

        //move left
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.A))
        {
            spriteRigidBody.velocity = new Vector2(spriteRigidBody.velocity.x - GameConstants.velocity_delta, spriteRigidBody.velocity.y);
        }

        //move right
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.D))
        {
            spriteRigidBody.velocity = new Vector2(spriteRigidBody.velocity.x + GameConstants.velocity_delta, spriteRigidBody.velocity.y);
        }
        //slow down
        else
        {
            SlowHorizontalVelocity();
        }

        //jump
        if (Input.GetKey(KeyCode.Space))
        {
            if (is_grounded || is_climbing)
            {
                is_grounded = false;
                is_climbing = false;
                spriteRigidBody.velocity = new Vector2(spriteRigidBody.velocity.x, spriteRigidBody.velocity.y + GameConstants.jump_value);
                GetComponentInChildren<Rigidbody2D>().gravityScale = 1.0f;
            }
        }

        if (is_climbing)
        {
            ClampVerticalVelocity();
        }

        ClampHorizontalVelocity();

    }

}
