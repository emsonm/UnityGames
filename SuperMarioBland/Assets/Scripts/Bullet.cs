using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    Rigidbody2D body;
    AudioSource explosionSound;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        explosionSound = FindObjectOfType<Gun>().explosionSound;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnBecameInvisible()
    {
        Debug.Log("Gone");
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        //Debug.Log(target.gameObject.name + " collision");
        if (target.gameObject.tag == "Collectible") 
        {
            explosionSound.Play();

            Destroy(target.gameObject);
            Destroy(gameObject);
            CoinLogic.CoinCount--;         
        }
    }        
}
