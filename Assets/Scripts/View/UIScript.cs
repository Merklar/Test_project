using UnityEngine.SceneManagement;
using UnityEngine;

public static class UIFacade
{
    public static UIScript UIScript { get; private set; }

    public static void SetUIScript(UIScript uiscript)
    {
        UIScript = uiscript;
    }

    public static void GameOver()
    {
        UIScript.GamveOver();
    }
}

public class UIScript : MonoBehaviour {

    public Canvas canvas;

    private void Awake()
    {
        UIFacade.SetUIScript(this);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GamveOver()
    {
        canvas.gameObject.SetActive(true);
    }
}
