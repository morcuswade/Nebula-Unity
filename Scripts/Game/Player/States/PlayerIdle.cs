using UnityEngine;

public class PlayerIdle : BaseState {

  public PlayerIdle(Player player, StateMachine stateMachine) : base(player, stateMachine) { }
  public override void OnEnter() {
    // _player.Heal();
  }

  public override void HandlePhysics() {
    _player.Move();
    if (_player.bounceVelocity.x != 0 || _player.bounceVelocity.y != 0) _player.DecrementBounce();
  }

  public override void HandleCollisions(Collision2D collizion) {
    _player.Bounce(collizion);
    _stateMachine.SetState(_player.hurtState);
  }

  public override void HandleInput() {
    _player.TargetMouse();
  }

  public override void HandleUpdate() {
    if (_player.GetBounceTimer() < _player.maxBounceTime) _player.IncrementBounceTimer();
  }

  public override void OnExit() {}
}
