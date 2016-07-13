using UnityEngine;
using System.Collections;

public enum Direction : byte { n, l, lu, u, ru, r, rd, d, ld };

public class TheBall : MonoBehaviour
{


    private Rigidbody2D body = null;
    private Animator animator = null;
    public float Speed = 5;

    public GameObject playerBullet;
    public GameObject TheBallBulletPosition;

    public static Direction direction = Direction.r;

    // Use this for initialization
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var mx = 0f;
        var my = 0f;

        var nd = Direction.n;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            mx = -1 * Speed;
            //Debug.Log("Left");

            nd = Direction.l;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            mx = 1 * Speed;
            //Debug.Log("Right");

            nd = Direction.r;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            my = 1 * Speed;
            //Debug.Log("Up");

            if (nd == Direction.l)
            {
                nd = Direction.lu;
            }
            else if (nd == Direction.r)
            {
                nd = Direction.ru;
            }
            else
            {
                nd = Direction.u;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            my = -1 * Speed;
            //Debug.Log("Down");

            if (nd == Direction.l)
            {
                nd = Direction.ld;
            }
            else if (nd == Direction.r)
            {
                nd = Direction.rd;
            }
            else
            {
                nd = Direction.d;
            }
        }

        if (nd != Direction.n)
            direction = nd;

        if (Input.GetKey(KeyCode.Space))
        {
            var bullet = (GameObject)Instantiate(playerBullet);
            bullet.transform.position = TheBallBulletPosition.transform.position;
        }

        var moving = mx != 0f || my != 0f;

        //Debug.Log(string.Format("mx = {0}, my = {1}, moving = {2}, x = {3}, y = {4}", mx, my, moving, x, y));

        animator.SetBool("Moving", moving);
        body.velocity = new Vector2(mx, my);
    }
}
