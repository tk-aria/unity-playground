/*
  reference:
    - 実装に関する参考資料をこちらに添付する
*/
using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace App.Runtime
{
	/// <summary>
	///  何の仕事を担当するクラスか？
	/// </summary>
	public sealed class AttributeTest : MonoBehaviour
	{
        #region Field

        [ReorderableList] public List<GameObject> objList;

        #endregion Field

		#region Method

		void Start()
		{

		}

		void Update()
		{

		}

		#endregion Method
	}
}
