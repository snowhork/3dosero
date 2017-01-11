﻿using System;
using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T instance;
	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				Type t = typeof(T);

				instance = (T)FindObjectOfType(t);
				if (instance == null)
				{
					Debug.LogError(t + " をアタッチしているGameObjectはありません");
				}
			}

			return instance;
		}
	}

	virtual protected void Awake ()
	{
		if (this != Instance)
		{
			Destroy(this);
			//Destroy(this.gameObject);
			Debug.LogError(
				typeof(T) +
				" は既に他のGameObjectにアタッチされているため、コンポーネントを破棄しました." +
				" アタッチされているGameObjectは " + Instance.gameObject.name + " です.");
			return;
		}
		//DontDestroyOnLoad(this.gameObject);
	}

}

