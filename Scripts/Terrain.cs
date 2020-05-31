using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour {
  public Vector2 terrainVelocity;
  private Rigidbody2D terrainRigidbody;
  private GameController gameController;

  void Start() {
    terrainRigidbody = GetComponent<Rigidbody2D>();
    terrainVelocity = new Vector2(-40f, 0f);
    gameController = GameController.Instance;
  }
  void Update(){
      terrainRigidbody.velocity = gameController.terrainVelocity;
  }

  static public void SetPosition(GameObject terrain, Vector3 position) {
    terrain.transform.position = position;
  }

  void OnCollisionEnter2D(Collision2D col) {
    terrainRigidbody.velocity = gameController.terrainVelocity;
  }
}
