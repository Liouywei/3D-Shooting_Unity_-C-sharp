using UnityEngine;
using System.Collections;

public class N_Defense : MonoBehaviour {
	public int hp;

	void OnTriggerEnter(Collider hit)
	{
		if (hit.tag == "Attack") {
			hp--;
			//Debug.Log (gameObject.name +  " hp: " + hp);
			if (hp <= 0) {
				WallDown ();
				Destroy (gameObject);
			}
		}
	}

	//牆被摧毀
	void WallDown()
	{
		N_GM.n_gm.EndGame ();
	}
}
