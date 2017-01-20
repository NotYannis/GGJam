using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeOfDay : MonoBehaviour {
    public float fullDayCycleDuration = 5.0f;
    public Texture TimeOfDayLightColorRamp;

    float t_timeOfDay;
    List<Material> materialsWithNightDayShader;

	// Use this for initialization
	void Start () {
        string shaderToLookFor = "Unlit/S_ImageOfTheNight";
        materialsWithNightDayShader = new List<Material>();
        Renderer[] allObjects = UnityEngine.Object.FindObjectsOfType<Renderer>();
        foreach (Renderer r in allObjects)
        {
            if (r.material.shader.name == shaderToLookFor)
            {
                materialsWithNightDayShader.Add(r.material);
                r.material.SetTexture("_TimeOfDayLightColorRampTex", TimeOfDayLightColorRamp);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        t_timeOfDay += Time.deltaTime;
        if (t_timeOfDay >= fullDayCycleDuration)
        {
            t_timeOfDay -= fullDayCycleDuration;
        }

        foreach (Material mat in materialsWithNightDayShader)
        {
            mat.SetFloat("_TimeOfDay", t_timeOfDay/fullDayCycleDuration);
        }

    }


    public float GetTimeOfDay()
    {
        return t_timeOfDay / fullDayCycleDuration;
    }
}
