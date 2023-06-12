using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public static CharacterMovement Instance;

    public GameObject BrickParent;
    public GameObject PrevBrick;
    public Transform CheckBrick;
    public LayerMask mask;

    [HideInInspector] public Rigidbody rb;
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private bool isMoving;

    Direction moveDirection;

    public int currentLevel;

    private Vector3 StartPos;

    private GameObject load_lv1, load_lv2, lv1, lv2;
    GameObject clone;

    private void Start()
    {
        currentLevel = 1;
        Instance = this;
        rb = GetComponent<Rigidbody>();

        load_lv2 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Game/Prefabs/Level2.prefab");
        
        load_lv1 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Game/Prefabs/Level.prefab");

        lv1 = Instantiate(load_lv1, Vector3.zero, Quaternion.identity);
    
        
        PlayerPrefs.SetInt("Level", currentLevel);
        StartPos = transform.position;
        
        clone = BrickParent.transform.GetChild(0).gameObject;
        
    }



    private void FixedUpdate()
    {
        moveDirection = InputHandler.Instance.GetDirection();

        DoRotate(moveDirection);
        if (isMoving)
        {
            DoMove(moveDirection);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }


        RaycastHit hit;
        if (Physics.Raycast(CheckBrick.transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, mask))
        {
            Debug.DrawRay(CheckBrick.transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);

            if (hit.collider.gameObject.layer == 7)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }

        }
    }

    public void PicKGold(GameObject brick)
    {
        brick.transform.SetParent(BrickParent.transform);
        Vector3 pos = PrevBrick.transform.localPosition;
        Quaternion rote = PrevBrick.transform.rotation;
        pos.y -= 0.25f;
        brick.transform.localPosition = pos;
        brick.transform.rotation = rote;
        Vector3 CharacterPos = transform.localPosition;
        CharacterPos.y += 0.25f;
        transform.localPosition = CharacterPos;
        PrevBrick = brick;
        PrevBrick.GetComponent<BoxCollider>().isTrigger = false;
    }

    public void DropGold()
    {
        Vector3 CharacterPos = transform.localPosition;
        CharacterPos.y -= 0.25f;
        transform.localPosition = CharacterPos;
    }

    private void DoMove(Direction moveDirection)
    {
        if (rb.velocity == Vector3.zero)
        {
            switch (moveDirection)
            {
                case Direction.Up:
                    rb.velocity = Vector3.forward * moveSpeed * Time.deltaTime;

                    break;
                case Direction.Down:
                    rb.velocity = -Vector3.forward * moveSpeed * Time.deltaTime;

                    break;
                case Direction.Left:
                    rb.velocity = Vector3.left * moveSpeed * Time.deltaTime;

                    break;
                case Direction.Right:
                    rb.velocity = Vector3.right * moveSpeed * Time.deltaTime;

                    break;
            }
        }
    }

    private void DoRotate(Direction moveDirection)
    {
        if (rb.velocity == Vector3.zero)
        {
            switch (moveDirection)
            {
                case Direction.Up:
                    transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                    break;
                case Direction.Down:
                    transform.eulerAngles = new Vector3(0.0f, -180.0f, 0.0f);
                    break;
                case Direction.Left:
                    transform.eulerAngles = new Vector3(0.0f, -90.0f, 0.0f);
                    break;
                case Direction.Right:
                    transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
                    break;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WinPos")
        {
            currentLevel ++;
            PlayerPrefs.SetInt("Level", currentLevel);
            Destroy(lv1);
            lv2 = Instantiate(load_lv2, Vector3.zero, Quaternion.identity);



            PrevBrick = clone;
            rb.velocity = Vector3.zero;
            BrickParent.transform.SetParent(transform);
            BrickParent.transform.position = transform.position;
            for (int i = 0; i < BrickParent.transform.childCount; i++)
            {
                Destroy(BrickParent.transform.GetChild(i).gameObject);
            }
            clone.transform.SetParent(BrickParent.transform);
            clone.transform.position = transform.position;
            clone.AddComponent<BrickScripts>();
            transform.position = StartPos; 
        }
    }
}
