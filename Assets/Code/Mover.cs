using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Schedule
{
    Day,
    Night,
}

public enum Mood
{
    ReallyCalm,
    Calm,
    Medium,
    Strong,
}

public class Mover : MonoBehaviour {

    public Vector2 velocity;

    protected Vector2 baseVelocity;

    public string waveCode;

    protected bool moving = true;

    public float movingTime;
    protected float movingTimeCooldown;

    public float notMovingTime;
    protected float notMovingTimeCooldown;


    public Schedule schedule;
    public Mood mood;

    protected Bounds bound;
    protected Rigidbody2D rig;

    protected virtual void Move() {
        rig.velocity = velocity;
    }

    protected virtual void CheckBounds() {
        if (transform.position.y > bound.max.y)
        {
            velocity.y += (bound.max.y - transform.position.y) * 0.08f;
        }
        if (transform.position.y < bound.min.y)
        {
            velocity.y -= (transform.position.y - bound.min.y) * 0.08f;
        }

        if (transform.position.x > bound.max.x)
        {
            velocity.x += (bound.max.x - transform.position.x) * 0.08f;
        }
        if (transform.position.x < bound.min.x)
        {
            velocity.x -= (transform.position.x - bound.min.x) * 0.08f;
        }
    }


    protected bool getStopMoving()
    {
        return movingTimeCooldown <= 0.0f;
    }

    protected bool getStartMoving()
    {
        return notMovingTimeCooldown <= 0.0f;
    }
}
