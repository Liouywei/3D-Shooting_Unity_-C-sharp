using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	public int moveSpeed;
	private Rigidbody2D rig;
	private Animator am;

	//武器物件
	private GameObject hand;
	private Transform h_tra;
	public GameObject[] weapon;
	//目前使用的武器
	private int nowWeapon = 0;

	void Start()
	{
		//設定初始武器
		hand = weapon[0];
		h_tra = hand.transform; 

		rig = GetComponent<Rigidbody2D>();
		am = GetComponent<Animator> ();
	}

	void Update () {
		if (GameManager.timeStop == 1) {
			Move ();
			LookMouse();
		}
	}
		
	void Move()
	{
		if (Input.GetKey (KeyCode.W)) {
			am.SetBool ("Walk", true);
			rig.AddForce (Vector2.up * moveSpeed);
		} else if (Input.GetKey (KeyCode.S)) {
			am.SetBool ("Walk", true);
			rig.AddForce (-Vector2.up * moveSpeed);
		} else if (Input.GetKey (KeyCode.A)) {
			am.SetBool ("Walk", true);
			rig.AddForce (Vector2.left * moveSpeed);
		} else if (Input.GetKey (KeyCode.D)) {
			am.SetBool ("Walk", true);
			rig.AddForce(-Vector2.left * moveSpeed);
		}
		else {
			am.SetBool ("Walk", false);
		}
	}

	void LookMouse()
	{
		if (hand == null) return;

		Vector2 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float angle = Mathf.Atan2(temp.y - h_tra.position.y, temp.x - h_tra.position.x) * Mathf.Rad2Deg; ;
		hand.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}

	public void ChangeWeapon(int num)
	{
		if (num == nowWeapon) return;

		hand = weapon[num];
		weapon[nowWeapon].SetActive(false);
		weapon[num].SetActive(true);
		nowWeapon = num;
	} 
}
