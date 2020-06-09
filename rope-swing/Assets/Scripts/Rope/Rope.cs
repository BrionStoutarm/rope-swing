using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    // Start is called before the first frame update
    private Player m_player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(Vector2 tgt)
    {
        //create head segment and shoot towards tgt
        //Debug.DrawLine(spriteRigidBody.position, mousePos, Color.red, 2.0f);
        

    }

    public void SetPlayer(Player p)
    {
        m_player = p;
    }
}
