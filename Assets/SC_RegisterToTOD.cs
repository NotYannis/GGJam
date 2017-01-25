using UnityEngine;
using System.Collections;

public class SC_RegisterToTOD : MonoBehaviour {
    public bool manualInit = false;
    TimeOfDay tod;

	// Use this for initialization
	void Start () {
        if (manualInit) { return; }
        INIT();
	}

    void OnDestroy()
    {
       // tod.Notify_ExitedScene(gameObject);
    }
    public void INIT()
    {
        tod = GameObject.Find("GAME").GetComponent<TimeOfDay>();
        tod.Notify_EnteredScene(gameObject);
    }
}
