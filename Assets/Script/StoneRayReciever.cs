using UnityEngine;
using System.Collections;

public class StoneRayReciever : RayReciever
{
    public const float ChangeStoneCnt = 1f;
    public RayCaster Caster { get; set; }

    private Player Player
    {
        get { return Caster.GetComponent<Player>(); }
    }

    Stone Stone
    {
        get { return GetComponent<Stone>(); }
    }

    private Const.Color RayonColor
    {
        get { return Player.Color; }
    }

    StoneInfo info
    {
        get { return GetComponent<Stone>().Info; }
    }

    bool settable
    {
        get { return info.Settable(RayonColor); }
    }

    Const.Color stoneColor
    {
        get { return info.color; }
    }

    void Update()
    {
        base.Update();
        if (!rayon || !(raycnt >= ChangeStoneCnt) || !settable) return;
        foreach (var stoneInfo in info.SetStone(RayonColor))
        {
            Stone.osero.SetStone(stoneInfo.position, RayonColor);
        }
        Stone.osero.SetStone(info.position, RayonColor);
        Player.EndTurn();
    }

    protected override void SwitchToRayOn(RayCaster caster)
    {
        base.SwitchToRayOn(caster);
        this.Caster = caster;
        if (stoneColor != Const.Color.Black)
        {
            return;
        }
        if (settable)
        {
            ParticleManager.Instance.HitEffect(Const.Color.Yellow, transform.position);
            MaterialManager.Instance.SetMaterial(Const.Color.Yellow, gameObject);
        }
        else
        {
            ParticleManager.Instance.HitEffect(Const.Color.Black, transform.position);
        }
    }

    protected override void SwitchToRayOff(RayCaster caster)
    {
        base.SwitchToRayOff(caster);
        MaterialManager.Instance.SetMaterial(stoneColor, gameObject);
    }

    public override void RayOn(RayCaster caster)
    {
        base.RayOn(caster);
    }

    public override void RayOff(RayCaster caster)
    {
        base.RayOff(caster);
    }
}