using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    //public variables
    public GameObject leftWall;
    public GameObject rightWall;

    public float moveSpeed;

    //private variables
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentLocation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentLocation = rightWall.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 location = currentLocation.position - transform.position;

        if (currentLocation == rightWall.transform)
        {
            rb.velocity = new Vector2(moveSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, 0);
        }

        if (Vector2.Distance(transform.position, currentLocation.position) < 0.5f && currentLocation == rightWall.transform)
        {
            currentLocation = leftWall.transform;
        }
        if (Vector2.Distance(transform.position, currentLocation.position) < 0.5f && currentLocation == leftWall.transform)
        { 
            currentLocation = rightWall.transform;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(leftWall.transform.position, 0.5f);
        Gizmos.DrawWireSphere(rightWall.transform.position, 0.5f);
        Gizmos.DrawLine(leftWall.transform.position, rightWall.transform.position);

    }
}
