using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MovingObjectCreator : MonoBehaviour {
    public List<MovingObject> calmBeachPeople;
    public List<MovingObject> mediumBeachPeople;
    public List<MovingObject> strongBeachPeople;

    public List<MovingObject> calmUnderSeaPeople;
    public List<MovingObject> mediumUnderSeaPeople;
    public List<MovingObject> strongUnderSeaPeople;


    public List<MovingObject> skyPeople;
    public List<MovingObject> intoSeaPeople;

    public GameObject beachPeoplePrefab;
    public GameObject underPeoplePrefab;
    public GameObject skyPeoplePrefab;
    public GameObject intoSeaPrefab;

    public Sprite[] calmBeachSprites;
    public Sprite[] intoSeaSprites;


    public int maxPeople;

    // Use this for initialization
    void Start () {
        //calmBeachSprites = (Sprite[]) Resources.LoadAll("Sprites/Beach");
        intoSeaSprites = Resources.LoadAll("Sprites/IntoSea", typeof(Sprite)).Cast<Sprite>().ToArray();
        intoSeaPrefab = Resources.Load("Prefabs/IntoSeaPrefab") as GameObject;
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void CreateObject(Type type, Schedule schedule)
    {
        Vector3 positionMin, positionMax, position;
        GameObject newObject;
        switch (type)
        {
            case Type.CalmBeach:
                positionMin = GameObject.Find("Beach").GetComponent<BoxCollider2D>().bounds.min;
                positionMax = GameObject.Find("Beach").GetComponent<BoxCollider2D>().bounds.max;
                position = new Vector3(Random.Range(positionMin.x, positionMax.y), Random.Range(positionMax.x, positionMax.y));
                newObject = Instantiate(beachPeoplePrefab, position, Quaternion.identity) as GameObject;
                newObject.GetComponent<SpriteRenderer>().sprite = (Sprite)calmBeachSprites[0];
                //MovingObject newBeachBehaviour = newBeach.GetComponent<MovingObject>();
            break;
            case Type.IntoSea:
                positionMin = GameObject.Find("IntoSea").GetComponent<BoxCollider2D>().bounds.min;
                positionMax = GameObject.Find("IntoSea").GetComponent<BoxCollider2D>().bounds.max;
                position = new Vector3(Random.Range(positionMin.x, positionMax.x), Random.Range(positionMax.y, positionMax.y));
                newObject = Instantiate(intoSeaPrefab, position, Quaternion.identity) as GameObject;
                newObject.GetComponent<SpriteRenderer>().sprite = (Sprite) intoSeaSprites[Random.Range(0, intoSeaSprites.Length - 1)];
                break;
        }
    }
}
