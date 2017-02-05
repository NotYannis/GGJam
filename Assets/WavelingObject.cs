using UnityEngine;
using System.Collections;

public class WavelingObject : MonoBehaviour {
    public float waveSpeed;
    float waveStopPosition;
    public float growthFactor;
    private string waveCode;

    private CrowdController crowd;
    private MoverManager movers;

    float power;
    float maxWavePower;
    enum EWaveloingObj { Spawning, Growing, Running, Dying };
    EWaveloingObj state;

    Waveling_Render waveRend;

      void Awake()
    {
         state = EWaveloingObj.Spawning;
    }
    void Start () {
        waveRend = transform.Find("Waveling").GetComponent<Waveling_Render>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (state == EWaveloingObj.Running)
        {
            if (transform.position.x < waveStopPosition)
            {
                movers.updateWavecombo(waveCode);

                waveRend.Kill();
                Destroy(gameObject, 1f);
                state = EWaveloingObj.Dying;
            }
            else //Update wave position
            {
                Vector3 pos = transform.position;
                pos.x -= waveSpeed * Time.deltaTime;
                transform.position = pos;

            }
        }
        
        if (state == EWaveloingObj.Growing && power <= maxWavePower)
        {
            power += Time.deltaTime * growthFactor;
            waveRend.UpdateWaveGrowing(power / maxWavePower);
        }
    }

    public void INIT(MoverManager _movers, float _maxWavePower, float _growthFactor, float _waveDeathPosX)
    {
        growthFactor = _growthFactor;
        movers = _movers;
        maxWavePower = _maxWavePower;
        waveStopPosition = _waveDeathPosX;
        state = EWaveloingObj.Growing;
        power = 0;
    }

    public void Release()
    {
        state = EWaveloingObj.Running;

        if(power < 0.5f)
        {
            waveCode = "1";
        }
        else if(power < 1.0f)
        {
            waveCode = "2";
        }
        else if(power < 1.5f)
        {
            waveCode = "3";
        }
        else
        {
            waveCode = "4";
        }
    }
}
