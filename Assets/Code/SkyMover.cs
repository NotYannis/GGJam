using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyMover : Mover {

    private bool comesFromLeft = false;

    void Awake()
    {
        INIT(false);

        bound = GameObject.Find("SpawnZones/Sky").GetComponent<BoxCollider2D>().bounds;

        if(Random.Range(0.0f, 1.0f) < 0.5f)
        {
            comesFromLeft = true;
        }

        if (comesFromLeft)
        {
            transform.position = new Vector2(bound.min.x, Random.Range(bound.min.y + 0.5f, bound.max.y - 0.5f));
        }
        else
        {
            transform.localScale = new Vector2(-1.0f, 1.0f);
            transform.position = new Vector2(bound.max.x, Random.Range(bound.min.y + 0.5f, bound.max.y - 0.5f));
            velocity.x *= -1;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!getStartMoving())
        {
            notMovingTimeCooldown -= Time.deltaTime;
        }

        Move();
        CheckBounds();
	}

    protected override void Move()
    {
        if (getStartMoving())
        {
            velocity.y =  Random.Range(0.0f, baseVelocity.y);
            velocity.y *= Mathf.Sign(Random.Range(-1, 1));
            notMovingTimeCooldown = notMovingTime;
        }

        rig.velocity = velocity;
    }

    protected override void CheckBounds()
    {
        if (transform.position.y > bound.max.y)
        {
            velocity.y += (bound.max.y - transform.position.y) * 0.08f;
        }
        if (transform.position.y < bound.min.y)
        {
            velocity.y -= (transform.position.y - bound.min.y) * 0.08f;
        }

        if (transform.position.x > bound.max.x && comesFromLeft)
        {
            Destroy(gameObject);
        }
        if (transform.position.x < bound.min.x && !comesFromLeft)
        {
            Destroy(gameObject);
        }
    }
}
