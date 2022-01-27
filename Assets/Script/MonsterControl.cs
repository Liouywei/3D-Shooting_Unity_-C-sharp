using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonsterControl : MonoBehaviour {
	public int moveSpeed;
	public int attackTime;  //攻擊間隔時間
	public int hp;
    private int nowhp;
	private bool alive = true;

	private BoxCollider2D bc;
	private Animator am; 
	private bool attacking = false;

    public GameObject hpbar;
    public Image hp_pic;

	public AudioClip[] au = new AudioClip[3];
	private AudioSource ads;

	void Start()
	{
        nowhp = hp;
		am = GetComponent<Animator> ();
		ads = GetComponent<AudioSource> ();
		bc = GetComponent<BoxCollider2D> ();
	}

	void Update () {
		if (GameManager.timeStop == 1) {
			if (alive) {
				if (!attacking)
					Move ();

				MonsterState ();
			}
		}
	}

	void MonsterState()
	{
		am.SetBool ("Attack", attacking);
	}

	void Move()
	{
		transform.Translate (Vector2.left * moveSpeed * Time.deltaTime);
	}

	public void Hurt(int att)
	{
		if (!alive) return;

		nowhp -= att;
        hp_pic.fillAmount = (float)nowhp / hp;

		if (nowhp <= 0) {
			bc.enabled = false;
			alive = false;
            am.SetTrigger("Dead");
		}
	}

    void Dead()
    {
		GameManager.gm.KillMonsterNum ();
        gameObject.SetActive(false);
    }

	void OnTriggerEnter2D(Collider2D hit)
	{
		if (hit.tag == "Defenes")
		{
			print ("In");
			attacking = true;
		}
	}

	void OnTriggerExit2D(Collider2D hit)
	{
		if (hit.tag == "Defenes")
		{
			print ("out");
			attacking = false;
		}
	}

    void OnMouseEnter()
    {
        hpbar.SetActive(true);
    }

    void OnMouseExit()
    {
        hpbar.SetActive(false);
    }

    void CoinAdd()
    {
        Camera.main.SendMessage("MoneyUpdate", 1);
    }

	void PlaySound(int num)
	{
		if (ads.isPlaying) {return;} //如果有音效正在播放，就不執行

		ads.clip = au [num];
		ads.Play();
	}
}
