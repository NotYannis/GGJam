using UnityEngine;
using System.Collections;

public class SoundHandler : MonoBehaviour {

    private float soundRate = 10.0f;
    private float soundCooldown = 0.0f;

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

    public void LoadSound(string soundName, GameObject objectSound)
    {
        if (CanPlaySound())
        {
            AudioSource sound = Resources.Load("Sound/" + soundName, typeof(AudioSource)) as AudioSource;
            if(sound != null)
            {
                objectSound.GetComponent<AudioSource>().clip = sound.clip;
                objectSound.GetComponent<AudioSource>().Play();
                soundCooldown = soundRate;
            }
        }
    }

    private bool CanPlaySound()
    {
        return soundCooldown <= 0.0f;
    }
}