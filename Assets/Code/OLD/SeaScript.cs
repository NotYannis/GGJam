﻿using UnityEngine;
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

    public GameObject wavePlaceHolder;

    public float waveSpeed;
    public Vector3 waveStartPosition;
    public float waveStopPosition;

	// Use this for initialization
	void Start () {
        waves = new List<wave>();
        wavesPH = new List<GameObject>();
        wavePlaceHolder = Resources.Load("Prefabs/WavePH") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
        //for(int i = 0; i < waves.Count; ++i)
        //{
        //    //If touch the beach, update jauges
        //    if(waves[i].position.x < waveStopPosition)
        //    {
        //        crowd.UpdateJauges(waves[i].power);
        //        waves.RemoveAt(i);

        //        wavesPH[i].transform.Find("Waveling").GetComponent<Waveling_Render>().Kill();
        //        Destroy(wavesPH[i], 1f);
               
        //        wavesPH.RemoveAt(i);
        //    }
        //    else //Update wave position
        //    {
        //        wave w = waves[i];
        //        w.position.x -= waveSpeed * Time.deltaTime;
        //        waves[i] = w;

        //        wavesPH[i].transform.position = waves[i].position;
        //    }
        //}
	}

    public void CreateWave(float _wavePower)
    {
       // wave newWave = new wave();
       // newWave.power = _wavePower;
       // newWave.position = waveStartPosition;
       // waves.Add(newWave);

       // GameObject wavePH = Instantiate(wavePlaceHolder, waveStartPosition, Quaternion.identity) as GameObject;
       //// wavePH.transform.Find("Waveling").GetComponent<Waveling_Render>().Init(_wavePower / 1.5f);
       // wavesPH.Add(wavePH);

    }
}
