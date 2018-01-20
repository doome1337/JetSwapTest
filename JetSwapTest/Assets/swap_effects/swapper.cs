using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapper : swappable {

    float swap_completion;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    swappable target;
    float swap_speed = 0.01f;   

    void Start() {
        shootableMask = LayerMask.GetMask("Obstacle") | LayerMask.GetMask("Swappable");
    }

    public void shoot(Ray origin)
    {
        Debug.Log("Shoot");
        Debug.Log(Physics.Raycast(origin));
        if (!Physics.Raycast(origin, out shootHit, shootableMask))
        {
            swap_completion = 0f;
            if (target)
            {
                target.DisengageSwap();
            }
            return;
        }
        Debug.Log("Hit");
        Debug.Log(shootHit.transform.gameObject.name);
        Debug.Log(shootHit.transform.gameObject.layer);
        Debug.Log(LayerMask.GetMask("Swappable"));
        if ((1 << shootHit.transform.gameObject.layer) != LayerMask.GetMask("Swappable"))
        {
            swap_completion = 0f;
            if (target)
            {
                target.DisengageSwap();
            }
            return;
        }
        Debug.Log("Good");
        if(target == null)
        {
            target = shootHit.transform.GetComponent<swappable>();
            swap_completion = 0f;
        }
        swap_completion += swap_speed / target.GetSwapResistance();
        target.UpdateSwap(this, swap_completion);
        if (swap_completion >= 1.0f)
        {
            this.DisengageSwap();
            target.DisengageSwap();
            Transform t = this.GetComponent<Transform>();
            Transform o = target.GetComponent<Transform>();
            Vector3 t_pos = t.position;
            Vector3 o_pos = o.position;
            t.SetPositionAndRotation(o_pos, t.rotation);
            o.SetPositionAndRotation(t_pos, o.rotation);
            target = null;
        }
    }

    void Update() {
        Debug.Log("NITICE ME!");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray)) {
                shoot(ray);
            }
    }
}
