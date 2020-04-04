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

    void OnCollisionEnter(Collision collision) // When the player hits the sculpture
    {
        if (isActive == false) { // If it hasn't been hit before
          GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; // Unfreeze the sculpture and let gravity work
          StartCoroutine (ResetSculpture(sculpture, initPos, originalRotationValue)); // Start counting down to reset the sculture
          isActive = true; 
        }
    }

    static IEnumerator ResetSculpture(GameObject obj, Vector3 initPos, Quaternion originalRotationValue){

      yield return new WaitForSeconds (5.0f); // Wait for 5 seconds

      obj.transform.position = initPos; // Reset the sculpture to it's original position
      obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, originalRotationValue, Time.time * 1.0f); // Reset to original rotation
      obj.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; // Freeze the sculpture again (so it won't fall immediately)

      isActive = false; // let the software know that it's inactive again, so the player can hit it again
   }
}
