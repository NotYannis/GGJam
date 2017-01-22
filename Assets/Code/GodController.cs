using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GodController : MonoBehaviour {
    private float wavePower = 0;
    private Text waveChargeDebug;
    private GameObject godArm;
    public SeaScript sea;

    private Vector3 godArmPosition;
	// Use this for initialization
	void Start () {
        waveChargeDebug = GameObject.Find("DEBUG/waveCharge").GetComponent<Text>();
        sea = GameObject.Find("Sea").GetComponent<SeaScript>();
        godArm = GameObject.Find("Arm");

        godArmPosition = godArm.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        bool waveCharging = Input.GetKey(KeyCode.Space);

        if (waveCharging && wavePower < 1.5f)
        {
            gameObject.GetComponent<Animator>().SetBool("PrepareWave", true);
            wavePower += (Time.deltaTime/2);
            godArm.transform.position = new Vector3(godArm.transform.position.x, godArm.transform.position.y + wavePower / 100, godArm.transform.position.z);
        }

        if (Input.GetKeyUp(KeyCode.Space)){
            gameObject.GetComponent<Animator>().SetBool("PrepareWave", false);
            sea.CreateWave(wavePower);
            wavePower = 0;
            godArm.transform.position = godArmPosition;
        }

        Debug();
	}

    void Debug()
    { 
        waveChargeDebug.text = ("Wave power = " + wavePower);
    }
}
