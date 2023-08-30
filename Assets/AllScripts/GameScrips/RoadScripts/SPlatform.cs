using System.Collections.Generic;
using UnityEngine;

public class SPlatform : MonoBehaviour
{
    public StateMonitor monitor;
    public SRoad road;
    public GameObject platform;
    public GameObject bridgeParticle;
    public List<GameObject> copyPlatform = new List<GameObject>();
    public SBridgeSpawner bridgeSpawner;
    public SCameraBody camBody;
    public Transform platformsTransform;

    private Renderer render;
    public int currentIndexPlatform;
    private readonly Vector3 firstPlatformCenterPos = new Vector3(-4, -20f, 0);
    protected internal int[] platforms = { 5, 5, 6, 7, 8, 9, 10, 11, 12 }; //���������� �������� (�� �������)
    protected internal float GetPlatformTop { get; private set; }
    protected internal float GetMaxPlatformZ { get; private set; }
    private float stepSize;

    private void Awake()
    {
        stepSize = bridgeParticle.GetComponent<Renderer>().bounds.size.y;
        AddNewPlatform();
        GetPlatformTop = GetRenderPlatformInfo(0).bounds.max.y;
        GetMaxPlatformZ = GetRenderPlatformInfo(0).bounds.max.z;
    }
    void Update()
    {
        if (currentIndexPlatform != platforms[SBoxPanel.SelectedLevel] && bridgeSpawner.brideComplite)
        {
            currentIndexPlatform++;
            bridgeSpawner.brideComplite = false;
        }
    }
    internal protected Vector3 GetPlatformPositionInfo()
    {
        return firstPlatformCenterPos;
    }
    internal protected Renderer GetRenderPlatformInfo(int platformNum)
    {
        render = copyPlatform[platformNum].GetComponent<Renderer>();
        return render;
    }
    public void AddNewPlatform()
    {
        const float minDistance = 10f;
        const float maxDistance = 20f;
        const float minScaleX = 0.2f;
        const float maxScaleX = 1.2f;

        Vector3 localScale;

        copyPlatform.Add(Instantiate(platform, Vector3.zero, Quaternion.identity, platformsTransform));
        copyPlatform[0].transform.position = new Vector3(firstPlatformCenterPos.x, firstPlatformCenterPos.y, firstPlatformCenterPos.z);

        for (int i = 1; i < platforms[SBoxPanel.SelectedLevel]; i++)
        {
            float randomScale = (Random.Range(minScaleX, maxScaleX) / stepSize) * stepSize;
            float randomDistance = Random.Range(minDistance, maxDistance);
            float roundedDistance = Mathf.Round(randomDistance / stepSize) * stepSize;
            float lastPlatformMaxPos_x = GetRenderPlatformInfo(i - 1).bounds.max.x;
            float newPlatformPos_x = lastPlatformMaxPos_x + roundedDistance;
            float newPlatformPos_y = copyPlatform[i - 1].transform.position.y;
            Vector3 newPlatformPos = new Vector3(newPlatformPos_x, newPlatformPos_y, 0);

            copyPlatform.Add(Instantiate(platform, Vector3.zero, Quaternion.identity, platformsTransform));
            if (i == platforms[SBoxPanel.SelectedLevel] - 1)
                localScale = platform.transform.localScale;
            else
                localScale = new Vector3(stepSize / 2, platform.transform.localScale.y, platform.transform.localScale.z);
            copyPlatform[i].transform.localScale = localScale;
            copyPlatform[i].transform.position = newPlatformPos;
            Debug.Log(stepSize);
        }
        
    }
}
