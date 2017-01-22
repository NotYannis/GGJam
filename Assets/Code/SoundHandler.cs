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

    public void LoadSound(string soundName)
    {
        if (CanPlaySound())
        {
            AudioSource sound = Resources.Load("Sound/" + soundName, typeof(AudioSource)) as AudioSource;
            sound.Play();
        }
    }

    private bool CanPlaySound()
    {
        return soundCooldown <= 0.0f;
    }
}