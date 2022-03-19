using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public Rigidbody rb;
    //[SerializeField]
    public float _moveSpeed;
    [SerializeField]
    private Vector2 _firstPos,_secondPos,_currentPos;
    public float _currentGroundNumber;
    public Image _levelBar;
    private GameManager gameManager;
    private Vector3 targetPos;
    private bool haraketEt = false;
    private Vector3 ballPosition = new Vector3(-3,0,-3);
    Vector2 distance;

    [SerializeField]
    private GameObject walls;
    [SerializeField]
    private GameObject grounds;

    private MapCreate mapCreate;

    void Start()
    {
        this.transform.position = ballPosition;
        Constraints();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        mapCreate = GameObject.FindObjectOfType<MapCreate>();
    }

    // Update is called once per frame
    void Update()
    {
        //Swipe();
        SwipeKeyboard();
        _levelBar.fillAmount = _currentGroundNumber / gameManager._groundNumbers;
        if (_levelBar.fillAmount == 1)
        {
            //gameManager.LeveLUpdate();//Bir Sonraki Sahneye Geçiş.
                      
            if (grounds.transform.childCount != 0)
            {
                Destroy(grounds.transform.GetChild(0).gameObject);
            }
            else if (walls.transform.childCount != 0)
            {
                Destroy(walls.transform.GetChild(0).gameObject);
            }
            else if (this.gameObject.activeSelf == true)
            {
                this.transform.position = ballPosition;
                if (mapCreate.levelCount <= mapCreate.images.Length)
                {
                    mapCreate.levelCount++;
                    gameManager.downLevel.text =  (mapCreate.levelCount + 1).ToString();
                    gameManager.upLevel.text = (mapCreate.levelCount + 2).ToString();
                    mapCreate.MapImageReadAndCreate();
                    _levelBar.fillAmount = 0;
                    _currentGroundNumber = 0;
                    gameManager.ParcaSayisiniBul();
                }
                else
                {
                    Debug.Log("<color=red><b>Oyun Bitti!</b></color>");
                }                
            }
            
        }

        //Hareket();
        if (haraketEt==true)
        {
            //Hareket(this.gameObject,targetPos,_moveSpeed);
            if (Hareket(this.gameObject, targetPos, _moveSpeed)==true)
            {
                haraketEt = false;
            }
        }
    }
    private void Swipe()
    {
        if (Input.GetMouseButtonDown(0)) {
            _firstPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0)) {
            _secondPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            _currentPos = new Vector2(_secondPos.x - _firstPos.x, _secondPos.y - _firstPos.y);
        }
        _currentPos.Normalize();

        if (_currentPos.y < 0 && _currentPos.x > -0.5f && _currentPos.x < 0.5f)
        {
            //Back
            rb.velocity = Vector3.back * _moveSpeed;
        }
        else if (_currentPos.y > 0 && _currentPos.x > -0.5f && _currentPos.x < 0.5f)
        {
            //Forwad
            rb.velocity = Vector3.forward * _moveSpeed;
        }
        if (_currentPos.x < 0 && _currentPos.y > -0.5f && _currentPos.y < 0.5f)
        {
            //Left
            rb.velocity = Vector3.left * _moveSpeed;
        }
        else if (_currentPos.x > 0 && _currentPos.y > -0.5f && _currentPos.y < 0.5f)
        {
            //Right
            rb.velocity = Vector3.right * _moveSpeed;
        }
    }
    private void SwipeKeyboard()
    {
        
        
        if (Input.GetKey(KeyCode.W))
        {           
            RaycastHit hit;
            
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),out hit,Mathf.Infinity))
            {
                Debug.DrawRay(transform.position,transform.TransformDirection(Vector3.forward)*hit.distance,Color.green);
                if (hit.collider.tag=="Wall")
                {
                    Debug.Log("Işın duvara Çarptı");
                    Debug.Log(hit.transform.position);
                    Debug.Log("Hedef Konum : Vector("+ hit.transform.position.x+" , "+ hit.transform.position.y +" , "+ (hit.transform.position.z-1f) + ");");
                    targetPos = new Vector3(hit.transform.position.x, hit.transform.position.y, (hit.transform.position.z - 1f));
                    Debug.Log(targetPos);
                    haraketEt = true;
                }
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * hit.distance, Color.green);
                if (hit.collider.tag == "Wall")
                {
                    Debug.Log("Işın duvara Çarptı");
                    Debug.Log(hit.transform.position);
                    Debug.Log("Hedef Konum : Vector(" + hit.transform.position.x + " , " + hit.transform.position.y + " , " + (hit.transform.position.z +1f) + ");");
                    targetPos = new Vector3(hit.transform.position.x, hit.transform.position.y, (hit.transform.position.z +1f));
                    Debug.Log(targetPos);
                    haraketEt = true;
                }
            }
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.green);
                if (hit.collider.tag == "Wall")
                {
                    Debug.Log("Işın duvara Çarptı");
                    Debug.Log(hit.transform.position);
                    Debug.Log("Hedef Konum : Vector(" + (hit.transform.position.x -1f)+ " , " + hit.transform.position.y + " , " + hit.transform.position.z + ");");
                    targetPos = new Vector3((hit.transform.position.x -1f), hit.transform.position.y, hit.transform.position.z);
                    Debug.Log(targetPos);
                    haraketEt = true;
                }
            }
        }

        else if (Input.GetKey(KeyCode.A))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * hit.distance, Color.green);
                if (hit.collider.tag == "Wall")
                {
                    Debug.Log("Işın duvara Çarptı");
                    Debug.Log(hit.transform.position);
                    Debug.Log("Hedef Konum : Vector(" + (hit.transform.position.x +1f) + " , " + hit.transform.position.y + " , " + hit.transform.position.z + ");");
                    targetPos = new Vector3((hit.transform.position.x  +1f), hit.transform.position.y, hit.transform.position.z);
                    Debug.Log(targetPos);
                    haraketEt = true;
                }
            }
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<MeshRenderer>().material.color != Color.red)
        {
            if (collision.gameObject.tag == "Ground")
            {
                collision.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                _currentGroundNumber++;
            }
        }
        
    }
    private void Constraints()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }
    public bool Hareket(GameObject obje1, Vector3 obje2, float haraketHizi)
    {
        bool hareketDurdur = false;
        
        if (obje1.transform.position == obje2)
        {
            hareketDurdur = true;
        }
        else
        {
            obje1.transform.position = Vector3.MoveTowards(obje1.transform.position, obje2, haraketHizi * Time.deltaTime);
        }

        return hareketDurdur;
    }
}
