using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class NextUI : MonoBehaviour
{
	[Header("UI")]
	[SerializeField] private Text resultText;
	[SerializeField] private Image percentImage;
	[SerializeField] private Button NextButton;

	[Header("Animation")]
	[SerializeField] private Transform resultTransform;
	[SerializeField] private float showTime;

	[Header("Other")]
	[SerializeField] private GameManager gameManager;

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

		if (choices != null || choices.Count > 0)
		{
			int choicesLikeYou = 0;
			foreach (var choiceVariant in choices)
			{
				if (choiceVariant.Pulled == choice)
				{
					choicesLikeYou++;
				}
			}

			float percentage = (float)choicesLikeYou / choices.Count;
			percentImage.fillAmount = percentage;

			transform.DOMove(resultTransform.position, showTime);

			resultText.text = $"{Mathf.Round(percentage * 100)}% of people agree with you, {Mathf.Round((1 - percentage) * 100)}% disagree ({choices.Count} votes)";
		}
		else
		{
			float maxPercentage = 1f;
			resultText.text = "You are first, who vote";
			percentImage.fillAmount = maxPercentage;
		}

		var resultTranslator = resultText.GetComponent<LanguageYG>();
		resultTranslator.Clear();
		resultTranslator.Translate(resultTranslator.countLang);
		resultTranslator.SwitchLanguage();

		await NetworkManager.Instance.AddChoiceAsync(new Choice
		{
			Level = gameManager.Level,
			Pulled = choice
		});
	}
}
