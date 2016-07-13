using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour
{

    public LevelManager levelManager;

    // Use this for initialization 
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
          
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        Debug.Log("Updated Checkpoint");
        if (target.name == "Player")
        {
            levelManager.currentRespawnPoint = gameObject;
        }
    }
}
