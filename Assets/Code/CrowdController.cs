using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CrowdController : MonoBehaviour {
    public int[] jauges = new int[4]; //0 is calm, 1 is medium, 2 is big and 3 is DESTROY

    public GameObject beachZone;
    public GameObject UnderSeaZone;
    public GameObject IntoSeaZone; 
    public GameObject skyZone;

    public float jaugesDropRate;
    private float jaugesDropCooldown;

    private Text jauge0;
    private Text jauge1;
    private Text jauge2;
    private Text jauge3;



    // Use this for initialization
    void Start () {
        jauge0 = GameObject.Find("DEBUG/Jauge0").GetComponent<Text>();
        jauge1 = GameObject.Find("DEBUG/Jauge1").GetComponent<Text>();
        jauge2 = GameObject.Find("DEBUG/Jauge2").GetComponent<Text>();
        jauge3 = GameObject.Find("DEBUG/Jauge3").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
	    if(jaugesDropCooldown <= 0.0f)
        {
            jaugesDropCooldown = jaugesDropRate;
            for (int i = 0; i < 4; ++i)
            {
                if (jauges[i] > 0)
                {
                    --jauges[i];
                }
            }
        }
        else
        {
            jaugesDropCooldown -= Time.deltaTime;
        }
        Debug();
	}

    public void UpdateJauges(float _wavePower)
    {
        if(_wavePower < 0.5f)
        {
            if(jauges[0] < 10) jauges[0]++;
        }
        else if(_wavePower < 1.0f)
        {
            if (jauges[1] < 10) jauges[1]++;
        }
        else if(_wavePower < 1.5f)
        {
            if (jauges[2] < 10) jauges[2]++;
        }
        else
        {
            if (jauges[3] < 10) jauges[3]++;
        }
    }

    void Debug()
    {
        jauge0.text = ("Jauge 0 : " + jauges[0]);
        jauge1.text = ("Jauge 1 : " + jauges[1]);
        jauge2.text = ("Jauge 2 : " + jauges[2]);
        jauge3.text = ("Jauge 3 : " + jauges[3]);
    }
}
