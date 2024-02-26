using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextUI : MonoBehaviour
{
	[SerializeField] private Text resultText;
	[SerializeField] private Image percentImage;
	[SerializeField] private Button NextButton;

	[SerializeField] private Transform resultTransform;
	[SerializeField] private float showTime;

	private void Awake()
	{
		NextButton.onClick.AddListener(() =>
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		});
	}

	public void ShowResult(int choice)
	{
		transform.DOMove(resultTransform.position, showTime);
	}
}
