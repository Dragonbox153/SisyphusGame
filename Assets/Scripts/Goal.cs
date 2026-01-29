using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private EndGameUI gameWinUI;
    [SerializeField] private string winTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(winTag))
        {
            gameWinUI.gameObject.SetActive(true);
            gameWinUI.SetWinText();
        }
    }
}
