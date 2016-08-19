using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour{
Vector3 initialPosition;
Vector3 move;
public int lifePoint = 1;

  void Start () {
    initialPosition = transform.localPosition;
    move = transform.localPosition;
  }

  //public bool IsAlive(){
  //  if(lifePoint <= 0){
  //    return false;
  //  }
  //  return true;
  //}
  
  void Update () {
    //if(IsAlive) Destroy(this.gameObject);
    //transform.localPosition = move;
    //move.z = move.z + 0.2f;
  }

  public void LifeReduce(){
    lifePoint--;
  }
}
