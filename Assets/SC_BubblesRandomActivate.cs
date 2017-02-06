using UnityEngine;
using System.Collections;

public class SC_BubblesRandomActivate : MonoBehaviour {
    ParticleSystem PS;
    ParticleSystem.EmissionModule EM;
    Vector2 toggledTime = new Vector2(0.5f, 1);
    Vector2 disabledTime = new Vector2(2, 5);
	// Use this for initialization
	void Awake () {
        PS = GetComponent<ParticleSystem>();
        EM = PS.emission;
	}


    IEnumerator StopBubbles()
    {
        yield return new WaitForSeconds(Random.Range(toggledTime.x, toggledTime.y));
        BubblesSwitch(false);
        StartCoroutine(ReenableBubbles());
    }
    IEnumerator ReenableBubbles()
    {
        yield return new WaitForSeconds(Random.Range(disabledTime.x, disabledTime.y));
        BubblesSwitch(true);
        StartCoroutine(StopBubbles());
    }
    void Start()
    {
        StartCoroutine(StopBubbles());
    }

    void BubblesSwitch(bool newState)
    {
        EM.enabled = newState;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
