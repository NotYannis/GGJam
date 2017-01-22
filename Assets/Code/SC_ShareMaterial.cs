using UnityEngine;
using System.Collections;

public class SC_ShareMaterial : MonoBehaviour {
    Material materialToShare;

    public Renderer r;
    // Use this for initialization
    void Awake() {
        materialToShare = GetComponent<Renderer>().material;
       
    }
    void Start()
    {
      //  ShareWith(r);
       // ShareWith(GameObject.Find("WavePlaneMesh").GetComponent<Renderer>());
    }

    // Update is called once per frame
    void Update() {
       
    }


    public void ShareWith(Renderer renderer)
    {
        renderer.material = materialToShare;
    }
    public void ShareWith(Renderer renderer, int materialId)
    {
        renderer.materials[materialId] = materialToShare;
        Debug.Log("Shared with " + renderer.gameObject.name);
    }


}
