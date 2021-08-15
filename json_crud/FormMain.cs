using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using json2obj_lib;
using System.IO;

namespace json_crud
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            string json = File.ReadAllText("test.json");
            Decode2 decode = new Decode2() { jsonString = new JsonString(json) };
            JSObjectNK jsnk = (JSObjectNK)decode.recursive_read_fn(enum_obj_array.obj, enum_key_value.key_begin, enum_datatype.dunno);
            pgMain.SelectedObject = jsnk;
            
        }
    }
}
