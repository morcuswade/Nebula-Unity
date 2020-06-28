using UnityEngine;

namespace UnityCore {
  namespace Audio {
    public class TestAudio : MonoBehaviour {
      public AudioController audioController;
      #region Unity Functions
      #if UNITY_EDITOR
        private void Update() {
          if (Input.GetKeyUp(KeyCode.A)) {
            audioController.PlayAudio(AudioType.ST_01, true, 1f);
          }
          if (Input.GetKeyUp(KeyCode.S)) {
            audioController.StopAudio(AudioType.ST_01, true);
          }
          if (Input.GetKeyUp(KeyCode.D)) {
            audioController.RestartAudio(AudioType.ST_01, true);
          }
          if (Input.GetKeyUp(KeyCode.Z)) {
            audioController.PlayAudio(AudioType.SFX_01);
          }
          if (Input.GetKeyUp(KeyCode.X)) {
            audioController.StopAudio(AudioType.SFX_01);
          }
          if (Input.GetKeyUp(KeyCode.C)) {
            audioController.RestartAudio(AudioType.SFX_01);
          }
        }
      #endif
      #endregion
    }
  }
}

