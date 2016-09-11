using UnityEngine;
using System.Collections;

public class VampireBase :  EnemyBase {

public bool direction = false;
//Vector3 initialPosition;
//Vector3 move;

  // Use this for initialization
  void Start () {
    //initialPosition = EnemyBase.transform.localPosition;
    //move = initialPosition;
  }
  
  // Update is called once per frame
  void Update () {
    if(move.y < 0.7f){
      direction = true;
    }else if(move.y > 2.2f){
      direction = false;
    }
    if(direction){
      move.y = move.y + 0.03f;
    }else{
      move.y = move.y - 0.03f;
    }
    transform.localPosition = move;
  }
}
