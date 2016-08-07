using UnityEngine;
using System.Collections;

public class EnemyBase {
int initialX;
int initialY;
int initialZ;
  void Start () {
    initialX = transform.position.x;
    initialY = transform.position.y;
    initialZ = transform.position.z;
  }
  
  void Update () {
  
  }
}
