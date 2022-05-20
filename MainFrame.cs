using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WF_Calc.CSharpObj;

namespace WF_Calc {

    public partial class MainFrame : Form {
        private string pathName = System.Environment.CurrentDirectory;
        private string propertyPath = "";
        private JObject currentTheme = null;

        public MainFrame() {
            pathName = System.IO.Directory.GetParent(pathName).Parent.FullName;
            propertyPath = GetApplicationPath() + "Data/system.json";
            this.currentTheme = JObject.Parse(GetJsonFile(propertyPath));
            //System.IO.Directory.GetParent(pathName).Parent.FullName +
            //创建组件，传递URL：可以是本地，也可以是远程地址
            this.browser = new ChromiumWebBrowser(pathName + "../../WebPages/MainFrame.html") {
                Dock = DockStyle.Fill
            };
            this.browser.RegisterAsyncJsObject("CommonJs", new CommonJs(), BindingOptions.DefaultBinder);
            InitializeComponent();
            this.MainPanel.Controls.Add(this.browser);
            InitializeThemes();
        }

        public ChromiumWebBrowser browser;

        private void btnAdd_Click(object sender, EventArgs e) {
            this.browser.GetBrowser().MainFrame.ExecuteJavaScriptAsync("alert('" + pathName + "../../WebPages/test.html" + "这是c#调用的js,给文本框赋值！')");

            //txtAccount
            this.browser.GetBrowser().MainFrame.ExecuteJavaScriptAsync("document.getElementById('txtAccount').value='" + GetJsonFile(propertyPath) + "'");
        }

        public class InitProperty {

            /// <param name="text"></param>
            public void theme(string text) {
                MessageBox.Show(text);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e) {

        }

        private void btnDel_Click(object sender, EventArgs e) {
            this.browser.ShowDevTools();
        }

        private void MainFrame_KeyDown(object sender, KeyEventArgs e) {
            MessageBox.Show("按下了:" + e.KeyCode);
            if (e.KeyCode == Keys.F12) {
                this.browser.ShowDevTools();
            }
        }

        private void InitializeThemes() {
            List<Themes> themes = new Themes().Init();
            cbxTheme.DataSource = themes;
            cbxTheme.DisplayMember = "ThemeName";
            cbxTheme.ValueMember = "ThemeName";
            cbxTheme.Tag = "主题切换";            
        }

        private void theme_Change(object sender, EventArgs e) {            
            this.currentTheme["Theme"] = cbxTheme.SelectedValue.ToString();
            this.browser.GetBrowser().MainFrame.ExecuteJavaScriptAsync(String.Format("changeThemes('{0}')", cbxTheme.SelectedValue));
            WriteJsonFile(propertyPath, currentTheme.ToString());
        }

        /// <summary>
        /// 获取到本地的Json文件并且解析返回对应的json字符串
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        private string GetJsonFile(string filepath) {
            string json = string.Empty;
            using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, FileShare.ReadWrite)) {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8)) {
                    json = sr.ReadToEnd().ToString();
                }
            }
            return json;
        }

        private void WriteJsonFile(string path, string jsonConents) {
            File.WriteAllText(path, jsonConents, System.Text.Encoding.UTF8);
        }

        private class Themes {
            public string ThemeName { get; set; }
            public string Value { get; set; }

            public Themes() {
            }

            public Themes(string ThemeName, string Value) {
                this.ThemeName = ThemeName;
                this.Value = Value;
            }

            public List<Themes> Init() {
                List<Themes> themes = new List<Themes>();
                themes.Add(new Themes() {
                    ThemeName = "default",
                    Value = "default"
                });
                themes.Add(new Themes() {
                    ThemeName = "black",
                    Value = "black"
                });

                themes.Add(new Themes() {
                    ThemeName = "bootstrap",
                    Value = "bootstrap"
                });
                themes.Add(new Themes() {
                    ThemeName = "gray",
                    Value = "gray"
                });
                themes.Add(new Themes() {
                    ThemeName = "material",
                    Value = "material"
                });
                themes.Add(new Themes() {
                    ThemeName = "material-teal",
                    Value = "material-teal"
                });
                themes.Add(new Themes() {
                    ThemeName = "metro",
                    Value = "metro"
                });
                return themes;
            }
        }

        /// <summary>
        /// 获取应⽤程序根路径
        /// </summary>
        private static string GetApplicationPath() {
            string path = Application.StartupPath;
            //string path=AppDomain.CurrentDomain.BaseDirectory; //另⼀种获取⽅式
            string folderName = String.Empty;
            while (folderName.ToLower() != "bin") {
                path = path.Substring(0, path.LastIndexOf("\\"));
                folderName = path.Substring(path.LastIndexOf("\\") + 1);
            }
            return path.Substring(0, path.LastIndexOf("\\") + 1);
        }

        private void MainFrame_Shown(object sender, EventArgs e) {
            this.browser.FrameLoadEnd += new EventHandler<FrameLoadEndEventArgs>(FrameEndFu);            
        }

        private void FrameEndFu(object sender, EventArgs e) {
            //将组件，添加到页面上来
            List<Themes> themes = new Themes().Init();
            foreach (Themes theme in themes) {
                if (theme.Value == currentTheme.GetValue("Theme").ToString())
                    cbxTheme.SelectedIndex = themes.IndexOf(theme);
            }
            this.cbxTheme.SelectedIndexChanged += theme_Change;
            this.browser.GetBrowser().MainFrame.ExecuteJavaScriptAsync(String.Format("changeThemes('{0}')", currentTheme.GetValue("Theme").ToString()));            
        }
    }
}