﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Каждое море может обладать своими уникальными параметрами
// генерироваться компьютером
// или же вручную (например, когда функция MakeLive загружает xml файл и расставляет объекты).
// Если же это генерация, то MakeLive - генерирует объекты

public class SimpleSea : MonoBehaviour, ISea
{
    public float minFishDeviation;

    public bool isUseDeviationOnMove = true; // для теста, проверить какой тип движения лучше

    public float width = 10.0f;

    public float Width
    {
        get
        {
            return this.width;
        }
    }

    public float depth = 6.0f;

    public float Depth
    {
        get
        {
            return this.depth;
        }
    }

    Vector3 center;

    public Vector3 Center
    {
        get
        {
            return this.center;
        }
    }

    public float flowHorizontal = 0.0f;

    public float FlowHorizontal
    {
        get
        {
            return this.flowHorizontal;
        }
    }

    public float flowVertical = 0.0f;

    public float FlowVertical
    {
        get
        {
            return this.flowVertical;
        }
    }

    public int totalFishAmount = 10;

    int numOfFishes = 0;

    public GameObject[] fishes;

    public int totalStarAmount = 10;

    int numOfStars = 0;

    public GameObject[] stars;

    public int totalSurpriseAmount = 10;

    int numOfSurprise = 0;

    public GameObject[] surprises;

    public GameObject bomb;
    public GameObject treasure;
    public GameObject folliage;
    List<GameObject> folliageObjects = new List<GameObject>();

    int necessaryCostOfFish = 0;
    int currentCostOfFish = 0;

    public void MakeLive(int param = 0, object data = null)
    {
        necessaryCostOfFish = (10 * param * param + 125 * param) - (10 * (param - 1) * (param - 1) + 125 * (param - 1)) + 50;

        //this.FishGen();
        //this.StarGen();
        //BombGen();
        //this.SurpriseGen ();       

        FishGenNew(param);
        StarGenNew(param);
        BombGenNew(param);
        SurpriceGenNew(param);
        TreasureGen(param);
        FolliageGen();
    }

    HashSet<ICatchable> createdCatchableObjects = new HashSet<ICatchable>();

    void FishGen()
    {
        for (int i = 0; i < this.totalFishAmount; i++)
        {
            GameObject newFish = GameObject.Instantiate(this.fishes[Random.Range(0, this.numOfFishes)]);

            newFish.transform.position = new Vector3(
                Random.Range(this.center.x - this.width / 2, this.center.x + this.width / 2),
                Random.Range(this.center.y - this.depth / 2, this.center.y + this.depth / 2),
                newFish.transform.position.z
            );

            ICatchable newCatchable = newFish.GetComponent<ICatchable>();

            newFish.GetComponent<Fish>().isDeviation = isUseDeviationOnMove;
            newCatchable.Sea = this;
            newCatchable.SetAction("SimpleFishBehaviour");

            this.createdCatchableObjects.Add(newCatchable);

            newCatchable.OnUsed += (ICatchable obj) =>
            {
                this.createdCatchableObjects.Remove(obj);
                GameObject.Destroy(obj.GameObject);
            };

            newFish.SetActive(true);
        }

    }
    void StarGen()
    {
        for (int i = 0; i < this.totalStarAmount; i++)
        {
            GameObject newStar = GameObject.Instantiate(this.stars[Random.Range(0, this.numOfStars)]);

            newStar.transform.position = new Vector3(
                Random.Range(this.center.x - this.width / 2, this.center.x + this.width / 2),
                Random.Range(this.center.y - this.depth / 4, this.center.y - this.depth / 2),
                newStar.transform.position.z
            );

            ICatchable newCatchable = newStar.GetComponent<ICatchable>();
            newCatchable.Sea = this;
            newCatchable.SetAction("SimpleStarBehaviour");

            this.createdCatchableObjects.Add(newCatchable);

            newCatchable.OnUsed += (ICatchable obj) =>
            {
                this.createdCatchableObjects.Remove(obj);
                GameObject.Destroy(obj.GameObject);
            };

            newStar.SetActive(true);
        }

    }
    void BombGen()
    {
        int totalBombAmount = Random.Range(3, 7);
        for (int i = 0; i < totalBombAmount; i++)
        {
            GameObject newBomb = Instantiate(bomb);

            newBomb.transform.position = new Vector3(
                Random.Range(this.center.x - this.width / 2, this.center.x + this.width / 2),
                Random.Range(this.center.y - this.depth / 2, this.center.y + this.depth / 2),
                newBomb.transform.position.z
            );

            ICatchable newCatchable = newBomb.GetComponent<ICatchable>();

            newCatchable.Sea = this;
            newCatchable.SetAction("SimpleBombBehaviour");
            createdCatchableObjects.Add(newCatchable);
        }
    }
    void SurpriseGen()
    {
        for (int i = 0; i < this.totalSurpriseAmount; i++)
        {
            GameObject newSurprise = GameObject.Instantiate(this.surprises[Random.Range(0, this.numOfSurprise)]);

            newSurprise.transform.position = new Vector3(
                Random.Range(this.center.x - this.width / 2, this.center.x + this.width / 2),
                Random.Range(this.center.y - this.depth / 4, this.center.y - this.depth / 2),
                newSurprise.transform.position.z
            );

            ICatchable newCatchable = newSurprise.GetComponent<ICatchable>();
            newCatchable.Sea = this;
            newCatchable.SetAction("SimpleSurpriseBottleBehaviour");

            this.createdCatchableObjects.Add(newCatchable);

            newCatchable.OnUsed += (ICatchable obj) =>
            {
                this.createdCatchableObjects.Remove(obj);
                GameObject.Destroy(obj.GameObject);
            };

            newSurprise.SetActive(true);
        }

    }

