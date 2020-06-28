using UnityEngine;
using UnityCore.Page;

namespace UnityCore {
  namespace Scene {
    public class TestScene : MonoBehaviour {
      public SceneController sceneController;

      #region Unity Functions
      #if UNITY_EDITOR
        private void Update() {
          if (Input.GetKeyUp(KeyCode.Z)){
            sceneController.Load(
              SceneType.TitleScene, 
              (_scene) => {
                  Debug.Log($"Scene [{_scene}] loaded from test script!");
                }, 
              true, 
              PageType.Loading
            );
          }

          if (Input.GetKeyUp(KeyCode.X)) {
            sceneController.Load(SceneType.Stage01);
          }

          if (Input.GetKeyUp(KeyCode.C)) {
            sceneController.Load(SceneType.TitleScene);
          }
        }
      #endif
      #endregion

    }
  }
}
