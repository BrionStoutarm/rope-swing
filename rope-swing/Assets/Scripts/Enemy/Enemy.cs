using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D spriteRigidBody;
    private  float enemy_speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        spriteRigidBody = GetComponentInChildren<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit()
    {
        foreach (Collider2D c in GetComponentsInChildren<Collider2D>())
        {
            c.enabled = false;
        }
    }

    public void LostSight()
    {
        spriteRigidBody.velocity = new Vector2(0f, 2f);
    }
    public void MoveTowards(Vector2 target)
    {
        if (this.spriteRigidBody.position.x < target.x)
        {
            spriteRigidBody.velocity = new Vector2(enemy_speed, 0f);
        }
        else
        {
            spriteRigidBody.velocity = new Vector2(-enemy_speed, 0f);
        }
    }
}
