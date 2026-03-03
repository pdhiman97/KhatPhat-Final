using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject menu;        // Menu (Title screen)
    public GameObject warning;     // Warning panel

    [Header("Buttons")]
    public Button startButton;     // Start button (inside Menu)
    public Button continueButton;  // Continue button (inside Warning)

    void Start()
    {
        ShowMenu();

        startButton.onClick.AddListener(ShowWarning);
        continueButton.onClick.AddListener(StartGame);
    }

    void ShowMenu()
    {
        menu.SetActive(true);
        warning.SetActive(false);
    }

    void ShowWarning()
    {
        menu.SetActive(false);
        warning.SetActive(true);
    }

    void StartGame()
    {
        SceneManager.LoadSceneAsync(1);  // Ensure your main game scene is index 1
    }
}