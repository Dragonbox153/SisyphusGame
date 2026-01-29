using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameGrid grid;
    public float speed;

    public event Action UpdatePosition;
    public event Action Undo;

    public void MovePlayer(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            ICommand command = new Move();
            Vector2 movement = context.ReadValue<Vector2>();

            if (movement.x != 0)
            {
                movement.y = 0;
            }

            command.Execute(GetComponent<Rigidbody2D>(), movement);
            UpdatePosition.Invoke();
        }
    }

    public void UndoPlayer(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Undo.Invoke();
        }
    }
}
