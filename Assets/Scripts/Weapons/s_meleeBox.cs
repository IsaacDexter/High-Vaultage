using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_meleeBox : MonoBehaviour
{
	// Start is called before the first frame update
	List<GameObject> m_targets = new List<GameObject>();

	private void OnTriggerEnter(Collider other)
	{
		if (!m_targets.Contains(other.transform.root.gameObject))
		{
			m_targets.Add(other.transform.root.gameObject);
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (m_targets.Contains(other.transform.root.gameObject))
		{
			m_targets.Remove(other.transform.root.gameObject);
		}
	}
}
