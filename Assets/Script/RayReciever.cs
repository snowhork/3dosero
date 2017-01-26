using UnityEngine;
using System.Collections;

public abstract class RayReciever : MonoBehaviour
{
    protected bool Rayon = false;
    protected float Raycnt = 0f;

    protected virtual void SwitchToRayOn(RayCaster caster)
    {
        Raycnt = 0f;
    }

    protected virtual void SwitchToRayOff(RayCaster caster)
    {
        Raycnt = 0f;
    }


    public virtual void RayOn(RayCaster caster)
    {
        if (!Rayon)
        {
            SwitchToRayOn(caster);
        }
        Raycnt += Time.deltaTime;
        Rayon = true;
    }

    public virtual void RayOff(RayCaster caster)
    {
        if (Rayon)
        {
            SwitchToRayOff(caster);
        }
        Rayon = false;
    }
}