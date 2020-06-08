using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeHead : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colObj = collision.gameObject;
        Debug.Log("Collided with: " + colObj.name);
    }
}
