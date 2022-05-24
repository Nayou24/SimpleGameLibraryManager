using NetDimension.NanUI;
using NetDimension.NanUI.HostWindow;
using NetDimension.NanUI.JavaScript;
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
        RegisterJavaScriptObject("natives", obj);
        //ExecuteJavaScript("alert('Hello NanUI')");
    }
}