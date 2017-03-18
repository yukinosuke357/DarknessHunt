using UnityEngine;
using System.Collections;

public class VampireBase :  EnemyBase {

public bool direction = false;
public GameObject crossBow;
bool shotFlag = true;
public GameObject[] Player;
public int playerDistanceX = 1;
public int playerDistanceZ = 20;

  void Start () {
    Player = GameObject.FindGameObjectsWithTag("Player"); 
  }
  
  void Update () {
    UpdateMove();
    UpdateAttack();
  }

  public override void UpdateMove(){
    if(move.y < 0.7f){
      direction = true;
    }else if(move.y > 2.2f){
      direction = false;
    }
    int moveSpeed = Random.Range(0, 10);
    float moveSpeedf = (float)moveSpeed / 100.0f;
    if(direction){
      move.y = move.y + moveSpeedf;
    }else{
      move.y = move.y - moveSpeedf;
    }
    transform.localPosition = move;
  }

  public override void UpdateAttack(){
    Vector3 position = transform.position;
    position.z -= 2.0f;
    position.y += 1.0f;
    if(Xflag() && Yflag()){
      if(shotFlag){
        GameObject crossbow = (GameObject)Instantiate(crossBow, position, Quaternion.identity);
        shotFlag = false;
      }
    }
  }

  bool Xflag(){
    if(Mathf.Abs(Player[0].transform.position.x - transform.position.x) < playerDistanceX){
      return true;
    }
    return false;
  }
  
  bool Yflag(){
    if(Mathf.Abs(Player[0].transform.position.z - transform.position.z) < playerDistanceZ){
      return true;
    }
    return false;
  }
}
