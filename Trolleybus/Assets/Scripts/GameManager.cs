using UnityEngine;

public class GameManager : MonoBehaviour
{
	[field: SerializeField] public int Level { get; private set; }
	[field: TextArea, SerializeField] public string LevelName;
	[field: TextArea, SerializeField] public string History;
}
