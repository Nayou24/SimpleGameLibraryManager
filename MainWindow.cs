using NetDimension.NanUI;
using NetDimension.NanUI.HostWindow;
using NetDimension.NanUI.JavaScript;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

class MainWindow : Formium
{
    // ���ô�����ʽ����
    public override HostWindowType WindowType => HostWindowType.System;
    // ָ������ Url
    public override string StartUrl => "https://static.app.local/Main.html";

    public MainWindow()
    {
        // �ڴ˴����ô�����ʽ
       
        Size = new System.Drawing.Size(1280, 720);
        EnableSplashScreen = false;
        Title = "SimpleGameLibraryManager";
        Icon = new System.Drawing.Icon(@"..\..\..\Libman\ico\gal.ico");
    }

    protected override void OnReady()
    {
        // �ڴ˴������������ز���

        ShowDevTools();
        var obj = new JavaScriptObject();
        
        obj.Add("SelectGamePath", (args =>
        {
            string filePath = "";
            InvokeIfRequired(() =>
            {
                FolderBrowserDialog folder = new FolderBrowserDialog();
                folder.Description = "ѡ��һ��Ŀ¼";
                if (folder.ShowDialog() == DialogResult.OK)
                {
                    filePath = folder.SelectedPath;
                }
            });
            return new JavaScriptValue(filePath);
        }));
        obj.Add("OpenGame", (args =>
        {
            var gid = args.FirstOrDefault(x => x.IsString)?.GetString() ?? "";
            int key;
            if (!int.TryParse(gid, out key))
            {
                return new JavaScriptValue("��ϷID����ȷ");
            }
            if (SimpleGameLibraryManager.BackGrounds.lib.ContainsKey(key))
            {
                var path = SimpleGameLibraryManager.BackGrounds.lib[key].m_execPath;
                SimpleGameLibraryManager.BackGrounds.lib_recent.Add(key, SimpleGameLibraryManager.BackGrounds.lib[key]);
                InvokeIfRequired(() =>
                {
                    Process.Start(path);
                });

            }
            
            return new JavaScriptValue(0);
        }));
        RegisterJavaScriptObject("natives", obj);
        //ExecuteJavaScript("alert('Hello NanUI')");
    }
}