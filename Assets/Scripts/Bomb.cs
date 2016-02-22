﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Bomb : MonoBehaviour, ICatchable
{
    public float horizontalSpeed = 1.0f;
    public float verticalSpeed = 1.0f;
    public float verticalDeviation = 1.0f;
    public float explodeRadius = 10.0f;
    public float weight = 1.0f;
    public float price = 1.0f;

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


    public float Price
    {
        get { return this.price; }
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
        StopAction();
        Explode();

        return null;
    }

    public void Use(IFisher fisher)
    {
        //по идее, не реализовывает, до рыбака так и не дойдёт
    }

    event Action<ICatchable> onUsed;
    public event Action<ICatchable> OnUsed
    {
        add { this.onUsed += value; }
        remove { this.onUsed -= value; }
    }

    Action bombBehaviour;
    IBehaviour behaviour;
    public void SetAction(string actionName, object data = null)
    {
        behaviour = BehaviourCreator.CreateBehaviour(actionName, this, data);
        bombBehaviour = behaviour.Action;
    }

    public void ChangeAction(object data)
    {
        if (behaviour != null)
            behaviour.Change(data);
    }

    public void StopAction()
    {
        if (behaviour != null)
            behaviour.Stop();
    }

    public void ResumeAction()
    {
        if (behaviour != null)
            behaviour.Resume();
    }
    // Дим я добавил код сюда, это чтобы как раз леска возвращалась правильно назад
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
        //GameObject.GetComponent<CircleCollider2D>().radius = explodeRadius;

		// Дим, я понял у тебя был какой-то код, который уничтожал окружающие предметы, я сейчас честно говоря уже устал 7 утра ))) просто не хочу бросать недострой
		// смотри если хочешь воостанови свою логику. Или оставь у тебя я как понял цепляет коллайдер.
		// Я внес небольшое изменение в ISea о котором говорил, чтобы он возвращал объекты
		// и вернув объекты сравниваю расстояние до бомбы. Если меньше explodeRadius, то взрываем
		// В общем смотри или оставь или верни свой. Нет проблем у тебя все работало.
		// Мой код тебя тоже с циклом пусть не смущает, поскольку внутри Unity коллайдеры будут сравниваться примерно также.

		// Да и еще совет если хочешь сделать детонацию, то введи некий параметр для бомбы, чтобы обозначить была она детонирована крючком от удочки
		// тогда просто Destroy, если же она была детонирована детонацией ))), то пускай Destroy (бомбы) вызовет Explode. А в Explode установи этот параметр,
		// чтобы не было бесконечного цикла. И детонация готова. Я не делаю, сам на свое усмотрение делай. Думаю понял что я имел ввиду )

		if (this.sea != null) {
			HashSet<ICatchable> cathcableObjectsInSea = this.sea.GetAllCatchableObjectInSea ();	// Возвращается копия!

			foreach (ICatchable catchable in cathcableObjectsInSea) {
				if (Vector3.Distance (catchable.GameObject.transform.position, this.transform.position) <= explodeRadius) {
					this.sea.DestroyObject (catchable);
				}
			}

			cathcableObjectsInSea.Clear();
		}
	//	state = State.Explode;
        Destroy();
    }

    void EmptyBehaviour()
    {
        
    }

    void Awake()
    {
        bombBehaviour = EmptyBehaviour;
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (state == State.Normal)
    //        return;

    //    if (other.CompareTag("Catchable"))
    //    {
    //        ICatchable catchable = other.GetComponent<ICatchable>();
    //        catchable.Destroy();
    //    }

    //}
    void Update()
    {
        bombBehaviour.Invoke();
    }
}
