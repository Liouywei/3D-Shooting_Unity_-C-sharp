using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UI_Shop : MonoBehaviour {
	public UI_PlayerControl up;
	public UI_Surport us;
	public Text moneyTxt;
	private int money;

	void OnEnable()
	{
		Debug.Log ("Open");
		money = N_GM.n_gm.money;
		Debug.Log (money);
		moneyTxt.text = "$" + money.ToString(); //更新金錢
	}

	//判斷購買的是武器或支援
	public void ItemKind(UI_ItemData ud)
	{
		if (ud.id <= 4) {
			WeaponItem (ud.id, ud.price);
		} else {
			SupportItem (ud.id, ud.price);
		}
	}

	void WeaponItem(int id, int price)
	{
		if ((money - price) >= 0) {
			money -= price;
			N_GM.n_gm.money = money;
			up.ChangeWeapon (id);
			gameObject.SetActive (false);
		}
	}

	void SupportItem(int id, int price)
	{
		if ((money - price) >= 0) {
			money -= price;
			N_GM.n_gm.money = money;
			us.SurportKind (id);
			gameObject.SetActive (false);
		}
	}

}
