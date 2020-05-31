using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ControlBounds {
  public float xMin, xMax, yMin, yMax;
}

public class Player : MonoBehaviour {
  private StateMachine stateMachine;
  public PlayerIdle idleState;
  public PlayerHurt hurtState;
  public PlayerDead deadState;
  public float speed, tilt;
  public int lives = 3;
  public Vector3 bounceVelocity;
  public float maxBounceTime = 2f;
  public float maxHurtTime = 3f;
  public ControlBounds controlBounds;
  private Camera mainCamera;
  public Vector3 target;
  public float bounceTimer, hurtTimer;
  private new Rigidbody2D playerRigidbody;
  private Animator animator;

  public static Player Instance;

  void Awake() {
    Instance = this;
  }
  
  void Start() {
    playerRigidbody = GetComponent<Rigidbody2D>();
    animator = GetComponentInChildren<Animator>(); 
    stateMachine = gameObject.AddComponent(typeof(StateMachine)) as StateMachine;
    idleState = new PlayerIdle(this, stateMachine);
    hurtState = new PlayerHurt(this, stateMachine);
    deadState = new PlayerDead(this, stateMachine);
    bounceVelocity = Vector3.zero;
    bounceTimer = maxBounceTime;
    hurtTimer = maxHurtTime;
    stateMachine.SetState(idleState);
    mainCamera = Camera.main;
  }

  public void IncrementHurtTimer() {
    hurtTimer += Time.deltaTime;
    if (hurtTimer > maxHurtTime) hurtTimer = maxHurtTime;
  }

  public void IncrementBounceTimer() {
    bounceTimer += Time.deltaTime;
    if (bounceTimer > maxBounceTime) bounceTimer = maxBounceTime;
  }
  
  public void DecrementBounce() {
    bounceVelocity = bounceVelocity - (bounceVelocity * bounceTimer / maxBounceTime);
  }

  public void TargetMouse() {
    Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 30.0f));
    target = new Vector3(
      Mathf.Clamp(mousePosition.x, controlBounds.xMin, controlBounds.xMax), 
      Mathf.Clamp(mousePosition.y, controlBounds.yMin, controlBounds.yMax), 
      0f
    );
  }

  public void MoveToMouse() {
    Vector3 directionToMouse = target - transform.position;
    playerRigidbody.velocity = (directionToMouse + bounceVelocity) * speed * Time.deltaTime;
    DecrementBounce();
    transform.rotation = Quaternion.Euler(Mathf.Clamp(playerRigidbody.velocity.y, -tilt, tilt), 0.0f, 0.0f);
  }

  public void Bounce(Collision2D collizion) {
    bounceTimer = 0f;
    Vector3 bounceDirection = (collizion.collider.bounds.center - collizion.otherCollider.bounds.center) * -1;
    bounceVelocity = bounceDirection * Mathf.Clamp(collizion.rigidbody.velocity.magnitude * 0.05f, 0f, 3f);
  }

  public void Hurt() {
      hurtTimer = 0;
      animator.SetTrigger("Hurt");
      lives--;
  }

  public void Heal() {
    animator.SetTrigger("Idle");
  }
}
