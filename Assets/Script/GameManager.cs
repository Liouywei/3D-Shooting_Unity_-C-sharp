using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager gm;

	private int monsterCount;   //場上怪物數量
	private bool[] weaponBuy = new bool[6]; //武器是否過買過了
	public static int timeStop = 1;   // 1 遊戲開始或 0 暫停

	public PlayerControl pc;    //玩家腳本
	public Weapon[] wp;         //武器腳本
	public GameObject img_Pad;
	public int money;

    //產生怪物
    public GameObject[] monsters;
    public Transform[] spawn;
    public float times;     //產生怪物的間隔
    private float nowtimes; //現在的時間間隔
    public Text hinto;       //提示現在幾秒生怪

    //遊戲結束
    public GameObject winPic;
    public GameObject losePic;

    //金錢
    public Text coinTxt;

    //Effect
    public Animator tank;
    public Animator plane;

	public AudioClip[] au = new AudioClip[2];
	private AudioSource ads;

	//勝負
	public int killMaxCount;   //勝利條件 擊殺多少隻
	private int killCount;     //擊殺數量 

    void Start()
    {
		gm = this;
		timeStop = 1;
        ads = GetComponent<AudioSource> ();
        InvokeRepeating("UpdateNewValue", 3, 3);
        MoneyUpdate(0);
    }

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
			OpenPad();

        TimesValue();
    }


	//商店UI
	//==============================================

	//打開選單
	void OpenPad()
	{
		if (timeStop == 1) {
			timeStop = 0;
			PlaySound (1);
		} else {
			timeStop = 1;
			PlaySound (0);
		}

		Time.timeScale = timeStop;
		img_Pad.SetActive(!img_Pad.activeSelf);
	}

	void PlaySound(int num)
	{
		ads.clip = au [num];
		ads.Play();
	}
		
	//點選商品按鈕
	public void ItemButton(ItemClick data)
	{
		if (weaponBuy [data.id])
		{
			//執行 使用
			pc.ChangeWeapon(data.id);
		}
		else
		{
			if (money >= data.price)
			{
                //音效         
                weaponBuy [data.id] = true;
                MoneyUpdate(-data.price);
                data.btnTxt.text = "使用";
				pc.ChangeWeapon(data.id);
			}
		}
	}

    public void SupportButton(SupportClick sc)
    {
        if (money >= sc.price)
        {
            //音效         
            MoneyUpdate(-sc.price);
            OpenPad();
            MapAttack(sc.id);
        }
    }
    //==============================================


    //產生怪物
    //==============================================

    void UpdateNewValue()
    {
        if (nowtimes == times)
            return;
        else
            nowtimes = times;

        print("new Time: " + times);
        CancelInvoke("CreateMonster");
        InvokeRepeating("CreateMonster", 0, times);
    }

    void TimesValue()
    {
		if (Input.GetKeyDown(KeyCode.O))
        {
            times += 0.5f;
            HinToMessage();
        }   

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (times - 0.5f > 0)
                times -= 0.5f;
            else
                times = 0.5f;

            HinToMessage();
        }
    }

    void CreateMonster()
    {
		if (timeStop == 0)
			return;

        int temp = Random.Range(0, spawn.Length);
        int mon = Random.Range(0, monsters.Length);
        Instantiate(monsters[mon], spawn[temp].position, Quaternion.identity);
    }

    //提示現在幾秒生怪
    void HinToMessage()
    {
        hinto.enabled = true;
        hinto.text = "" + times;
        Invoke("HinToMessageHide", 0.8f);
    }
    
    //隱藏提示
    void HinToMessageHide()
    {
        hinto.enabled = false;
    }
    //==============================================

    public void GameEnd(int x)
    {
		timeStop = 0;

        if(x == 1)
        {
			winPic.SetActive(true);
        }

        if (x == 0)
        {
            losePic.SetActive(true);
        }
    }

    public void MoneyUpdate(int coine)
    {
        money += coine;
        coinTxt.text = money.ToString();
    }


    //Support功能
    void MapAttack(int id)
    {
        if (id == 1)
        {
            tank.SetTrigger("Attack");
        }

        if (id == 2)
        {
            plane.SetTrigger("Attack");
        }
    }

	//計算已殺死的怪物數量
	public void KillMonsterNum()  
	{
		killCount++;
		print ("Kill :" + killCount);

		if (killCount >= killMaxCount) {
			GameEnd(1);
		}
	}

}
