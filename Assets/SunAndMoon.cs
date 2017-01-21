using UnityEngine;
using System.Collections;

public class SunAndMoon : MonoBehaviour {
    public float ShowUp_StartHour;
    public float ShowUp_ZenithHour;
    public float Hide_StartHour;
    public float Hide_ZenithHour;


    Transform objectToMove;
    Vector3 ShowUpTransform;
    Vector3 hideTransform;
    TimeOfDay TOD;

    float hoursVisible;
    float hoursHidden;
    // Use this for initialization
    void Awake () {
        objectToMove = transform.Find("Object");
        ShowUpTransform = transform.Find("Start").position;
        hideTransform = transform.Find("End").position;

        TOD = GameObject.Find("GAME").GetComponent<TimeOfDay>() as TimeOfDay; 
    }

    void Start()
    {
        hoursVisible = ShowUp_ZenithHour - ShowUp_StartHour;
        hoursHidden = Hide_ZenithHour - Hide_StartHour;
    }
	
	// Update is called once per frame
	void Update () {
        float hour = TOD.GetHour();
        Vector3 object_pos = objectToMove.position;
        if (hour > ShowUp_StartHour && hour < ShowUp_ZenithHour)
        {
            hour -= ShowUp_StartHour;
            object_pos = Vector3.Lerp(hideTransform, ShowUpTransform, hour / hoursVisible);
        }
        else if (hour > Hide_StartHour && hour < Hide_ZenithHour)
        {
            hour -= Hide_StartHour;
            object_pos = Vector3.Lerp(ShowUpTransform, hideTransform, hour / hoursHidden);
        }
        objectToMove.position = object_pos;
    }
}
