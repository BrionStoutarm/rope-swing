using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class climbable : MonoBehaviour
{
    public Player m_player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_player.ClimbEnter();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        m_player.ClimbExit();
    }
}
