using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nut : MonoBehaviour
{
    public float speed = 10;
    public GameObject NutSpawner;
    GameObject plr;
    Vector3 EndPos;

    // Start is called before the first frame update
    void Start()
    {
        //NutSpawner = GameObject.Find("NutSpawner");
        plr = GameObject.Find("Player");
        EndPos = new Vector3(plr.transform.position.x - transform.position.x, plr.transform.position.y - transform.position.y);
        EndPos *= 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position= Vector3.MoveTowards(transform.position, EndPos, speed * Time.deltaTime);
        if (transform.position == EndPos)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            gameObject.transform.SetParent(collision.gameObject.transform);
            gameObject.GetComponent<Nut>().enabled = false;
        }
    }
}
