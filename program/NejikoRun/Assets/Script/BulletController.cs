using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
  Vector3 moveDirection = Vector3.zero;
  public float accelerationZ;
  public float speedZ;
  float initialPosition;
  public int bulletPower = 0;

  void Start () {
    initialPosition = (float)transform.position.z;
  }
  
  void Update () {
    Vector3 nowPosition = transform.position;
    float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
    moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

    Vector3 globalDirection = transform.TransformDirection(moveDirection);
    transform.position = nowPosition + globalDirection * Time.deltaTime;
  
    if(transform.position.z - initialPosition > 30.0f){
      Destroy(this.gameObject);
    }
  }

  void OnCollisionEnter(Collision hit){
    if(hit.gameObject.tag == "Robo"){
      Destroy(hit.gameObject);
      Destroy(this.gameObject);
    }
  }

  public void SetBulletPower(int power){
    bulletPower = power;
  }
}
