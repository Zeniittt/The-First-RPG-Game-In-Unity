using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator anim;
    public string id;
    public bool activationStatus;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    [ContextMenu("Generate Checkpoint Id")]
    private void GenerateId()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
            ActivateCheckpoint();
    }

    public void ActivateCheckpoint()
    {
        if(activationStatus == false)
            AudioManager.instance.PlaySFX(5, transform);

        if (anim == null)
            anim = GetComponent<Animator>();
        activationStatus = true;
        anim.SetBool("active", true);
    }

}
