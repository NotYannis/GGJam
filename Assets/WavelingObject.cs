using UnityEngine;
using System.Collections;

public class WavelingObject : MonoBehaviour {
    public float waveSpeed;
    float waveStopPosition;
    public float growthFactor = 0.5f;

    private CrowdController crowd;
    float power;
    float maxWavePower;
    enum EWaveloingObj { Spawning, Growing, Running, Dying };
    EWaveloingObj state;

    Waveling_Render waveRend;
    // Use this for initialization

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
                crowd.UpdateJauges(power);

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

    public void INIT(CrowdController crowdContrl, float _maxWavePower, float _waveDeathPosX)
    {
        crowd = crowdContrl;
        maxWavePower = _maxWavePower;
        waveStopPosition = _waveDeathPosX;
        state = EWaveloingObj.Growing;
        power = 0;
    }
    public void Release()
    {
        state = EWaveloingObj.Running;
    }
}
