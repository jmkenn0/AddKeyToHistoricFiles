using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AddKeyToHistoricFiles
{
    public partial class Form1 : Form
    {

        public static DataTable fileArray = new DataTable();


        public Form1()
        {
            InitializeComponent();

            DataColumn InputColumn = new DataColumn();
            InputColumn.DataType = System.Type.GetType("System.String");
            InputColumn.ColumnName = "FileName";

            fileArray.Columns.Add(InputColumn);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int counter = 0;
            StreamReader streamreaderTXT;
            char[] delimiter = new char[] { ',', ';', '\t' };
            string path = "";
            string outfile="c:\\output\\";
            StreamWriter streamWriterTXT;
            

            
                foreach (DataRow dt in fileArray.Rows)
                {
                counter++;
                streamreaderTXT = new StreamReader(dt[0].ToString());
                    string[] inputstring = streamreaderTXT.ReadLine().Split(delimiter);
                path = Path.GetFileNameWithoutExtension(dt[0].ToString());
                inputstring[0] = path + "-" + counter.ToString();
           
                
                MessageBox.Show(outfile+path);
                streamWriterTXT = new StreamWriter(outfile + path + ".csv");
                //streamWriterTXT.WriteLine(String.Join(";", inputstring));
                //streamWriterTXT.WriteLine("here");
                

                while(!streamreaderTXT.EndOfStream)
                {
                    inputstring = streamreaderTXT.ReadLine().Split(delimiter);
                    foreach (string s in inputstring)
                    {
                        s.Replace("\"", "");
                    }
                    
                    inputstring[0] = path + "-" + counter.ToString();
                    streamWriterTXT.WriteLine(String.Join(";", inputstring));

                    counter++;

                }
                streamWriterTXT.Close();
            }
            






        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //Stream myStream = null;
            openFileDialog1.InitialDirectory = "C:\\Users'\'john.mark.kennedy'\'Documents'\'Constantia'\'Sales Reporting'\'Transaction Data'\'";
            //openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }


            }

            //DataTable outputtable = Form1.OutputDataTable1();
            //loop through selection populating global string array varaible

            DataRow fileArrayRow;

            foreach (String file in openFileDialog1.FileNames)
            {

                fileArrayRow = fileArray.NewRow();
                fileArrayRow[0] = file;
                fileArray.Rows.Add(fileArrayRow);
                //MessageBox.Show(fileArray.Rows[0][0].ToString());
            }

            //stop here
        }
    }
}
