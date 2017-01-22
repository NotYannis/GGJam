using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeOfDay : MonoBehaviour {
    public float GameStartingHour = 10;
    public float fullDayCycleDuration = 5.0f;
    public float Dayrise = 6;
    public float NightfallHour = 18.5f;
    public Texture TimeOfDayLightColorRamp;

    float t_timeOfDay;
    float halfDay;
    Dictionary<string,Material> materialsWithNightDayShader;
    Transform skyGradient;

	// Use this for initialization
	void Awake () {

        string shaderToLookFor = "Unlit/S_ImageOfTheNight";
        materialsWithNightDayShader = new Dictionary<string, Material>();
        Renderer[] allObjects = UnityEngine.Object.FindObjectsOfType<Renderer>();
        foreach (Renderer r in allObjects)
        {
            if (r.material.shader.name == shaderToLookFor)
            {
                materialsWithNightDayShader.Add(r.name, r.material);
                r.material.SetTexture("_TimeOfDayLightColorRampTex", TimeOfDayLightColorRamp);
            }
        }

        halfDay = fullDayCycleDuration / 2;
        t_timeOfDay += (fullDayCycleDuration / 24) * GameStartingHour;
    }

    // Update is called once per frame
    void Update()
    {
        t_timeOfDay += Time.deltaTime;
        if (t_timeOfDay >= fullDayCycleDuration)
        {
            t_timeOfDay -= fullDayCycleDuration;
        }


        float time;
        if (t_timeOfDay > halfDay)
        {
            time = Mathf.Lerp(0, 1, (t_timeOfDay - halfDay) / halfDay);
        }
        else
        {
            time = Mathf.Lerp(1, 0, (t_timeOfDay) / halfDay);
        }


        foreach (Material mat in materialsWithNightDayShader.Values)
        {
            mat.SetFloat("_TimeOfDay",time);
        }

      //  Debug.Log(GetHour());
    }


    public void Notify_EnteredScene(GameObject go)
    {
        materialsWithNightDayShader.Add(go.name, go.GetComponent<Renderer>().material);
    }
    public void Notify_ExitedScene(GameObject go)
    {
        materialsWithNightDayShader.Remove(go.name);
    }

    public float GetTimeOfDay()
    {
        return t_timeOfDay / fullDayCycleDuration;
    }

    public float GetHour()
    {
        return 24.0f * (GetTimeOfDay());
    }

    public Schedule GetSchedule()
    {
        Schedule schedule;
        if(GetHour() > NightfallHour && GetHour() < Dayrise)
        {
            schedule = Schedule.Night;
        }
        else
        {
            schedule = Schedule.Day;
        }

        return schedule;
    }
}
