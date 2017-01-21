using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CrowdController : MonoBehaviour {
    public int[] jauges = new int[4]; //0 is calm, 1 is medium, 2 is big and 3 is DESTROY
    public MovingObjectCreator objectCreation;

    public float jaugesDropRate;
    private float jaugesDropCooldown;

    private float intoSeaPopRate;
    private float intoSeaPopCooldown;

    private float skyPopRate;
    private float skyPopCooldown;


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


        objectCreation = GameObject.Find("GameScripts").GetComponent<MovingObjectCreator>();
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

        if(intoSeaPopCooldown <= 0.0f)
        {
            intoSeaPopCooldown = Random.Range(10.0f, 20.0f);
            objectCreation.CreateObject(Type.IntoSea, Schedule.Day);
        }
        else
        {
            intoSeaPopCooldown -= Time.deltaTime;
        }

        if(skyPopCooldown <= 0.0f)
        {
            skyPopCooldown = Random.Range(10.0f, 20.0f);
            objectCreation.CreateObject(Type.Sky, Schedule.Day);
        }
        else
        {
            skyPopCooldown -= Time.deltaTime;
        }


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

    public void CreateIntoSea()
    {
        intoSeaPopCooldown = Random.Range(10.0f, 20.0f);
        objectCreation.CreateObject(Type.IntoSea, Schedule.Day);
    }

    public void CreateSky()
    {
        skyPopCooldown = Random.Range(10.0f, 20.0f);
        objectCreation.CreateObject(Type.Sky, Schedule.Day);
    }

    void Debug()
    {
        jauge0.text = ("Jauge 0 : " + jauges[0]);
        jauge1.text = ("Jauge 1 : " + jauges[1]);
        jauge2.text = ("Jauge 2 : " + jauges[2]);
        jauge3.text = ("Jauge 3 : " + jauges[3]);
    }
}
