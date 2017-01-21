using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public struct wave
{
    public float power;
    public Vector3 position;
}

public class SeaScript : MonoBehaviour {
    private List<wave> waves;
    private List<GameObject> wavesPH;

    private CrowdController crowd;

    public GameObject wavePlaceHolder;

    public float waveSpeed;
    public Vector3 waveStartPosition;
    public float waveStopPosition;

	// Use this for initialization
	void Start () {
        waves = new List<wave>();
        wavesPH = new List<GameObject>();
        crowd = GameObject.Find("GameScripts").GetComponent<CrowdController>();
        wavePlaceHolder = Resources.Load("Prefabs/WavePH") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
        for(int i = 0; i < waves.Count; ++i)
        {
            //If touch the beach, update jauges
            if(waves[i].position.x < waveStopPosition)
            {
                crowd.UpdateJauges(waves[i].power);
                waves.RemoveAt(i);

                Destroy(wavesPH[i]);
                wavesPH.RemoveAt(i);
            }
            else //Update wave position
            {
                wave w = waves[i];
                w.position.x -= waveSpeed * Time.deltaTime;
                waves[i] = w;

                wavesPH[i].transform.position = waves[i].position;
            }
        }
	}

    public void CreateWave(float _wavePower)
    {
        wave newWave = new wave();
        newWave.power = _wavePower;
        newWave.position = waveStartPosition;
        waves.Add(newWave);

        GameObject wavePH = Instantiate(wavePlaceHolder, waveStartPosition, Quaternion.identity) as GameObject;
        wavesPH.Add(wavePH);

    }
}
