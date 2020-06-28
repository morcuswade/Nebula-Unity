using UnityEngine;

public class Bullet : MonoBehaviour {
  public float moveSpeed;
  public float maxLifetime = 2f;
  private Collider2D bulletCollider, playerCollider;
  private Rigidbody2D rbody = new Rigidbody2D();

  void OnEnable() {
    bulletCollider = GetComponent<Collider2D>();
    playerCollider = Player.Instance.gameObject.GetComponent<Collider2D>();
    rbody = GetComponent<Rigidbody2D>();

    Physics2D.IgnoreCollision(bulletCollider, playerCollider);
    rbody.velocity = new Vector2(90.0f, 0.0f);
  }

  void OnCollisionEnter2D(Collision2D col) {
    BulletPool.Instance.ReturnToPool(this);
  }
}