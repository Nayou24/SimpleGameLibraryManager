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
    }

    protected override void OnReady()
    {
        // �ڴ˴������������ز���

        //ShowDevTools();
        //ExecuteJavaScript("alert('Hello NanUI')");
    }
}