using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveScript : MonoBehaviour {
    public Vector3 velocity;
    public Vector3 direction;

    private Rigidbody2D rig;
	// Use this for initialization
	void Start () {
        rig = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        rig.velocity = new Vector2(velocity.x * direction.x, velocity.y * direction.y);
        if(Mathf.Sign(rig.velocity.x) != Mathf.Sign(transform.localScale.x))
        transform.localScale = new Vector2(transform.localScale.x * Mathf.Sign(rig.velocity.x), transform.localScale.y);
	}
}