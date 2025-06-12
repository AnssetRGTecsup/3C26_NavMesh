using TMPro;
using UnityEngine;

public class CurveTest : MonoBehaviour
{
    [SerializeField] private BaseClassData characterData;
    [SerializeField, Range(1,90)] private int level;

    [SerializeField] private TMP_Text textComponent;

    private void Update()
    {
        characterData.Level = level;

        UpdateText(level, characterData.HP, characterData.ATK, characterData.MAG);
    }

    public void UpdateText(int t, int HP, int ATK, int MAG)
    {
        textComponent.text = $"Character Level {t}" +
            $"\n\tTotal HP {HP}" +
            $"\n\tTotal ATK {ATK}" +
            $"\n\tTotal MAG {MAG}";
    }
}
