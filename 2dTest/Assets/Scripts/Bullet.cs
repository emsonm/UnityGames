using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float Speed = 15f;

    Rigidbody2D body;

    Direction direction;

    // Use this for initialization
    void Start()
    {
        direction = TheBall.direction;

        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;

        var x = position.x;
        var y = position.y;

        switch (direction)
        {
            case Direction.lu:
            case Direction.ld:
            case Direction.l:
                x = position.x - Speed * Time.deltaTime;
                break;

            case Direction.ru:
            case Direction.rd:
            case Direction.r:
                x = position.x + Speed * Time.deltaTime;
                break;
        }

        switch (direction)
        {
            case Direction.lu:
            case Direction.ru:
            case Direction.u:
                y = position.y + Speed * Time.deltaTime;
                break;

            case Direction.ld:
            case Direction.rd:
            case Direction.d:
                y = position.y - Speed * Time.deltaTime;
                break;
        }

        transform.position = new Vector2(x, y);

        //var max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //if (transform.position.y > max.y)
        //{
        //    Destroy(gameObject);
        //}

    }

    void OnBecameInvisible()
    {
        Debug.Log("Gone");
        Destroy(gameObject);
    }
}
