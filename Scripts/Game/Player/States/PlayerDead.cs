using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : BaseState { 

  public PlayerDead(Player player, StateMachine stateMachine) : base(player, stateMachine) {}
  
  public override void OnEnter() {
  }

  public override void HandleUpdate() {
  }

  public override void HandlePhysics() {
  }

  public override void HandleCollisions(Collision2D collizion) {
  }

  public override void HandleInput() {
  }

  public override void OnExit() {
  }
}