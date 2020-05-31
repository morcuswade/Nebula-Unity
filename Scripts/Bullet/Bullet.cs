using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
  public float moveSpeed;
  private float lifeTime;
  public float maxLifetime = 2f;
  private Collider2D bulletCollider, playerCollider;
  private Rigidbody2D rigidbody;

  void OnEnable() {
    lifeTime = 0f;
    bulletCollider = GetComponent<Collider2D>();
    playerCollider = Player.Instance.gameObject.GetComponent<Collider2D>();
    rigidbody = GetComponent<Rigidbody2D>();

    Physics2D.IgnoreCollision(bulletCollider, playerCollider);
    rigidbody.velocity = new Vector2(90.0f, 0.0f);
  }

  void OnCollisionEnter2D(Collision2D col) {
    BulletPool.Instance.ReturnToPool(this);
  }
}