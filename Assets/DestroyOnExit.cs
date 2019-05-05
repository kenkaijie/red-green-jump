using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnExit : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(string.Format("Destroyer Triggered on {0}", collision.gameObject.name));
        Destroy(collision.gameObject);
    }
}
