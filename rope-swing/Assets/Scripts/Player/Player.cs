using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float cur_horzVelocity; // -1 to 1
    private float cur_vertVelocity; // -1 to 1

    private const float max_speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        cur_horzVelocity = 0;
        cur_vertVelocity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
        GetWorldInput();
        Vector2 nextPos = transform.position;
        nextPos.x += cur_horzVelocity;
        nextPos.y += cur_vertVelocity;
        transform.position = Vector2.Lerp(transform.position, nextPos, 1.0f * Time.fixedDeltaTime);
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
            if(cur_horzVelocity < 0f)
            {
                cur_horzVelocity += GameConstants.velocity_delta;
            }
            else if(cur_horzVelocity > 0f)
            {
                cur_horzVelocity -= GameConstants.velocity_delta;

            }
        }
    }

    void GetWorldInput()
    {
        //if not on platform or hanging from rope
        cur_vertVelocity -=  GameConstants.velocity_delta;
    }

}
