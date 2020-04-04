using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sculptureCollide : MonoBehaviour
{

    public Vector3 initPos;
    public static bool isActive = false;
    public GameObject sculpture;

    public Quaternion originalRotationValue; 
    void Start() {
      initPos = transform.position;
      originalRotationValue = transform.rotation;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isActive == false) {
          GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
          StartCoroutine (ResetSculpture(sculpture, initPos, originalRotationValue));
          isActive = true;
        }
    }

    static IEnumerator ResetSculpture(GameObject obj, Vector3 initPos, Quaternion originalRotationValue){

    	yield return new WaitForSeconds (5.0f);

      obj.transform.position = initPos;
      obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, originalRotationValue, Time.time * 1.0f);
      obj.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

      isActive = false;
   }
}
