using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingController : MonoBehaviour
{
    [SerializeField] private GameObject timing;
    private bool active;
    // Start is called before the first frame update
    void Start()
    {
        active = false;
    }

    public void ToggleActive() {
        timing.SetActive(active);
        active = !active;
    }
}
