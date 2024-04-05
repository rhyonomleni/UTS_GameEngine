using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public int force;
    int scoreKuning;
    int scoreBiru;
    Text scoreUIKuning;
    Text scoreUIBiru;
    Text scoreUIKuning2;
    Text scoreUIBiru2;
    GameObject panelSelesai;
    Text Winner;

    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        scoreKuning = 0;
        scoreBiru = 0;
        rigid = GetComponent<Rigidbody2D>();
        Vector2 arah = new Vector2(0,2).normalized;
        rigid.AddForce(arah*force);
        scoreUIKuning = GameObject.Find("ScoreKuning").GetComponent<Text>();
        scoreUIBiru = GameObject.Find("ScoreBiru").GetComponent<Text>();
        scoreUIKuning2 = GameObject.Find("ScoreKuning2").GetComponent<Text>();
        scoreUIBiru2 = GameObject.Find("ScoreBiru2").GetComponent<Text>();
        panelSelesai = GameObject.Find("PanelSelesai");
        panelSelesai.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void TampilkanScore(){
        Debug.Log("Score Kuning: "+scoreKuning+" Score Biru: "+scoreBiru);
        scoreUIKuning.text=scoreKuning + "";
        scoreUIBiru.text=scoreBiru + "";
        scoreUIKuning2.text=scoreKuning + "";
        scoreUIBiru2.text=scoreBiru + "";
    }

    void OnCollisionEnter2D (Collision2D coll){
        if(coll.gameObject.name == "GoalAtas"){
            scoreBiru += 1;
            TampilkanScore();
            if (scoreBiru == 5)
            {
                panelSelesai.SetActive(true);
                Winner = GameObject.Find("Info").GetComponent<Text>();
                Winner.text = "Player Biru Menang";
                Destroy (gameObject);
                return;
            }
            ResetBola();    
            Vector2 arah = new Vector2(0,2).normalized;
            rigid.AddForce(arah * force);        
        }        
        if(coll.gameObject.name == "GoalBawah"){
            scoreKuning += 1;
            TampilkanScore();
            if (scoreKuning == 5)
            {
                panelSelesai.SetActive(true);
                Winner = GameObject.Find("Info").GetComponent<Text>();
                Winner.text = "Player Kuning Menang";
                Destroy (gameObject);
                return;
            }
            ResetBola();
            Vector2 arah = new Vector2(0,-2).normalized;
            rigid.AddForce(arah * force);
            
        }
        if (coll.gameObject.name == "PaddleAtas" || coll.gameObject.name == "PaddleBawah")
        {
            float sudut = (transform.position.x - coll.transform.position.x) *5f;
            Vector2 arah = new Vector2(sudut, rigid.velocity.y).normalized;
            rigid.velocity = new Vector2(0,0);
            rigid.AddForce(arah*force*2);
        }
    }

    void ResetBola(){
        transform.localPosition = new Vector2(0,0);
        rigid.velocity = new Vector2(0,0);
    }
}
