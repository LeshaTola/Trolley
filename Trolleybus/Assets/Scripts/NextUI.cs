using DG.Tweening;
using System.Collections.Generic;
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

	public async void ShowResult(bool choice)
	{
		List<Choice> choices = await NetworkManager.Instance.GetChoicesAsync();

		int choicesLikeYou = 0;
		foreach (var choiceVariant in choices)
		{
			if (choiceVariant.Pull == choice)
			{
				choicesLikeYou++;
			}
		}

		float percentage = choicesLikeYou / choices.Count;
		percentImage.fillAmount = percentage;

		transform.DOMove(resultTransform.position, showTime);
		resultText.text = $"{percentage * 100}% of people think like you, other {(1 - percentage) * 100}% don't ({choices.Count} votes)";
	}
}
