using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{

    public GameObject currentRespawnPoint;

    private Player player;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<Player>(); 
    }

    // Update is called once per frame
    void Update()
    { 

    }

    public void RespawnPlayer()
    {
        Debug.Log("Player Respawned");

        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero; 
        player.transform.position = currentRespawnPoint.transform.position;
    }
}
