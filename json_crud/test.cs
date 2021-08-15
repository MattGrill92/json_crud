using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System;
using System.IO;
using json2obj_lib;
using System.Collections.Generic;
class MyType
{
    private Foo foo = new Foo();
    public Foo Foo { get { return foo; } }
}

[Editor(typeof(FooEditor), typeof(UITypeEditor))]
[TypeConverter(typeof(ExpandableObjectConverter))]
class Foo
{
    private string bar;
    public string Bar
    {
        get { return bar; }
        set { bar = value; }
    }
}
class FooEditor : UITypeEditor
{
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
        return UITypeEditorEditStyle.Modal;
    }
    public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
    {
        IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
        //Foo foo = value as Foo;
        JSGenericNK jsnk = (JSGenericNK) value;
        if (svc != null && jsnk != null)
        {
            using (FooForm form = new FooForm())
            {
                //form.Value = foo.Bar;
                if (svc.ShowDialog(form) == DialogResult.OK)
                {
                    //foo.Bar = form.Value; // update object
                }
            }
        }
        return value; // can also replace the wrapper object here
    }
}
class FooForm : Form
{
    private TextBox textbox;
    private Button okButton;
    public FooForm()
    {
        Text = "ciao sono io";
        textbox = new TextBox();
        Controls.Add(textbox);
        okButton = new Button();
        okButton.Text = "OK";
        okButton.Dock = DockStyle.Bottom;
        okButton.DialogResult = DialogResult.OK;
        Controls.Add(okButton);
    }
    public string Value
    {
        get { return textbox.Text; }
        set { textbox.Text = value; }
    }
}
//static class Program
//{
//    [STAThread]
//    static void Main()
//    {
//        Application.EnableVisualStyles();
//        Form form = new Form();
//        PropertyGrid grid = new PropertyGrid();
//        grid.Dock = DockStyle.Fill;
//        form.Controls.Add(grid);
//        //grid.SelectedObject = new MyType();
//        string json = File.ReadAllText("test.json");
//        Decode2 decode = new Decode2() { jsonString = new JsonString(json) };
//        JSObjectNK jsnk = (JSObjectNK)decode.recursive_read_fn(enum_obj_array.obj, enum_key_value.key_begin, enum_datatype.dunno);
//        grid.SelectedObject = jsnk;
//        Application.Run(form);
//        
//        /*
//         string json = File.ReadAllText("test.json");
//         Decode2 decode = new Decode2() { jsonString = new JsonString(json) };
//         JSObjectNK jsnk = (JSObjectNK)decode.recursive_read_fn(enum_obj_array.obj, enum_key_value.key_begin, enum_datatype.dunno);
//         
//        */
//    }
//}