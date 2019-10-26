using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody m_rigid;

    [Header("角色的起始位置")]
    public Vector3 initPos;

    [Header("角色目前的位置")]
    public Vector3 curPos;
	private bool canJump;

    public AudioSource audioSourse;
    public AudioClip pickUp;
    public AudioClip getCoin;

    public AudioClip Attacking;
    public AudioClip playerAttacked;
    public AudioClip enemyAttecked;
    public GameObject healthBar;
    public TextMeshProUGUI scoreText;
    private int score = 0;
    private float health = 1.0f;

    
    Vector3 direction; //發射方向
    public GameObject bullet; //子彈
    GameObject star, gun; //準星 與 槍管
    GameObject BulletPrefab; //用來接生成的物件
    ParticleSystem gunParticles;

    // Use this for initialization
    void Start () {
        m_rigid = this.gameObject.GetComponent<Rigidbody>();
        initPos = this.gameObject.transform.position;
		canJump = true;
        gun = GameObject.Find("Player/Gun");
        star = GameObject.Find("Player/Star");
        // gunParticles = ParticleSystem.Find("Player/GunParticles");
        gunParticles = GameObject.Find("Player/GunParticles").GetComponent<ParticleSystem>();

    }
	
	// Update is called once per frame
	void FixedUpdate(){ 
        float moveVertical = Input.GetAxis ("Vertical");
		float moveHorizontal = Input.GetAxis ("Horizontal");
        float speed = 3.0f;
        if(health == 0.0f){
            SceneManager.LoadScene("End", LoadSceneMode.Single);

        }
        if(canJump && Input.GetKeyDown(KeyCode.Space)){
            m_rigid.AddForce(new Vector3(0, 300, 0));
			canJump = false;
		}

		if(Mathf.Abs(moveVertical) > 0.2f || Mathf.Abs(moveHorizontal) > 0.2f){
        	this.gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(moveHorizontal, 0, moveVertical));
		}
		m_rigid.velocity =  speed * (Vector3.forward * moveVertical + Vector3.right * moveHorizontal) + new Vector3(0, m_rigid.velocity.y, 0);
		m_rigid.angularVelocity = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.R)){
            this.gameObject.transform.position = initPos;

        }
        direction = star.transform.position - gun.transform.position;
        if (Input.GetKeyDown(KeyCode.K) || Input.GetMouseButtonDown(0))
        {
            audioSourse.PlayOneShot(Attacking);
            BulletPrefab = Instantiate(bullet, gun.transform.position, Quaternion.identity);
            BulletPrefab.GetComponent<Rigidbody>().velocity = direction * 70;
            gunParticles.Stop ();
            gunParticles.Play();
        }

    }

    public void LateUpdate(){
        UpdateHealthBar();
    }

    void UpdateHealthBar(){
        //update position
        healthBar.transform.position = transform.position + Vector3.up * 0.8f;
        //update health amount
        healthBar.transform.GetChild(1).GetComponent<Image>().fillAmount = health;
    }
    void OnParticleCollision(GameObject other) {  
        if(other.gameObject.tag == "EnemyFire"){
            audioSourse.PlayOneShot(playerAttacked);
            // this.gameObject.transform.position = initPos;
            health -= 0.1f;
        }
    }    
	void OnCollisionEnter(Collision collision){
		canJump = true;
	}
    public void QuitClick(){
        Debug.Log("Quit");
        SceneManager.LoadScene("End", LoadSceneMode.Single);
    }
    void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Out"){
            audioSourse.PlayOneShot(playerAttacked);
			this.gameObject.transform.position = initPos;
            health -= 0.2f;
        }
		if(other.gameObject.tag == "Enemy1"){
            audioSourse.PlayOneShot(playerAttacked);
            // this.gameObject.transform.position = initPos;
            health -= 0.1f;
        }
        if(other.gameObject.tag == "Enemy2"){
            audioSourse.PlayOneShot(playerAttacked);
            // this.gameObject.transform.position = initPos;
            health -= 0.05f;
        }
        if(other.gameObject.tag == "Medicine"){
            audioSourse.PlayOneShot(pickUp);
            health += 0.1f;
            Destroy(other.gameObject);

        }
		if(other.gameObject.tag == "Box"){
            audioSourse.PlayOneShot(pickUp);
            score++;
            scoreText.text = "Score : " + score.ToString();
			Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "trap1"){
			Destroy(other.gameObject);

        }
        if(other.gameObject.tag == "NextLevel"){
            SceneManager.LoadScene("Game_2", LoadSceneMode.Single);

        }
		if(other.gameObject.tag == "Coin"){
            audioSourse.PlayOneShot(getCoin);
            score+=10;
            scoreText.text = "Score : " + score.ToString();
			Destroy(other.gameObject);
        }
    }
}
