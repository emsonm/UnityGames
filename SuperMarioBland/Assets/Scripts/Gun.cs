using UnityEngine;
using System.Collections;
using System;

public class Gun : MonoBehaviour
{
    public AudioSource explosionSound;

    // Use this for initialization 
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        var mouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        var screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
