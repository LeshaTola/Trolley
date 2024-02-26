using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	[SerializeField] private Button pullButton;
	[SerializeField] private Button doNothingButton;

	public void Hide()
	{
		gameObject.SetActive(false);
	}

}
