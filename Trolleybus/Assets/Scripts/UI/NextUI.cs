using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
		NextButton.onClick.AddListener(() => LoadNextScene());
	}

	public async void ShowResult(bool choice)
	{
		try
		{

			await NetworkManager.Instance.AddChoiceAsync(new Choice
			{
				Level = gameManager.Level,
				Pulled = choice
			});

			await GetResultText(choice);

		}
		catch (Exception e)
		{
			resultText.text = e.ToString();
		}
		finally
		{
			var resultTranslator = resultText.GetComponent<LanguageYG>();
			resultTranslator.Clear();
			resultTranslator.Translate(resultTranslator.countLang);
			resultTranslator.SwitchLanguage();

			transform.DOMove(resultTransform.position, showTime);
		}
	}

	private async Task GetResultText(bool choice)
	{
		List<Choice> choices = await NetworkManager.Instance.GetChoicesAsync(gameManager.Level);

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

			resultText.text = $"{Mathf.Round(percentage * 100)}% of people agree with you, {Mathf.Round((1 - percentage) * 100)}% disagree ({choices.Count} votes)";
		}
		else
		{
			float maxPercentage = 1f;
			resultText.text = "You are first, who vote";
			percentImage.fillAmount = maxPercentage;
		}
	}

	private void LoadNextScene()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		int nextSceneIndex = currentSceneIndex + 1;

		Debug.Log("click");

		if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
		{
			SceneManager.LoadScene(nextSceneIndex);
		}
		else
		{
			Debug.Log("It's the last scene!");
		}
	}
}
