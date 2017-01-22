using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CrowdController : MonoBehaviour {
    public int[] jauges = new int[4]; //0 is calm, 1 is medium, 2 is big and 3 is DESTROY
    public MovingObjectCreator objectCreation;
    public TimeOfDay globalTime;

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
    void Awake () {
        jauge0 = GameObject.Find("DEBUG/Jauge0").GetComponent<Text>();
        jauge1 = GameObject.Find("DEBUG/Jauge1").GetComponent<Text>();
        jauge2 = GameObject.Find("DEBUG/Jauge2").GetComponent<Text>();
        jauge3 = GameObject.Find("DEBUG/Jauge3").GetComponent<Text>();

        globalTime = GameObject.Find("GameScripts").GetComponent<TimeOfDay>();
        objectCreation = GameObject.Find("GameScripts").GetComponent<MovingObjectCreator>();
    }

    // Update is called once per frame
    void Update () {
        if (jaugesDropCooldown <= 0.0f)
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
        int rand = (int)Random.value;

        if (_wavePower < 0.5f)
        {
            if (jauges[0] < 10)
            {
                jauges[0]++;
                objectCreation.CreateObject(Type.CalmBeach, globalTime.GetSchedule());
                objectCreation.CreateObject(Type.CalmUnderSea, globalTime.GetSchedule());
                if(rand == 0)
                {
                    objectCreation.DeleteObject(Type.MediumBeach, globalTime.GetSchedule());
                    objectCreation.DeleteObject(Type.MediumUnderSea, globalTime.GetSchedule());
                }
                if (rand == 1)
                {
                    objectCreation.DeleteObject(Type.StrongBeach, globalTime.GetSchedule());
                    objectCreation.DeleteObject(Type.StrongUnderSea, globalTime.GetSchedule());
                }
            }
        }
        else if(_wavePower < 1.0f)
        {
            if (jauges[1] < 10)
            {
                jauges[1]++;
                objectCreation.CreateObject(Type.MediumBeach, globalTime.GetSchedule());
                objectCreation.CreateObject(Type.MediumUnderSea, globalTime.GetSchedule());
                if (rand == 0)
                {
                    objectCreation.DeleteObject(Type.CalmBeach, globalTime.GetSchedule());
                    objectCreation.DeleteObject(Type.CalmUnderSea, globalTime.GetSchedule());
                }
                if (rand == 1)
                {
                    objectCreation.DeleteObject(Type.StrongBeach, globalTime.GetSchedule());
                    objectCreation.DeleteObject(Type.StrongUnderSea, globalTime.GetSchedule());
                }
            }
        }
        else if(_wavePower < 1.5f)
        {
            if (jauges[2] < 10)
            {
                jauges[2]++;
                objectCreation.CreateObject(Type.StrongBeach, globalTime.GetSchedule());
                objectCreation.CreateObject(Type.StrongUnderSea, globalTime.GetSchedule());
                if (rand == 0)
                {
                    objectCreation.DeleteObject(Type.MediumBeach, globalTime.GetSchedule());
                    objectCreation.DeleteObject(Type.MediumUnderSea, globalTime.GetSchedule());
                }
                if (rand == 1)
                {
                    objectCreation.DeleteObject(Type.CalmBeach, globalTime.GetSchedule());
                    objectCreation.DeleteObject(Type.CalmUnderSea, globalTime.GetSchedule());
                }
            }
        }
        else
        {
            if (jauges[3] < 10)
            {
                jauges[3]++;
                objectCreation.DeleteObject(Type.CalmBeach, globalTime.GetSchedule());
                objectCreation.DeleteObject(Type.CalmUnderSea, globalTime.GetSchedule());
                objectCreation.DeleteObject(Type.MediumBeach, globalTime.GetSchedule());
                objectCreation.DeleteObject(Type.MediumUnderSea, globalTime.GetSchedule());
                objectCreation.DeleteObject(Type.StrongBeach, globalTime.GetSchedule());
                objectCreation.DeleteObject(Type.StrongUnderSea, globalTime.GetSchedule());
            }
        }
    }

    public void CreateIntoSea()
    {
        intoSeaPopCooldown = Random.Range(10.0f, 20.0f);
        objectCreation.CreateObject(Type.IntoSea, globalTime.GetSchedule());
    }

    public void CreateSky()
    {
        skyPopCooldown = Random.Range(10.0f, 20.0f);
        objectCreation.CreateObject(Type.Sky, globalTime.GetSchedule());
    }
}
