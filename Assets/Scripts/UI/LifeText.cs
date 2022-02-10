using TMPro;
using UnityEngine;

public class LifeText : MonoBehaviour, IPlayerListener
{
    [SerializeField]
    private TextMeshProUGUI Text;
    [SerializeField]
    private GameObject PlayerGameObject;

    private Player Player;

    void Start()
    {
        Text = GetComponent<TextMeshProUGUI>();
        Player = PlayerGameObject.GetComponent<Player>();

        SetPlayerListener();
    }

    private void SetPlayerListener()
    {
        Player.AddToListeners(this);
    }

    public void UpdatePlayerProprierts(Player player)
    {
        UpdateCounterText(player.Lifes);
    }

    void UpdateCounterText(int value)
    {
        Text.text = value.ToString();
    }
}
