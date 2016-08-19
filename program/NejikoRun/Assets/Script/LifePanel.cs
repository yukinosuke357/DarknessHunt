﻿using UnityEngine;
using System.Collections;

public class LifePanel : MonoBehaviour {

  public GameObject[] icons;
  
  //ライフに応じてスプライトを出しわける
  public void UpdateLife (int life){
    for (int i = 0; i< icons.Length; i++){
      if(i < life) icons[i].SetActive(true);
      else icons[i].SetActive(false);
    }
  }
}
