using UnityEngine;
using System.Collections;

public class UndeadBase : EnemyBase{
  Animator animator;

  void Start () {
    animator = GetComponent<Animator>();
  }
  
  void Update () {
    if(!IsAlive()) {
      GetComponent<CharacterController>().enabled = false;
      int animationSelect = (int)Random.Range(0,2);
      if(animationSelect == 0){
        animator.SetTrigger("down001");
      }else{
        animator.SetTrigger("down002");
      }
    }
  }
}
