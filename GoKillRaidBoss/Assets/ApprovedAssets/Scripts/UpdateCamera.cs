using UnityEngine;

public class UpdateCamera : MonoBehaviour {

    public GameObject target;


	void Start () {
		
	}
	
	void Update () {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 17f, target.transform.position.z - 12f);
    }
}
