using UnityEngine;

[CreateAssetMenu(fileName = "Class Data", menuName = "Scriptable Objects/Class Data", order = 0)]
public class BaseClassData : ScriptableObject
{
    [SerializeField] protected AnimationCurve animactionCurveHP;
    [SerializeField] protected AnimationCurve animactionCurveAtk;
    [SerializeField] protected AnimationCurve animactionCurveMagic;

    public int Level;

    public int HP => Mathf.FloorToInt(animactionCurveHP.Evaluate(Level));
    public int ATK => Mathf.FloorToInt(animactionCurveAtk.Evaluate(Level));
    public int MAG => Mathf.FloorToInt(animactionCurveMagic.Evaluate(Level));
}
