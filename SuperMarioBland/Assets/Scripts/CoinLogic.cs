using UnityEngine;
using System.Collections;

public class CoinLogic : MonoBehaviour {

    public GameObject Coin;

    public float Interval = 10f;
    public float CoinTimer = 10f;

    public static int CoinCount = 0;
    public int MaxCoinCount = 10;


	// Use this for initialization
	void Start() 
    {
	
	}
	
	// Update is called once per frame
	void Update() 
    {

        var dt = Time.deltaTime;

        CoinTimer += CoinTimer + (1f * dt);
        if(CoinCount < MaxCoinCount)
        {
            if (CoinTimer < Interval)
                return;
            
            var top = Camera.main.transform.position;
            //spawn another coin
            var coin = (GameObject)Instantiate(Coin);


            var v3Left = new Vector3(-0.15f, .5f, 10); //10 is the distance from the camera
            v3Left = Camera.main.ViewportToWorldPoint(v3Left);

            var v3Right = new Vector3(Screen.width, 0, 0);
            v3Right = Camera.main.ScreenToViewportPoint(v3Right);
            v3Right = new Vector3(v3Right.x, .5f, 10);
            v3Right = Camera.main.ViewportToWorldPoint(v3Right);

            coin.transform.position = new Vector2(Random.Range(v3Left.x, v3Right.x + 1), top.y); 

            CoinCount++;
            Debug.LogFormat("There are {0} coins", CoinCount);

            CoinTimer = 0f;
        }	
	}
}
