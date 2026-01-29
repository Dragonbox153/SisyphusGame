using UnityEngine;

public interface ICommand
{
    public abstract void Execute(Rigidbody2D rb, Vector2 direction);
}
