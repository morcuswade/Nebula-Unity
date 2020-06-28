using UnityEngine;

public class CameraController : MonoBehaviour {
  public Player player; 
  private Rigidbody2D pRigidbody;
  private GameController gameController;
  public float maxCamDistance;

  private void Start() {
    gameController = GameController.Instance;
    pRigidbody = player.GetComponent<Rigidbody2D>();
  }

  private void Update() {
    FollowPlayerY();
  }

  private void FollowPlayerY() {
    float distance = (player.target.y - player.transform.position.y) * .5f;
    distance = Mathf.Clamp(distance, -maxCamDistance, maxCamDistance);
    transform.position = new Vector3(
      transform.position.x + gameController.mapSpeed * Time.deltaTime, 
      player.transform.position.y + distance, 
      transform.position.z);
  }

}
