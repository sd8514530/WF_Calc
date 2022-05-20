using System.Windows;

namespace WF_Calc.CSharpObj {

    public class CommonJs {
        
        /// <param name="text"></param>
        public void alert(string text, string type) {
            switch (type) {
                case "warning":
                    MessageBox.Show(text, "系统警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;

                case "error":
                    MessageBox.Show(text, "系统错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;

                default:
                    MessageBox.Show(text, "温馨提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }
        }
    }
}