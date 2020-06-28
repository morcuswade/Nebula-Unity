using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : PlayerIdle {  

  public PlayerHurt(Player player, StateMachine stateMachine) : base(player, stateMachine) {}
  
  public override void OnEnter() {
    _player.Hurt();
    if (_player.lives < 1) _stateMachine.SetState(_player.deadState);
  }

  public override void HandleUpdate() {
    base.HandleUpdate();

    _player.IncrementHurtTimer();
    if (_player.GetHurtTimer() >= _player.maxHurtTime) {
      _stateMachine.SetState(_player.idleState);
    }
  }

  public override void HandlePhysics() {
    base.HandlePhysics();
  }

  public override void HandleCollisions(Collision2D collizion) {
    _player.Bounce(collizion);
  }

  public override void HandleInput() {
    base.HandleInput();
  }

  public override void OnExit() {
    base.OnExit();
  }
}