using UnityEngine;
using UnityEngine.UI;

public class ButtonTextHoverColor : MonoBehaviour
{

	[SerializeField] private Color hoverColor;
	[SerializeField] private Text text;

	private Color defaultColor;

	private void Awake()
	{
		defaultColor = text.color;
	}

	public void SetDefaultColor()
	{
		text.color = defaultColor;
	}

	public void SetHoverColor()
	{
		text.color = hoverColor;
	}
}
