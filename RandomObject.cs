using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class RandomObject : MonoBehaviour
{
    public GameObject[] prefab = new GameObject[5];
    public GameObject obj;
    RaycastHit hit, theObject;
    Ray ray;
    Vector3 screenPoints;
    Vector3 offset;
    private Color prevcolor;
    int temp = 0;

    public static string selectedObject;
    public string internalObject;

    public float animationTime = 1f;
    public float threshold = 1.5f;

    private HighlightController controller;
    private Material material;
    private Color normalColor;
    private Color selectedColor;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        controller = FindObjectOfType<HighlightController>();

        normalColor = material.color;
        selectedColor = new Color(
          Mathf.Clamp01(normalColor.r * threshold),
          Mathf.Clamp01(normalColor.g * threshold),
          Mathf.Clamp01(normalColor.b * threshold)
        );
    }
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0) && temp < 5)
            {
                // Instantiate new prefab with every left click
                GameObject obj = Instantiate(prefab[Random.Range(0,4)], new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity) as GameObject;
                screenPoints = Camera.main.WorldToScreenPoint(transform.position);

                temp++;

                RaycastHit[] hits = Physics.RaycastAll(ray);
                GameObject hitObject = hit.transform.root.gameObject;
               // selectObject(hitObject);
            }
        }
    }
    public void StartHighlight()
    {
        iTween.ColorTo(gameObject, iTween.Hash(
          "color", selectedColor,
          "time", animationTime,
          "easetype", iTween.EaseType.linear,
          "looptype", iTween.LoopType.pingPong
        ));
    }

    public void StopHighlight()
    {
        iTween.Stop(gameObject);
        material.color = normalColor;
    }

    private void OnMouseDown()
    {
        controller.SelectObject(this);
    }
}
