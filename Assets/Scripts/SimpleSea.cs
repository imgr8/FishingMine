using UnityEngine;
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
    
    int necessaryCostOfFish = 0;
    int currentCostOfFish = 0;

    public void MakeLive(int? prm = null, object data = null)
    {
		if (prm == null) {
			return;
		}

		int param = (int)prm;

		necessaryCostOfFish = (10 * param * param + 125 * param) - (10 * (param - 1) * (param - 1) + 125 * (param - 1)) + 50; // FIX вместо чисел использовать переменные. Для более удобного контроля и лучшей читамости. Например baseCoefficient

        FishGenNew(param);
        StarGenNew(param);
        BombGenNew(param);
        SurpriceGenNew(param);
        TreasureGen(param);
        FolliageGen();
    }

    HashSet<ICatchable> createdCatchableObjects = new HashSet<ICatchable>();
	HashSet<IUncatchable> createdUncatchableObjects = new HashSet<IUncatchable>();

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

            this.createdCatchableObjects.Add(newCatchable);

            newCatchable.OnUsed += (ICatchable obj) =>
            {
                //this.createdCatchableObjects.Remove(obj);
                //GameObject.Destroy(obj.GameObject);
				this.DestroyObject(obj);
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

            this.createdCatchableObjects.Add(newCatchable);

            newCatchable.OnUsed += (ICatchable obj) =>
            {
                //this.createdCatchableObjects.Remove(obj);
                //GameObject.Destroy(obj.GameObject);
				this.DestroyObject(obj);
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

            this.createdCatchableObjects.Add(newCatchable);

            newCatchable.OnUsed += (ICatchable obj) =>
            {
                //this.createdCatchableObjects.Remove(obj);
                //GameObject.Destroy(obj.GameObject);
				this.DestroyObject(obj);
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

			createdCatchableObjects.Add(newCatchable);

            newCatchable.OnUsed += (ICatchable obj) =>
            {
                //createdCatchableObjects.Remove(obj);
                //GameObject.Destroy(obj.GameObject);
				this.DestroyObject(obj);
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

            this.createdCatchableObjects.Add(newCatchable);

            newCatchable.OnUsed += (ICatchable obj) =>
            {
                //this.createdCatchableObjects.Remove(obj);
                //GameObject.Destroy(obj.GameObject);
				this.DestroyObject(obj);
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

            createdCatchableObjects.Add(newCatchable);
        }
    }
    void SurpriceGenNew(int levelNum)
    {
        for (int i = 0; i < 8 + Mathf.Floor(levelNum/5); i++)
        {
            GameObject newSurprise = GameObject.Instantiate(this.surprises[Random.Range(0, this.numOfSurprise)]);

            newSurprise.transform.position = new Vector3(
                Random.Range(this.center.x - this.width / 2, this.center.x + this.width / 2),
                Random.Range(this.center.y - this.depth / 4, this.center.y ),//- this.depth / 2),
                newSurprise.transform.position.z
            );

            ICatchable newCatchable = newSurprise.GetComponent<ICatchable>();
            newCatchable.Sea = this;

            this.createdCatchableObjects.Add(newCatchable);

            newCatchable.OnUsed += (ICatchable obj) =>
            {
                //this.createdCatchableObjects.Remove(obj);
                //GameObject.Destroy(obj.GameObject);
				this.DestroyObject(obj);
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

            createdCatchableObjects.Add(newCatchable);

            newCatchable.OnUsed += (ICatchable obj) =>
            {
                //createdCatchableObjects.Remove(obj);
                //GameObject.Destroy(obj.GameObject);
				this.DestroyObject(obj);
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

			this.createdUncatchableObjects.Add(newFolliage.GetComponent<IUncatchable>());
        }
    }

    public void Clear()
    {
        foreach (ICatchable catchable in this.createdCatchableObjects)
        {
            GameObject.Destroy(catchable.GameObject);
        }

        this.createdCatchableObjects.Clear();

		foreach (IUncatchable uncatchable in this.createdUncatchableObjects)
		{
			GameObject.Destroy(uncatchable.GameObject);
		}

		this.createdUncatchableObjects.Clear();
    }

	public void ClearAllCatchable() {
	
		foreach (ICatchable catchable in this.createdCatchableObjects)
		{
			GameObject.Destroy(catchable.GameObject);
		}

		this.createdCatchableObjects.Clear();
	}

	public void ClearAllUncatchable() {
		foreach (IUncatchable uncatchable in this.createdUncatchableObjects)
		{
			GameObject.Destroy(uncatchable.GameObject);
		}

		this.createdUncatchableObjects.Clear();
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

			if (this.onDestroyCatchableObject != null) {
				this.onDestroyCatchableObject.Invoke (catchableObject);
			}

			GameObject.Destroy(catchableObject.GameObject);

        }
    }

	public void DestroyObject(IUncatchable uncatchableObject)
	{
		if (this.createdUncatchableObjects.Contains(uncatchableObject))
		{
			this.createdUncatchableObjects.Remove(uncatchableObject);

			if (this.onDestroyUncatchableObject != null) {
				this.onDestroyUncatchableObject.Invoke (uncatchableObject);
			}

			GameObject.Destroy(uncatchableObject.GameObject);
		}
	}

	public void AddObject (ICatchable catchableObject) {
		if (!this.createdCatchableObjects.Contains(catchableObject))
		{
			this.createdCatchableObjects.Add(catchableObject);

			catchableObject.OnUsed += (ICatchable obj) =>
			{
				//this.createdCatchableObjects.Remove(obj);
				//GameObject.Destroy(obj.GameObject);
				this.DestroyObject(obj);
			};
		}
	}

	public void AddObject (IUncatchable uncatchableObject) {
		if (!this.createdUncatchableObjects.Contains(uncatchableObject))
		{
			this.createdUncatchableObjects.Add(uncatchableObject);
		}
	}

	public void AddObject (GameObject newGameObject) {

		if (newGameObject.GetComponent<ICatchable>() == null && newGameObject.GetComponent<IUncatchable>() == null) {
			return;
		}

		ICatchable catchable = newGameObject.GetComponent<ICatchable> ();

		if (catchable != null) {

			if (!this.createdCatchableObjects.Contains (catchable)) {
				this.createdCatchableObjects.Add (catchable);

				catchable.OnUsed += (ICatchable obj) => {
					//this.createdCatchableObjects.Remove (obj);
					//GameObject.Destroy (obj.GameObject);
					this.DestroyObject(obj);
				};

			}

			return;
		}

		IUncatchable uncatchable = newGameObject.GetComponent<IUncatchable> ();

		if (uncatchable != null) {

			if (!this.createdUncatchableObjects.Contains (uncatchable)) {
				this.createdUncatchableObjects.Add (uncatchable);
			}

			return;
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

	public HashSet<IUncatchable> GetAllUncatchableObjectInSea()
	{
		HashSet<IUncatchable> copy = new HashSet<IUncatchable>();

		foreach (IUncatchable uncatchable in this.createdUncatchableObjects)
		{
			copy.Add(uncatchable);
		}

		return copy;
	}

	public int CountOfCatchableObjects {
		get {
			return this.createdCatchableObjects.Count;
		}
	}

	public int CountOfUncatchableObjects {
		get {
			return this.createdUncatchableObjects.Count;
		}
	}

	public int CountOfAllObjects {
		get {
			return (this.createdCatchableObjects.Count + this.createdUncatchableObjects.Count);
		}
	}

	event System.Action<ICatchable> onDestroyCatchableObject;

	public event System.Action<ICatchable> OnDestroyCatchableObject {	// Событие происходит, когда уничтожается ICatchable объект
		add {
			this.onDestroyCatchableObject += value;
		}

		remove {
			this.onDestroyCatchableObject -= value;
		}
	}

	event System.Action<IUncatchable> onDestroyUncatchableObject;

	public event System.Action<IUncatchable> OnDestroyUncatchableObject {	// Событие происходит, когда уничтожается Iuncatchable объект
		add {
			this.onDestroyUncatchableObject += value;
		}

		remove {
			this.onDestroyUncatchableObject -= value;
		}
	}

	public void OnEveryCatchableObject (System.Action<ICatchable> foo) {	// В этой функции нельзя удалять!
		foreach (ICatchable catchable in this.createdCatchableObjects) {
			foo (catchable);
		}
	}

	public void OnEveryUncatchableObject (System.Action<IUncatchable> foo) {	// В этой функции нельзя удалять!
		foreach (IUncatchable uncatchable in this.createdUncatchableObjects) {
			foo (uncatchable);
		}
	}
}
