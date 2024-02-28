using UnityEngine;
using UnityEngine.UI;
using YG;

public class GameUI : MonoBehaviour
{
	[Header("Texts")]
	[SerializeField] private Text levelText;
	[SerializeField] private Text HistoryText;

	[Header("Choices")]
	[SerializeField] private Button pullButton;
	[SerializeField] private Button doNothingButton;

	[Header("Other")]
	[SerializeField] private GameManager gameManager;

	private void Awake()
	{
		UpdateTexts();
	}

	private void UpdateTexts()
	{
		levelText.text = $"Level: {gameManager.Level} {gameManager.LevelName}";
		var levelTranslator = levelText.GetComponent<LanguageYG>();
		levelTranslator.Clear();
		levelTranslator.Translate(levelTranslator.countLang);


		HistoryText.text = $"{gameManager.History}";
		var HistoryTranslator = HistoryText.GetComponent<LanguageYG>();
		HistoryTranslator.Clear();
		HistoryTranslator.Translate(HistoryTranslator.countLang);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}

}
