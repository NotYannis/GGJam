using UnityEngine;
using System.Collections;

public enum Type{
    CalmBeach,
    MediumBeach,
    StrongBeach,
    CalmUnderSea,
    MediumUnderSea,
    StrongUnderSea,
    Sky,
    IntoSea,
}

public enum State
{
    Happy,
    Angry,
    Dead,
}

public enum Schedule
{
    Day,
    Night,
}

public class MovingObject : MonoBehaviour {
    public Type type;
    public State state;
    public Schedule schedule;
    protected MoveScript move;

    private MovingObjectCreator objectCreator;

    public float xLeftDir;
    public float xRightDir;
    public float yDownDir;
    public float yUpDir;

    public bool getOut = false;

    public float moveRefresh;
    private float moveRefreshCooldown;

    public Bounds bound;

    void Awake()
    {
        objectCreator = GameObject.Find("GameScripts").GetComponent<MovingObjectCreator>();
        move = gameObject.GetComponent<MoveScript>();

        switch (type)
        {
            case Type.CalmBeach:
            case Type.MediumBeach:
            case Type.StrongBeach:
                move.velocity = new Vector2(1.0f, 1.0f);
                bound = GameObject.Find("Beach").GetComponent<BoxCollider2D>().bounds;
                Debug.Log(bound.min);
                break;
            case Type.CalmUnderSea:
            case Type.MediumUnderSea:
            case Type.StrongUnderSea:
                move.velocity = new Vector2(1.0f, 0.0f);
                bound = GameObject.Find("UnderSea").GetComponent<BoxCollider2D>().bounds;
                break;
            case Type.Sky:
                move.velocity = new Vector2(1.0f, 1.0f);
                bound = GameObject.Find("Sky").GetComponent<BoxCollider2D>().bounds;
                break;
            case Type.IntoSea:
                move.velocity = new Vector2(1.0f, 1.0f);
                bound = GameObject.Find("IntoSea").GetComponent<BoxCollider2D>().bounds;
                break;
        }
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if(moveRefreshCooldown <= 0.0f)
        {
            MakeSomeMovement();
            moveRefreshCooldown = Random.Range(moveRefresh, moveRefresh * 2.0f);
        }
        else
        {
            moveRefreshCooldown -= Time.deltaTime;
        }
        if (type != Type.Sky && !getOut)
        {
            CheckBounds();
        }
    }

    protected virtual void MakeSomeMovement()
    {
        move.direction = new Vector2(Random.Range(xLeftDir, xRightDir), Random.Range(yDownDir, yUpDir));
    }

    public void ChangeState(State _state)
    {
        state = _state;
    }

    private void CheckBounds()
    {
        Vector2 nextStep = move.nextStep();
        if (nextStep.y > bound.max.y || nextStep.y < bound.min.y)
        {
            move.direction.y *= -1;
            moveRefreshCooldown = Random.Range(moveRefresh, moveRefresh * 2.0f);
        }
        if (nextStep.x > bound.max.x || nextStep.x < bound.min.x)
        {
            if(type == Type.Sky)
            {
                objectCreator.DeleteParticularObject(this);
            }
            else
            {
                move.direction.x *= -1;
                moveRefreshCooldown = Random.Range(moveRefresh, moveRefresh * 2.0f);
            }
        }


    }
}
