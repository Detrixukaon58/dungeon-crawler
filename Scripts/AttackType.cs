using System;
using System.Collections.Generic;
using Godot;

public partial class Attack : GodotObject
{
    public String name;

    public EffectType effectType;

    public AttackType attackType;

    public List<EffectType> subEffectTypes;

    public float damageMultiplier;

    public bool damageOverTime;

    public float damageOverTimeChance;

    public float damageTime;

    public float damageRadius;


    public static Attack SIMPLE_SLASH = new Attack
    {
        name="Simple Slash",
        effectType=EffectType.NORMAL,
        attackType=AttackType.SLASH,
        damageMultiplier=1.0f,
        damageOverTime=false
    };

    public static Attack SIMPLE_SHOT = new Attack
    {
        name="Simple Shot",
        effectType=EffectType.NORMAL,
        attackType=AttackType.PEIRCE_SHOT,
        damageMultiplier=1.2f,
        damageOverTime=false
    };

    public static Attack ELECTRIC_SHOT = new Attack
    {
        name="Electric Shot",
        effectType=EffectType.ELECTRIC,
        attackType=AttackType.PEIRCE_SHOT,
        damageMultiplier=1.2f,
        damageOverTime=true,
        damageOverTimeChance=0.12f,
        damageTime=5.0f
    };

    public static Attack WATER_SHOT = new Attack
    {
        name="Water Shot",
        effectType=EffectType.WATER,
        attackType=AttackType.PEIRCE_SHOT,
        damageMultiplier=1.2f,
        damageOverTime=false
    };


    public void PlayAttack()
    {
        
    }

}