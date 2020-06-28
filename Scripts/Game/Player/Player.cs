using UnityEngine;

public class Player : MonoBehaviour {
  private StateMachine stateMachine;
  public PlayerIdle idleState;
  public PlayerHurt hurtState;
  public PlayerDead deadState;
  public float speed, tilt;
  
  public int lives = 3;
  public Vector3 bounceVelocity;
  public Vector3 playerVelocity;
  public float maxBounceTime = 2f;
  public float maxHurtTime = 3f;
  private bool isMovingUp;
  private bool isMovingDown;
  private float bounceTimer, hurtTimer;
  private Transform m_Transform;
  private Vector3 m_Target;
  private Camera mainCamera;
  private Rigidbody2D playerRigidbody;
  private Animator animator;

  public static Player Instance;
  public Vector3 target {
    get { return m_Target; }
    private set { m_Target = value; }
  }
  public new Transform transform {
    get { return m_Transform; }
    private set { m_Transform = value; }
  }

  private void Awake() {
    Instance = this;
  }
  
  private void Start() {
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
    transform = gameObject.transform;
  }

  private void Update() {
    playerVelocity = playerRigidbody.velocity;
    isMovingDown = playerVelocity.y < 0;
    isMovingUp = playerVelocity.y > 0;
  }

  public float GetBounceTimer() => bounceTimer;
  public float GetHurtTimer() => hurtTimer;
  public void SetTarget(Vector3 t) => target = t;

  public void IncrementHurtTimer() {
    hurtTimer += Time.deltaTime;
    if (hurtTimer > maxHurtTime) hurtTimer = maxHurtTime;
  }

  public void IncrementBounceTimer() {
    if (bounceTimer < maxBounceTime) {
      bounceTimer += Time.deltaTime;
      if (bounceTimer > maxBounceTime) bounceTimer = maxBounceTime;
    }
  }
  
  public void DecrementBounce() {
    if (bounceVelocity == Vector3.zero) return;
    bounceVelocity = bounceVelocity - (bounceVelocity * bounceTimer / maxBounceTime);
  }

  public void TargetMouse() {
    Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 30.0f));
    target = new Vector3(
      mousePosition.x,
      mousePosition.y,
      0f
    );
  }

  public void Move() {
    Vector3 directionToTarget = target - transform.position;
    playerRigidbody.velocity = new Vector3(
      directionToTarget.x,
      directionToTarget.y,
      0
      ) * speed * Time.deltaTime;
    transform.rotation = Quaternion.Euler(Mathf.Clamp(playerRigidbody.velocity.y, -tilt, tilt), 0.0f, 0.0f);
    DecrementBounce();
  }

  public void Bounce(Collision2D collizion) {
    bounceTimer = 0f;
    bounceVelocity = playerVelocity * -GameController.Instance.mapSpeed;
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
