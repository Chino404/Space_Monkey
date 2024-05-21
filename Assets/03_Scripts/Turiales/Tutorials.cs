using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorials : MonoBehaviour
{
    [SerializeField] private GameObject tutorialCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<ModelMonkey>())
        {
            StartCoroutine(Active());
        }
    }

    IEnumerator Active()
    {
        tutorialCanvas.SetActive(true);
        yield return new WaitForSeconds(3);
        tutorialCanvas.SetActive(false);
        gameObject.SetActive(false);

    }
}
