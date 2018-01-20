using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swappable : MonoBehaviour {
    public float swap_resistance = 0.0f;
    private float max_swap_completion = 0.0f;
    private int swapless_time = 0;

    public void UpdateSwap (swapper other, float swap_completion) {
        if (swap_completion >= max_swap_completion) {
            max_swap_completion = swap_completion;
            UpdateSwapInternal(other, swap_completion);
        }
        swapless_time = 0;
    }

    private void UpdateSwapInternal (swapper other, float swap_completion) {
    }

    public float GetSwapResistance () {
        return swap_resistance;
    }

    public void DisengageSwap () {
        max_swap_completion = 0.0f;
        swapless_time = 0;
    }

    void Update () {
        swapless_time++;
        if (swapless_time > 60) {
            DisengageSwap();
        }
    }
}
