using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisplayScript : MonoBehaviour
{
    void OffAnim () {
        transform.GetComponent<Animator>().SetBool("On", false);
    }
}
