﻿using UnityEngine;
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

    public RuntimeAnimatorController[] dayCalmBeachSprites;
    public RuntimeAnimatorController[] dayMediumBeachSprites;
    public RuntimeAnimatorController[] dayStrongBeachSprites;
    public RuntimeAnimatorController[] dayBeachSprites;

    public RuntimeAnimatorController[] nightCalmBeachSprites;
    public RuntimeAnimatorController[] nightMediumBeachSprites;
    public RuntimeAnimatorController[] nightStrongBeachSprites;
    public RuntimeAnimatorController[] nightBeachSprites;

    public RuntimeAnimatorController[] dayUnderSeaSprites;
    public RuntimeAnimatorController[] dayCalmUnderSeaSprites;
    public RuntimeAnimatorController[] dayMediumUnderSeaSprites;
    public RuntimeAnimatorController[] dayStrongUnderSeaSprites;

    public RuntimeAnimatorController[] nightUnderSeaSprites;
    public RuntimeAnimatorController[] nightCalmUnderSeaSprites;
    public RuntimeAnimatorController[] nightMediumUnderSeaSprites;
    public RuntimeAnimatorController[] nightStrongUnderSeaSprites;

    public RuntimeAnimatorController[] dayIntoSeaSprites;
    public RuntimeAnimatorController[] nightIntoSeaSprites;

    public RuntimeAnimatorController[] daySkySprites;
    public RuntimeAnimatorController[] nightSkySprites;

    public int maxPeople;

    // Use this for initialization
    void Awake () {
        dayBeachSprites = Resources.LoadAll("Sprites/Day/Beach/Plate", typeof(RuntimeAnimatorController)).Cast<RuntimeAnimatorController>().ToArray();
        dayCalmBeachSprites = Resources.LoadAll("Sprites/Day/Beach/Calm", typeof(RuntimeAnimatorController)).Cast<RuntimeAnimatorController>().ToArray();
        dayMediumBeachSprites = Resources.LoadAll("Sprites/Day/Beach/Medium", typeof(RuntimeAnimatorController)).Cast<RuntimeAnimatorController>().ToArray();
        dayStrongBeachSprites = Resources.LoadAll("Sprites/Day/Beach/Strong", typeof(RuntimeAnimatorController)).Cast<RuntimeAnimatorController>().ToArray();

        nightBeachSprites = Resources.LoadAll("Sprites/Day/Beach/Plate", typeof(RuntimeAnimatorController)).Cast<RuntimeAnimatorController>().ToArray();
        nightCalmBeachSprites = Resources.LoadAll("Sprites/Night/Beach/Calm", typeof(RuntimeAnimatorController)).Cast<RuntimeAnimatorController>().ToArray();
        nightMediumBeachSprites = Resources.LoadAll("Sprites/Night/Beach/Medium", typeof(RuntimeAnimatorController)).Cast<RuntimeAnimatorController>().ToArray();
        nightStrongBeachSprites = Resources.LoadAll("Sprites/Night/Beach/Strong", typeof(RuntimeAnimatorController)).Cast<RuntimeAnimatorController>().ToArray();

        dayUnderSeaSprites = Resources.LoadAll("Sprites/Day/UnderSea/Plate", typeof(RuntimeAnimatorController)).Cast<RuntimeAnimatorController>().ToArray();
        dayCalmUnderSeaSprites = Resources.LoadAll("Sprites/Day/UnderSea/Calm", typeof(RuntimeAnimatorController)).Cast<RuntimeAnimatorController>().ToArray();
        dayMediumUnderSeaSprites = Resources.LoadAll("Sprites/Day/UnderSea/Medium", typeof(RuntimeAnimatorController)).Cast<RuntimeAnimatorController>().ToArray();
        dayStrongUnderSeaSprites = Resources.LoadAll("Sprites/Day/UnderSea/Strong", typeof(RuntimeAnimatorController)).Cast<RuntimeAnimatorController>().ToArray();

        nightUnderSeaSprites = Resources.LoadAll("Sprites/Night/UnderSea/Plate", typeof(RuntimeAnimatorController)).Cast<RuntimeAnimatorController>().ToArray();
        nightCalmUnderSeaSprites = Resources.LoadAll("Sprites/Night/UnderSea/Calm", typeof(RuntimeAnimatorController)).Cast<RuntimeAnimatorController>().ToArray();
        nightMediumUnderSeaSprites = Resources.LoadAll("Sprites/Night/UnderSea/Medium", typeof(RuntimeAnimatorController)).Cast<RuntimeAnimatorController>().ToArray();
        nightStrongUnderSeaSprites = Resources.LoadAll("Sprites/Night/UnderSea/Strong", typeof(RuntimeAnimatorController)).Cast<RuntimeAnimatorController>().ToArray();

        dayIntoSeaSprites = Resources.LoadAll("Sprites/Day/IntoSea", typeof(RuntimeAnimatorController)).Cast<RuntimeAnimatorController>().ToArray();
        nightIntoSeaSprites = Resources.LoadAll("Sprites/Night/IntoSea", typeof(RuntimeAnimatorController)).Cast<RuntimeAnimatorController>().ToArray();

        daySkySprites = Resources.LoadAll("Sprites/Day/Sky", typeof(RuntimeAnimatorController)).Cast<RuntimeAnimatorController>().ToArray();
        nightSkySprites = Resources.LoadAll("Sprites/Night/Sky", typeof(RuntimeAnimatorController)).Cast<RuntimeAnimatorController>().ToArray();

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
        GameObject newObject = null;
        
        switch(type)
        {
            case Type.PlateBeach:
            case Type.CalmBeach:
            case Type.MediumBeach:
            case Type.StrongBeach:
                newObject = Instantiate(beachPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                break;
            case Type.PlateUnderSea:
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
        newObject.GetComponent<Animator>().runtimeAnimatorController = GetAnimator(type, schedule);
        
        switch (type)
        {
            case Type.PlateBeach:
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
            case Type.PlateUnderSea:
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
            case Type.PlateBeach:
            case Type.CalmBeach:
            case Type.MediumBeach:
            case Type.StrongBeach:
                for(int i = 0; i < beachPeople.Count; ++i)
                {
                    if(beachPeople[i].type == type && beachPeople[i].schedule == schedule)
                    {
                        Destroy(beachPeople[i].gameObject);
                        beachPeople.RemoveAt(i);
                        return;
                    }
                }
                break;
            case Type.PlateUnderSea:
            case Type.CalmUnderSea:
            case Type.MediumUnderSea:
            case Type.StrongUnderSea:
                for (int i = 0; i < underSeaPeople.Count; ++i)
                {
                    if (underSeaPeople[i].type == type && underSeaPeople[i].schedule == schedule)
                    {
                        Destroy(underSeaPeople[i].gameObject);
                        underSeaPeople.RemoveAt(i);
                        return;
                    }
                }
                break;
            case Type.IntoSea:
                for (int i = 0; i < intoSeaPeople.Count; ++i)
                {
                    if (intoSeaPeople[i].schedule == schedule)
                    {
                        Destroy(intoSeaPeople[i].gameObject);
                        intoSeaPeople.RemoveAt(i);
                        return;
                    }
                }
                break;
            case Type.Sky:
                for (int i = 0; i < skyPeople.Count; ++i)
                {
                    if (skyPeople[i].schedule == schedule)
                    {
                        Destroy(skyPeople[i].gameObject);
                        skyPeople.RemoveAt(i);
                        return;
                    }
                }
                break;
        }
    }

   public void DeleteParticularObject(MovingObject objectToDestroy)
    {
        switch (objectToDestroy.type)
        {
            case Type.PlateBeach:
            case Type.CalmBeach:
            case Type.MediumBeach:
            case Type.StrongBeach:
                Destroy(beachPeople[beachPeople.IndexOf(objectToDestroy)].gameObject);
                beachPeople.Remove(objectToDestroy);
                break;
            case Type.PlateUnderSea:
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

    public RuntimeAnimatorController GetAnimator(Type type, Schedule schedule)
    {
        RuntimeAnimatorController newSprite = new RuntimeAnimatorController();
        if(schedule == Schedule.Day)
        {
            switch (type)
            {
                case Type.PlateBeach:
                    newSprite = dayBeachSprites[Random.Range(0, dayBeachSprites.Length)];
                    break;
                case Type.CalmBeach:
                    newSprite = dayCalmBeachSprites[Random.Range(0, dayCalmBeachSprites.Length)];
                break;
                case Type.MediumBeach:
                    newSprite = dayMediumBeachSprites[Random.Range(0, dayMediumBeachSprites.Length )];
                    break;
                case Type.StrongBeach:
                    newSprite = dayStrongBeachSprites[Random.Range(0, dayStrongBeachSprites.Length )];
                    break;
                case Type.PlateUnderSea:
                    newSprite = dayUnderSeaSprites[Random.Range(0, dayUnderSeaSprites.Length)];
                    break;
                case Type.CalmUnderSea:
                    newSprite = dayCalmUnderSeaSprites[Random.Range(0, dayCalmUnderSeaSprites.Length )];
                    break;
                case Type.MediumUnderSea:
                    newSprite = dayMediumUnderSeaSprites[Random.Range(0, dayMediumUnderSeaSprites.Length )];
                    break;
                case Type.StrongUnderSea:
                    newSprite = dayStrongUnderSeaSprites[Random.Range(0, dayStrongUnderSeaSprites.Length )];
                    break;
                case Type.IntoSea:
                    newSprite = dayIntoSeaSprites[Random.Range(0, dayIntoSeaSprites.Length )];
                    break;
                case Type.Sky:
                    newSprite = daySkySprites[Random.Range(0, daySkySprites.Length )];
                    break;
            }
        }
        else
        {
            switch (type)
            {
                case Type.PlateBeach:
                    newSprite = nightBeachSprites[Random.Range(0, nightBeachSprites.Length)];
                    break;
                case Type.CalmBeach:
                    newSprite = nightCalmBeachSprites[Random.Range(0, nightCalmBeachSprites.Length )];
                    break;
                case Type.MediumBeach:
                    newSprite = nightMediumBeachSprites[Random.Range(0, nightMediumBeachSprites.Length )];
                    break;
                case Type.StrongBeach:
                    newSprite = nightStrongBeachSprites[Random.Range(0, nightStrongBeachSprites.Length )];
                    break;
                case Type.PlateUnderSea:
                    newSprite = nightUnderSeaSprites[Random.Range(0, nightUnderSeaSprites.Length)];
                    break;
                case Type.CalmUnderSea:
                    newSprite = nightCalmUnderSeaSprites[Random.Range(0, nightCalmUnderSeaSprites.Length )];
                    break;
                case Type.MediumUnderSea:
                    newSprite = nightMediumUnderSeaSprites[Random.Range(0, nightMediumUnderSeaSprites.Length )];
                    break;
                case Type.StrongUnderSea:
                    newSprite = nightStrongUnderSeaSprites[Random.Range(0, nightStrongUnderSeaSprites.Length )];
                    break;
                case Type.IntoSea:
                    newSprite = nightIntoSeaSprites[Random.Range(0, nightIntoSeaSprites.Length )];
                    break;
                case Type.Sky:
                    newSprite = nightSkySprites[Random.Range(0, nightSkySprites.Length )];
                    break;
            }
        }
        return newSprite;
    }

    public void DestroyEveryone()
    {
        Debug.Log("kikoo");
        int i;
        for(i = 0; i < beachPeople.Count; ++i)
        {
            Destroy(beachPeople[i].gameObject);
        }
        for (i = 0; i < underSeaPeople.Count; ++i)
        {
            Destroy(underSeaPeople[i].gameObject);
        }
        for (i = 0; i < skyPeople.Count; ++i)
        {
            Destroy(skyPeople[i].gameObject);
        }
        for (i = 0; i < intoSeaPeople.Count; ++i)
        {
            Destroy(intoSeaPeople[i].gameObject);
        }
        beachPeople.Clear();
        underSeaPeople.Clear();
        skyPeople.Clear();
        intoSeaPeople.Clear();
    }
}
