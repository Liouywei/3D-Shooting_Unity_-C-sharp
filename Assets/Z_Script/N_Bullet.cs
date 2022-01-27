using UnityEngine;
using System.Collections;

public class N_Bullet : MonoBehaviour {

	public float lifeTime;
	public int damage;
	public bool twicAttack;//可否穿透
	public int attackCount;//可以穿透的數量
	private int hasCount;  //已經穿透的數量

	public bool isbon;  //是否會爆炸
	public N_Explsion ep;
	public GameObject bulletObj;
	private BoxCollider bulletCol;
	private Rigidbody bulletRigi;

	void Start()
	{
		bulletCol = GetComponent<BoxCollider> ();
		bulletRigi = GetComponent<Rigidbody> ();
		Destroy (gameObject, lifeTime);
		hasCount = 0;
	}


	void OnTriggerEnter(Collider hit)
	{
		if (hit.tag == "Monster") {
			if (twicAttack) {
				hit.SendMessage ("Hurt", damage);
				hasCount++;
				if (hasCount >= attackCount) {
					Destroy (gameObject);
					return;
				}
			} else {
				hit.SendMessage ("Hurt", damage);
				if (isbon) {
					Destroy (bulletRigi);
					bulletObj.SetActive (false);
					bulletCol.enabled = false;
					ep.OpenCollider ();
					return;
				}
				Destroy (gameObject);
			}
		}
	}

}
