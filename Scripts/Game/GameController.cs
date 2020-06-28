using UnityCore.Session;
using UnityEngine.Tilemaps;
using UnityEngine;

public class GameController : MonoBehaviour {

  public Camera cam;
  public Player player;
  public ControlBounds controlBounds;

  public float mapSpeed;
  public SpriteRenderer[] BackgroundTiles;
  public TilemapCollider2D terrain;

  private SessionController m_Session;
  private float m_MapWidth, m_MapHeight, m_BgWidth, m_BgHeight;


  #region Static Functions
    static public GameController Instance { get; private set; }

    static public void SetPosition(GameObject terrain, Vector3 position) {
      terrain.transform.position = position;
    }
  #endregion
  #region Getters & Setters
    private SessionController session {
      get {
        if (!m_Session) m_Session = SessionController.instance;
        
        if (!m_Session) {
          Debug.LogWarning($"Game is trying to acces the session, but no instance exists");
        }

        return m_Session;
      }
    }
    public float mapWidth {
      get { return m_MapWidth; }
      private set { m_MapWidth = value; }
    }

    public float backgroundHeight {
      get { return m_BgHeight; }
      private set { m_BgHeight = value; }
    }

    public float backgroundWidth {
      get { return m_BgWidth; }
      private set { m_BgWidth = value; }
    }

    public float mapHeight {
      get { return m_MapHeight; }
      private set { m_MapHeight = value; }
    }
  #endregion
  #region Unity Functions
    private void Awake() {
      Instance = this;
    }

    private void Start() {
      if (!session) return;
      session.InitializeGame(this);
    }
  #endregion
  #region Public Functions  
    public void OnInit() {
      SetDimensions();
    }
    public void OnUpdate() {

    }
  #endregion
  #region Private Functions
    private void SetDimensions() {

      Bounds mapBounds = new Bounds(Vector3.zero, Vector3.zero);
      mapBounds.Encapsulate(terrain.bounds);

      mapWidth = mapBounds.size.x;

      Bounds backgroundBounds = new Bounds(Vector3.zero, Vector3.zero);
      foreach (SpriteRenderer tile in BackgroundTiles) {
          backgroundBounds.Encapsulate(tile.bounds);
      }

      backgroundHeight  = backgroundBounds.size.y;
      backgroundWidth   = backgroundBounds.size.x;
    }
  #endregion
}

[System.Serializable]
public class ControlBounds {
  public float xMin, xMax, yMin, yMax;
}
