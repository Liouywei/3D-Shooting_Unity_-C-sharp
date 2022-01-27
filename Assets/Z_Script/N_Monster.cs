using UnityEngine;
using System.Collections;

public class N_Monster : MonoBehaviour {

	public int moveSpeed;
	public int attackTime;  //攻擊間隔時間
	public int hp;
	private int nowhp;

	private BoxCollider bc;
	public BoxCollider attackCollider;  //攻擊用的Trigger
	private bool moving;
	private bool attacking;

	private float timmer = 3f;
	private float nowTime;

	public GameObject monsterObj;
	public ParticleSystem pr;

	void Start()
	{
		nowhp = hp;
		bc = GetComponent<BoxCollider> ();
		attackCollider.enabled = false;    //移動中不需要攻擊
		moving = true;
		attacking = false;
		nowTime = timmer;
	}


	void Update () {
		if (N_GM.n_gm.playing)
		{
			if (moving == true){
				Move ();
			}else if (attacking && nowTime > 0) {
				nowTime -= Time.deltaTime;
				if (nowTime <= 0) {
					attacking = false;
					StartAttack ();
				}
			}
		}
	}
		
	void StartAttack()
	{
		//Debug.Log ("Start Monster Attack");
		attackCollider.enabled = true;  //開啟Trigger
		Invoke("FinishAttack", 1f);
	}

	void FinishAttack()
	{
		//Debug.Log ("Finish Monster Attack"); 
		attackCollider.enabled = false; //停止Trigger
		nowTime = timmer;
		attacking = true;
	}

	void Move()
	{
		transform.Translate (Vector3.left * moveSpeed * Time.deltaTime);
	}

	//怪物受到傷害
	public void Hurt(int att)
	{
		nowhp -= att;

		if (nowhp <= 0) {
			moving = false;
			Dead ();
		}
	}

	//怪物被擊倒
	void Dead()
	{
		bc.enabled = false;
		monsterObj.SetActive (false);
		pr.Play ();
		CoinAdd ();
		N_GM.n_gm.KillMonsterNum ();
		Invoke ("DleteObj",1.5f);
	}

	//刪除物件
	void DleteObj()
	{
		Destroy(gameObject);
	}

	//怪物就定位，開始攻擊
	void OnTriggerEnter(Collider hit)
	{
		if (hit.tag == "Defenes")
		{
			//print ("In");
			moving = false;
			attacking = true;
		}
	}

	//怪物被擊殺後，玩家得到金錢
	void CoinAdd()
	{
		N_GM.n_gm.MoneyAdd (10);
	}
}
