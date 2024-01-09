using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputTapMove : MonoBehaviour
{
    EventSystem system;
    public Selectable firstInput;
    // Start is called before the first frame update

    private void Start()
    {
#if UNITY_ANDROID
#else
        system = EventSystem.current;
        firstInput.Select();
#endif
    }

    private void Update()
    {
#if UNITY_ANDROID
#else

        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (previous != null)
            {
                previous.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                next.Select();
            }
        }
    }
#endif
    }
}
