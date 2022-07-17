using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePatternController : MonoBehaviour
{
    [SerializeField] FirePattern[] patterns;

    private FireController _fireController;
    private int index = -1;
    // Start is called before the first frame update
    void Awake()
    {
        _fireController = GetComponent<FireController>();
    }


    private void OnEnable()
    {
        index = Random.Range(0, patterns.Length);
        _fireController.SetFirePattern(patterns[index]);
    }
    private void OnLevelWasLoaded(int level)
    {
        index = Random.Range(0, patterns.Length);
        _fireController.SetFirePattern(patterns[index]);

    }
}
