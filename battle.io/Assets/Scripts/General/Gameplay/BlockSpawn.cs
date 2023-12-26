using System.Collections;
using UnityEngine;

public class BlockSpawn : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
