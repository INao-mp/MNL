using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Camera mainCamera;

    float shakeAmount = 0;

    void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }    
    }
    
    public void Shake(float amt, float length)
    {
        shakeAmount = amt;
        InvokeRepeating("BeginShake", 0, 0.01f);
        Invoke("StopShake", length);     
    }

    void BeginShake()
    {
        if (shakeAmount >0)
        {
            Vector3 camPos = mainCamera.transform.position;

            float OffsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float OffsetY = Random.value * shakeAmount * 2 - shakeAmount;

            camPos.x += OffsetX;
            camPos.y += OffsetY;

            mainCamera.transform.position = camPos;
        }
    }

    void StopShake()
    {
        CancelInvoke("BeginShake");
        mainCamera.transform.localPosition = Vector3.zero;
    }
}
