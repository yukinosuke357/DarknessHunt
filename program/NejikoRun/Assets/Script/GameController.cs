using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
  public NejikoController nejiko;
  public Text scoreLabel;
  public LifePanel lifePanel;

  void Update () {
    //スコアラベルを更新
    int score = CalcScore();
    scoreLabel.text = "Score : " + score + "m";

    //ライフパネルを更新
    lifePanel.UpdateLife(nejiko.Life());

    //ねじ子のライフが0になったらゲームオーバー
    if(nejiko.Life() <= 0){
      //これ以降のUpdateは止める
      enabled = false;

      //ハイスコアを更新
      if(PlayerPrefs.GetInt("HighScore") < score){
        PlayerPrefs.SetInt("HighScore", score);
      }

      //2秒後にReturnToTitleを呼び出す
      Invoke("ReturnToTitle", 2.0f);
    }
  }

  int CalcScore(){
    return (int)nejiko.transform.position.z;
  }

  void ReturnToTitle(){
    //タイトルシーンに切り替え
    SceneManager.LoadScene("Title");
  }
}
