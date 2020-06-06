using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool is_grounded;
    private float cur_horzVelocity; // -1 to 1
    private float cur_vertVelocity; // -1 to 1

    private const float max_speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        is_grounded = true;
        cur_horzVelocity = 0;
        cur_vertVelocity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
        //GetWorldInput();

        Vector2 nextPos = transform.position;
        nextPos.x += cur_horzVelocity;
        nextPos.y += cur_vertVelocity;
        transform.position = Vector2.Lerp(transform.position, nextPos, 1.0f * Time.fixedDeltaTime);
    }

    public void FeetEnter()
    {
        is_grounded = true;
        cur_vertVelocity = 0;
    }

    public void FeetExit()
    {
        is_grounded = false;
    }

    public void ClampHorizontalVelocity()
    {
        if (cur_horzVelocity > max_speed)
        {
            cur_horzVelocity = max_speed;
        }
        else if (cur_horzVelocity < -max_speed)
        {
            cur_horzVelocity = -max_speed;
        }
    }

    void GetPlayerInput()
    {

        //move left
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.A))
        {
            cur_horzVelocity -= GameConstants.velocity_delta;
        }

        //move right
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.D))
        {
            cur_horzVelocity += GameConstants.velocity_delta;
        }
        //slow down
        else
        {
            if (cur_horzVelocity < 0f)
            {
                cur_horzVelocity += GameConstants.velocity_delta;
            }
            else if (cur_horzVelocity > 0f)
            {
                cur_horzVelocity -= GameConstants.velocity_delta;

            }
        }

        //jump
        if (Input.GetKey(KeyCode.Space))
        {
            if(is_grounded)
                cur_vertVelocity += GameConstants.jump_value;
        }

        ClampHorizontalVelocity();

    }

    void GetWorldInput()
    {
        if (!is_grounded)
        {
            //if not on platform or hanging from rope
            cur_vertVelocity -= GameConstants.velocity_delta;
        }
    }

}
