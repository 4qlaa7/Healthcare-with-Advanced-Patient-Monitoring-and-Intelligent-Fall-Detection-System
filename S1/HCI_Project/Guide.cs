using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HCI_Project
{
    public partial class Guide : Form
    {
        Timer t = new Timer();
        string pat_name = "";
        int onetime = 0;
        public static int index = 0;
        public static bool prev = false, next = false, P_enter = false;
        TextFileHandler pnn = new TextFileHandler("patient.txt");
        List<string> L_Patient = new List<string>();
        List<string> allow_pt = new List<string>();
        List<string> L_med = new List<string>();
        List<string> allow_med = new List<string>();
        int len_rows = 0;
        public Guide()
        {
            InitializeComponent();
            this.Load += Guide_Load;
            t.Tick += T_Tick;

        }

        private void Guide_Load(object sender, EventArgs e)
        {
            L_Patient = pnn.ReadFile();
            this.dataGridView1.ColumnCount = L_Patient[0].Length;
            len_rows = L_Patient.Count;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Name";
            dataGridView1.Columns[2].HeaderText = "PhoneNum";
            dataGridView1.Columns[3].HeaderText = "Age";
            pnn.filePath = "nur_pat.txt";
            allow_pt = pnn.ReadFile();
            for (int i = 0; i < allow_pt.Count; i++)
            {
                string[] parts = allow_pt[i].Split(',');
                if (parts[1] == "1")
                {
                    for (int j = 0; j < L_Patient.Count; j++)
                    {
                        string[] parts2 = L_Patient[j].Split(',');
                        if (parts[2] == parts2[0])
                        {
                            dataGridView1.Rows.Add(parts2);
                        }
                    }
                }
            }
            pnn.filePath = "medicen.txt";
            L_med = pnn.ReadFile();
            pnn.filePath = "med_pat.txt";
            allow_med = pnn.ReadFile();

            t.Start();
        }
        //
        int index_row = 0;
        int pet_id = 0;
        private void T_Tick(object sender, EventArgs e)
        {
            if (next || prev)
            {
                if (index == 1)
                {
                    if (next && index_row + 1 < len_rows)
                    {
                        //Console.WriteLine(index_row);
                        index_row++;
                    }
                    if (prev && index_row - 1 > -1)
                    {
                        //Console.WriteLine(index_row);
                        index_row--;
                    }
                }
            }
            pet_id = int.Parse(this.dataGridView1[0, index_row].Value.ToString());
            this.label2.Text = this.dataGridView1[1, index_row].Value.ToString();
            this.label5.Text = this.dataGridView1[3, index_row].Value.ToString();
            this.label6.Text = this.dataGridView1[2, index_row].Value.ToString();
            check_enter();

        }
        void check_enter()
        {
            if (P_enter)
            {
                if (onetime == 0)
                {
                    this.dataGridView2.ColumnCount = 3;
                    dataGridView2.Columns[0].HeaderText = "ID";
                    dataGridView2.Columns[1].HeaderText = "Med Name";
                    dataGridView2.Columns[2].HeaderText = "Med Taken";
                    for (int i = 0; i < allow_med.Count; i++)
                    {
                        string[] parts = allow_med[i].Split(',');
                        if (int.Parse(parts[1]) == pet_id)
                        {
                            for (int j = 0; j < L_med.Count; j++)
                            {
                                string[] parts2 = L_med[j].Split(',');
                                if (parts[2] == parts2[0])
                                {
                                    string[] finalp = { parts[0], parts2[1], parts[3] };

                                    dataGridView2.Rows.Add(finalp);
                                }
                            }
                        }
                    }
                    onetime++;
                }
            }
        }
    }
}
