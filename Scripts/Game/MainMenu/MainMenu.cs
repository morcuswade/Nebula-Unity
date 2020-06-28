using UnityEngine;
using UnityCore.Scene;
using UnityCore.Page;

public class MainMenu : MonoBehaviour {
  public SceneController sceneController;
  public PageController pageController;
  public void PlayGame() {
    Debug.Log("PlayGame");
    sceneController.Load(SceneType.Stage01);
  }

  public void OpenOptions() {
    Debug.Log("OpenOptions");
    pageController.TurnPageOff(PageType.MainMenu, PageType.OptionsMenu);
  }

  public void OpenMainMenu() {
    Debug.Log("OpenMainMenu");
    pageController.TurnPageOff(PageType.OptionsMenu, PageType.MainMenu);
  }

  public void QuitGame() {
    Application.Quit();
  }
}
