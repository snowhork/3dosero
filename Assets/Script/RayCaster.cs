using UnityEngine;
using System.Collections;

public class RayCaster : MonoBehaviour
{
    protected RayReciever Reciever = null;

    private void RayOn(RayReciever reciever)
    {
        if (reciever == null)
        {
            return;
        }
        reciever.RayOn(this);
    }

    private void RayOff(RayReciever reciever)
    {
        if (reciever == null)
        {
            return;
        }
        reciever.RayOff(this);
    }

    protected RaycastHit RayCast(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            var reciever = hit.collider.GetComponent(typeof(RayReciever)) as RayReciever;

            if (this.Reciever == reciever)
            {
                return hit;
            }

            RayOff(this.Reciever);
            RayOn(reciever);
            this.Reciever = reciever;
        }
        else
        {
            RayOff(this.Reciever);
            this.Reciever = null;
        }
        return hit;
    }
}