using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {
	public GameObject bullet;  //子彈物件
	public float power;        //子彈速度
	public Transform pos;      //出生點

	public int bulletCount;    //子彈數量上限
	private int nowbulletCount;//目前子彈數量
	public float cd;           //裝子彈時間
	private bool reloading;
	private float nowCD;

	public AudioClip[] au = new AudioClip[2];
	private AudioSource ads;

	public Image bulletUI;
	public Text bulletTxt;

	public bool hasFire;
	public SpriteRenderer sr;

	void Start()
	{
		UpdateBulletUI ();
		ads = GetComponent<AudioSource> ();
	}

	void OnEnable()      
	{
		UpdateBulletUI ();
		nowbulletCount = bulletCount;
		reloading = false;
		nowCD = 0f;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0) && GameManager.timeStop == 1)
		{
			Attack();
		}

		if (reloading && GameManager.timeStop == 1)
			ReLoading();
	}

	public void Attack()
	{
		if (nowbulletCount <= 0) { reloading = true; return;}

		if (hasFire) {
			sr.enabled = true;
			Invoke ("FirePic", 0.2f);
		}

		PlaySound (0);
		nowbulletCount--;
		UdateBulletImg ();
		UpdateBulletUI ();
		GameObject obj = Instantiate(bullet, pos.position, bullet.transform.rotation) as GameObject;
		Rigidbody2D rig = obj.GetComponent<Rigidbody2D>();
		rig.AddForce(pos.right * power);
	}



	//換子彈
	void ReLoading()
	{
		nowCD += Time.deltaTime;
		//print(nowCD);
		if (nowCD >= cd)
		{
			PlaySound (1);
			reloading = false;
			nowCD = 0f;
			nowbulletCount = bulletCount;
			UpdateBulletUI ();
			UdateBulletImg ();
		}
	}

	void PlaySound(int num)
	{
		ads.clip = au [num];
		ads.Play();
	}

	void UpdateBulletUI()
	{
		bulletTxt.text = "" + nowbulletCount;
	}

	void UdateBulletImg()
	{
		bulletUI.fillAmount = (float)nowbulletCount / bulletCount;
	}

	//開槍火焰
	void FirePic()
	{
		sr.enabled = false;
	}
}
