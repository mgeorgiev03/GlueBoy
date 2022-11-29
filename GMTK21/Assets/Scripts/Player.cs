using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(FacingController))]
public class Player : MonoBehaviour
{
    public float HP = 6;
    public Animator animator;
    new BoxCollider2D collider;
    new Rigidbody2D rigidbody;
    FacingController facingController;
    private bool flag = true;
    private GameObject nutSpawner;

    bool dash = false;
    int dashCount = 1;
    float DashCooldown;

    private Vector2 move;
    [SerializeField] public float moveSpeed = 6f;
    [SerializeField] public float dashSpeed = 60f;


    void Start()
    {
        DashCooldown = 0f;
        nutSpawner = GameObject.Find("NutSpawner");
        facingController = GetComponent<FacingController>();
        collider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");

        if (move.x < 0)
            facingController.Facing = Facing.Left;
        else if (move.x > 0)
            facingController.Facing = Facing.Right;
        else if (move.y > 0)
            facingController.Facing = Facing.Up;
        else if (move.y < 0)
            facingController.Facing = Facing.Down;


        if (move.x != 0 || move.y != 0)
        {
            if (flag)
            {
                StartCoroutine(Walking());
            }
        }

        if (dashCount <= 1)
        {
            dashCount++;
        }

        if (Input.GetKeyDown(KeyCode.K) && dashCount != 0 && DashCooldown == 0f)
        {
            dash = true;
            dashCount--;
        }


        //animator.SetFloat("Horizontal", move.x);
        //animator.SetFloat("Vertical", move.y);
        //animator.SetFloat("Speed", Mathf.Abs(move.sqrMagnitude));

        if (HP == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (this.transform.childCount >= 5)
        {
            nutSpawner.SetActive(false);
            moveSpeed = 3f;
        }
        else if (transform.childCount >= 1)
            moveSpeed = 5f;
        else if (transform.childCount >= 3)
            moveSpeed = 4f;
        else if (transform.childCount == 0)
        {
            nutSpawner.SetActive(true);
            moveSpeed = 6f;
        }
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + move * moveSpeed * Time.fixedDeltaTime);

        if (dash && dashCount > 0 && DashCooldown <= 0f)
            Dash();

        if (!dash)
        {
            DashCooldown -= Time.fixedDeltaTime;

            if (DashCooldown < 0)
                DashCooldown = 0f;

        }
        else if (dash)
            DashCooldown += Time.fixedDeltaTime * 25;

        dash = false;
    }

    void Dash()
    {
        Vector2 direction = new Vector2();


        if (Input.GetKey(KeyCode.W)) { direction += Vector2.up; }
        if (Input.GetKey(KeyCode.S)) { direction += Vector2.down; }
        if (Input.GetKey(KeyCode.A)) { direction += Vector2.left; }
        if (Input.GetKey(KeyCode.D)) { direction += Vector2.right; }

        if (direction == Vector2.zero)
            return;

        direction.Normalize();

        rigidbody.MovePosition(rigidbody.transform.position + new Vector3(direction.x, direction.y)
            * dashSpeed * Time.deltaTime);
    }

    public IEnumerator Walking()
    {
        flag = false;
        yield return new WaitForSeconds(0.4f);
        flag = true;
    }
}