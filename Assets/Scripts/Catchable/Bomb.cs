using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Bomb : AMCatchable, ISaveFromEditor
{
    public float explodeRadius = 10.0f;
  
    public float ExplodeRadius
    {
        get
        {
            return explodeRadius;
        }
    }
	     
    public override ICatchable WhenCatched(IHook hook)
    {
		this.MoveBehaviour.StopMove ();
        Explode();

        return null;
    }

    void Explode()
    {
		if (this.sea != null) {

			HashSet<ICatchable> cathcableObjectsInSea = this.sea.GetAllCatchableObjectInSea ();	// Возвращается копия!

			foreach (ICatchable catchable in cathcableObjectsInSea) {
				if (Vector3.Distance (catchable.GameObject.transform.position, this.transform.position) <= explodeRadius) {
					catchable.Destroy();	// Если нужна цепная реакция, то переопределить метод Destroy() у бомбы
				}
			}

			cathcableObjectsInSea.Clear();
	
		}

        Destroy();
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
	//	string parameters = this.horizontalSpeed.ToString() + "@" + this.verticalSpeed.ToString();

	//	return parameters;
		return "";
	}

	public void Load(ISea sea, string param) {
		/*this.Sea = sea;

		string [] parameters = param.Split (new char[]{ '@' });

		this.horizontalSpeed = float.Parse(parameters [0]);
		this.verticalSpeed = float.Parse(parameters [1]);
*/
	}
}
