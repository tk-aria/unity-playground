using NUnit.Framework;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Player;

internal sealed class PlatformTester
{
	private const string OUTPUT_FOLDER = "Temp/CompileChecker";

	[Test]
	public void TestWindows()
	{
		Check( BuildTarget.StandaloneWindows, BuildTargetGroup.Standalone );
	}

	[Test]
	public void TestMacOS()
	{
		Check( BuildTarget.StandaloneOSX, BuildTargetGroup.Standalone );
	}

	[Test]
	public void TestLinux()
	{
		Check( BuildTarget.StandaloneLinux, BuildTargetGroup.Standalone );
	}

	[Test]
	public void TestiOS()
	{
		Check( BuildTarget.iOS, BuildTargetGroup.iOS );
	}

	[Test]
	public void TestAndroid()
	{
		Check( BuildTarget.Android, BuildTargetGroup.Android );
	}

	[Test]
	public void TestkWebGL()
	{
		Check( BuildTarget.WebGL, BuildTargetGroup.WebGL );
	}

	private static void Check( BuildTarget target, BuildTargetGroup group )
	{
		var input = new ScriptCompilationSettings
		{
			target = target,
			group  = group,
		};

		var result     = PlayerBuildInterface.CompilePlayerScripts( input, OUTPUT_FOLDER );
		var assemblies = result.assemblies;

		var isSuccess =
				assemblies != null &&
				assemblies.Count != 0 &&
				result.typeDB != null
			;

		if ( Directory.Exists( OUTPUT_FOLDER ) )
		{
			Directory.Delete( OUTPUT_FOLDER, true );
		}

		Assert.IsTrue( isSuccess );
	}
}