﻿
using UnityEngine;

namespace StardropTools.Singletons
{
	public abstract class SingletonBaseManager<T> : BaseManager where T : Component
	{
		#region Manager Singleton
		/// <summary>
		/// The instance.
		/// </summary>
		private static T instance;


		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					instance = FindObjectOfType<T>();
					if (instance == null)
					{
						GameObject obj = new GameObject();
						obj.name = typeof(T).Name;
						instance = obj.AddComponent<T>();
					}
				}
				return instance;
			}
		}


		void SingletonInitialization()
		{
			if (instance == null)
			{
				instance = this as T;
				DontDestroyOnLoad(gameObject);
			}

			else
				Destroy(gameObject);
		}


		public override void Initialize()
		{
			base.Initialize();

			SingletonInitialization();
		}
		#endregion // manager singleton
	}
}

