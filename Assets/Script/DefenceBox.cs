using UnityEngine;
using System.Collections;

public class DefenceBox : MonoBehaviour {
	public int Hp;
	public int hurt;  //每次受到的傷害
	private Animator am;


    void Start()
    {
        am = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.tag == "Attack")
        {
            am.SetTrigger("Hit");
			GetHurt ();
        }
    }

	void GetHurt()
	{
		Hp -= hurt;
		print ("Hp :" + Hp);

		if (Hp <= 0) {
			GameManager.gm.GameEnd(0);
			gameObject.SetActive (false);
		}
	}
}
