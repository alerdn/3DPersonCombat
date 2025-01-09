using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public int Level => _vigor + _endurance + _mind + _strength + _intelligence - 50;

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

    public int Mind
    {
        get => _mind; set
        {
            _mind = value;
        }
    }

    public int Strength
    {
        get => _strength; set
        {
            _strength = value;
        }
    }

    public int Intelligence
    {
        get => _intelligence; set
        {
            _intelligence = value;
        }
    }

    [SerializeField] private int _vigor = 10;
    [SerializeField] private int _endurance = 10;
    [SerializeField] private int _mind = 10;
    [SerializeField] private int _strength = 10;
    [SerializeField] private int _intelligence = 10;

    public int GetSoulsToNextLevel(int nextLevel = 0)
    {
        int level = nextLevel == 0 ? Level : nextLevel;

        float x = Mathf.Max((level + 81 - 92) * 0.02f, 0f);
        int cost = Mathf.FloorToInt(((x + 0.1f) * Mathf.Pow(level + 81, 2)) + 1);

        return cost;
    }
}