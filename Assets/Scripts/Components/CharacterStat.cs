using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public int Level => _strength + _vigor + _endurance - 30;

    public int Strength
    {
        get => _strength; set
        {
            _strength = value;
        }
    }

    public int Vigor
    {
        get => _vigor; set
        {
            _vigor = value;
        }
    }

    public int Endurance
    {
        get => _endurance; set
        {
            _endurance = value;
        }
    }

    [SerializeField] private int _strength = 10;
    [SerializeField] private int _vigor = 10;
    [SerializeField] private int _endurance = 10;

    public int GetSoulsToNextLevel(int nextLevel = 0)
    {
        int level = nextLevel == 0 ? Level : nextLevel;

        float x = Mathf.Max((level + 81 - 92) * 0.02f, 0f);
        int cost = Mathf.FloorToInt(((x + 0.1f) * Mathf.Pow(level + 81, 2)) + 1);

        return cost;
    }
}