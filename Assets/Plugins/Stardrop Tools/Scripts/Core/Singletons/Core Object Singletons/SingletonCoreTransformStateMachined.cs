﻿
using UnityEngine;
using StardropTools.FiniteStateMachine.EventFiniteStateMachine;

namespace StardropTools.Singletons
{
	[RequireComponent(typeof(EventStateMachine))]
	public abstract class SingletonCoreTransformStateMachined<T> : CoreTransformStateMachined where T : Component
	{
		//public static CustomEvent OnEnter { get; private set; }
		//public static CustomEvent OnExit { get; private set; }
		//public static CustomEvent OnUpdate { get; private set; }

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
		#endregion // manager singleton

		public override void Initialize()
		{
			base.Initialize();

			SingletonInitialization();
		}

		protected override void OnValidate()
		{
			base.OnValidate();

			if (eStateMachine == null)
				eStateMachine = GetComponent<EventStateMachine>();
		}
	}
}

