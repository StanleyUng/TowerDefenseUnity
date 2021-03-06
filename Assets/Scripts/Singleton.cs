﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// T is a MonoBehaviour no matter what
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

   private static T instance;
   public static T Instance { 
      get {
         if (instance == null) {
            instance = FindObjectOfType<T>();
         }
         return instance;
      } 
   }
}
