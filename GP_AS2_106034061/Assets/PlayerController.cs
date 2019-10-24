using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    
    public GameObject healthBar;
    public TextMeshProUGUI scoreText;
    private int score = 0;
    private float health = 1.0f;

    // Use this for initialization
    void Start () {
        m_rigid = this.gameObject.GetComponent<Rigidbody>();
        initPos = this.gameObject.transform.position;
		canJump = true;
    }
	
	// Update is called once per frame
	void FixedUpdate(){ 
        float moveVertical = Input.GetAxis ("Vertical");
		float moveHorizontal = Input.GetAxis ("Horizontal");
        float speed = 3.0f;
        if(canJump && Input.GetKeyDown(KeyCode.Space)){
            m_rigid.AddForce(new Vector3(0, 200, 0));
			canJump = false;
		}

		if(Mathf.Abs(moveVertical) > 0.2f || Mathf.Abs(moveHorizontal) > 0.2f){
        	this.gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(moveHorizontal, 0, moveVertical));
		}
		m_rigid.velocity =  speed * (Vector3.forward * moveVertical + Vector3.right * moveHorizontal) + new Vector3(0, m_rigid.velocity.y, 0);
		m_rigid.angularVelocity = Vector3.zero;

		
        if (Input.GetKeyDown(KeyCode.R))
            this.gameObject.transform.position = initPos;



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

	void OnCollisionEnter(Collision collision){
		canJump = true;
	}

    void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Out"){
			this.gameObject.transform.position = initPos;
            health -= 0.2f;
        }

		if(other.gameObject.tag == "Box"){
            audioSourse.PlayOneShot(pickUp);
            score++;
            scoreText.text = "Score : " + score.ToString();
			Destroy(other.gameObject);
        }
    }
}
