using UnityEngine;
using System.Collections;

public class UI_PlayerControl : MonoBehaviour {
	public Transform[] movePoint = new Transform[3]; //玩家移動的三個座標
	private Transform playerPos; //玩家Object的座標
	private int currentPos;  //玩家現在位置 0:left  1:center  2:right

	//武器
	private GameObject hand;  //目前使用的武器Object
	public GameObject[] weapon = new GameObject[5]; //所有武器
	private int nowWeapon = 0; //目前使用的武器編號

	void Start()
	{
		currentPos = 1;
		playerPos = transform; //取得玩家Object座標
		playerPos.position = movePoint [1].position;  //重設玩家位置到中間

		nowWeapon = 1;    //設定初始武器
		hand = weapon[1]; //啟動初始武器
	}
		
	//切換武器
	public void ChangeWeapon(int id)
	{
		weapon [nowWeapon].SetActive (false);  //關閉原本武器
		nowWeapon = id;                        //切換武器編號
		hand = weapon[nowWeapon];
		hand.SetActive(true);                  //開啟新的武器
	}

	//按鈕往左移
	public void MoveLeft() 
	{
		if (!N_GM.n_gm.playing) {return;}

		if (currentPos > 0)
		{
			currentPos--;
			playerPos.position = movePoint [currentPos].position;
		}
	}

	//按鈕往右移
	public void MoveRight() 
	{
		if (!N_GM.n_gm.playing) {return;}

		if (currentPos < 2)
		{
			currentPos++;
			playerPos.position = movePoint [currentPos].position;
		}
	}

	//按鈕攻擊
	public void Attack()
	{
		if (!N_GM.n_gm.playing) {return;}

		hand.SendMessage ("Attack");
	}

}
