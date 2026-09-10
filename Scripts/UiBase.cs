using Godot;
using System;

public partial class UiBase : Control
{

    public PanelContainer UI_FIGHT_MENU;   
    public Panel UI_ATK;
    public Panel UI_ITM;
    public Panel UI_MGK;
    public Panel UI_TLK;

    public ProgressBar Health;
    public ProgressBar Stamina;

    [Signal] public delegate void SendAttackEventHandler(Attack attack);

    [Signal] public delegate void UseItemEventHandler();

    public override void _Ready()
    {
        UI_FIGHT_MENU = GetNode<PanelContainer>("UI_FIGHT_MENU");

        UI_ATK = GetNode<Panel>("UI_FIGHT_MENU/UI_ATTK");
        UI_ITM = GetNode<Panel>("UI_FIGHT_MENU/UI_ITM");
        UI_MGK = GetNode<Panel>("UI_FIGHT_MENU/UI_MGK");
        UI_TLK = GetNode<Panel>("UI_FIGHT_MENU/UI_TLK");

        Health = GetNode<ProgressBar>("UI_HEALTH/Health");
        Stamina = GetNode<ProgressBar>("UI_HEALTH/Stamina");
    }

    public void SelectTab(int tab)
    {
        UI_ATK.Hide();
        UI_ITM.Hide();
        UI_MGK.Hide();
        UI_TLK.Hide();
        switch (tab)
        {
            default:
            case 0:
                UI_ATK.Show();
                break;
            case 1:
                UI_ITM.Show();
                break;
            case 2:
                UI_MGK.Show();
                break;
            case 3:
                UI_TLK.Show();
                break;

        }
    }

    public void SetHealth(int value)
    {
        Health.Value = value;
    }

    public void SetStamina(int value)
    {
        Stamina.Value = value;
    }

    public void HideFightMenu()
    {
        UI_FIGHT_MENU.Hide();
    }

    public void ShowFightMenu()
    {
        UI_FIGHT_MENU.Show();
    }

    public void Attack1()
    {
        EmitSignal("SendAttack", Attack.SIMPLE_SLASH);
    }

    public void Attack2()
    {
        EmitSignal("SendAttack", Attack.SIMPLE_SHOT);
    }

    public void Attack3()
    {
        EmitSignal("SendAttack", Attack.ELECTRIC_SHOT);
    }

    public void Attack4()
    {
        EmitSignal("SendAttack", Attack.WATER_SHOT);
    }


}
