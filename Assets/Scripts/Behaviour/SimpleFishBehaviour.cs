using UnityEngine;
using System.Collections;

// Простое поведение рыбы
public class SimpleFishBehaviour : IBehaviour {
	ICatchable catchable;
	ISea sea;
	Transform obj;
	float speed;
    float deviation;
    float startHorizontalPosition;

	enum MoveDirection {Right, Left};
	MoveDirection moveDirection = MoveDirection.Right;

	public SimpleFishBehaviour(ICatchable obj) {
		this.catchable = obj;
		this.obj = obj.GameObject.transform;
		this.sea = obj.Sea;
		this.speed = obj.GameObject.GetComponent<Fish>().speed;
        this.deviation = obj.GameObject.GetComponent<Fish>().deviation;

		InitialLook initialLook = obj.GameObject.GetComponent<Fish> ().InitialLook;

		if (initialLook == InitialLook.None) {
			this.moveDirection = (UnityEngine.Random.Range (0, 2) == 1 ? MoveDirection.Right : MoveDirection.Left);

			if (this.moveDirection == MoveDirection.Left) {	// Предполагается, что рыба смотрит направо, если это не так, то ее надо перевернуть, или задать направление, через InitialLook
				this.obj.transform.Rotate (new Vector3 (0, 180, 0));
			}
		} else {
			if (initialLook == InitialLook.Left) {
				this.moveDirection = MoveDirection.Left;
			} else if (initialLook == InitialLook.Right) {
				this.moveDirection = MoveDirection.Right;
			}
		}

		startHorizontalPosition = obj.GameObject.transform.position.x;
	}

	public void Action() {
        if (!obj.gameObject.GetComponent<Fish>().isDeviation)
        {
            if (this.moveDirection == MoveDirection.Left && this.obj.position.x > -this.sea.Width / 2)
            {
                obj.position += Vector3.left * Time.deltaTime * this.speed;
            }
            else if (this.moveDirection == MoveDirection.Left && this.obj.position.x <= -this.sea.Width / 2)
            {
                this.moveDirection = MoveDirection.Right;
                this.obj.transform.Rotate(new Vector3(0, 180, 0));
            }
            else if (this.moveDirection == MoveDirection.Right && this.obj.position.x < this.sea.Width / 2)
            {
                obj.position += Vector3.right * Time.deltaTime * this.speed;
            }
            else if (this.moveDirection == MoveDirection.Right && this.obj.position.x >= this.sea.Width / 2)
            {
                this.moveDirection = MoveDirection.Left;
                this.obj.transform.Rotate(new Vector3(0, 180, 0));
            }
        }

        else
        {
            
            if (moveDirection == MoveDirection.Left && obj.position.x > startHorizontalPosition - deviation && obj.position.x > -sea.Width / 2)
                obj.position += Vector3.left * Time.deltaTime * speed;
            else if (moveDirection == MoveDirection.Left &&( (obj.position.x <= startHorizontalPosition - deviation )|| (obj.position.x <= -sea.Width / 2)))
            {
                moveDirection = MoveDirection.Right;
                obj.transform.Rotate(new Vector3(0, 180, 0));
            }
            else if (moveDirection == MoveDirection.Right && obj.position.x < startHorizontalPosition + deviation && obj.position.x < sea.Width / 2)
                obj.position += Vector3.right * Time.deltaTime * speed;
            else if (moveDirection == MoveDirection.Right && (obj.position.x >= startHorizontalPosition + deviation || obj.position.x >= sea.Width / 2))
            {
                moveDirection = MoveDirection.Left;
                obj.transform.Rotate(new Vector3(0, 180, 0));
            }
        }
	}

	float savedSpeed;

	public void Stop() {
		this.savedSpeed = this.speed;
		this.speed = 0;
	}

	public void Resume() {
		this.speed = this.savedSpeed;
	}

	public void Change(object data = null) {
		this.speed = (float)data;
	}
}

