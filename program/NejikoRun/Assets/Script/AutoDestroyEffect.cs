using UnityEngine;
using System.Collections;

public class AutoDestroyEffect : MonoBehaviour {
  ParticleSystem particle;

  void Start () {
    particle = GetComponent<ParticleSystem>();
  }
  
  void Update () {
    if(particle.isPlaying == false) Destroy(gameObject);
  }
}
