using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour{
public Vector3 initialPosition;
public Vector3 move;
public int lifePoint;
int downDuaration = 40;

  void Start () {
    initialPosition = transform.localPosition;
    move = initialPosition;
  }

  public bool IsAlive(){
    if(lifePoint <= 0){
      return false;
    }
    return true;
  }
  
  void Update () {
    if(!IsAlive()) {
      downDuaration--;
      if(downDuaration < 0){
        Destroy(this.gameObject);
      }
    }
  }

  public void LifeReduce(int power){
    lifePoint = lifePoint - power;
  }
  
  public virtual void UpdateMove(){}
  public virtual void UpdateAttack(){}
}
