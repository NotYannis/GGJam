using UnityEngine;
using System.Collections;

public class SC_TimedDeath : MonoBehaviour {
    public float LifeTime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0f)
        {
            Destroy(gameObject);
        }
	}
}
