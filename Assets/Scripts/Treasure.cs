using UnityEngine;
using System.Collections;
using System;

public class Treasure : MonoBehaviour, ICatchable, ISaveFromEditor {

    public float weight;
    public int price;

    public float Weight
    {
        get { return this.weight; }
    }

    public int Price
    {
        get { return this.price; }
    }

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

    public void Destroy()
    {
        if (this.sea != null)
        {
            this.sea.DestroyObject(this);
        }
        else
        {
            GameObject.Destroy(this.GameObject);
        }
    }

    public ICatchable WhenCatched(IHook hook)
    {
        StopAction(); 
        return this;
    }

    event Action<ICatchable> onUsed;

    public event Action<ICatchable> OnUsed
    {
        add
        {
            this.onUsed += value;
        }

        remove
        {
            this.onUsed -= value;
        }
    }
    public void Use(IFisher fisher)
    {
        fisher.Boat.PutStaff(this);

        if (this.onUsed != null)
        {
            this.onUsed.Invoke(this);
        }
    }

	public string defaultAction = "";

	public string DefaultAction {
		get {
			return this.defaultAction;
		}
	}

    public void SetAction(string actionName, object data = null)
    {
        
    }

    public void ChangeAction(object data)
    {
       
    }

    public void StopAction()
    {
        
    }

    public void ResumeAction()
    {
        
    }

	public string path;

	public string Path {
		get {
			return this.path;
		}
	}

	public string Save() {
		return "";
	}

	public void Load(ISea sea, string param = "") {
		this.Sea = sea;
		this.SetAction("SimpleFishBehaviour");	// Поскольку море не знает об объекте, устанавливаем поведение по-умолчанию сами, в последствии море уже будет само контролировать поведение
	}
}