    void FishGenNew(int levelNum)
    {
        currentCostOfFish = 0;
        while (currentCostOfFish < necessaryCostOfFish)
        {

            GameObject typeOfFish = fishes[Random.Range(0, fishes.Length)];
            if (typeOfFish.GetComponent<Fish>().Price > necessaryCostOfFish - currentCostOfFish)
                typeOfFish = fishes[0];

            GameObject newFish = Instantiate(typeOfFish);

            newFish.transform.position = new Vector3(
                Random.Range(this.center.x - this.width / 2, this.center.x + this.width / 2),
                Random.Range(this.center.y - this.depth / 2, this.center.y + this.depth / 2),
                newFish.transform.position.z
            );

            ICatchable newCatchable = newFish.GetComponent<ICatchable>();

            newFish.GetComponent<Fish>().isDeviation = isUseDeviationOnMove;
            newCatchable.Sea = this;
            newFish.GetComponent<Fish>().deviation = Random.Range(minFishDeviation, width);
            newFish.GetComponent<Fish>().speed += levelNum / 15;
            currentCostOfFish += newFish.GetComponent<Fish>().Price;

            newCatchable.SetAction("SimpleFishBehaviour");
            createdCatchableObjects.Add(newCatchable);

            newCatchable.OnUsed += (ICatchable obj) =>
            {
                createdCatchableObjects.Remove(obj);
                GameObject.Destroy(obj.GameObject);
            };
        }
        print(currentCostOfFish);
    }
    void StarGenNew(int levelNum)
    {
        for (int i = 0; i < 4 + Mathf.Floor(levelNum / 4); i++)
        {
            GameObject newStar = GameObject.Instantiate(this.stars[Random.Range(0, stars.Length)]);

            newStar.transform.position = new Vector3(
                Random.Range(this.center.x - this.width / 2, this.center.x + this.width / 2),
                Random.Range(this.center.y - this.depth / 4, this.center.y - this.depth / 2),
                newStar.transform.position.z
            );

            ICatchable newCatchable = newStar.GetComponent<ICatchable>();
            newCatchable.Sea = this;
            newCatchable.SetAction("SimpleStarBehaviour");

            this.createdCatchableObjects.Add(newCatchable);

            newCatchable.OnUsed += (ICatchable obj) =>
            {
                this.createdCatchableObjects.Remove(obj);
                GameObject.Destroy(obj.GameObject);
            };


        }
    }
    void BombGenNew(int levelNum)
    {
        for (int i = 0; i < 1 + Mathf.Floor(levelNum / 5); i++)
        {
            GameObject newBomb = Instantiate(bomb);

            newBomb.transform.position = new Vector3(
                Random.Range(this.center.x - this.width / 2, this.center.x + this.width / 2),
                Random.Range(this.center.y - this.depth / 2, this.center.y + this.depth / 2),
                newBomb.transform.position.z
            );

            ICatchable newCatchable = newBomb.GetComponent<ICatchable>();

            newCatchable.Sea = this;
            newCatchable.SetAction("SimpleBombBehaviour");
            createdCatchableObjects.Add(newCatchable);
        }
    }
    void SurpriceGenNew(int levelNum)
    {
        for (int i = 0; i < 1 + Mathf.Floor(levelNum/5); i++)
        {
            GameObject newSurprise = GameObject.Instantiate(this.surprises[Random.Range(0, this.numOfSurprise)]);

            newSurprise.transform.position = new Vector3(
                Random.Range(this.center.x - this.width / 2, this.center.x + this.width / 2),
                Random.Range(this.center.y - this.depth / 4, this.center.y - this.depth / 2),
                newSurprise.transform.position.z
            );

            ICatchable newCatchable = newSurprise.GetComponent<ICatchable>();
            newCatchable.Sea = this;
            newCatchable.SetAction("SimpleSurpriseBottleBehaviour");

            this.createdCatchableObjects.Add(newCatchable);

            newCatchable.OnUsed += (ICatchable obj) =>
            {
                this.createdCatchableObjects.Remove(obj);
                GameObject.Destroy(obj.GameObject);
            };
        }
    }
    void TreasureGen(int levelNum)
    {
        int maxTreasureCount;
        if (levelNum < 7) maxTreasureCount = 1;
        else if (levelNum < 15) maxTreasureCount = 2;
        else maxTreasureCount = 3;

        for (int i = 0; i < maxTreasureCount; i++)
        {
            GameObject newTreasure = Instantiate(treasure);

            newTreasure.transform.position = new Vector3(
                Random.Range(this.center.x - this.width / 2, this.center.x + this.width / 2),
                center.y - depth / 2,
                newTreasure.transform.position.z
            );

            ICatchable newCatchable = newTreasure.GetComponent<ICatchable>();

            newTreasure.GetComponent<Treasure>().price = Random.Range(150, 301);
            newCatchable.Sea = this;
            newCatchable.SetAction("SimpleBombBehaviour");
            createdCatchableObjects.Add(newCatchable);

            newCatchable.OnUsed += (ICatchable obj) =>
            {
                createdCatchableObjects.Remove(obj);
                GameObject.Destroy(obj.GameObject);
            };
        }
    }
    void FolliageGen()
    {
        for(int i=0; i< 7;i++)
        {
            GameObject newFolliage = Instantiate(folliage);
            newFolliage.transform.position = new Vector3(
                Random.Range(this.center.x - this.width / 2, this.center.x + this.width / 2),
                center.y - depth / 2,
                newFolliage.transform.position.z
            );

            folliageObjects.Add(newFolliage);
        }
    }

