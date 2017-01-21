using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeOfDay : MonoBehaviour {
    public float fullDayCycleDuration = 5.0f;
    public Texture TimeOfDayLightColorRamp;
    public Vector2 skyGradientYHeights;

    float t_timeOfDay;
    float halfDay;
    Dictionary<string,Material> materialsWithNightDayShader;
    Transform skyGradient;

	// Use this for initialization
	void Start () {

        skyGradient = transform.FindChild("SkyGradient");

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
            time = Mathf.Lerp(1, 0, (t_timeOfDay - halfDay) / halfDay);
        }
        else
        {
            time = Mathf.Lerp(0, 1, (t_timeOfDay) / halfDay);
        }


        foreach (Material mat in materialsWithNightDayShader.Values)
        {
            mat.SetFloat("_TimeOfDay",time);
        }

        Vector3 skyGradPos = skyGradient.position;
        skyGradPos.y = Mathf.Lerp(skyGradientYHeights.x, skyGradientYHeights.y, time);
        skyGradient.position = skyGradPos;

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
}
