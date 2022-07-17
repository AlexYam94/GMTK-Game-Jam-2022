using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePatternController : MonoBehaviour
{
    [SerializeField] FirePattern[] patterns;

    private FireController _fireController;
    // Start is called before the first frame update
    void Awake()
    {
        _fireController = GetComponent<FireController>();
    }

    private void OnEnable()
    {
        int i = Random.Range(0, patterns.Length);
        _fireController.SetFirePattern(patterns[i]);
    }
}
