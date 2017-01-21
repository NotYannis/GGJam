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

    public GameObject beachPrefab;
    public GameObject underSeaPrefab;
    public GameObject skyPrefab;
    public GameObject intoSeaPrefab;

    public Sprite[] dayCalmBeachSprites;
    public Sprite[] dayMediumBeachSprites;
    public Sprite[] dayStrongBeachSprites;
    public Sprite[] dayBeachSprites;
    public Sprite[] nightCalmBeachSprites;
    public Sprite[] nightMediumBeachSprites;
    public Sprite[] nightStrongBeachSprites;
    public Sprite[] nightBeachSprites;
    public Sprite[] dayUnderSeaSprites;
    public Sprite[] dayCalmUnderSeaSprites;
    public Sprite[] dayMediumUnderSeaSprites;
    public Sprite[] dayStrongUnderSeaSprites;
    public Sprite[] nightUnderSeaSprites;
    public Sprite[] nightCalmUnderSeaSprites;
    public Sprite[] nightMediumUnderSeaSprites;
    public Sprite[] nightStrongUnderSeaSprites;
    public Sprite[] dayIntoSeaSprites;
    public Sprite[] nightIntoSeaSprites;
    public Sprite[] daySkySprites;
    public Sprite[] nightSkySprites;

    public int maxPeople;

    // Use this for initialization
    void Awake () {
        dayBeachSprites = Resources.LoadAll("Sprites/Day/Beach", typeof(Sprite)).Cast<Sprite>().ToArray();
        dayCalmBeachSprites = Resources.LoadAll("Sprites/Day/Beach/Calm", typeof(Sprite)).Cast<Sprite>().ToArray();
        dayMediumBeachSprites = Resources.LoadAll("Sprites/Day/Beach/Medium", typeof(Sprite)).Cast<Sprite>().ToArray();
        dayStrongBeachSprites = Resources.LoadAll("Sprites/Day/Beach/Strong", typeof(Sprite)).Cast<Sprite>().ToArray();

        nightBeachSprites = Resources.LoadAll("Sprites/Night/Beach", typeof(Sprite)).Cast<Sprite>().ToArray();
        nightCalmBeachSprites = Resources.LoadAll("Sprites/Night/Beach/Calm", typeof(Sprite)).Cast<Sprite>().ToArray();
        nightMediumBeachSprites = Resources.LoadAll("Sprites/Night/Beach/Medium", typeof(Sprite)).Cast<Sprite>().ToArray();
        nightStrongBeachSprites = Resources.LoadAll("Sprites/Night/Beach/Strong", typeof(Sprite)).Cast<Sprite>().ToArray();

        dayUnderSeaSprites = Resources.LoadAll("Sprites/Day/UnderSea", typeof(Sprite)).Cast<Sprite>().ToArray();
        dayCalmUnderSeaSprites = Resources.LoadAll("Sprites/Day/UnderSea/Calm", typeof(Sprite)).Cast<Sprite>().ToArray();
        dayMediumUnderSeaSprites = Resources.LoadAll("Sprites/Day/UnderSea/Medium", typeof(Sprite)).Cast<Sprite>().ToArray();
        dayStrongUnderSeaSprites = Resources.LoadAll("Sprites/Day/UnderSea/Strong", typeof(Sprite)).Cast<Sprite>().ToArray();

        nightUnderSeaSprites = Resources.LoadAll("Sprites/Night/UnderSea", typeof(Sprite)).Cast<Sprite>().ToArray();
        nightCalmUnderSeaSprites = Resources.LoadAll("Sprites/Night/UnderSea/Calm", typeof(Sprite)).Cast<Sprite>().ToArray();
        nightMediumUnderSeaSprites = Resources.LoadAll("Sprites/Night/UnderSea/Medium", typeof(Sprite)).Cast<Sprite>().ToArray();
        nightStrongUnderSeaSprites = Resources.LoadAll("Sprites/Night/UnderSea/Strong", typeof(Sprite)).Cast<Sprite>().ToArray();

        dayIntoSeaSprites = Resources.LoadAll("Sprites/Day/IntoSea", typeof(Sprite)).Cast<Sprite>().ToArray();
        nightIntoSeaSprites = Resources.LoadAll("Sprites/Night/IntoSea", typeof(Sprite)).Cast<Sprite>().ToArray();

        daySkySprites = Resources.LoadAll("Sprites/Day/Sky", typeof(Sprite)).Cast<Sprite>().ToArray();
        nightSkySprites = Resources.LoadAll("Sprites/Night/Sky", typeof(Sprite)).Cast<Sprite>().ToArray();

        intoSeaPrefab = Resources.Load("Prefabs/IntoSeaPrefab") as GameObject;
        skyPrefab = Resources.Load("Prefabs/SkyPrefab") as GameObject;
        underSeaPrefab = Resources.Load("Prefabs/UnderSeaPrefab") as GameObject;
        beachPrefab = Resources.Load("Prefabs/BeachPrefab") as GameObject;
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
            case Type.MediumBeach:
            case Type.StrongBeach:
                positionMin = GameObject.Find("Beach").GetComponent<BoxCollider2D>().bounds.min;
                positionMax = GameObject.Find("Beach").GetComponent<BoxCollider2D>().bounds.max;
                position = new Vector3(Random.Range(positionMin.x, positionMax.y), Random.Range(positionMax.x, positionMax.y));
                newObject = Instantiate(beachPrefab, position, Quaternion.identity) as GameObject;
                newObject.GetComponent<SpriteRenderer>().sprite = GetSprite(type, schedule);
                break;
            case Type.CalmUnderSea:
            case Type.MediumUnderSea:
            case Type.StrongUnderSea:
                positionMin = GameObject.Find("UnderSea").GetComponent<BoxCollider2D>().bounds.min;
                positionMax = GameObject.Find("UnderSea").GetComponent<BoxCollider2D>().bounds.max;
                position = new Vector3(Random.Range(positionMin.x, positionMax.y), Random.Range(positionMax.x, positionMax.y));
                newObject = Instantiate(underSeaPrefab, position, Quaternion.identity) as GameObject;
                newObject.GetComponent<SpriteRenderer>().sprite = GetSprite(type, schedule);
                break;
            case Type.IntoSea:
                positionMin = GameObject.Find("IntoSea").GetComponent<BoxCollider2D>().bounds.min;
                positionMax = GameObject.Find("IntoSea").GetComponent<BoxCollider2D>().bounds.max;
                position = new Vector3(Random.Range(positionMin.x, positionMax.x), Random.Range(positionMax.y, positionMax.y));
                newObject = Instantiate(intoSeaPrefab, position, Quaternion.identity) as GameObject;
                newObject.GetComponent<SpriteRenderer>().sprite = GetSprite(type, schedule);
                break;
            case Type.Sky:
                positionMin = GameObject.Find("Sky").GetComponent<BoxCollider2D>().bounds.min;
                positionMax = GameObject.Find("Sky").GetComponent<BoxCollider2D>().bounds.max;
                position = new Vector3(Random.Range(positionMin.x, positionMax.x), Random.Range(positionMax.y, positionMax.y));
                newObject = Instantiate(skyPrefab, position, Quaternion.identity) as GameObject;
                newObject.GetComponent<SpriteRenderer>().sprite = GetSprite(type, schedule);
                break;
        }
    }

    public Sprite GetSprite(Type type, Schedule schedule)
    {
        Sprite newSprite = new Sprite();
        if(schedule == Schedule.Day)
        {
            switch (type)
            {
                case Type.CalmBeach:
                    newSprite = dayCalmBeachSprites[Random.Range(0, dayCalmBeachSprites.Length)];
                break;
                case Type.MediumBeach:
                    newSprite = dayMediumBeachSprites[Random.Range(0, dayMediumBeachSprites.Length)];
                    break;
                case Type.StrongBeach:
                    newSprite = dayStrongBeachSprites[Random.Range(0, dayStrongBeachSprites.Length)];
                    break;
                case Type.CalmUnderSea:
                    newSprite = dayCalmUnderSeaSprites[Random.Range(0, dayCalmUnderSeaSprites.Length)];
                    break;
                case Type.MediumUnderSea:
                    newSprite = dayMediumUnderSeaSprites[Random.Range(0, dayMediumUnderSeaSprites.Length)];
                    break;
                case Type.StrongUnderSea:
                    newSprite = dayStrongUnderSeaSprites[Random.Range(0, dayStrongUnderSeaSprites.Length)];
                    break;
                case Type.IntoSea:
                    newSprite = dayIntoSeaSprites[Random.Range(0, dayIntoSeaSprites.Length)];
                    break;
                case Type.Sky:
                    newSprite = daySkySprites[Random.Range(0, daySkySprites.Length)];
                    break;
            }
        }
        else
        {
            switch (type)
            {
                case Type.CalmBeach:
                    newSprite = nightCalmBeachSprites[Random.Range(0, nightCalmBeachSprites.Length)];
                    break;
                case Type.MediumBeach:
                    newSprite = nightMediumBeachSprites[Random.Range(0, nightMediumBeachSprites.Length)];
                    break;
                case Type.StrongBeach:
                    newSprite = nightStrongBeachSprites[Random.Range(0, nightStrongBeachSprites.Length)];
                    break;
                case Type.CalmUnderSea:
                    newSprite = nightCalmUnderSeaSprites[Random.Range(0, nightCalmUnderSeaSprites.Length)];
                    break;
                case Type.MediumUnderSea:
                    newSprite = nightMediumUnderSeaSprites[Random.Range(0, nightMediumUnderSeaSprites.Length)];
                    break;
                case Type.StrongUnderSea:
                    newSprite = nightStrongUnderSeaSprites[Random.Range(0, nightStrongUnderSeaSprites.Length)];
                    break;
                case Type.IntoSea:
                    newSprite = nightIntoSeaSprites[Random.Range(0, nightIntoSeaSprites.Length)];
                    break;
                case Type.Sky:
                    newSprite = nightSkySprites[Random.Range(0, nightSkySprites.Length)];
                    break;
            }
        }
        return newSprite;
    }
}
