using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class N_Weapon : MonoBehaviour {

	public GameObject bullet;  //子彈物件
	public float speed;        //子彈速度
	public Transform pos;      //出生點

	public int bulletCount;    //子彈數量上限
	private int nowbulletCount;//目前子彈數量
	private bool reloading;
	public float shootingCD;   //射擊間隔時間
	private float nowCD;

	public UI_PlayerControl ui_PC; //玩家腳本
	public string weaponName;
	public Text text_WeaponName;
	public Text bulletTxt;     

	void OnEnable()      
	{
		text_WeaponName.text = weaponName;   //改變武器名字
		nowbulletCount = bulletCount;
		UpdateBulletUI ();                   //更新子彈數量
		reloading = false;    
		nowCD = shootingCD;
	}

	void Update()
	{
		if (reloading == true) {
			nowCD -= Time.deltaTime;
			if (nowCD <= 0)
			{
				reloading = false;
				nowCD = shootingCD;
			}
		}
	}

	void Attack()
	{
		if (reloading == true) {return;}

		//Debug.Log ("Shooting~~");
		if (weaponName != "Melee"){nowbulletCount--;}  //減少子彈數量
		UpdateBulletUI (); //更新子彈UI
		reloading = true;  //武器射擊間隔時間，期間不能再次射擊直到冷卻結束
		//生成子彈
		//bullet.transform.rotation
		GameObject obj = Instantiate(bullet, pos.position, pos.transform.rotation) as GameObject;
		Rigidbody rig = obj.GetComponent<Rigidbody>();
		rig.AddForce(pos.right * speed);


		if (nowbulletCount <= 0) {ui_PC.ChangeWeapon (0); return;} //子彈用光，切回近戰
	}
		
	//更新子彈數量
	void UpdateBulletUI()   
	{
		bulletTxt.text = nowbulletCount + "/" + bulletCount;
	}
}
