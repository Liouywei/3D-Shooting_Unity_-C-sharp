using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
    public int damage;

	private BoxCollider2D bc;
	public ParticleSystem[] ps;
	public bool hasMusic;
	private AudioSource ads;

	void Start()
	{
		if (hasMusic)
			ads = GetComponent<AudioSource> ();
		
		bc = GetComponent<BoxCollider2D> ();
	}

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.tag == "Monster")
        {
			hit.SendMessage("Hurt", damage);
			//bc.enabled = false;
        }
    }

	public void OpenCollider()
	{
		bc.enabled = true;
		for (int i = 0; i < ps.Length; i++)
		{
			if (ads != null)
			{
				ads.Play ();
			}
			ps[i].Play ();
		}
	}
}
