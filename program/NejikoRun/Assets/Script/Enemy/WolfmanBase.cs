using UnityEngine;
using System.Collections;

public class WolfmanBase : EnemyBase{
bool   player_searched = false;
public GameObject CheckEnemy;
GameObject check =null;
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
    if(player_searched){
      move.z = move.z + 0.1f;
      transform.localPosition = move;
    }
  }

  public override void UpdateAttack(){
    Vector3 position = transform.position;
    position.z -= 2.0f;
    position.y += 1.0f;
    if(Xflag() && Yflag()){
      if(check == null){
        check = (GameObject)Instantiate(CheckEnemy, position, Quaternion.identity);
      }
      check.transform.parent = this.transform;
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

  void checkBetweenPlayer( int check){
    player_searched = true;
  }

}
