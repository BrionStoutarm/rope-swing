﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public Rigidbody2D player_sprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FaceMouse();
    }

    private void FaceMouse()
    {
        Vector3 mp = Input.mousePosition;
        mp = Camera.main.ScreenToWorldPoint(mp);

        Vector2 mousePos = new Vector2(mp.x, mp.y);

        Vector2 playerPos = player_sprite.transform.localPosition;
        float distance = Vector2.Distance(playerPos, mousePos);

        if(distance > GameConstants.player_hand_radius)
        {
            Vector2 fromOriginToObject = mousePos - playerPos;
            fromOriginToObject *= GameConstants.player_hand_radius / distance;
            Vector2 newLocation = playerPos + fromOriginToObject;
            transform.position = newLocation;
        }
    }
}