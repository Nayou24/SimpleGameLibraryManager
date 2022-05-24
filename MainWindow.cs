using NetDimension.NanUI;
using NetDimension.NanUI.HostWindow;

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
        //ExecuteJavaScript("alert('Hello NanUI')");
    }
}