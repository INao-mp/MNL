using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireRate = 0;
    public float Dmg = 10;
    public LayerMask WhatHit;

    float TTF = 0;
    Transform firePoint;

    void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.Log("Error!");
        }
    }

    void Update()
    {
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > TTF)
            {
                TTF = Time.time + 1 / fireRate;
                Shoot();
            }
        }    

    }

    void Shoot()
    {
        Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPos = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPos, mousePosition - firePointPos, 100, WhatHit);
        Debug.DrawLine(firePointPos, (mousePosition-firePointPos)*100, Color.black);
        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPos, mousePosition, Color.red);
            Debug.Log("We hit: " + hit.collider + " and did " + Dmg + " damage.");
        }
    }
}
