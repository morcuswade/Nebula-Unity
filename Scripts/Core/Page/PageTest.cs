using UnityEngine;

namespace UnityCore {
  namespace Page {
    public class PageTest : MonoBehaviour {
      public PageController controller;

#if UNITY_EDITOR
      private void Update() {
        if (Input.GetKeyUp(KeyCode.A)) {
          controller.TurnToPage(PageType.None);
          Debug.Log($"{controller.currentPage}");
        }

        if (Input.GetKeyUp(KeyCode.S)) {
          controller.TurnToPage(PageType.Loading);
          Debug.Log($"{controller.currentPage}");
        }

        if (Input.GetKeyUp(KeyCode.D)) {
          controller.TurnToPage(PageType.MainMenu);
          Debug.Log($"{controller.currentPage}");
        }

        if (Input.GetKeyUp(KeyCode.F)) {
          controller.TurnToPage(PageType.OptionsMenu);
          Debug.Log($"{controller.currentPage}");
        }

        if (Input.GetKeyUp(KeyCode.G)) {
          controller.TurnToPage(PageType.MainMenu, true);
          Debug.Log($"{controller.currentPage}");
        }
      }
#endif

    }
  }
}

