using UnityEngine;

public class CameraController : MonoBehaviour {

	public float Sensitivity {
		get { return sensitivity; }
		set { sensitivity = value; }
	}
	[Range(0.1f, 9f)][SerializeField] float sensitivity = 2f;
	[Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]
	[Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;

	Vector2 rotation = Vector2.zero;
	const string xAxis = "Mouse X";
	const string yAxis = "Mouse Y";

	void Start() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update(){
		if (Cursor.lockState == CursorLockMode.Locked) {
			rotation.x += Input.GetAxis(xAxis) * sensitivity;
			rotation.y += Input.GetAxis(yAxis) * sensitivity;
			rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);

			var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
			var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

			transform.localRotation = xQuat * yQuat;
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}

		if (Input.GetMouseButtonDown(0)) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
}
