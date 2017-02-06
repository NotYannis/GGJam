using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntoSeaMover : Mover {

    void Awake()
    {
        INIT(true);

        bound = GameObject.Find("SpawnZones/IntoSea").GetComponent<BoxCollider2D>().bounds;

        transform.position = new Vector2(Random.Range(bound.min.x, bound.max.x),
                                         Random.Range(bound.min.y, bound.max.y));
    }
	
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
            notMovingTimeCooldown = notMovingTime;
            velocity = new Vector2(Random.Range(0.0f, baseVelocity.x), Random.Range(0.0f, baseVelocity.y));

            velocity.x *= Mathf.Sign(Random.Range(-1, 1));
            velocity.y *= Mathf.Sign(Random.Range(-1, 1));
        }

        transform.localScale = new Vector2(Mathf.Sign(velocity.x), 1.0f);

        rig.velocity = velocity;
    }

    public override void GoAwayTime()
    {
        Destroy(gameObject);
    }
}
