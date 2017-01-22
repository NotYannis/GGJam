using UnityEngine;
using System.Collections;

public class SC_RegisterToTOD : MonoBehaviour {

    TimeOfDay tod;

	// Use this for initialization
	void Start () {
        tod = GameObject.Find("GAME").GetComponent<TimeOfDay>();
        tod.Notify_EnteredScene(gameObject);
	}

    void OnDestroy()
    {
        tod.Notify_ExitedScene(gameObject);
    }
}
