using UnityEngine;
using System.Collections;

public class TOD_Enabled : MonoBehaviour {
    public float EnableAtTOD;
    public float DisableAtTOD;

    ParticleSystem PS;
    TimeOfDay TOD;
    bool isEnabled;
	// Use this for initialization
	void Start () {
        TOD = GameObject.Find("GAME").GetComponent<TimeOfDay>() as TimeOfDay;
        PS = GetComponent<ParticleSystem>();

        float hour = TOD.GetHour();
        if (hour > EnableAtTOD && hour < (EnableAtTOD + 0.4f))
        {
            isEnabled = true;
            PS.enableEmission = true;

        }
        else 
        {
            isEnabled = false;
            PS.enableEmission = false;

        }


    }
	
	// Update is called once per frame
	void Update () {
        float hour = TOD.GetHour();
        if (!isEnabled && hour > EnableAtTOD && hour < (EnableAtTOD + 0.4f))
        {
            isEnabled = true;
            PS.enableEmission = true;
        }
        else if (isEnabled && hour > DisableAtTOD && hour < (DisableAtTOD + 0.4f))
        {
            isEnabled = false;
            PS.enableEmission = false;
        }

	}
}
