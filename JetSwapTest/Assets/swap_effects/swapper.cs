using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapper : swappable {

    public float swap_completion;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    swappable target;
    float swap_speed = 0.01f;
    public RectTransform fill;
    public Camera cam;

    void Start() {
        shootableMask = LayerMask.GetMask("Obstacle") | LayerMask.GetMask("Swappable");
    }

    public void shoot(Ray origin)
    {
        Debug.Log("Shoot");
        Debug.Log(Physics.Raycast(origin));
        if (Physics.Raycast(origin, out shootHit)) {
            Debug.Log(shootHit.transform.gameObject.name);
        }
        if (!Physics.Raycast(origin, out shootHit, Mathf.Infinity, shootableMask))
        {
            swap_completion = 0f;
            if (target)
            {
                target.DisengageSwap();
            }
            fill.localScale = new Vector3(swap_completion,swap_completion);
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
            fill.localScale = new Vector3(swap_completion,swap_completion);
            return;
        }
        Debug.Log("Good");
        swappable new_target = shootHit.transform.GetComponent<swappable>();
        if(target != new_target)
        {
            target = new_target;
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
        fill.localScale = new Vector3(swap_completion,swap_completion);
    }

    void Update() {
        Debug.Log("NITICE ME!");
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f,0.5f));
            if (Physics.Raycast(ray)) {
                shoot(ray);
            }
    }
}
