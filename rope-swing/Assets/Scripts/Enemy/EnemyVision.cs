using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public Enemy m_enemy;
    public Collider2D m_target;
    bool m_localLostSight;

    // Start is called before the first frame update
    void Start()
    {
        m_target = null;
        m_localLostSight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_target != null)
        {
            if (CanSee(m_target))
            {
                m_localLostSight = false;
                m_enemy.MoveTowards(m_target.transform.position);
            }
            else
            {
                if (!m_localLostSight)
                {
                    m_localLostSight = true;
                    m_enemy.LostSight();
                }   
            }
        }
    }

    private bool CanSee(Collider2D collision)
    {
        int layerMask = ~(1 << 9);
        RaycastHit2D hitCheck = Physics2D.Linecast(this.transform.position, collision.transform.position, layerMask);
        if(hitCheck.collider == collision)
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            m_target = collision;
            m_localLostSight = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == m_target)
        {
            m_target = null;
            if (!m_localLostSight)
            { 
                           m_enemy.LostSight();
            }
        }
    }
}
