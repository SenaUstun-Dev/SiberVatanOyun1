using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float movementSpeed = 5.0f;
    public float rotationSpeed = 1.0f;
    private Animator animator;
    public List<GameObject> goldList;
    public int carry;

    public float reduceSpeed = 0.5f;
    private float baseMovementSpeed;

    public int CarryLimit => goldList.Count;
    public Transform boneParent;
    public bool CanMove = true;
    public Transform spinePosition;


    private void Start()
    {
        baseMovementSpeed = movementSpeed;

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        Ragdoll(false);
    }

    private void Update()
    {
        if (!CanMove) return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        var movementDirection = new Vector3(horizontal, 0, vertical);

        Debug.Log(movementDirection);

        animator.SetBool("isRunning", rb.velocity != Vector3.zero);
        animator.SetBool("isCarrying", carry != 0);
        //animator.SetBool("isRunning", rb.velocity != Vector3.zero); alterntif yol

        if (movementDirection == Vector3.zero)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        rb.velocity = movementDirection * movementSpeed;

        var rotationDirection = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationDirection, rotationSpeed * Time.deltaTime);
    }

    public bool CollectGold()
    {
        if (carry == CarryLimit) return false;

        goldList[carry].gameObject.SetActive(true);
        carry++;
        movementSpeed -= reduceSpeed;
        return true;
    }

    public int LoadGoldsToTruck()
    {
        var carryingGold = carry;

        if (carryingGold == 0) return 0;

        foreach (var gold in goldList)
        {
            gold.SetActive(false);
        }
        carry = 0;
        movementSpeed = baseMovementSpeed;

        return carryingGold;
    }

    public void Ragdoll(bool isActive)
    {
        animator.enabled = !isActive;

        var colliders = boneParent.GetComponentsInChildren<Collider>(); // GetComponentsInChildren kullanýlmalý
        var rigidbodies = boneParent.GetComponentsInChildren<Rigidbody>(); // GetComponentsInChildren kullanýlmalý

        foreach (var coll in colliders)
            coll.enabled = isActive;

        foreach (var rigidbody in rigidbodies)
            rigidbody.isKinematic = !isActive; // rigidbody yerine rigidbodies olmalý

        GetComponent<Collider>().enabled = !isActive;

        CanMove = !isActive;

        if (!isActive) // isActive = false yerine !isActive olmalý
        {
            StartCoroutine(CloseRagdoll());
        }
    }

    public IEnumerator CloseRagdoll() // IEnumerable yerine IEnumerator olmalý
    {
        yield return new WaitForSeconds(5f);
        Ragdoll(false);

        transform.position = new Vector3(spinePosition.position.x, 0, spinePosition.position.z);
    }
}
