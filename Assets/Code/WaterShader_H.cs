
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterShader_H : MonoBehaviour {
    public List<string> shaderGetTexScrollParams = new List<string>();
    public string shaderGetWaveSpeedMult;
    public string shaderGiveTexScrollUVs;

    int NB_Scrollers;

    Material shader;

    //Fetched from Shader
    TexScrollerInfo[] texScrollerInfos;
    float WaveSpeedMult;

    //Sent to shader every frame
    float[] S_TextureCoordinates;


    /*
        Weird stuff explained: Delayed Start. On  some projects this script refuses to execute if it isnt loaded after WaterCollision. No matter if WaterCollision is enable or even there.
    */

    IEnumerator delayedStart()
    {
        yield return new WaitForSeconds(.3f);
        StartDelayed();
    }
    void Start()
    {

        StartCoroutine(delayedStart());
        GetComponent<WaterShader_H>().enabled = false;
    }

	void StartDelayed () {
        GetComponent<WaterShader_H>().enabled = true;
        shader = GetComponent<Renderer>().sharedMaterial;
        NB_Scrollers = shaderGetTexScrollParams.Count;
        S_TextureCoordinates = new float[2 * NB_Scrollers];
        WaveSpeedMult = shader.GetFloat(shaderGetWaveSpeedMult);
        texScrollerInfos = new TexScrollerInfo[NB_Scrollers];
        for (int i = 0; i < NB_Scrollers; i++)
        {
            texScrollerInfos[i] = new TexScrollerInfo(shader.GetVector(shaderGetTexScrollParams[i]), WaveSpeedMult);
        } 
    }
	
	// Update is called once per frame
	void Update () {
       // if (!started) { return; }
        for (int i = 0; i < NB_Scrollers; i++)
        {
            texScrollerInfos[i].Pan(Time.deltaTime * WaveSpeedMult);
            S_TextureCoordinates[i * 2] = texScrollerInfos[i].uvCoordinates.x;
            S_TextureCoordinates[(i * 2) +1] = texScrollerInfos[i].uvCoordinates.y; 
            //Debug.Log("> "+S_TextureCoordinates[0])
        }
       
        shader.SetFloatArray(shaderGiveTexScrollUVs, S_TextureCoordinates);
       
    }




    struct TexScrollerInfo
    {
        public Vector2 uvCoordinates;
        Vector2 Direction;
        float Speed;

        public TexScrollerInfo(Vector4 waterShader_texInfo, float overallWaveSpeedMult)
        {
            Direction = new Vector2(Mathf.Cos(waterShader_texInfo.x * Mathf.Deg2Rad), Mathf.Sin(waterShader_texInfo.x * Mathf.Deg2Rad));
            Speed = waterShader_texInfo.y;
            Direction *= Speed * overallWaveSpeedMult; //smash it into so we !recalculate every time 
            uvCoordinates = new Vector2();
        }

        public void Pan(float deltaTime)
        {
            uvCoordinates += deltaTime * Direction;
            uvCoordinates.x %= 1f;
            uvCoordinates.y %= 1f;
        }
    };

}
