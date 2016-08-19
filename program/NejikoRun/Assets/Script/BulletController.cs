using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
  Vector3 moveDirection = Vector3.zero;
  public float accelerationZ;
  public float speedZ;
  float initialPosition;
  public int bulletPower = 1;
  GameObject thisBullet;

   public BulletController(GameObject prefab, Vector3 position, int nearMiss){
    Instantiate(prefab, position, Quaternion.identity);
    SetBulletPower(nearMiss);
  }
  
  //public BulletController Instantiate(GameObject prefab, Vector3 position, int nearMiss){
  //  Instantiate(prefab, position, Quaternion.identity) ;
  //  SetBulletPower(nearMiss);
  //  bulletPower = nearMiss;
  //}

  //public void Initialize(GameObject prefab, Vector3 position, int power){
  //  thisBullet = (GameObject)Instantiate(prefab, position, Quaternion.identity);
  //  SetBulletPower(power);
  //  initialPosition = (float)transform.position.z;
  //}

  void Start () {
    ///Instantiate(prefab, position, Quaternion.identity);
    initialPosition = (float)transform.position.z;
    //SetBulletPower(nearMiss);
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
      //EnemyBase enemy = gameObject.GetComponent("hit");
      Destroy(this.gameObject);
    }
  }

  public void SetBulletPower(int power){
    bulletPower = power;
  }
}
