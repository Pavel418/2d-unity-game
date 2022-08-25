using UnityEngine;

public class CameraMove : MonoBehaviour


{
    [SerializeField] private int speed;
    [SerializeField] private Transform target;
    [SerializeField] private float offsetX, offsetY;
    [SerializeField] private SpriteRenderer spr;
    

    private Vector3 temp;
    public GameObject _obj;
    private CharacterController _import;

    private void Start()
    {
        _import = _obj.GetComponent<CharacterController>();
    }

    void LateUpdate()
    {
        temp = target.position;
        temp.x += offsetX;
        temp.y += offsetY;
        temp.z = -10;
        transform.position = Vector3.Lerp(transform.position, temp, speed * Time.deltaTime);


         if (_import.isFlipped == true && Input.GetKey(KeyCode.LeftShift))
        {
            offsetX = -10;
        }
        else if (_import.isFlipped != true && Input.GetKey(KeyCode.LeftShift))
        {
            offsetX = 10;
        }
        else
        {
            offsetX = 0;
        }
        
    }
}
