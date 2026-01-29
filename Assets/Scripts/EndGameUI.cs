using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private Undoable undoable;
    [SerializeField] private string nextScene;

    public void SetWinText()
    {
        winText.text = "You reached the goal in " + undoable.previousPositions.Count + " steps";
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayNextLevel()
    {
        SceneManager.LoadScene(nextScene);
    }
}
