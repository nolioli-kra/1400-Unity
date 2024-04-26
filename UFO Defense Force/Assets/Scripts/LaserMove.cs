using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMove : MonoBehaviour
{
    [SerializeField] float speed;

    private AudioSource laserAudio;
    public AudioClip destroySound;

    // Start is called before the first frame update
    void Start()
    {
        laserAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            laserAudio.PlayOneShot(destroySound, 1f);
        }
    }
}
