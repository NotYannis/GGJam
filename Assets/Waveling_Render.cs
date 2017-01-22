using UnityEngine;
using System;
using System.Collections;

public class Waveling_Render : MonoBehaviour {
    public Vector2 ScaleRange;
    public float SecondsForGrowth = 0.7f;

    Transform meshTrans;
    Transform psFoamTrans;
    float scale;
    float t_SecondsForGrowth = 0.7f;
    enum EWavelingState {Spawned, Growing, Living, Dying};
    EWavelingState state;

    Vector3 sc_meshTrans;
    Vector3 sc_psFoam;
    // Use this for initialization
    void Awake () {
        meshTrans = transform.Find("WaveVis");
        psFoamTrans = transform.Find("PS_WidthWave");

        state = EWavelingState.Spawned;
        sc_meshTrans = meshTrans.localScale;
        sc_psFoam = psFoamTrans.localScale;
        meshTrans.localScale = Vector3.zero;
        psFoamTrans.localScale = Vector3.zero;
    }
    void Start()
    {      

    }

    // Update is called once per frame
    void Update () {
       // Debug.Log(state);
        if (state == EWavelingState.Growing)
        {
            t_SecondsForGrowth -= Time.deltaTime;
            if (t_SecondsForGrowth < 0) { state = EWavelingState.Living; }
            meshTrans.localScale = sc_meshTrans* Mathf.Lerp(scale,0, t_SecondsForGrowth/ SecondsForGrowth);
            psFoamTrans.localScale = sc_psFoam* Mathf.Lerp( scale,0, t_SecondsForGrowth/ SecondsForGrowth);
        }
        else if (state == EWavelingState.Dying)
        {
            t_SecondsForGrowth -= Time.deltaTime;
            if (t_SecondsForGrowth < 0) { return; }
            meshTrans.localScale = sc_meshTrans * Mathf.Lerp(0, scale, t_SecondsForGrowth / SecondsForGrowth);
            psFoamTrans.localScale = sc_psFoam * Mathf.Lerp(0, scale, t_SecondsForGrowth / SecondsForGrowth);
        }
    }

    public void Init(float sizeRel)
    {
        scale = Mathf.Lerp(ScaleRange.x, ScaleRange.y, sizeRel);
        t_SecondsForGrowth = SecondsForGrowth;
        state = EWavelingState.Growing;
    }
    public void Kill()
    {
        t_SecondsForGrowth = SecondsForGrowth;
        state = EWavelingState.Dying;
    }
}
