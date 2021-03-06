﻿using UnityEngine;
using System.Collections;

public class CrossbowController: MonoBehaviour {
  Vector3 moveDirection = Vector3.zero;
  public float accelerationZ;
  public float speedZ;
  Vector3 initialPosition;
  public float bulletLife;

  void Start () {
    initialPosition = transform.position;
  }
  
  void Update () {
    Vector3 nowPosition = transform.position;
    float acceleratedZ = 1 * (moveDirection.z + (accelerationZ * Time.deltaTime));
    moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

    Vector3 globalDirection = transform.TransformDirection(moveDirection);
    transform.position = nowPosition - globalDirection * Time.deltaTime;
  
    float positionZ = (float)initialPosition.z;
    if(transform.position.z - positionZ < -bulletLife){
      Destroy(gameObject);
    }
  }

  void OnTriggerEnter(Collider hit){
    if(hit.gameObject.tag == "Player"){
      hit.gameObject.SendMessage("LifeReduce");
      Destroy(gameObject);
    }
  }
}
