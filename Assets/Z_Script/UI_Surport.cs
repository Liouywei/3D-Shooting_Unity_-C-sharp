using UnityEngine;
using System.Collections;

public class UI_Surport : MonoBehaviour {
	public GameObject tankObj;
	public GameObject planeObj;

	public BoxCollider tankCol;
	public BoxCollider planeCol;

	public ParticleSystem[] tankPS = new ParticleSystem[3];
	public ParticleSystem planePS;

	public void SurportKind(int id)
	{
		if (id == 5)
			StartCoroutine(Tank ());
		else
			StartCoroutine(Plane ());
	}

	IEnumerator Tank()
	{
		tankObj.SetActive (true);
		yield return new WaitForSeconds (2f);
		//爆炸特效
		tankPS[0].Play();
		yield return new WaitForSeconds (0.5f);
		tankPS[1].Play();
		yield return new WaitForSeconds (0.5f);
		tankPS[2].Play();

		tankCol.enabled = true;
		yield return new WaitForSeconds (1f);
		tankCol.enabled = false;
		yield return new WaitForSeconds (1f);
		tankObj.SetActive (false);
	}

	IEnumerator Plane()
	{
		planeObj.SetActive (true);
		yield return new WaitForSeconds (2f);
		//爆炸特效
		planePS.Play();

		yield return new WaitForSeconds (1f);
		planeCol.enabled = true;
		yield return new WaitForSeconds (1f);
		planeObj.SetActive (false);
		planeCol.enabled = false;
	}
}
