using UnityEngine;
using System.Collections;

public class SC_TODRegisterToMaterial : MonoBehaviour {

    TimeOfDay tod;

    // Use this for initialization
    void Start()
    {
        tod = GameObject.Find("GAME").GetComponent<TimeOfDay>();

        Material mat = GetComponent<Renderer>().material;
        Texture mainTex = mat.GetTexture("_MainTex");

        GetComponent<Renderer>().material = GameObject.Find("VAMPIRE").GetComponent<Renderer>().material;
        GetComponent<Renderer>().material.SetTexture("Texture", mainTex);

        tod.Notify_EnteredScene(gameObject);
    }

    void OnDestroy()
    {
        tod.Notify_ExitedScene(gameObject);
    }
}
