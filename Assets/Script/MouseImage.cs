using UnityEngine;
using System.Collections;

public class MouseImage : MonoBehaviour {

	void Awake()
	{
		Cursor.visible = false;
	}

	void Update()
	{
		Move ();
	}

	void Move()
	{
		Vector2 v = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		transform.position = v;
	}
}
