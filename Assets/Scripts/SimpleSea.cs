using UnityEngine;
using System.Collections;

// Каждое море может обладать своими уникальными параметрами
// генерироваться компьютером
// или же вручную (например, когда функция MakeLive загружает xml файл и расставляет объекты).
// Если же это генерация, то MakeLive - генерирует объекты

public class SimpleSea : MonoBehaviour, ISea {

	public float width = 10.0f;	

	public float Width {
		get {
			return this.width;
		}
	}

	public float depth = 6.0f;

	public float Depth {
		get {
			return this.depth;
		}
	}

	Vector3 center;

	public Vector3 Center {
		get {
			return this.center;
		}
	}

	public float flowHorizontal = 0.0f;

	public float FlowHorizontal {
		get {
			return this.flowHorizontal;	
		}
	}

	public float flowVertical = 0.0f;

	public float FlowVertical {
		get { 
			return this.flowVertical;
		}
	}

	public int totalFishAmount = 10;

	int numOfFishes = 0;

	public GameObject [] fishes;

	public int totalStarAmount = 10;

	int numOfStars = 0;

	public GameObject [] stars;

	public void MakeLive() {
		this.FishGen ();
		this.StarGen ();
	}
		
	void FishGen() {
		for (int i = 0; i < this.totalFishAmount; i++) {
			GameObject newFish = GameObject.Instantiate (this.fishes [Random.Range (0, this.numOfFishes)]);

			newFish.transform.position = new Vector3 (
				Random.Range (this.center.x - this.width / 2, this.center.x + this.width / 2), 
				Random.Range (this.center.y - this.depth / 2, this.center.y + this.depth / 2), 
				newFish.transform.position.z
			);

			newFish.GetComponent<ICatchable> ().Sea = this;
			newFish.GetComponent<ICatchable> ().SetAction ("SimpleFishBehaviour");

			newFish.SetActive (true);
		}

	}

	void StarGen() {
		for (int i = 0; i < this.totalStarAmount; i++) {
			GameObject newStar = GameObject.Instantiate (this.stars [Random.Range (0, this.numOfStars)]);

			newStar.transform.position = new Vector3 (
				Random.Range (this.center.x - this.width / 2, this.center.x + this.width / 2), 
				Random.Range (this.center.y - this.depth / 4, this.center.y - this.depth / 2), 
				newStar.transform.position.z
			);

			newStar.GetComponent<ICatchable> ().Sea = this;
			newStar.GetComponent<ICatchable> ().SetAction ("SimpleStarBehaviour");

			newStar.SetActive (true);
		}

	}

	// Use this for initialization
	void Awake () {
		this.center = this.transform.position;

		if (this.width == 0 || this.depth == 0) {
			Collider2D seaSizeCollider = this.GetComponent<Collider2D> ();

			if (seaSizeCollider == null) {
				throw new UnityException ("Unable to find out sea size");
			} 

			this.width = seaSizeCollider.bounds.size.x;
			this.depth = seaSizeCollider.bounds.size.y;

			this.center = seaSizeCollider.bounds.center;
		}

		this.numOfFishes = this.fishes.Length;
		this.numOfStars = this.stars.Length;
	}

	void Start() {
		
	}

}
