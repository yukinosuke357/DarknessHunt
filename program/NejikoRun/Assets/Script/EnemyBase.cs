using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour{
public Vector3 initialPosition;
public Vector3 move;
public int lifePoint;

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
    if(!IsAlive()) Destroy(this.gameObject);
  }

  public void LifeReduce(int power){
    lifePoint = lifePoint - power;
    Debug.Log("damage" + power + "life" + lifePoint );
  }
}
