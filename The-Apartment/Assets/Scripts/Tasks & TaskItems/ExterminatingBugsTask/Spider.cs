using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{

    #region Variables
    public GameObject sinkOverlay;
    ExterminateBugTask bugTask;
    float y1 = 17.8f;
    float y2 = 19.4f;
    float y3 = 25f;
    float rx1 = -117.5f;
    float rx2 = -120.0f;
    float angle = 45;
    public Animator anim;
    private SpriteRenderer sr;
    public Sprite smush1;
    public Sprite smush2;
    public Sprite smush3;
    bool dead = false;
    public GameObject smush;
    #endregion

    void Start()
    {
        sinkOverlay = GameObject.Find("SinkOverlay");
        bugTask = sinkOverlay.GetComponent<ExterminateBugTask>();
        anim = GetComponent<Animator>();
        StartCoroutine(CrawlRight());
        sr = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {

        Debug.Log("Killed Bug");
        //Changed to squashed bug sprite
        bugTask.incrementProgress();
        StartCoroutine(Squash());
    }

    IEnumerator Squash()
    {
        Instantiate(smush, transform.position, Quaternion.identity);
        //TODO: Add fade out
        Destroy(this.gameObject);
        yield return null;
    }

    IEnumerator CrawlUp()
    {
        double crawlAngle = (double)Random.Range(80, 120);//90-angle, 90 + angle);
        transform.Rotate(Vector3.forward * ((float)crawlAngle - 90f));
        crawlAngle *= System.Math.PI / 180;
        Debug.Log(crawlAngle);
        Debug.Log("Crawling");
        Debug.Log(transform.position);
        while (transform.position.y < y1)
        {
            transform.position += new Vector3(0.1f * (float)System.Math.Cos(crawlAngle), 0.1f * (float)System.Math.Sin(crawlAngle), 0);
            yield return new WaitForSeconds(0.07f);
        }
        anim.SetBool("OnSide", true);
        while (transform.position.y < y2)
        {
                transform.position += new Vector3(0.1f * (float)System.Math.Cos(crawlAngle), 0.1f * (float)System.Math.Sin(crawlAngle), 0);
                yield return new WaitForSeconds(0.085f);
        }
        anim.SetBool("OnSide", false);
        while (transform.position.y < y3)
        {
            transform.position += new Vector3(0.1f * (float)System.Math.Cos(crawlAngle), 0.1f * (float)System.Math.Sin(crawlAngle), 0);
            yield return new WaitForSeconds(0.07f);
        }
        Destroy(this.gameObject);
    }

    IEnumerator CrawlRight()
    {
        transform.Rotate(Vector3.forward * (90f));
        while (transform.position.x > rx1)
        {
                transform.position += new Vector3(-0.1f, 0, 0);//0.1f * (float)System.Math.Cos(crawlAngle), 0.1f * (float)System.Math.Sin(crawlAngle), 0);
                yield return new WaitForSeconds(0.07f);
        }
        transform.position = new Vector3(-115.5f, transform.position.y, 0);
        sr.sortingOrder = 3;
        while (transform.position.x > rx2)
        {
                transform.position += new Vector3(-0.1f, 0, 0);//0.1f * (float)System.Math.Cos(crawlAngle), 0.1f * (float)System.Math.Sin(crawlAngle), 0);
                yield return new WaitForSeconds(0.07f);
        }
        Destroy(this.gameObject);
    }

    IEnumerator CrawlDown()
    {
        yield return null;
    }

}