    public void Clear()
    {
        foreach (ICatchable catchable in this.createdCatchableObjects)
        {
            GameObject.Destroy(catchable.GameObject);
        }

        this.createdCatchableObjects.Clear();

        foreach (GameObject fol in folliageObjects)
            Destroy(fol);

        folliageObjects.Clear();
    }

    // Use this for initialization
    void Awake()
    {
        this.center = this.transform.position;

        if (this.width == 0 || this.depth == 0)
        {
            Collider2D seaSizeCollider = this.GetComponent<Collider2D>();

            if (seaSizeCollider == null)
            {
                throw new UnityException("Unable to find out sea size");
            }

            this.width = seaSizeCollider.bounds.size.x;
            this.depth = seaSizeCollider.bounds.size.y;

            this.center = seaSizeCollider.bounds.center;
        }

        this.numOfFishes = this.fishes.Length;
        this.numOfStars = this.stars.Length;
        this.numOfSurprise = this.surprises.Length;
    }

    public void DestroyObject(ICatchable catchableObject)
    {
        if (this.createdCatchableObjects.Contains(catchableObject))
        {
            this.createdCatchableObjects.Remove(catchableObject);
            GameObject.Destroy(catchableObject.GameObject);
        }
    }

    void Start()
    {

    }

    public HashSet<ICatchable> GetAllCatchableObjectInSea()
    {
        HashSet<ICatchable> copy = new HashSet<ICatchable>();

        foreach (ICatchable catchable in this.createdCatchableObjects)
        {
            copy.Add(catchable);
        }

        return copy;
    }

}
