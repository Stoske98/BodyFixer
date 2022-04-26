using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Girl : MonoBehaviour
{
    // Start is called before the first frame update
    private bool End;
    public Transform target;
    private float distance;
    private Vector3 startPosition;
    private Vector3 targetStartPosition;
    public float timer = 1;
    float time = 0;

    public GameObject particleWellDone1;
    public GameObject particleWellDone2;

    public GameObject particleBrilliant1;
    public GameObject particleBrilliant2;

    public GameObject particle;
    public Material material;
    public Texture2D[] textures = new Texture2D[3];
    void Start()
    {
        timer /= 100;
        startPosition = transform.position;
        targetStartPosition = target.localPosition;
        
    }
    public void ActivateEnd()
    {
        End = true;
        if (Manager.GameManager.Instance.percent > 75)
        {
            material.mainTexture = textures[0];
            particle.SetActive(true);
            particleBrilliant1.SetActive(true);
            particleBrilliant2.SetActive(true);

        }
        else if (Manager.GameManager.Instance.percent >= 25)
        {
            material.mainTexture = textures[1];
            particle.SetActive(true);
            particleWellDone1.SetActive(true);
            particleWellDone2.SetActive(true);
        }
        else
        {
            material.mainTexture = textures[2];
            particle.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(End)
        {
            time += Time.deltaTime;
            if (Manager.GameManager.Instance.percent > 75)
            {
                
                distance = Vector3.Distance(startPosition, startPosition - new Vector3(0.05f, 0, 0));
                transform.position = Vector3.Lerp(startPosition, startPosition - new Vector3(0.05f, 0, 0), (time / timer) * distance);

                distance = Vector3.Distance(targetStartPosition, new Vector3(-1, -0.1f, 0.85f));
                target.localPosition = Vector3.Lerp(targetStartPosition, new Vector3(-1, -0.1f, 0.85f), (time / timer) * distance);
            }
            else if(Manager.GameManager.Instance.percent >= 25)
            {
                distance = Vector3.Distance(startPosition, startPosition - new Vector3(0.05f, 0, 0));
                transform.position = Vector3.Lerp(startPosition, startPosition - new Vector3(0.05f, 0, 0), (time / timer) * distance);
            }
            else
            {
                distance = Vector3.Distance(startPosition, startPosition - new Vector3(-0.05f, 0, 0));
                transform.position = Vector3.Lerp(startPosition, startPosition - new Vector3(-0.05f, 0, 0), (time / timer) * distance);

                distance = Vector3.Distance(targetStartPosition, new Vector3(1, -0.1f, 0.85f));
                target.localPosition = Vector3.Lerp(targetStartPosition, new Vector3(1, -0.1f, 0.85f), (time / timer) * distance);
            }
        }
    }
}
