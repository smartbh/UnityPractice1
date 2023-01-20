using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowContainerController : MonoBehaviour
{
    public GameObject arrowObject;
    public Transform arrowContainer;

    public float shotInterval = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FireArrow());
    }

    IEnumerator FireArrow()
    {
        while(true)
        {
            for(int i = 0; i<3; i++)
            {
                GameObject tempObject = Instantiate(arrowObject, arrowContainer);
                Vector3 direction = new Vector2(Mathf.Cos((20 + 20 * i) * Mathf.Deg2Rad),
                                    Mathf.Sin((20 + 20 * i) * Mathf.Deg2Rad));

                tempObject.transform.right = direction;
                tempObject.transform.position = transform.position + shotInterval * direction;

            }

            yield return new WaitForSeconds(4f);
        }
    }

}
