using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachMover : Mover {


    void Awake()
    {
        bound = GameObject.Find("SpawnZones/Beach").GetComponent<BoxCollider2D>().bounds;
        rig = GetComponent<Rigidbody2D>();
        transform.position = bound.center;
        baseVelocity = velocity;

        movingTimeCooldown = movingTime;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!getStopMoving())
        {
            movingTimeCooldown -= Time.deltaTime;
        }

        if (!getStartMoving())
        {
            notMovingTimeCooldown -= Time.deltaTime;
        }

        Move();
        CheckBounds();
	}

    protected override void Move()
    {
        if (getStopMoving() && moving)
        {
            velocity = Vector2.zero;
            moving = false;
            notMovingTimeCooldown = Random.Range(notMovingTime / 2, notMovingTime);
        }

        if (getStartMoving() && !moving)
        {
            moving = true;
            //direction.x *= Mathf.Sign(Random.Range(-1.0f, 1.0f));
            //direction.y *= Mathf.Sign(Random.Range(-1.0f, 1.0f));

            //velocity = new Vector2(Random.Range(0.0f, baseVelocity.x), Random.Range(0.0f, baseVelocity.y));
            //transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            velocity = baseVelocity;


            movingTimeCooldown = Random.Range(movingTime / 4, movingTime);
        }

        rig.velocity = velocity;
    }
}
