using UnityEngine;
using System.Collections;

public class SoundHandler : MonoBehaviour {

    private float soundRate;
    private float soundCooldown;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if(soundCooldown > 0.0f)
        {
            soundCooldown -= Time.deltaTime;
        }
    }

    public void LoadSound(string soundName, Vector3 position)
    {
        if (CanPlaySound())
        {
            AudioSource sound = Resources.Load("Sound/" + soundName) as AudioSource;
            if(sound != null)
            {
                Debug.Log("SOUND" + soundName);
                GameObject soundObject = Instantiate(sound, position, Quaternion.identity) as GameObject;
                Destroy(soundObject, sound.clip.length);
            }
        }
    }

    private bool CanPlaySound()
    {
        return soundCooldown <= 0.0f;
    }
}