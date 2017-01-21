using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MovingObjectCreator : MonoBehaviour {
    public List<MovingObject> beachPeople;
    public List<MovingObject> underSeaPeople;
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
        GameObject newObject = null;// = new GameObject();
        
        switch (type)
        {
            case Type.CalmBeach:
            case Type.MediumBeach:
            case Type.StrongBeach:
                newObject = Instantiate(beachPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                break;
            case Type.CalmUnderSea:
            case Type.MediumUnderSea:
            case Type.StrongUnderSea:
                newObject = Instantiate(underSeaPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                break;
            case Type.IntoSea:
                newObject = Instantiate(intoSeaPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                break;
            case Type.Sky:
                newObject = Instantiate(skyPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                break;
        }

        MovingObject newObjMove = newObject.GetComponent<MovingObject>();
        Debug.Log(newObjMove);
        positionMin = newObjMove.bound.min;
        positionMax = newObjMove.bound.max;

        position = new Vector3(Random.Range(positionMin.x, positionMax.x), Random.Range(positionMin.y, positionMax.y));
        if(type == Type.Sky)
        {
            position.x = 9.5f;
        }
        newObject.transform.position = position;
        newObjMove.type = type;
        newObjMove.schedule = schedule;
        newObject.GetComponent<SpriteRenderer>().sprite = GetSprite(type, schedule);

        switch (type)
        {
            case Type.CalmBeach:
            case Type.MediumBeach:
            case Type.StrongBeach:
                if(beachPeople.Count > maxPeople)
                {
                    Destroy(beachPeople[0].gameObject);
                    beachPeople.RemoveAt(0);
                }
                beachPeople.Add(newObject.GetComponent<MovingObject>());
                break;
            case Type.CalmUnderSea:
            case Type.MediumUnderSea:
            case Type.StrongUnderSea:
                if (underSeaPeople.Count > maxPeople)
                {
                    Destroy(underSeaPeople[0].gameObject);
                    underSeaPeople.RemoveAt(0);
                }
                underSeaPeople.Add(newObject.GetComponent<MovingObject>());
                break;
            case Type.IntoSea:
                if (intoSeaPeople.Count > maxPeople)
                {
                    Destroy(intoSeaPeople[0].gameObject);
                    intoSeaPeople.RemoveAt(0);
                }
                intoSeaPeople.Add(newObject.GetComponent<MovingObject>());
                break;
            case Type.Sky:
                if (skyPeople.Count > maxPeople)
                {
                    Destroy(skyPeople[0].gameObject);
                    skyPeople.RemoveAt(0);
                }
                skyPeople.Add(newObject.GetComponent<MovingObject>());
                break;
        }
    }

    public void DeleteObject(Type type, Schedule schedule)
    {
        switch(type){
            case Type.CalmBeach:
            case Type.MediumBeach:
            case Type.StrongBeach:
                for(int i = 0; i < beachPeople.Count; ++i)
                {
                    if(beachPeople[i].type == type && beachPeople[i].schedule == schedule)
                    {
                        Destroy(beachPeople[i].gameObject);
                        beachPeople.RemoveAt(0);
                    }
                }
                break;
            case Type.CalmUnderSea:
            case Type.MediumUnderSea:
            case Type.StrongUnderSea:
                for (int i = 0; i < underSeaPeople.Count; ++i)
                {
                    if (underSeaPeople[i].type == type && underSeaPeople[i].schedule == schedule)
                    {
                        Destroy(underSeaPeople[i].gameObject);
                        underSeaPeople.RemoveAt(0);
                    }
                }
                break;
            case Type.IntoSea:
                for (int i = 0; i < intoSeaPeople.Count; ++i)
                {
                    if (intoSeaPeople[i].schedule == schedule)
                    {
                        Destroy(intoSeaPeople[i].gameObject);
                        intoSeaPeople.RemoveAt(0);
                    }
                }
                break;
            case Type.Sky:
                for (int i = 0; i < skyPeople.Count; ++i)
                {
                    if (skyPeople[i].schedule == schedule)
                    {
                        Destroy(skyPeople[i].gameObject);
                        skyPeople.RemoveAt(0);
                    }
                }
                break;
        }
    }

   public void DeleteParticularObject(MovingObject objectToDestroy)
    {
        Debug.Log(objectToDestroy);
        switch (objectToDestroy.type)
        {
            case Type.CalmBeach:
            case Type.MediumBeach:
            case Type.StrongBeach:
                Destroy(beachPeople[beachPeople.IndexOf(objectToDestroy)].gameObject);
                beachPeople.Remove(objectToDestroy);
                break;
            case Type.CalmUnderSea:
            case Type.MediumUnderSea:
            case Type.StrongUnderSea:
                Destroy(underSeaPeople[underSeaPeople.IndexOf(objectToDestroy)].gameObject);
                underSeaPeople.Remove(objectToDestroy);
                break;
            case Type.IntoSea:
                Destroy(intoSeaPeople[intoSeaPeople.IndexOf(objectToDestroy)].gameObject);
                intoSeaPeople.Remove(objectToDestroy);
                break;
            case Type.Sky:
                Destroy(skyPeople[skyPeople.IndexOf(objectToDestroy)].gameObject);
                skyPeople.Remove(objectToDestroy);
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
                    newSprite = dayCalmBeachSprites[Random.Range(0, dayCalmBeachSprites.Length - 1)];
                break;
                case Type.MediumBeach:
                    newSprite = dayMediumBeachSprites[Random.Range(0, dayMediumBeachSprites.Length - 1)];
                    break;
                case Type.StrongBeach:
                    newSprite = dayStrongBeachSprites[Random.Range(0, dayStrongBeachSprites.Length - 1)];
                    break;
                case Type.CalmUnderSea:
                    newSprite = dayCalmUnderSeaSprites[Random.Range(0, dayCalmUnderSeaSprites.Length - 1)];
                    break;
                case Type.MediumUnderSea:
                    newSprite = dayMediumUnderSeaSprites[Random.Range(0, dayMediumUnderSeaSprites.Length - 1)];
                    break;
                case Type.StrongUnderSea:
                    newSprite = dayStrongUnderSeaSprites[Random.Range(0, dayStrongUnderSeaSprites.Length - 1)];
                    break;
                case Type.IntoSea:
                    newSprite = dayIntoSeaSprites[Random.Range(0, dayIntoSeaSprites.Length - 1)];
                    break;
                case Type.Sky:
                    newSprite = daySkySprites[Random.Range(0, daySkySprites.Length - 1)];
                    break;
            }
        }
        else
        {
            switch (type)
            {
                case Type.CalmBeach:
                    newSprite = nightCalmBeachSprites[Random.Range(0, nightCalmBeachSprites.Length - 1)];
                    break;
                case Type.MediumBeach:
                    newSprite = nightMediumBeachSprites[Random.Range(0, nightMediumBeachSprites.Length - 1)];
                    break;
                case Type.StrongBeach:
                    newSprite = nightStrongBeachSprites[Random.Range(0, nightStrongBeachSprites.Length - 1)];
                    break;
                case Type.CalmUnderSea:
                    newSprite = nightCalmUnderSeaSprites[Random.Range(0, nightCalmUnderSeaSprites.Length - 1)];
                    break;
                case Type.MediumUnderSea:
                    newSprite = nightMediumUnderSeaSprites[Random.Range(0, nightMediumUnderSeaSprites.Length - 1)];
                    break;
                case Type.StrongUnderSea:
                    newSprite = nightStrongUnderSeaSprites[Random.Range(0, nightStrongUnderSeaSprites.Length - 1)];
                    break;
                case Type.IntoSea:
                    newSprite = nightIntoSeaSprites[Random.Range(0, nightIntoSeaSprites.Length - 1)];
                    break;
                case Type.Sky:
                    newSprite = nightSkySprites[Random.Range(0, nightSkySprites.Length - 1)];
                    break;
            }
        }
        return newSprite;
    }
}
