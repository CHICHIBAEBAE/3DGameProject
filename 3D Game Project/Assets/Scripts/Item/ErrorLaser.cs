using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ErrorLaser : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float maxDistance;
    [SerializeField] float time;
    public GameObject errorPanel;

    public GameObject curGameObject;

    void Update()
    {
        Debug.DrawRay(transform.position, transform.right * maxDistance, Color.red, 0.3f);

        if(Physics.Raycast(transform.position, transform.right , out hit, maxDistance, layerMask))
        {
            if(hit.collider.gameObject != curGameObject)
            {
                curGameObject = hit.collider.gameObject;
                errorPanel.SetActive(true);
                StartCoroutine(WarningPanel());

            }
        }
    }

    IEnumerator WarningPanel()
    {
        yield return new WaitForSecondsRealtime(time);
        errorPanel.SetActive(false);
        curGameObject = null;
    }

    //public void OnClickDownWarningPanel()
    //{
    //    errorPanel.SetActive(false);
    //}
}