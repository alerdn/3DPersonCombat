using UnityEngine;

public class BonfireUI : MonoBehaviour
{
    [SerializeField] private GameObject _frame;
    [SerializeField] private BonfireLevelUpUI _levelUpframe;
    [SerializeField] private BonfireUpgradeUI _upgradeframe;

    private PlayerStateMachine _player;

    private void Start()
    {
        _player = PlayerStateMachine.Instance;

        _levelUpframe.Init(_player, this);
        _upgradeframe.Init(_player, this);

        _levelUpframe.HideFrame();
        _upgradeframe.HideFrame();
    }

    public void ShowUI()
    {
        _player.InputReader.SetControllerMode(ControllerMode.UI);
        ShowMainFrame();
    }

    public void ShowMainFrame()
    {
        _frame.SetActive(true);
    }

    public void HideMainFrame()
    {
        _frame.SetActive(false);
    }

    public void OnClick_Leave()
    {
        _player.InputReader.SetControllerMode(ControllerMode.Gameplay);
        _player.SwitchState(new PlayerFreeLookState(_player));

        HideMainFrame();
        _levelUpframe.HideFrame();
        _upgradeframe.HideFrame();
    }

    public void OnClick_ShowLevelUpFrame()
    {
        HideMainFrame();
        _levelUpframe.ShowLevelUpFrame();
    }

    public void OnClick_ShowUpgradeFrame()
    {
        HideMainFrame();
        _upgradeframe.ShowUpgradeFrame();
    }
}
