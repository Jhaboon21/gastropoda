using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int speed = 20;
    private int dmg = 50;

    private float fly_time = 2f;

    private RaycastHit2D raycast;
    public Transform rayposition;
    
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    { 
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        int layermask = 1 << 8;
        layermask = ~layermask;
        raycast = Physics2D.Raycast(rayposition.position, transform.right * speed, Mathf.Infinity, layermask);
        if(raycast.collider.CompareTag("SpawnObj"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.FindWithTag("SpawnObj").GetComponent<Collider2D>());
        }
        if(raycast.collider.name == "Lever")
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Lever").GetComponent<Collider2D>());
        }
        Destroy(gameObject, fly_time);
        
    }
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D hitInfo)
    {   
        GameObject ground = GameObject.FindWithTag("Ground");
        snailHealth snail = hitInfo.GetComponent<snailHealth>();
        if (ground != null)
        {
            Destroy(gameObject);
        }
        if(snail != null)
        {
            snail.takeDamage(dmg);
            Destroy(gameObject);
        }
        
    }
    
}