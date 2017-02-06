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
    protected bool moveFreely = true;

    public float movingTime;
    protected float movingTimeCooldown;

    public float notMovingTime;
    protected float notMovingTimeCooldown;


    public Schedule schedule;
    public Mood mood;

    protected Bounds bound;
    protected Rigidbody2D rig;
    protected MoverManager manager;

    protected void INIT(bool destroyTime)
    {
        manager = GameObject.Find("GAME").GetComponent<MoverManager>();
        rig = GetComponent<Rigidbody2D>();
        baseVelocity = velocity;

        movingTimeCooldown = movingTime;

        rig.velocity = velocity;

        if (destroyTime)
        {
            Invoke("GoAwayTime", Random.Range(40.0f, 60.0f));
        }
    }

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

    public virtual void GoAwayTime()
    {
        rig.velocity = Vector2.zero;
        Invoke("GoAway", Random.Range(0.0f, 10.0f));
    }

    protected virtual void GoAway(){ }

    protected bool getStopMoving()
    {
        return movingTimeCooldown <= 0.0f;
    }

    protected bool getStartMoving()
    {
        return notMovingTimeCooldown <= 0.0f;
    }
}
