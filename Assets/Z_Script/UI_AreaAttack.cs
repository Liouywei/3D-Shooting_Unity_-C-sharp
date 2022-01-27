using UnityEngine;
using System.Collections;

public class UI_AreaAttack : MonoBehaviour {
	public int damage;

	void OnTriggerEnter(Collider hit)
	{
		if (hit.tag == "Monster") {
			hit.SendMessage ("Hurt", damage);
		}
	}
}
