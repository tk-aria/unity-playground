#if UNITY_EDITOR
using System.IO;
using UnityEngine;
using UnityEditor;

namespace UnityProject.Editor
{

    /// <summary>
    ///  プロジェクト専用のアセットインポーター.
    /// </summary>
    public class AssetImporter : AssetPostprocessor
    {

        /// <summary>
        ///  設定を自動化する対象ディレクトリ.
        /// </summary>
        private string[] m_folderList = new string[]
        {
            "Assets/App/_AssetFolder"
        };

        /// <summary>
        ///  アセットインポート時に呼ばれる.
        /// </summary>
        /// <param name="import">アセットインポート時</param>
        /// <param name="delete">アセット削除時</param>
        /// <param name="move">アセットの移動先</param>
        /// <param name="moveFrom">アセットの移動元</param>
        void OnPostprocessAllAssets (string[] import, string[] delete, string[] move, string[] moveFrom)
        {
        }

        /// <summary>
        ///  TextureAssetがインポートされた時に呼ばれるやつ.
        /// </summary>
        private void OnPreprocessTexture()
        {
            if (IsExistMetaFile(assetPath))
            {
                return;
            }

            var importer = assetImporter as TextureImporter;
            if (importer == null)
            {
                return;
            }

            if (!IsTargetFolder(assetPath))
            {
                return;
            }

            // Projectの設定を行う.
            importer.textureType = TextureImporterType.Default;

            importer.sRGBTexture = true;
            importer.alphaSource = TextureImporterAlphaSource.FromInput;
            importer.alphaIsTransparency = true;

            importer.npotScale = TextureImporterNPOTScale.None;
            importer.isReadable = false;
            importer.streamingMipmaps = false;
            importer.mipmapEnabled = false;
            importer.wrapMode = TextureWrapMode.Clamp;
            importer.filterMode = FilterMode.Bilinear;

            // platform毎の設定.
            importer.SetPlatformTextureSettings (new TextureImporterPlatformSettings
            {
                overridden = true,
                name = "Android",
                maxTextureSize = 1024,
                format = TextureImporterFormat.RGBA16
            });
            importer.SetPlatformTextureSettings (new TextureImporterPlatformSettings
            {
                overridden = true,
                name = "iPhone",
                maxTextureSize = 1024,
                format = TextureImporterFormat.RGBA16
            });
        }

        /// <summary>
        ///  モデルファイルのimport時
        /// </summary>
        void OnPostprocessModel(GameObject obj) 
        {
            ModelImporter importer = assetImporter as ModelImporter;
        }

        /// <summary>
        ///  対象のパスが適用可能かどうか.
        /// </summary>
        private bool IsTargetFolder(string path)
        {
            bool result = false;
            foreach (var folder in m_folderList)
            {
                result |= assetPath.StartsWith(folder);
            }
            return result;
        }

        /// <summary>
        ///  対象アセットにMetaが存在するか.
        /// </summary>
        /// <returns></returns>
        protected bool IsExistMetaFile(string path)
        {
            return File.Exists(assetPath + ".meta");
        }

    }

}

#endif // UNITY_EDITOR END.