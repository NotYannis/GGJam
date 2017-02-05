using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachMover : Mover {

    private Bounds[] bounds;

    void Awake()
    {
        manager = GameObject.Find("GameScripts").GetComponent<MoverManager>();
        bounds = new Bounds[2];
        BoxCollider2D[] colliders = GameObject.Find("SpawnZones/Beach").GetComponents<BoxCollider2D>();

       for(int i = 0; i < colliders.Length; ++i)
        {
            bounds[i] = colliders[i].bounds;
        }

        rig = GetComponent<Rigidbody2D>();

        transform.position = new Vector2(Random.Range(bounds[0].min.x, bounds[0].max.x), Random.Range(bounds[0].min.y, bounds[0].max.y));

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

        if (moveFreely)
        {
            Move();
            CheckBounds();
        }
        else
        {
            GoAway();
        }
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

            velocity = baseVelocity;

            velocity.x *= Mathf.Sign(Random.Range(-1.0f, 1.0f));
            velocity.y *= Mathf.Sign(Random.Range(-1.0f, 1.0f));

            movingTimeCooldown = Random.Range(movingTime / 4, movingTime);
        }

        rig.velocity = velocity;
    }

    protected override void CheckBounds()
    {
        if (transform.position.y > bounds[0].max.y)
        {
            velocity.y += (bounds[0].max.y - transform.position.y) * 0.08f;
        }
        if (transform.position.y < bounds[0].min.y)
        {
            velocity.y -= (transform.position.y - bounds[0].min.y) * 0.08f;
        }

        if (transform.position.x > bounds[0].max.x)
        {
            velocity.x += (bounds[0].max.x - transform.position.x) * 0.08f;
        }
        if (transform.position.x < bounds[0].min.x)
        {
            velocity.x -= (transform.position.x - bounds[0].min.x) * 0.08f;
        }

        if(transform.position.x > bounds[1].min.x && transform.position.y > bounds[1].min.y)
        {
            velocity.x += (bounds[1].min.x - transform.position.x) * 0.08f;
        }
    }

    protected override void GoAway()
    {
        moveFreely = false;
        Vector3 target = new Vector3(bounds[0].min.x, bounds[0].center.y, 0.0f);
        if(transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 1.0f);
        }
        else
        {
            manager.DeleteMover(this);
            Destroy(gameObject);
        }

        if(baseVelocity == Vector2.zero)
        {
            manager.DeleteMover(this);
            Destroy(gameObject);
        }
    }
}
