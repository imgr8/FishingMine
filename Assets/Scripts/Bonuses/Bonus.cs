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

	public abstract float Weight {
		get;
	}

	public virtual int Price {
		get {
			return 0;
		}
	}

	public virtual GameObject GameObject {
		get {
			return null;
		}
	}

	public virtual ISea Sea {	
		get {
			return null;
		}

		set {

		}
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
