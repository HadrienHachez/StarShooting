﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderIntro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.StartIntro();
    }
}
