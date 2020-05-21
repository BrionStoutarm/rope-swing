using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        Vector2 pos = transform.position;

        //move left
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.A))
        {
            pos.x -= GameConstants.movement_delta;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.D))
        {
            pos.x += GameConstants.movement_delta;
        }

        transform.position = Vector2.Lerp(transform.position, pos, 1.0f * Time.fixedDeltaTime);
    }
}
