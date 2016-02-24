using UnityEngine;
using System;
using System.Collections;

// Бонусы имеют тип ICatchable
// однако многие бонусы не нуждаются в реализации многих
// специфических методов и свойств присущих ICatchable
// Тем самым введен абстрактный класс Bonus,
// реализующий многие методы и свойства заглушками
// Если же какому-то бонусы необходима конкретная реализация
// то он может просто перегрузить виртуальный метод

// Замечпание: здесь была дилемма о том, должны ли бонусы быть ICatchable.
// Несмотря на то, что многие бонусы могут не использовать методы и свойства ICatchable
// по своей идеологии Bonus'ы можно присвоить к данному типу. Хотя возможно это и не самая 
// лучшая детализация, но на уровне поставленной задачи, данная реализация весьма работоспособна

public abstract class Bonus : ICatchable {
	protected float weight = 0;

	public virtual float Weight {
		get {
			return this.weight;		
		}
	}

	protected int price = 0;

	public virtual int Price {
		get {
			return this.price;
		}
	}

	protected string name = "Bonus";

	public virtual string Name {
		get {
			return this.name;
		}
	}

	public virtual GameObject GameObject {
		get {
			return null;
		}
	}

	ISea sea = null;

	public virtual ISea Sea {	
		get {
			return this.sea;
		}

		set {
			this.sea = value;
		}
	}

	public virtual void ChangeWeight (float ratio = 1.0f) {
		if (ratio < 0) {
			ratio = 1.0f;
		}

		this.weight *= ratio;	
	}

	public virtual void ChangePrice (float ratio = 1.0f) {
		if (ratio < 0) {
			ratio = 1.0f;
		}

		int tmp = (int)(this.price * ratio);

		this.price = tmp;	
	}

	public virtual void ChangeSpeed (float ratio = 1.0f) {
		
	}

	public virtual void Destroy() {
		
	}

	public ICatchable WhenCatched (IHook hook) {
		return null;
	}
		
	public abstract void Use(IFisher fisher);	 
	public abstract event Action<ICatchable> OnUsed;

	public string defaultAction = "";

	public string DefaultAction {
		get {
			return this.defaultAction;
		}
	}

	public virtual void SetAction (string actionName, object data = null) {
	
	}

	public virtual void ChangeAction (object data) {

	}

	public virtual void StopAction () {
	
	}

	public virtual void ResumeAction() {
		
	}


}
