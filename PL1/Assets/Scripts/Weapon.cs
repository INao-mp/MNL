using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int Dmg = 10;
    public float fireRate = 0;
    public float EspawnRate = 10;
    public float camShakeAmt = 0.1f;
    public float camShakeLength = 0.05f;
    public LayerMask WhatHit;
    public Transform BulTrial;
    public Transform MuzzleFlash;
    public Transform HitPrefab;

    float TTF = 0;
    float TTSe = 0;
    Transform firePoint;
    CameraShake camShake;

    void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("Error!");
        }
    }

    void Start()
    {
        camShake = GM_Main.gm.GetComponent<CameraShake>();
        if (camShake == null)
        {
            Debug.LogError("No camera Shake obj");
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
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("We hit: " + hit.collider + " and did " + Dmg + " damage.");
                enemy.DamageEnemy(Dmg);
            }
        }

        if (Time.time >= TTSe)
        {
            Vector3 hitPos;
            Vector3 hitNormal;

            if (hit.collider == null)
            {
                hitPos = (mousePosition - firePointPos) * 30;
                hitNormal = new Vector3(999, 999, 999);
            }
            else
            {
                hitPos = hit.point;
                hitNormal = hit.normal;
            }

            Effect(hitPos, hitNormal);
            TTSe = Time.time + 1 / EspawnRate;
        }
    }

    void Effect(Vector3 hitPos, Vector3 hitNormal)
    {
        Transform trail = (Transform)Instantiate(BulTrial, firePoint.position, firePoint.rotation);
        LineRenderer lr = trail.GetComponent<LineRenderer>();

        if (lr != null)
        {
            lr.SetPosition(0, firePoint.position);
            lr.SetPosition(1, hitPos);
        }

        Destroy(trail.gameObject, 0.03f);
        if (hitNormal != new Vector3(999,999,999))
        {
           Transform hitPaticle =(Transform)Instantiate(HitPrefab,hitPos,Quaternion.FromToRotation(Vector3.right, hitNormal));
            Destroy(hitPaticle.gameObject, 0.5f);
        }

        Transform clone = (Transform)Instantiate(MuzzleFlash, firePoint.position, firePoint.rotation);
        clone.parent = firePoint;
        float size = Random.Range(0.6f, 0.9f);
        clone.localScale = new Vector3(size, size, size);
        Destroy(clone.gameObject, 0.03f);

        camShake.Shake(camShakeAmt, camShakeLength);
    }
    
}
