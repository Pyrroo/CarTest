using UnityEngine;

public class CanvasSwaper : MonoBehaviour
{
    [SerializeField] private GameObject startCanvas, gameplayCanvas, endCanvas;

    private void Start()
    {
        ToStart();
    }

    public void ToStart()
    {
        startCanvas.SetActive(true);
        gameplayCanvas.SetActive(false);
        endCanvas.SetActive(false);
    }

    public void ToGame()
    {
        startCanvas.SetActive(false);
        gameplayCanvas.SetActive(true);
        endCanvas.SetActive(false);
    }

    public void ToEnd()
    {
        startCanvas.SetActive(false);
        gameplayCanvas.SetActive(false);
        endCanvas.SetActive(true);
    }
}
