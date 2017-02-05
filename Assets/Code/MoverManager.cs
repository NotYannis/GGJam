using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoverManager : MonoBehaviour {
    
    public Dictionary<string, BeachMover> beachMoverPool;
    public Dictionary<string, UnderSeaMover> underSeaMoverPool;

    public GameObject beachZone;
    public GameObject underSeaZone;
    public GameObject skyZone;
    public GameObject intoSeaZone;

    private string currentWaveCode = "";

    private float waveComboTimer = 5.0f;
    private float waveComboCooldown;

    private TimeOfDay tod;

    // Use this for initialization
    void Awake()
    {
        tod = GameObject.Find("GAME").GetComponent<TimeOfDay>();

        beachMoverPool = new Dictionary<string, BeachMover>();
        underSeaMoverPool = new Dictionary<string, UnderSeaMover>();

        GameObject[] beachMovers = Resources.LoadAll("Prefabs/Beach").Cast<GameObject>().ToArray();
        foreach(GameObject mover in beachMovers)
        {
            BeachMover beachM = mover.GetComponent<BeachMover>();
            beachMoverPool.Add(beachM.waveCode, beachM);
        }

        GameObject[] underSeaMovers = Resources.LoadAll("Prefabs/UnderSea").Cast<GameObject>().ToArray();
        foreach (GameObject mover in underSeaMovers)
        {
            UnderSeaMover underSeaM = mover.GetComponent<UnderSeaMover>();
            underSeaMoverPool.Add(underSeaM.waveCode, underSeaM);
        }
    }

    void Start() {

	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(tod.GetSchedule());
		if(waveComboCooldown > 0.0f)
        {
            waveComboCooldown -= Time.deltaTime;
        }
        else
        {
            if(currentWaveCode != "")
            {
                updateCrowd();
            }
        }
	}

    public void updateWavecombo(string waveCode)
    {
        currentWaveCode += waveCode;

        waveComboCooldown = waveComboTimer;

        if(currentWaveCode.Length >= 4)
        {
            Debug.Log(currentWaveCode);
            updateCrowd();
        }

    }

    private void updateCrowd()
    {
        if (beachMoverPool.ContainsKey(currentWaveCode))
        {
            if(beachMoverPool[currentWaveCode].schedule == tod.GetSchedule())
            {
                Instantiate(beachMoverPool[currentWaveCode]);
            }
        }

        if (underSeaMoverPool.ContainsKey(currentWaveCode))
        {
            if(underSeaMoverPool[currentWaveCode].schedule == tod.GetSchedule())
            {
                Instantiate(underSeaMoverPool[currentWaveCode]);
            }
        }

        currentWaveCode = "";
        Debug.Log("crowded");
    }
}
