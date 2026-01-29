using System.Collections;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public static MovementManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public void AnimatePush(GameObject gameObject, GameObject pushedObject, Vector2 direction)
    {
        gameObject.GetComponent<Animator>().SetBool("IsPushing", true);
        gameObject.GetComponent<Animator>().SetFloat("MoveX", direction.x);
        gameObject.GetComponent<Animator>().SetFloat("MoveY", direction.y);
        StartCoroutine(DoPush(gameObject.GetComponent<Rigidbody2D>(), pushedObject.GetComponent<Rigidbody2D>(), direction));
        StartCoroutine(ResetAnimator(gameObject));
    }

    private IEnumerator DoPush(Rigidbody2D rb, Rigidbody2D pushedRB, Vector2 direction)
    {
        yield return new WaitForSeconds(0.2f);
        ICommand command = new Move();
        command.Execute(pushedRB, direction);
    }

    public void AnimateMove(GameObject gameObject, Vector2 direction)
    {
        gameObject.GetComponent<Animator>().SetFloat("MoveX", direction.x);
        gameObject.GetComponent<Animator>().SetFloat("MoveY", direction.y);
        StartCoroutine(DoMove(gameObject.GetComponent<Rigidbody2D>(), direction));
        StartCoroutine (ResetAnimator(gameObject));
    }

    private IEnumerator DoMove(Rigidbody2D rb, Vector2 direction)
    {
        do
        {
            yield return new WaitForSeconds(0.2f);
            GameGrid.Instance.MovePosition(Vector2Int.RoundToInt(rb.position), Vector2Int.RoundToInt(direction));
            rb.position = rb.position + direction;
        } while (GameGrid.Instance.icyTiles.HasTile(Vector3Int.RoundToInt(rb.position)) && GameGrid.Instance.IsFree(Vector2Int.RoundToInt(rb.position + direction)));
    }

    public IEnumerator ResetAnimator(GameObject gameObject)
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<Animator>().SetBool("IsPushing", false);
        gameObject.GetComponent<Animator>().SetFloat("MoveX", 0);
        gameObject.GetComponent<Animator>().SetFloat("MoveY", 0);
    }
}
