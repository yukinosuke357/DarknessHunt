using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
  Vector3 moveDirection = Vector3.zero;
  public float accelerationZ;
  public float speedZ;
  float initialPosition;
  public int bulletPower = 1;
  public float bulletLife;
  GameObject thisBullet;
  public GameObject character;

  void Start () {
    initialPosition = (float)transform.position.z;
  }
  
  void Update () {
    Vector3 nowPosition = transform.position;
    float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
    moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

    Vector3 globalDirection = transform.TransformDirection(moveDirection);
    transform.position = nowPosition + globalDirection * Time.deltaTime;
  
    if(transform.position.z - initialPosition > bulletLife){
      Debug.Log("near" + bulletPower );
      Destroy(gameObject);
    }
  }

  void OnCollisionEnter(Collision hit){
    if(hit.gameObject.tag == "Robo"){
      hit.gameObject.SendMessage("LifeReduce", bulletPower);
      Destroy(gameObject);
    }
  }

  public void SetBulletPower(int power){
    bulletPower = power + 1;
  }
}
