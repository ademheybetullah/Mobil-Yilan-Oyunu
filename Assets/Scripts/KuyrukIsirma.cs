using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KuyrukIsirma : MonoBehaviour
{
    public AudioClip yanma;
    GameObject yilan;
    void Start()
    {
        yilan = GameObject.FindGameObjectWithTag("yilan");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position == yilan.gameObject.transform.position)
        {
            yilan.GetComponent<Animator>().enabled = true;
            yilan.GetComponent<Hareket>().yilanAdimlari = 0;
            yilan.GetComponent<Hareket>().yilanYon = new Vector3(0, 0, 0);
            yilan.GetComponent<AudioSource>().PlayOneShot(yanma,0.7f);
            Invoke("tekrarBasla", 1.5f);
        }
    }
    public void tekrarBasla()
    {
        SceneManager.LoadScene(1);
    }
}
