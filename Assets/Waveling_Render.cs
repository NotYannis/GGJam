using UnityEngine;
using System.Collections;

public class Waveling_Render : MonoBehaviour {
    SC_ShareMaterial waterMaterialShared;
	// Use this for initialization
	void AWake () {
        waterMaterialShared = GameObject.Find("WaterPlane2D").GetComponent<SC_ShareMaterial>();
      //  waterMaterialShared.ShareWith(GetComponent<Renderer>(), 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
