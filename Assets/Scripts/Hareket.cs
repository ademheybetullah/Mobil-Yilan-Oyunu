using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hareket : MonoBehaviour
{
    public Text skorText;
    public AudioClip yemek;
    public List<GameObject> kuyruklar;
    public Vector3 yilanYon;
    public float yilanAdimlari,skor;
    public List<Vector3> pozisyonlar;
    public GameObject yeniKuyruk;
    public GameObject meyve;
    Vector2 ilkPoz, sonPoz;
    void Start()
    {
        skor = 0;
        skorText.text = skor + "";
        yilanAdimlari = this.gameObject.GetComponent<SpriteRenderer>().size.x;
        yilanYon = new Vector3(yilanAdimlari, 0, 0);
        InvokeRepeating("HareketEt", 0.8f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                ilkPoz = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                sonPoz = new Vector2(t.position.x, t.position.y);

                if (Math.Abs(ilkPoz.x - sonPoz.x) > Math.Abs(ilkPoz.y - sonPoz.y))
                {
                    if (ilkPoz.x > sonPoz.x)
                    {
                        yilanYon = new Vector3(-yilanAdimlari, 0, 0);
                    }
                    if (ilkPoz.x < sonPoz.x)
                    {
                        yilanYon = new Vector3(yilanAdimlari, 0, 0);
                    }
                }
                if (Math.Abs(ilkPoz.x - sonPoz.x) < Math.Abs(ilkPoz.y - sonPoz.y))
                {
                    if (ilkPoz.y > sonPoz.y)
                    {
                        yilanYon = new Vector3(0, -yilanAdimlari, 0);
                    }
                    if (ilkPoz.x < sonPoz.x)
                    {
                        yilanYon = new Vector3(0, yilanAdimlari, 0);
                    }
                }
            }
        }
        if (GameObject.FindGameObjectWithTag("meyve") == null)
            {
                Instantiate(meyve, new Vector3(UnityEngine.Random.Range(-20, 20) * yilanAdimlari, UnityEngine.Random.Range(-10, 10) * yilanAdimlari, this.gameObject.transform.position.z), Quaternion.identity);
            }
            if (this.gameObject.transform.position == GameObject.FindGameObjectWithTag("meyve").transform.position)
            {
                kuyruklar.Add(Instantiate(yeniKuyruk, GameObject.FindGameObjectWithTag("meyve").transform.position, new Quaternion(0, 0, 0, 0)));
                skor++;
                skorText.text = skor + "";
                Destroy(GameObject.FindGameObjectWithTag("meyve"));
                gameObject.GetComponent<AudioSource>().PlayOneShot(yemek, 1f);
             }
        for (int i = 0; i < kuyruklar.Count; i++)
        {
            kuyruklar[kuyruklar.Count - 1 - i].transform.position = pozisyonlar[pozisyonlar.Count - 1 - i];
        }
    }
        public void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.gameObject.tag == "altUst")
            {
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, -this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            }
            if (collision.gameObject.tag == "sagSol")
            {
                this.gameObject.transform.position = new Vector3(-this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            }
        }
        public void HareketEt()
        {
            pozisyonlar.Add(this.gameObject.transform.position);
            this.gameObject.transform.position += yilanYon;
            for (int i = 0; i < kuyruklar.Count; i++)
            {
                kuyruklar[kuyruklar.Count - 1 - i].transform.position = pozisyonlar[pozisyonlar.Count - 1 - i];
            }
        }
    }


