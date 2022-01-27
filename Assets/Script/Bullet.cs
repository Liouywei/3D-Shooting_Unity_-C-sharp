using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public int damage;
	public bool twicAttack;
	public int attackCount;//可以穿透的數量
	private int hasCount;  //已經穿透的數量

	public bool isbon;  //是否會爆炸
	public Explosion ep;

	private AudioSource ads;

	private BoxCollider2D bc;  //父物件的碰撞
	public SpriteRenderer sr;  //子彈的圖片

	void Start()
	{
		bc = GetComponent<BoxCollider2D> ();
		ads = GetComponent<AudioSource> ();
		Destroy (gameObject, 3f);
	}

	void BulletColose()
	{
		bc.enabled = false;
		sr.enabled = false;
	}

	void OnTriggerEnter2D(Collider2D hit)
	{
		if (hit.tag == "Monster") {
			if (twicAttack) {
				ads.Play ();
				hit.SendMessage ("Hurt", damage);
				hasCount++;
				if (hasCount >= attackCount)
					BulletColose ();
			} else {
				ads.Play();
				hit.SendMessage ("Hurt", damage);
				BulletColose ();
				if (isbon) {
					ep.OpenCollider ();
				}
			}

			Destroy (gameObject, 2.5f);
		}
	}
}
