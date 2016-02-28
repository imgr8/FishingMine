using UnityEngine;
using System.Collections;

public class SimpleBombBehaviour : IBehaviour {
    ICatchable catchable;
    ISea sea;
    Transform obj;
    float horizontalSpeed, verticalSpeed;
    float verticalDeviation;
    Vector3 startVerticalPosition;

    enum MoveHorizontalDirection { Right, Left };
    MoveHorizontalDirection moveHorizontalDirection;

    enum MoveVerticalDirection { Up, Down };
    MoveVerticalDirection moveVerticalDirection;

    public SimpleBombBehaviour(ICatchable obj)
    {
        /*
		this.catchable = obj;
        this.sea = obj.Sea;
        this.obj = obj.GameObject.transform;

        this.verticalSpeed = obj.GameObject.GetComponent<Bomb>().verticalSpeed;
        this.horizontalSpeed = obj.GameObject.GetComponent<Bomb>().horizontalSpeed;
        this.verticalDeviation = obj.GameObject.GetComponent<Bomb>().verticalDeviation;
        startVerticalPosition = obj.GameObject.transform.position;

        moveHorizontalDirection = (Random.Range(0, 2) == 0 ? MoveHorizontalDirection.Left : MoveHorizontalDirection.Right);
        moveVerticalDirection = (Random.Range(0, 2) == 0 ? MoveVerticalDirection.Down : MoveVerticalDirection.Up);
*/

    }

    public void Action()
    {
        if (moveHorizontalDirection == MoveHorizontalDirection.Left && obj.position.x > -sea.Width / 2)
        {
            obj.position += Vector3.left * Time.deltaTime * horizontalSpeed;
        }
        else if (moveHorizontalDirection == MoveHorizontalDirection.Left && obj.position.x <= -sea.Width / 2)
        {
            moveHorizontalDirection = MoveHorizontalDirection.Right;
        }
        else if (moveHorizontalDirection == MoveHorizontalDirection.Right && obj.position.x < sea.Width / 2)
        {
            obj.position += Vector3.right * Time.deltaTime * horizontalSpeed;
        }
        else if (moveHorizontalDirection == MoveHorizontalDirection.Right && obj.position.x >= sea.Width / 2)
        {
            moveHorizontalDirection = MoveHorizontalDirection.Left;
        }

        if (moveVerticalDirection == MoveVerticalDirection.Up && obj.position.y < startVerticalPosition.y + verticalDeviation)
            obj.position += Vector3.up * Time.deltaTime * verticalSpeed;
        else if (moveVerticalDirection == MoveVerticalDirection.Up && obj.position.y >= startVerticalPosition.y + verticalDeviation)
            moveVerticalDirection = MoveVerticalDirection.Down;
        else if (moveVerticalDirection == MoveVerticalDirection.Down && obj.position.y > startVerticalPosition.y - verticalDeviation)
            obj.position += Vector3.down * Time.deltaTime * verticalSpeed;
        else if (moveVerticalDirection == MoveVerticalDirection.Down && obj.position.y <= startVerticalPosition.y - verticalDeviation)
            moveVerticalDirection = MoveVerticalDirection.Up;
    }

    float savedVertiaclSpeed, savedHorizontalSpeed;

    public void Stop()
    {
        savedHorizontalSpeed = horizontalSpeed;
        savedVertiaclSpeed = verticalSpeed;
        horizontalSpeed = verticalSpeed = 0;
    }

    public void Resume()
    {
        verticalSpeed = savedVertiaclSpeed;
        horizontalSpeed = savedHorizontalSpeed;
    }

    public void Change(object data)
    {
       
    }
}
