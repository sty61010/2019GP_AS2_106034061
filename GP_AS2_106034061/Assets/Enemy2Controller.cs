using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    // Start is called before the first frame update
    
    public AudioSource audioSourse;
    public AudioClip EnemyAttacked;
    private Transform player;
    public float attackDistance = 2;//这是攻击目标的距离，也是寻路的目标距离
    private Animator animator;
    public float speed;
    private CharacterController cc;
    public float attackTime = 5;   //设置定时器时间 3秒攻击一次
    private float attackCounter = 0; //计时器变量
    // GameObject EnemyParticles;
	// ParticleSystem EnemyParticlesEF;
    public GameObject ExplosionEffect;    
    public GameObject AttackEffect;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // cc = this.GetComponent<CharacterController>();
//         animator = this.GetComponent<Animator>();//控制动画状态机中的奔跑动作调用
        attackCounter = attackTime;//一开始只要抵达目标立即攻击
        // EnemyParticles = GameObject.Find("EnemyParticles");
        // EnemyParticlesEF = GameObject.Find("EnemyParticles").GetComponent<ParticleSystem>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos =player.position;
        targetPos.y = transform.position.y;//此处的作用将自身的Y轴值赋值给目标Y值
        transform.LookAt(targetPos);//旋转的时候就保证已自己Y轴为轴心旋转
        float distance = Vector3.Distance(targetPos,transform.position);
        if (distance <= attackDistance)
        {
            attackCounter += Time.deltaTime;
            if (attackCounter >attackTime)//定时器功能实现
            {
//                 int num = Random.Range(0, 2);//攻击动画有两种，此处就利用随机数（【0】，【1】）切换两种动画
//                 if (num == 0)animator.SetTrigger("Attack1");
//                 else animator.SetTrigger("Attack2");
                GameObject atk = GameObject.Instantiate(AttackEffect, Vector3.zero, Quaternion.identity) as GameObject;
                atk.transform.position = this.gameObject.transform.position;
                // Destroy after 4 sec
                GameObject.Destroy(atk, 2);            
                Debug.Log("Attack");
                attackCounter = 0;
            }
            else
            {
//                 animator.SetBool("Walk", false);
            }
        }
        else
        {
            Debug.Log("Attack");
            attackCounter = attackTime;//每次移动到最小攻击距离时就会立即攻击
//             if(animator.GetCurrentAnimatorStateInfo(0).IsName("EnimyWalk"))//EnimyWalk是动画状态机中的行走的状态
//             cc.SimpleMove(transform.forward*speed);
//             animator.SetBool("Walk ",true);//移动的时候播放跑步动画
            GameObject atk = GameObject.Instantiate(AttackEffect, Vector3.zero, Quaternion.identity) as GameObject;
            atk.transform.position = this.gameObject.transform.position;
            GameObject.Destroy(atk, 3);            
            Debug.Log("Attack");
            attackCounter = 0;
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
                GameObject.Destroy(exp, 3);
                // Destroy Self
                // GameObject.Destroy(gameObject);                   
            }   
            Destroy(gameObject);
        }
    }
}
