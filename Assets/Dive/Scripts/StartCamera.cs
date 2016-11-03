using UnityEngine;
using System.Collections;

public class StartCamera : MonoBehaviour {

  Quaternion gyro;
	// Use this for initialization
  void Start () {
      gyro = Input.gyro.attitude;
      this.transform.localRotation = Quaternion.Euler(0, -gyro.y, 0);
  }
  // Update is called once per frame
  void LateUpdate () {
      Input.gyro.enabled = true;
      if (Input.gyro.enabled)
      {
          gyro = Input.gyro.attitude;
          this.transform.localRotation = Quaternion.Euler(90, 0, 0) * (new Quaternion(-gyro.x,-gyro.y, gyro.z, gyro.w));
      }
  }

}
