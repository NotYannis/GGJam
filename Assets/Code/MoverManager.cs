using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoverManager : MonoBehaviour {
    
    private Dictionary<string, BeachMover> beachMoverPool;
    private Dictionary<string, UnderSeaMover> underSeaMoverPool;

    private List<SkyMover> nightSkyMoverPool;
    private List<SkyMover> daySkyMoverPool;

    private List<IntoSeaMover> nightSeaMoverPool;
    private List<IntoSeaMover> daySeaMoverPool;

    private List<Mover> moversOutside;

    private string currentWaveCode = "";

    public float waveComboTimer = 5.0f;
    private float waveComboCooldown;

    public float createSkyMoverTime = 50.0f;
    private float createSkyMoverCooldown;
    public float createSeaMoverTime = 20.0f;
    private float createSeaMoverCooldown;

    private Schedule previousSchedule;
    private TimeOfDay tod;
    
    // Use this for initialization
    void Awake()
    {

        tod = GameObject.Find("GAME").GetComponent<TimeOfDay>();

        moversOutside = new List<Mover>();
        beachMoverPool = new Dictionary<string, BeachMover>();
        underSeaMoverPool = new Dictionary<string, UnderSeaMover>();
        daySkyMoverPool = new List<SkyMover>();
        nightSkyMoverPool = new List<SkyMover>();
        daySeaMoverPool = new List<IntoSeaMover>();
        nightSeaMoverPool = new List<IntoSeaMover>();


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

        GameObject[] skyMovers = Resources.LoadAll("Prefabs/Sky").Cast<GameObject>().ToArray();
        foreach (GameObject mover in skyMovers)
        {
            SkyMover skyM = mover.GetComponent<SkyMover>();
            if (skyM.schedule == Schedule.Day)
            {
                daySkyMoverPool.Add(skyM);
            }
            else
            {
                nightSkyMoverPool.Add(skyM);
            }
        }

        GameObject[] seaMovers = Resources.LoadAll("Prefabs/IntoSea").Cast<GameObject>().ToArray();
        foreach (GameObject mover in seaMovers)
        {
            IntoSeaMover seaM = mover.GetComponent<IntoSeaMover>();
            if (seaM.schedule == Schedule.Day)
            {
                daySeaMoverPool.Add(seaM);
            }
            else
            {
                nightSeaMoverPool.Add(seaM);
            }
        }
    }

	void Update () {
		if(waveComboCooldown > 0.0f)
        {
            waveComboCooldown -= Time.deltaTime;
        }
        else
        {
            if(currentWaveCode != "")
            {
                UpdateCrowd();
            }
        }

        if(createSkyMoverCooldown > 0.0f)
        {
            createSkyMoverCooldown -= Time.deltaTime;
        }
        else
        {
            createSkyMoverCooldown = Random.Range(createSkyMoverTime / 4, createSkyMoverTime);
            if(tod.GetSchedule() == Schedule.Day)
            {
                Instantiate(daySkyMoverPool[Random.Range(0, daySkyMoverPool.Count)]);
            }
            else
            {
                Instantiate(nightSkyMoverPool[Random.Range(0, nightSkyMoverPool.Count)]);
            }
        }

        if (createSeaMoverCooldown > 0.0f)
        {
            createSeaMoverCooldown -= Time.deltaTime;
        }
        else
        {
            createSeaMoverCooldown = Random.Range(createSeaMoverTime / 4, createSeaMoverTime);
            if (tod.GetSchedule() == Schedule.Day)
            {
                Instantiate(daySeaMoverPool[Random.Range(0, daySeaMoverPool.Count)]);
            }
            else
            {
                Instantiate(nightSeaMoverPool[Random.Range(0, nightSeaMoverPool.Count)]);
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
            UpdateCrowd();
        }

    }

    private void UpdateCrowd()
    {
        if (previousSchedule != tod.GetSchedule())
        {
            foreach(Mover m in moversOutside)
            {
                m.GoAwayTime();
            }
        }

        previousSchedule = tod.GetSchedule();

        if (beachMoverPool.ContainsKey(currentWaveCode))
        {
            if(beachMoverPool[currentWaveCode].schedule == tod.GetSchedule())
            {
                BeachMover newMover = Instantiate(beachMoverPool[currentWaveCode]);
                moversOutside.Add(newMover);
            }
        }

        if (underSeaMoverPool.ContainsKey(currentWaveCode))
        {
            if(underSeaMoverPool[currentWaveCode].schedule == tod.GetSchedule())
            {
                UnderSeaMover newMover = Instantiate(underSeaMoverPool[currentWaveCode]);
                moversOutside.Add(newMover);
            }
        }

        currentWaveCode = "";
    }

    public void DeleteMover(Mover mover)
    {
        moversOutside.Remove(mover);
    }
}
