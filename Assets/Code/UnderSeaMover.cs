using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderSeaMover : Mover
{
    public bool comesFromLeft;

    void Awake()
    {
        bound = GameObject.Find("SpawnZones/UnderSea").GetComponent<BoxCollider2D>().bounds;
        rig = GetComponent<Rigidbody2D>();
        transform.position = bound.center;
        baseVelocity = velocity;


        if (comesFromLeft)
        {
            transform.position = new Vector3(bound.min.x, bound.center.y, 0.0f);
        }
        else
        {
            transform.position = new Vector3(bound.max.x, bound.center.y, 0.0f);
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }

        movingTimeCooldown = movingTime;

        rig.velocity = velocity;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
            notMovingTimeCooldown = Random.Range(notMovingTime/2, notMovingTime);
        }

        if (getStartMoving() && !moving)
        {
            moving = true;
            baseVelocity.x *= -1;
            velocity = baseVelocity;

            movingTimeCooldown = Random.Range(movingTime / 4, movingTime);
        }

        rig.velocity = velocity;

        transform.localScale = new Vector2(Mathf.Sign(velocity.x), transform.localScale.y);
    }
}
