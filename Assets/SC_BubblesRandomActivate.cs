using UnityEngine;
using System.Collections;

public class SC_BubblesRandomActivate : MonoBehaviour {
    ParticleSystem PS;
    Vector2 toggledTime = new Vector2(0.5f, 1);
    Vector2 disabledTime = new Vector2(2, 5);
	// Use this for initialization
	void Awake () {
        PS = GetComponent<ParticleSystem>();
	}


    IEnumerator StopBubbles()
    {
        yield return new WaitForSeconds(Random.RandomRange(toggledTime.x, toggledTime.y));
        BubblesSwitch(false);
        StartCoroutine(ReenableBubbles());
    }
    IEnumerator ReenableBubbles()
    {
        yield return new WaitForSeconds(Random.RandomRange(disabledTime.x, disabledTime.y));
        BubblesSwitch(true);
        StartCoroutine(StopBubbles());
    }
    void Start()
    {
        StartCoroutine(StopBubbles());
    }

    void BubblesSwitch(bool newState)
    {
        PS.enableEmission = newState;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
