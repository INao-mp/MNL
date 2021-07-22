using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]

public class Tilling : MonoBehaviour
{
    public int offsetX = 2;
    public bool hasARightBoddy = false;
    public bool hasALeftBoddy = false;

    public bool ReverseScale = false;

    private float spriteWidth = 0f;
    private Camera cam;
    private Transform myTransform;

    void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }
   
    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;
    }

    void Update()
    {
        if (hasALeftBoddy == false || hasARightBoddy == false)
        {
            float camHorizontalEx = cam.orthographicSize * Screen.width / Screen.height;

            float edgeVisiblePosRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalEx;
            float edgeVisiblePosLeft = (myTransform.position.x - spriteWidth / 2) - camHorizontalEx;

            if (cam.transform.position.x >= edgeVisiblePosRight - offsetX && hasARightBoddy == false)
            {
                MakeNB(1);
                hasARightBoddy = true;
            }
            else if(cam.transform.position.x <= edgeVisiblePosLeft + offsetX && hasALeftBoddy == false)
            {
                MakeNB(-1);
                hasALeftBoddy = true;
            }
        }
    }

    void MakeNB(int rightORleft)
    {
        Vector3 newPosition = new Vector3(myTransform.position.x + spriteWidth * rightORleft, myTransform.position.y, myTransform.position.z);
        Transform newBoddy = (Transform)Instantiate(myTransform, newPosition, myTransform.rotation);

        if (ReverseScale == true)
        {
            newBoddy.localScale = new Vector3(newBoddy.localScale.x * -1, newBoddy.localScale.y, newBoddy.localScale.z);            
        }

        newBoddy.parent = myTransform.parent;

        if (rightORleft > 0)
        {
            newBoddy.GetComponent<Tilling>().hasALeftBoddy = true;
        }
        else
        {
            newBoddy.GetComponent<Tilling>().hasARightBoddy = true;
        }
    }
}
