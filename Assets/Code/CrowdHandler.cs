using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdHandler : MonoBehaviour {

    public delegate void AddWalker(GameObject walker, EventArgs e);
    public event AddWalker NewWalker;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
