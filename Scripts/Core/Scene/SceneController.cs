using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityCore.Page;

namespace UnityCore {
  namespace Scene {
    public class SceneController : MonoBehaviour {
      public delegate void SceneLoadDelegate(SceneType _scene);

      public static SceneController instance;

      public bool debug;

      private PageController m_PageController;
      private SceneType m_TargetScene;
      private PageType m_LoadingPage;
      private SceneLoadDelegate m_SceneLoadDelegate;
      private bool m_SceneIsLoading;
      
      private PageController pageController {
        get {
          if (m_PageController == null) {
            m_PageController = PageController.instance;
          }
          if (m_PageController == null) {
            LogWarning($"You are trying to access the PageController, but no instance was found");
          }
          return m_PageController;
        }
      }

      private string currentSceneName {
        get {
          return SceneManager.GetActiveScene().name;
        }
      }

      #region Unity Functions
        private void Awake() {
          if (!instance) {
            Configure();
            DontDestroyOnLoad(gameObject);
          } else {
            Destroy(gameObject);
          }
        }

        private void OnDisable() {
          Dispose();
        }
      #endregion

      #region Public Functions
        public void Load(
          SceneType _scene,
          SceneLoadDelegate _sceneLoadDelegate=null,
          bool _reload=false,
          PageType _loadingPage=PageType.None) {
          
            if (_loadingPage != PageType.None && !pageController) {
              Log($"Scene [{_scene}] could not be loaded, pageController: {pageController}");
              return;
            }

            if (!SceneCanBeLoaded(_scene, _reload)) {
              LogWarning($"Scene [{_scene}] could not be loaded");
              return;
            }

            m_SceneIsLoading = true;
            m_TargetScene = _scene;
            m_LoadingPage = _loadingPage;
            m_SceneLoadDelegate = _sceneLoadDelegate;
            StartCoroutine("LoadScene");
        }
      #endregion

      #region Private Functions
        private void Configure() {
          instance = this;
          SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void Dispose() {
          SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private async void OnSceneLoaded(UnityEngine.SceneManagement.Scene _scene, LoadSceneMode _mode) {
          if (m_TargetScene == SceneType.None) return;

          SceneType _sceneType = StringToSceneType(_scene.name);
          if (m_TargetScene != _sceneType) return;

          if (m_SceneLoadDelegate != null) {
            try {
              m_SceneLoadDelegate(_sceneType);
            } catch (System.Exception) {
              LogWarning($"Unable to respond with sceneLoadDelegate after scene [{_sceneType}] loaded");
            }
          }

          await Task.Delay(1000);
          pageController.TurnPageOff(m_LoadingPage);

          m_SceneIsLoading = false;
        }

        private IEnumerator LoadScene() {
          if (m_LoadingPage != PageType.None) {
            pageController.TurnPageOn(m_LoadingPage);
            while (!pageController.PageIsOn(m_LoadingPage)) {
              yield return null;
            }
          } 

          string _targetSceneName = SceneTypeToString(m_TargetScene);
          SceneManager.LoadScene(_targetSceneName);
        }

        private bool SceneCanBeLoaded(SceneType _scene, bool _reload){
          string _targetSceneName = SceneTypeToString(_scene);
          if (currentSceneName == _targetSceneName && !_reload) {
            LogWarning($"You are trying to load a scene [{_scene}] that is already active");
            return false;
          } else if (_targetSceneName == string.Empty) {
            LogWarning($"The scene you are trying to load [{_scene}] is not valid");
            return false;
          } else if (m_SceneIsLoading) {
            LogWarning($"Unable to load scene [{_scene}]. Another scene [{m_TargetScene}] is already loading.");
          }

          return true;
        }

        private string SceneTypeToString(SceneType _scene) {
          switch (_scene ) {
            case SceneType.Stage01: return "Stage01";
            case SceneType.TitleScene: return "TitleScene";
            default:
              LogWarning($"Scene [{_scene}] does not contain a string for a valid scene.");
              return string.Empty;
          }
        }

        private SceneType StringToSceneType(string _scene) {switch (_scene ) {
            case "Stage01": return SceneType.Stage01;
            case "TitleScene": return SceneType.TitleScene;
            default:
              LogWarning($"Scene [{_scene}] does not contain a type for a valid scene.");
              return SceneType.None;
          }
        }

        private void Log(string _msg) {
          if (!debug) return;
          Debug.Log($"[SceneController]: {_msg}");
        }

        private void LogWarning(string _msg) {
          if (!debug) return;
          Debug.LogWarning($"[SceneController]: {_msg}");
        }
      #endregion

    }
  }
}
