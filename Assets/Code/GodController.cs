using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GodController : MonoBehaviour {
    private float wavePower = 0;
    private Text waveChargeDebug;
    private GameObject godArm;
    public SeaScript sea;
    public float waveGrowthFactor = 1.0f;
    public float maxWavePower = 1.5f;

    WavelingObject chargingWave;
    GameObject chargingWaveObj;

    GameObject wavePlaceHolder;
    public Transform waveStartPosition;
    public Transform waveEndPosition;
    private Vector3 godArmPosition;
    private CrowdController crowd;
    private MoverManager movers;

    private AudioSource armAudioFeedback;

    public float calmSeaTime = 5.0f; //will send a "0" wave sometimes when no waves are sends
    public float calmSeaCooldown;

    // Use this for initialization
    void Awake()
    {
        wavePlaceHolder = Resources.Load("Prefabs/WavePH") as GameObject;
        armAudioFeedback = GetComponent<AudioSource>();
        godArm = GameObject.Find("Arm");
        movers = GameObject.Find("GameScripts").GetComponent<MoverManager>();
    }
    void Start () {
        calmSeaCooldown = calmSeaTime;

        godArmPosition = godArm.transform.position;
        crowd = GameObject.Find("GameScripts").GetComponent<CrowdController>();
    }
	
	// Update is called once per frame
	void Update () {
        bool waveCharging = Input.GetKey(KeyCode.Space);

        if (waveCharging && wavePower < maxWavePower)
        {
            if (chargingWave == null) {
                SpawnWave();
                armAudioFeedback.Play();
            }

            gameObject.GetComponent<Animator>().SetBool("PrepareWave", true);
            wavePower += (Time.deltaTime/2);
            godArm.transform.position = new Vector3(godArm.transform.position.x, godArm.transform.position.y + wavePower / 100, godArm.transform.position.z);
            
        }

        if (Input.GetKeyUp(KeyCode.Space)){
            calmSeaCooldown = calmSeaTime;
            gameObject.GetComponent<Animator>().SetBool("PrepareWave", false);
            wavePower = 0;
            godArm.transform.position = godArmPosition;
            armAudioFeedback.Stop();
            chargingWave.Release();
            chargingWave = null;
            chargingWaveObj = null;
        }

        if (calmSeaCooldown >= 0.0f)
        {
            calmSeaCooldown -= Time.deltaTime;
        }
        else
        {
            calmSeaCooldown = calmSeaTime;
            movers.updateWavecombo("0");
        }

    }

    void SpawnWave()
    {
        if (wavePlaceHolder== null) { Debug.Log("FUCK"); }
        chargingWaveObj = Instantiate(wavePlaceHolder, waveStartPosition.position, Quaternion.identity) as GameObject;
        chargingWave = chargingWaveObj.GetComponent<WavelingObject>();
        chargingWave.INIT(movers, maxWavePower, waveGrowthFactor, waveEndPosition.position.x);
    }
}
