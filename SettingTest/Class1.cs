using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace SettingTest
{
    public class PropertyGridFileSelector : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {

            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (edSvc != null)
            {

                // 可以打开任何特定的对话框  
                OpenFileDialog dialog = new OpenFileDialog
                {
                    AddExtension = false,
                    Title = "打开文件",
                    Filter = "*.xls|*.xls|*.xlsx|*.xlsx|*.*|*.*"
                };
                if (dialog.ShowDialog().Equals(DialogResult.OK))
                {
                    return dialog.FileName;
                }
            }
            return value;
        }
    }
    class aaa
    {
        [Category("文件")]
        [DisplayName("数据文件")]
        [Description("用户数据列表文件路径")]
        [ReadOnlyAttribute(false), Browsable(true)]
        [Editor(typeof(PropertyGridFileSelector), typeof(UITypeEditor))]
        public string File
        {
            get;
            set;
        }
        [Category("余额")]
        [Description("是否启用余额")]
        [DisplayName("余额匹配")]
        [ReadOnlyAttribute(false), Browsable(true)]
        public bool Enabled
        {
            get { return mEnabledBalance; }
            set
            {
                PropertyGridHelper.SetPropertyValue<ReadOnlyAttribute>(this, "Balance", "isReadOnly", !value);
                mEnabledBalance = value;

            }
        }
        private bool mEnabledBalance = true;
    }
    public class PropertyGridHelper
    {
        /// <summary>
        /// 设置指定PropertyGrid中已设置了SelectObject对象的属性控件中指定某个自定义特性的值
        /// </summary>
        /// <typeparam name="T">要设置的属性类型</typeparam>
        /// <param name="grid">PropertyGrid对象</param>
        /// <param name="name">要设置的属性的名称</param>
        /// <param name="field">要设置的自定义特性名称</param>
        /// <param name="value">要设置的自定义特性的值</param>
        public static void SetPropertyValue<T>(PropertyGrid grid, string name, string field, object value)
        {
            if (grid.SelectedObject == null)
                throw new ArgumentException(string.Format("指定的PropertyGrid对象不包含{0}对象", "SelectObject"));

            AttributeCollection attrs = TypeDescriptor.GetProperties(grid.SelectedObject)[name].Attributes;

            FieldInfo fld = typeof(T).GetField(field, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance);

            if (fld != null) fld.SetValue(attrs[typeof(T)], value);

            grid.Refresh();
        }
        /// <summary>
        /// 设置指对象指定自定义特性或其相关特性的值
        /// </summary>
        /// <typeparam name="T">要设置的属性类型</typeparam>
        /// <param name="graph">要设置的对象</param>
        /// <param name="name">要设置的属性的名称</param>
        /// <param name="field">要设置的自定义特性名称</param>
        /// <param name="value">要设置的自定义特性的值</param>
        public static void SetPropertyValue<T>(object graph, string name, string field, object value)
        {
            if (graph == null)
                throw new ArgumentException(string.Format("指定的对象{0}不能为空", graph));

            AttributeCollection attrs = TypeDescriptor.GetProperties(graph)[name].Attributes;

            FieldInfo fld = typeof(T).GetField(field, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance);

            fld.SetValue(attrs[typeof(T)], value);
        }
    }
}
