using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    [SerializeField] private float tileMoveSpeed = 5f;
    [SerializeField] private float moveDuration = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public IEnumerator MoveGroundTile()
    {
        var startPos = transform.position;
        var endPos = startPos + Vector3.left;

        float elapsedTime = 0;
            
        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime * tileMoveSpeed;
            yield return null;
        }

        transform.position = endPos;
    }
}
