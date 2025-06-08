using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject levelMenu;

    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        levelMenu.SetActive(false);
    }

    public void OpenLevelSelect()
    {
        mainMenu.SetActive(false);
        levelMenu.SetActive(true);
    }

    public void loadLevel(int levelNum)
    {
        SceneManager.LoadScene(levelNum + 1);
    }
}
