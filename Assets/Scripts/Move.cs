using UnityEngine;

public class Move : ICommand
{
    public Move() {}

    public void Execute(Rigidbody2D rb, Vector2 direction)
    {
        Vector2Int newPosition = Vector2Int.RoundToInt(rb.position + direction);
        if (!GameGrid.Instance.IsFree(newPosition) && GameGrid.Instance.IsPushable(newPosition))
        {
            MovementManager.Instance.AnimatePush(rb.gameObject, GameGrid.Instance.GetObjectAt(newPosition), direction);
        }
        else if(GameGrid.Instance.IsFree(newPosition))
        {
            MovementManager.Instance.AnimateMove(rb.gameObject, direction);
        }
    }
}
