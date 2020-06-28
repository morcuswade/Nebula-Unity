using UnityEngine;

public class TileHorizontal : MonoBehaviour {
  public float parallaxEffect; 
  private GameController game;

  private void Start() {
    game = GameController.Instance;
  }

  private void Update() {
    float distance = game.mapSpeed * parallaxEffect * Time.deltaTime;
    transform.position += Vector3.left * distance;
  }
}
