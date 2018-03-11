using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalCarousel : MonoBehaviour
{
    public GameObject[] ItemPrefabs;
    [SerializeField]
    private List<GameObject> mItemsObj;
    private List<Vector3> mItemsPos;
    private List<Vector3> mItemsScale;
    private List<int> mItemsLayer;
    /// <summary>
    /// The number of game frame which object properties change value.
    /// </summary>
    private int mFrame = 10;

    private void Awake()
    {
        mItemsObj = new List<GameObject>();
        mItemsPos = new List<Vector3>();
        mItemsScale = new List<Vector3>();
        mItemsLayer = new List<int>();
        if (ItemPrefabs != null && ItemPrefabs.Length > 0)
        {
            for (int i = 0; i < ItemPrefabs.Length; i++)
            {
                Vector3 temPos = new Vector3(ItemPrefabs[i].transform.localPosition.x
                    , ItemPrefabs[i].transform.localPosition.y
                    , ItemPrefabs[i].transform.localPosition.z);
                mItemsPos.Add(temPos);
                Vector3 temScale = new Vector3(ItemPrefabs[i].transform.localScale.x
                    , ItemPrefabs[i].transform.localScale.y
                    , ItemPrefabs[i].transform.localScale.z);
                mItemsScale.Add(temScale);
                mItemsLayer.Add(ItemPrefabs[i].GetComponent<SpriteRenderer>().sortingOrder);
                mItemsObj.Add(ItemPrefabs[i]);
            }
        }

        //InvokeRepeating("OnRepeatSpin", 0.0f, 1f);
        StartCoroutine(Spin());
    }

    private void OnRepeatSpin()
    {
        StartCoroutine(Spin());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("=========");
            StartCoroutine(Spin());
        }
    }

    private IEnumerator Spin()
    {
        int loop = ItemPrefabs.Length;
        int j = 0;
        for (int i = 0; i < loop; i++)
        {
            j = i + 1;
            if (j > loop - 1)
            {
                j = 0;
            }
            SwapTo(ItemPrefabs[i], i, j);

            yield return new WaitForEndOfFrame();
        }

        for (int i = 0; i < loop; i++)
        {
            ItemPrefabs[i] = mItemsObj[i];
        }
    }

    private void SwapTo(GameObject tObj, int curIndex, int desIndex)
    {
        LayerTo(tObj, mItemsLayer[desIndex]);

        StartCoroutine(MoveTo(tObj, mItemsPos[desIndex]));
        StartCoroutine(ScaleTo(tObj, mItemsScale[desIndex]));
        mItemsObj[desIndex] = tObj;
    }

    private IEnumerator MoveTo(GameObject tObj, Vector3 des)
    {
        Transform tTrans = tObj.transform;
        float deltaMoveX = tTrans.localPosition.x - des.x;
        float deltaMoveY = tTrans.localPosition.y - des.y;

        float rate = (float)1 / mFrame;
        float moveX = Mathf.Abs(deltaMoveX) * rate;
        float moveY = Mathf.Abs(deltaMoveY) * rate;
        if (deltaMoveX > 0)
        {
            moveX *= -1;
        }
        if (deltaMoveY > 0)
        {
            moveY *= -1;
        }
        for (int i = 0; i < mFrame; i++)
        {
            tTrans.localPosition += new Vector3(moveX, moveY, 0.0f);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator ScaleTo(GameObject tObj, Vector3 des)
    {
        Transform tTrans = tObj.transform;
        float deltaScaleX = tTrans.localScale.x - des.x;
        float deltaScaleY = tTrans.localScale.y - des.y;

        float rate = (float)1 / mFrame;
        float scaleX = Mathf.Abs(deltaScaleX) * rate;
        float scaleY = Mathf.Abs(deltaScaleY) * rate;
        if (deltaScaleX > 0)
        {
            scaleX *= -1;
        }
        if (deltaScaleY > 0)
        {
            scaleY *= -1;
        }
        for (int i = 0; i < mFrame; i++)
        {
            tTrans.localScale += new Vector3(scaleX, scaleY, 0.0f);

            yield return new WaitForEndOfFrame();
        }
    }

    private void LayerTo(GameObject tObj, int desLayer)
    {
        SpriteRenderer renderer = tObj.GetComponent<SpriteRenderer>();
        renderer.sortingOrder = desLayer;
    }
}
