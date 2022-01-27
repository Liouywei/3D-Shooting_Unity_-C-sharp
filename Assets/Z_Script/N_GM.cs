using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class N_GM : MonoBehaviour {
	public static N_GM n_gm;
	public bool playing = true; //true:遊戲進行 false:遊戲停止

	public int money;
	private int killCount;     //擊殺數量 
	private float gameTimes;   //遊戲時間

	//怪物
	public GameObject[] monsters;
	public Transform[] spawn;
	private float spawnTimes;   //產生怪物的間隔

	//測試
	public Text txt_Cheat;
	public Button leftBTN;
	public Button rightBTN;
	public Button fireBTN;

	//結束
	public GameObject Img_Background;
	public Text txt_EndTime;
	public Text txt_ENdKill;

	void Start()
	{
		n_gm = this;
		playing = true;
		money = 1000;

		spawnTimes = 6f;
		InvokeRepeating("UpdateNewValue", 3, 20);
	}

	void Update()
	{
		if (N_GM.n_gm.playing) {
			gameTimes += Time.deltaTime;
			Cheat ();
			CheatControl ();
		}	
	}

	//測試用按鈕 增減生怪時間
	void Cheat()
	{
		//拉長出怪時間
		if (Input.GetKeyDown (KeyCode.O)) {
			spawnTimes += 0.5f;
			txt_Cheat.text = spawnTimes.ToString();
			txt_Cheat.enabled = true;
			UpdateNewValue ();
			Invoke ("CloseCheat", 1f);
		}
		//縮短出怪時間
		if (Input.GetKeyDown (KeyCode.P)) {
			spawnTimes -= 0.5f;
			txt_Cheat.text = spawnTimes.ToString();
			txt_Cheat.enabled = true;
			UpdateNewValue ();
			Invoke ("CloseCheat", 1f);
		}

		//遊戲暫停
		if (Input.GetKeyDown (KeyCode.Space)){
			if (Time.timeScale == 1f)
				Time.timeScale = 0f; //時間暫停
			else
				Time.timeScale = 1f; //時間繼續
		}
	}

	//關閉測試參數Text
	void CloseCheat()
	{
		txt_Cheat.enabled = false;
	}

	//測試用鍵盤操作
	void CheatControl()
	{
		if (Input.GetKeyDown (KeyCode.J))
			leftBTN.onClick.Invoke ();

		if (Input.GetKeyDown (KeyCode.L))
			rightBTN.onClick.Invoke ();

		if (Input.GetKeyDown (KeyCode.K))
			fireBTN.onClick.Invoke ();
	}

	//決定是否產生怪物
	void UpdateNewValue()
	{
		CancelInvoke ("CreateMonster");
		InvokeRepeating("CreateMonster", 1, spawnTimes);
	}

	//產生怪物
	void CreateMonster()
	{
		if (playing == false) {CancelInvoke(); return;}

		int temp = Random.Range(0, spawn.Length);
		int mon = Random.Range(0, monsters.Length);
		Vector3 v = new Vector3 (spawn[temp].position.x, monsters[mon].transform.position.y, spawn[temp].position.z);
		Instantiate(monsters[mon], v, Quaternion.identity);
	}

	//擊倒怪物，獲得怪物
	public void MoneyAdd(int coine)
	{
		money += coine;
	}
		
	//計算已殺死的怪物數量
	public void KillMonsterNum()  
	{
		killCount++;
		//combo
	}

	//防守失敗，計算成績
	public void EndGame()
	{
		playing = false;

		Img_Background.SetActive (true);
		int m = (int)gameTimes / 60; //計算分
		int s = (int)gameTimes % 60; //計算秒
		txt_EndTime.text = m.ToString ("00") + ":" + s.ToString ("00");
		txt_ENdKill.text = killCount.ToString();
	}
}
