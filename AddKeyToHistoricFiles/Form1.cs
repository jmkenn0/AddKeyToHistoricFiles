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
            int prvCM = 0;
            int curCM = 0;

            
                foreach (DataRow dt in fileArray.Rows)
                {
                counter++;
                streamreaderTXT = new StreamReader(dt[0].ToString());
                    string[] inputstring = streamreaderTXT.ReadLine().Split(delimiter);
                path = Path.GetFileNameWithoutExtension(dt[0].ToString());
                
                
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

                    {
                        curCM = Convert.ToInt32(inputstring[6].Substring(2, 5));

                        if (prvCM >= curCM)
                        {
                            counter++;
                        }
                    }

                    //cfc73
                    ///column 0        CC                           identstring                customer number                                 date                             order number
                    //inputstring[0] = inputstring[9].ToString() + "-" + inputstring[48].ToString() + "-" + inputstring[28].ToString() + "-" + inputstring[70].ToString() + "-" + inputstring[51].ToString() + "-" + inputstring[32].ToString() + "-" + inputstring[34].ToString() + "-" + inputstring[3].ToString();

                    //cfc71
                    ///column 0        CC                           identstring                customer number                                 date                             order number      missing                  bill to                            payer                             budget/act
                    // inputstring[0] = inputstring[9].ToString() + "-" + inputstring[44].ToString() + "-" + inputstring[28].ToString() + "-" + inputstring[74].ToString() + "-" /*+ inputstring[51].ToString() + "-"*/ + inputstring[32].ToString() + "-" + inputstring[34].ToString() + "-" + inputstring[3].ToString();

                    //cfc72
                    ///column 0        CC                           identstring                customer number                                 date                             order number      missing                  bill to                            payer                             budget/act
                    inputstring[0] = inputstring[9].ToString() + "-" + inputstring[44].ToString() + "-" + inputstring[28].Substring(inputstring[28].Length-6, 6) + "-" + inputstring[66].ToString() + "-" /*+ inputstring[51].ToString() + "-"*/ + inputstring[32].Substring(inputstring[32].Length - 6, 6) + "-" + inputstring[34].ToString() + "-" + inputstring[3].ToString() + "-" + inputstring[47].ToString() + "-" + inputstring[49].ToString() + "-" + inputstring[57].ToString() + "-" + inputstring[51].ToString() + "-" + inputstring[46].ToString() + "-" + inputstring[50].ToString() + "-" + inputstring[56].Substring(inputstring[56].Length - 6, 6) + "-"+counter.ToString();

                    prvCM = curCM;


                    streamWriterTXT.WriteLine(String.Join(";", inputstring));

                   

                }
                streamWriterTXT.Close();
            }

            







        }

        public static string convertDate(string month, string year)
        {
            /*if (month.Substring(1, 1) == "0")
                month = month.Substring(2, 1);
            else
                month = month.Substring(1, 2); */



            return month + "/" + DateTime.DaysInMonth(Convert.ToInt16(year), Convert.ToInt16(month)).ToString() + "/" + year;
        }

        public static string inverseString(string input)
        {
            double i = 0;
            input = input.Replace("\"", "");

            if (input.Contains("-"))
            {
                i = Convert.ToDouble(input.Substring(0, input.Length - 1));
                return i.ToString();
            }

            i = Convert.ToDouble(input) * (-1);
            return i.ToString();

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
