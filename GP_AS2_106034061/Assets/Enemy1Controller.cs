using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    // Start is called before the first frame update

 
	//定义怪物的四种状态：站立、行走、奔跑、无所事事
	public const int STATE_STAND=0;
	public const int STATE_WALK=1;
	public const int STATE_RUN=2;
	
	//怪物当前状态
	private int NowState;
	//游戏角色
	public GameObject Hero;
	//怪物思考时间
	public const int AI_THINK_TIME=2;
	//触发怪物攻击的临界距离
	public const int AI_ATTACT_DISTANCE=10;
	
	//上一次思考的时间
	private float LastThinkTime;

    public AudioSource audioSourse;
    public AudioClip EnemyAttacked;
	// public GameObject EnemyParticles;
	// ParticleSystem EnemyParticlesEF;
    public Object ExplosionEffect;

    void Start()
    {
        // EnemyParticles = GameObject.Find("EnemyParticles");
        // EnemyParticlesEF = GameObject.Find("EnemyParticles").GetComponent<ParticleSystem>();
		// EnemyParticlesEF = EnemyParticles.GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        //当敌人与怪物间的距离小于攻击范围半径的时候
	   if(Vector3.Distance(transform.position,Hero.transform.position)<AI_ATTACT_DISTANCE)
	   {
	      //敌人开始奔跑
	    //   this.GetComponent<Animation>().Play("run");
	      //敌人进入奔跑状态
	      NowState=STATE_RUN;
	      //使敌人面向角色
	      transform.LookAt(Hero.transform);
	      //向玩家靠近
	      transform.Translate(Vector3.forward*Time.deltaTime * 1);
	   }else
	   {
	      //当当前时间与上一次思考时间的差值大于怪物的思考时间时怪物开始思考
	      if(Time.time-LastThinkTime>AI_THINK_TIME)
	      {
	         //开始思考
	         LastThinkTime=Time.time;
	         //获取0-3之间的随机数字
	         int Rnd=Random.Range(0,2);	     
	         //根据随机数值为怪物赋予不同的状态行为
	         switch(Rnd)
	         {
	            case 0:
	            //站立状态
	            // this.GetComponent<Animation>().Play("idle");
	            NowState=STATE_STAND;
	            break;
	    
	            case 1:
	            //行走状态
	            //使怪物旋转以完成行走动作
	            Quaternion mRotation=Quaternion.Euler(0,Random.Range(1,5)*90,0);
	            transform.rotation=Quaternion.Slerp(transform.rotation,mRotation,Time.deltaTime*1000);
	            //播放动画
	            // this.GetComponent<Animation>().Play("walk");
	            //改变位置
	            transform.Translate(Vector3.forward*Time.deltaTime * 3);
	            NowState=STATE_WALK;
	            break;
	           
	            case 2:
	            //奔跑状态
	            // this.GetComponent<Animation>().Play("run");
	            transform.Translate(Vector3.forward*Time.deltaTime * 5);
	            NowState=STATE_RUN;
	            break;
	         } 
	      }
	   }
    }
    void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Bullet"){
            audioSourse.PlayOneShot(EnemyAttacked);
			// EnemyParticles.transform.position = this.gameObject.transform.position;
			// EnemyParticlesEF.Stop();
			// EnemyParticlesEF.Play();
			if (ExplosionEffect != null)
            {
                GameObject exp = GameObject.Instantiate(ExplosionEffect, Vector3.zero, Quaternion.identity) as GameObject;
                exp.transform.position = this.gameObject.transform.position;
                // Destroy after 4 sec
                GameObject.Destroy(exp, 4);
                // Destroy Self
                // GameObject.Destroy(gameObject);                   
            }   
            Destroy(gameObject);
        }
    }
}
