/*
  reference:
    - (https://docs.unity3d.com/ja/2019.4/Manual/IL2CPP-CompilerOptions.html)
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

namespace App.Runtime.Common
{
	/// <summary>
	///  プロジェクト専用のMonoBehaviourベースクラス.
	///  ※　MonoBehaviourは直接継承せず、基本的にこちらを継承させる.
	/// </summary>
	[Il2CppSetOption(Option.NullChecks, false)]
	internal abstract class MonoBehaviourExtend : MonoBehaviour
	{
	}
}
