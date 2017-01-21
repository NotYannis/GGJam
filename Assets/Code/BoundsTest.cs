using UnityEngine;
using System.Collections;

public class BoundsTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Bounds bound = GetComponent<PolygonCollider2D>().bounds;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
