﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Bomb : MonoBehaviour, ICatchable, ISaveFromEditor
{
    public float horizontalSpeed = 1.0f;
    public float verticalSpeed = 1.0f;
    public float verticalDeviation = 1.0f;
    public float explodeRadius = 10.0f;
    public float weight = 1.0f;
    public int price = 0;
	public string name_ = "Bomb";

	public string Name {
		get {
			return this.name_;
		}
	}

    public float Weight
    {
        get { return this.weight; }
    }

   
    public float ExplodeRadius
    {
        get
        {
            return explodeRadius;
        }
    }


    public int Price
    {
        get { return this.price; }
    }

	public void ChangeWeight (float ratio = 1.0f) {
		if (ratio < 0) {
			ratio = 1.0f;
		}

		this.weight *= ratio;	
	}

	public void ChangePrice (float ratio = 1.0f) {
		if (ratio < 0) {
			ratio = 1.0f;
		}

		int tmp = (int)(this.price * ratio);

		this.price = tmp;	
	}


    enum State { Normal, Explode };
    State state = State.Normal;

    public GameObject GameObject
    {
        get { return this.gameObject; }
    }

    ISea sea;
    public ISea Sea
    {
        get
        {
            return this.sea;
        }
        set
        {
            this.sea = value;
        }
    }

   
    public ICatchable WhenCatched(IHook hook)
    {
        //StopAction();
        Explode();

        return null;
    }

	event Action<ICatchable> onUsed;

	public event Action<ICatchable> OnUsed {
		add {
			this.onUsed += value;		
		}

		remove {
			this.onUsed -= value;
		}
	}

    public void Use(IFisher fisher)
    {
		if (this.onUsed != null) {
			this.onUsed.Invoke (this);
		}
    }

	public string defaultAction = "";

	public string DefaultAction {
		get {
			return this.defaultAction;
		}
	}

  	public void Destroy()
    {
		if (this.sea != null) {
			this.sea.DestroyObject (this);
		} else {
			GameObject.Destroy (this.GameObject);
		}
    }
    void Explode()
    {
		if (this.sea != null) {

			HashSet<ICatchable> cathcableObjectsInSea = this.sea.GetAllCatchableObjectInSea ();	// Возвращается копия!

			foreach (ICatchable catchable in cathcableObjectsInSea) {
				if (Vector3.Distance (catchable.GameObject.transform.position, this.transform.position) <= explodeRadius) {
					this.sea.DestroyObject (catchable);
				}
			}

			cathcableObjectsInSea.Clear();
	
		}

        Destroy();
    }

    void EmptyBehaviour()
    {
        
    }

    void Start()
    {
		
    }

    void Update()
    {
      
    }

	public string path;

	public string Path {
		get {
			return this.path;
		}
	}

	// Не использовать в качестве разделителя "|"
	// 1 параметр Horizontal Speed (на само деле не важно какой главное при Load все правильно восстановить)
	public string Save() {
		string parameters = this.horizontalSpeed.ToString() + "@" + this.verticalSpeed.ToString();

		return parameters;
	}

	public void Load(ISea sea, string param) {
		this.Sea = sea;

		string [] parameters = param.Split (new char[]{ '@' });

		this.horizontalSpeed = float.Parse(parameters [0]);
		this.verticalSpeed = float.Parse(parameters [1]);

	}
}
