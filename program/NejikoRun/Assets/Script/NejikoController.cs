using UnityEngine;
using System.Collections;

public class NejikoController : MonoBehaviour {

  const int MinLane = -1;
  const int MaxLane = 1;
  const float LaneWidth = 2.0f;
  const int DefaultLife = 1;
  const int MaxShotPower = 6;
  const int ShotRecoverySecond = 3;
  const int ShotWaitTime = 1;
  const float StunDeration = 0.5f;
  const float slideDeration = 2.0f;
  const float shotDuaration = 0.4f;
  const float shotRecoveryDuaration = 0.4f;
  
  CharacterController controller;
  Animator animator;
  //BulletController bullet;
  public GameObject effectPrefab;
  public GameObject bulletPrefab;

  Vector3 moveDirection = Vector3.zero;
  int targetLane;
  int life = DefaultLife;
  int shotPower = MaxShotPower;
  int shotSecond;
  int shotWait = ShotWaitTime;
  int nearMiss = 0;
  float recoverTime = 0.0f;
  float slideTime = 0.0f;

  public float gravity;
  public float speedZ;
  public float speedX;
  public float speedJump;
  public float accelerationZ;

  bool jumpMove = false;

  public int Life(){
    return life;
  }

  public void LifeReduce(){
    life--;
  }

  public int ShotPower(){
    return shotPower;
  }

  public int NearMiss(){
    return nearMiss;
  }

  public void NearMissAdd(){
    nearMiss++;
    if(nearMiss >= 3) nearMiss = 3;
  }

  public void NearMissReset(){
    nearMiss = 0;
  }

  public bool IsStan(){
    return recoverTime > 0.0f || life <= 0;
  }

  public bool IsSlide(){
    return slideTime > 0.0f;
  }

  // Use this for initialization
  void Start () {
    //必要なコンポーネントを自動取得
    controller = GetComponent<CharacterController>();
    animator = GetComponent<Animator>();
  }
  
  // Update is called once per frame
  void Update () {
    if(Input.GetKeyDown("left")) MoveToLeft();
    if(Input.GetKeyDown("right")) MoveToRight();
    if(Input.GetKeyDown("up")) Jump();
    if(Input.GetKeyDown("down")) MoveToSlide();
    if(Input.GetKeyDown("space")) ShotBullet();

    if( shotPower < MaxShotPower && shotSecond <= 0 ){
      StartCoroutine(RecoverShot());
    }

    if(IsStan()){
      //動きを止め気絶状態から復帰カウントを進める
      moveDirection.x = 0.0f;
      moveDirection.z = 0.0f;
      recoverTime -= Time.deltaTime;
      animator.SetTrigger("down");
    }else{
      //徐々に加速しZ方向に常に前進させる
      float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
      moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

      //X方向は目標のポジションまでの差分の割合で速度を計算
      float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
      moveDirection.x =ratioX * speedX;
    }
    if(IsSlide()){
      slideTime -= Time.deltaTime;
    }

    //重力文の力を毎フレーム追加
    if(IsSlide()){
      moveDirection.y -= 5 * gravity * Time.deltaTime;
    }else{
      moveDirection.y -= gravity * Time.deltaTime;
    }

    //移動実行
    Vector3 globalDirection = transform.TransformDirection(moveDirection);
    controller.Move(globalDirection * Time.deltaTime);

    //移動後設置してたらY方向の速度はリセットする
    if(controller.isGrounded){
      //animator.SetBool("jump", false);
      moveDirection.y = 0;
      jumpMove = false;
    }

//    if(!IsSlide()) animator.SetTrigger("slide");

    //速度が0以上なら走っているフラグをtrueにする
    animator.SetBool("run", moveDirection.z > 0.0f);
  }

  //左のレーンに移動を開始
  public void MoveToLeft(){
    if(IsStan() || IsSlide() || jumpMove) return;
    if(targetLane > MinLane) targetLane--;
    if(!controller.isGrounded) jumpMove = true;
  }

  //右のレーンに移動を開始
  public void MoveToRight(){
    if(IsStan() || IsSlide() || jumpMove) return;
    if(targetLane < MaxLane) targetLane++;
    if(!controller.isGrounded) jumpMove = true;
  }

  public void Jump(){
    if(IsStan()) return;
    if(controller.isGrounded){
      animator.SetTrigger("jump");
      //animator.SetBool("jump", true);
      moveDirection.y = speedJump;
    }
  }

  public void MoveToSlide(){
    if(IsStan()) return;
 //   controller.transform.localScale = new Vector3(1.0f, 0.3f, 1.0f);
    animator.SetTrigger("slide");
    slideTime = slideDeration;
  }

  public void ShotBullet(){
    if(IsStan()) return;
    if(shotPower <= 0) return;
    if(shotWait < ShotWaitTime) return;
    
    Vector3 position = transform.position;
      position.z += 2.0f;
    if(!IsSlide()){
      position.y += 1.0f;
    }else{
      position.y += 0.3f;
    }
    GameObject bullet = (GameObject)Instantiate(bulletPrefab, position, Quaternion.identity);
    bullet.SendMessage("SetBulletPower", NearMiss());
    NearMissReset();

    ConsumeShotPower();
  }

  void ConsumeShotPower(){
    shotPower--;
    shotWait--;
    StartCoroutine(RecoverShotTime());
  }

  //CharacterControllerにコリジョンが生じたときの処理
  void OnControllerColliderHit(ControllerColliderHit hit){
    if(IsStan()) return;

    if(hit.gameObject.tag == "Robo"){
      //ライフを減らして気絶状態に移行
      life--;
      recoverTime = StunDeration;

      //ダメージトリガーを設定

      //ヒットしたオブジェクトは削除
      //Destroy(hit.gameObject);
    }
  }

  void OnTriggerEnter(Collider other){
    Vector3 effect_position;
    effect_position = other.transform.position;
    effect_position.z += 2.0f;
    if(other.gameObject.tag == "Robo"){
      NearMissAdd();
      Instantiate(effectPrefab, effect_position, Quaternion.identity);
    }
  }

  IEnumerator RecoverShot(){
    shotSecond = ShotRecoverySecond;

    while(shotSecond > 0){
      yield return new WaitForSeconds(shotRecoveryDuaration);
      shotSecond--;
    }
    shotPower++;
    if(shotPower > MaxShotPower) shotPower = MaxShotPower;
  }

  IEnumerator RecoverShotTime(){
    yield return new WaitForSeconds(shotDuaration);
    shotWait++;
  }
}
