using UnityEngine;
using System.Collections;

public class N_Explsion : MonoBehaviour {
	public int damage;

	private BoxCollider bc;
	public ParticleSystem[] ps;

	void Start()
	{
		bc = GetComponent<BoxCollider> ();
	}

	void OnTriggerEnter(Collider hit)
	{
		if (hit.tag == "Monster")
		{
			hit.SendMessage("Hurt", damage);
		}
	}

	public void OpenCollider()
	{
		bc.enabled = true;
		Invoke ("CloseCollider", 0.1f);
		for (int i = 0; i < ps.Length; i++)
		{
			ps[i].Play ();
		}
	}

	void CloseCollider()
	{
		bc.enabled = false;
	}
}
