using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

namespace ChickStar.CommonLibrary.Editor.Installer
{
    /// <summary>
    /// UPMのpackage.jsonではdependenciesにURL指定ができない
    /// このクラスは「ライブラリをUPM配布したいが、依存UPMライブラリをURLで指定する必要があるケースに対して、
    /// UPMインストール前に依存ライブラリをUnityのプロジェクト側にインストールさせる」ことで解決するのを目的としたものである
    /// </summary>
    internal static class DependenciesUrlInstaller
    {
        /// <summary>
        /// ここで指定したURLのUPMがUnityのプロジェクト内に存在しない場合にインストールされる
        /// </summary>
        private static readonly string[] DependencyUrls =
        {
        };

        private enum Status
        {
            Succeeded,
            Failed
        }

        private static Request _currentRequest;
        private static Action<Status> _currentOnCompleteAction;

        [InitializeOnLoadMethod]
        private static void Install()
        {
            StartInstall();
        }

        private static void StartInstall()
        {
            EditorApplication.update += OnProgress;
            GetInstalledList();
        }

        /// <summary>
        /// 現在インストールされているPackageの一覧を取得する
        /// </summary>
        private static void GetInstalledList()
        {
            _currentRequest = Client.List(offlineMode: true);
            _currentOnCompleteAction = status =>
            {
                try
                {
                    var listRequest = (ListRequest)_currentRequest;
                    var resultCollection = listRequest.Result;

                    if (status == Status.Succeeded)
                    {
                        // 取得に成功したら必要なパッケージのインストールを開始する

                        // DependencyUrlsの内、現在インストールされている、
                        // いずれのPackageCollectionのIdにも文字列が含まれないものだけをインストールの対象とする
                        var packageIds = resultCollection
                            .Select(x => x.packageId)
                            .ToList();
                        var installTargets = DependencyUrls
                            .Where(dependencyUrl => !packageIds.Any(x => x.Contains(dependencyUrl)))
                            .ToList();


                        // 対象が無ければここでインストールを終了する
                        if (installTargets.Count == 0)
                        {
                            FinishInstall();
                            return;
                        }

                        InstallPackage(installTargets, 0);
                    }
                    else
                    {
                        // このタイミングでエラーがでる場合は何か致命的な問題がある。
                        // インストールを終了する
                        FinishInstall();
                    }
                }
                catch (Exception e)
                {
                    ExceptionDispatchInfo.Capture(e);
                    FinishInstall();
                }
            };
        }

        /// <summary>
        /// パッケージのインストールリクエストを投げる
        /// </summary>
        /// <param name="installTargets"></param>
        /// <param name="index"></param>
        private static void InstallPackage(List<string> installTargets, int index)
        {
            _currentRequest = Client.Add(installTargets[index]);
            _currentOnCompleteAction = _ =>
            {
                try
                {
                    if (installTargets.Count == index + 1)
                    {
                        // インストール対象の数と次のインデックスが同じになったら最後。Installを終了する
                        FinishInstall();
                        return;
                    }

                    // そうでなければInstallTargetsの次のインデックスをインストールする
                    InstallPackage(installTargets, index + 1);
                }
                catch (Exception e)
                {
                    ExceptionDispatchInfo.Capture(e);
                    FinishInstall();
                }
            };
        }

        private static void FinishInstall()
        {
            SetNullRequestAndAction();
            EditorApplication.update -= OnProgress;
        }

        private static void OnProgress()
        {
            if (_currentRequest == null)
            {
                return;
            }

            if (!_currentRequest.IsCompleted)
            {
                return;
            }

            switch (_currentRequest.Status)
            {
                case StatusCode.Success:
                    _currentOnCompleteAction?.Invoke(Status.Succeeded);
                    break;
                case >= StatusCode.Failure:
                    UnityEngine.Debug.LogError(_currentRequest.Error.message);
                    _currentOnCompleteAction?.Invoke(Status.Failed);
                    break;
            }

            SetNullRequestAndAction();
        }

        private static void SetNullRequestAndAction()
        {
            _currentRequest = null;
            _currentOnCompleteAction = null;
        }
    }
}