using UnityEngine;

public class RailObject : MonoBehaviour
{
	[SerializeField] private GameObject splash;

	public void CreateSplash()
	{
		Instantiate(splash, transform.position, Quaternion.identity);
		gameObject.SetActive(false);
	}
}
