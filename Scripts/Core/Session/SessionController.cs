using UnityEngine;
using UnityCore.Page;

namespace UnityCore {
  namespace Session {
    public class SessionController : MonoBehaviour {
      public static SessionController instance;

      private long m_SessionStartTime;
      private bool m_IsPaused;
      private GameController m_Game;
      private float m_FPS;

      public long sessionStartTime {
        get {
          return m_SessionStartTime;
        }
      }

      public float fps {
        get {
          return m_FPS;
        }
      }

      #region Unity Functions
        private void Awake() {
          Configure();
        }

        private void OnApplicationFocus(bool _focus) {
          if (_focus) {
            // PageController.instance.TurnPageOn(PageType.Paused);
          } else {
            m_IsPaused = true;
          }
        }

        private void Update() {
          if (m_IsPaused) return;
          if (!m_Game) return;
          m_Game.OnUpdate();
          m_FPS = Time.frameCount / Time.time;
        }
      #endregion
      
      #region Public Functions
        public void InitializeGame(GameController _game) {
          Debug.Log($"Initializing game.");
          m_Game = _game;
          m_Game.OnInit();
        }
        public void UnPause() {
          PageController.instance.TurnPageOff(PageType.Paused);
          m_IsPaused = false;
        }
      #endregion
      
      #region Private Functions
        private void Configure() {
          if (!instance) {
            instance = this;
            StartSession();
            DontDestroyOnLoad(gameObject);
          } else {
            Destroy(gameObject);
          }
        }

        private void StartSession() {
          m_SessionStartTime = EpochSeconds();
        }

        private long EpochSeconds() {
          var _epoch = new System.DateTimeOffset(System.DateTime.UtcNow);
          return _epoch.ToUnixTimeSeconds();
        }
      #endregion
       
    }
  }
}

