using NetDimension.NanUI;
using NetDimension.NanUI.HostWindow;
using NetDimension.NanUI.JavaScript;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleGameLibraryManager;

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
#if RELEASE
        Icon = new System.Drawing.Icon(@".\Libman\ico\gal.ico");
#else
        Icon = new System.Drawing.Icon(@"C:\Users\ASUS\source\repos\LibMang\Libman\ico\gal.ico");    
#endif

    }

    protected override void OnReady()
    {
        // �ڴ˴������������ز���

        //ShowDevTools();
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
                else
                {
                    filePath = "None";
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
            Task task1 = new Task(() =>
            {
                if (BackGrounds.lib.ContainsKey(key))
                {
                    var path = BackGrounds.lib[key].m_execPath;
                    BackGrounds.lib[key].m_lastOpenedTime = DateTime.Now.ToString();
                    if (!BackGrounds.lib_recent.ContainsKey(key))
                    {
                        BackGrounds.lib_recent.Add(key, BackGrounds.lib[key]);
                    }
/*                    InvokeIfRequired(() =>
                    {*/
                    var StartTime = DateTime.Now;
                    var curRuntime = 0;
                    Process gp = new Process();
                    gp.StartInfo.FileName = path;
                    gp.StartInfo.WorkingDirectory = BackGrounds.lib[key].m_path;
                    gp.Start();
                    BackGrounds.lib[key].m_isOnline = true;
                    ExecuteJavaScript("window.location.reload()");
                    do
                    {
                        if (!gp.HasExited)
                        {
                            curRuntime = (DateTime.Now - StartTime).Minutes;
                        }
                    }
                    while (!gp.WaitForExit(60000));
                    BackGrounds.lib[key].m_time = BackGrounds.lib[key].m_time.HasValue ? BackGrounds.lib[key].m_time + curRuntime : curRuntime;
                    BackGrounds.lib[key].m_isOnline = false;
                    ExecuteJavaScript("window.location.reload()");

                    //});

                }       
            });
            task1.Start();
            return new JavaScriptValue(0);
        }));
        RegisterJavaScriptObject("natives", obj);
        //ExecuteJavaScript("alert('Hello NanUI')");
    }
}