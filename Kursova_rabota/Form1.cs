using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace kurse_work
{

    public partial class Form1 : Form
    {
        OpenFileDialog openFileDialog;
        SaveFileDialog saveFileDialog;
        List<Investor> investors = new List<Investor>();

        public Form1()
        {
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();
            InitializeComponent();
        }

        public void Write(string path)
        {
            Excel.Application excelapp = new Excel.Application();
            Excel.Workbook workbook = excelapp.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            excelapp.Visible = false;

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                    worksheet.Rows[i + 1].Columns[1].Value = investors[i].InvestorName;
                    worksheet.Rows[i + 1].Columns[2].Value = investors[i].ContractNumber;
                    worksheet.Rows[i + 1].Columns[3].Value = investors[i].HomeAdress;
                    worksheet.Rows[i + 1].Columns[4].Value = investors[i].DepositAmount;
                    worksheet.Rows[i + 1].Columns[5].Value = investors[i].ContractTerm;
            }
            excelapp.AlertBeforeOverwriting = false;
            workbook.SaveAs(path, ReadOnlyRecommended: true);
            excelapp.Quit();

        }


        void UpdateNotes()
        {

            dataGridView1.Rows.Clear();
            foreach (var investor in investors)
            {
                dataGridView1.Rows.Add(investor.InvestorName, investor.ContractNumber, investor.HomeAdress, investor.DepositAmount, investor.ContractTerm);
            }

        }


        bool IsPositiveDigit(string st)
        {
            bool check = false;
            int temp;
            if (int.TryParse(st, out temp) == true && temp > 0)
                check = true;

            return check;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if(IsPositiveDigit(textBox5.Text) && IsPositiveDigit(textBox7.Text) && IsPositiveDigit(textBox8.Text))
            {
                investors.Add(new Investor()
                {
                    InvestorName = textBox4.Text,
                    ContractNumber = Convert.ToInt32(textBox5.Text),
                    HomeAdress = textBox6.Text,
                    DepositAmount = Convert.ToInt32(textBox7.Text),
                    ContractTerm = Convert.ToInt32(textBox8.Text)
                });
                UpdateNotes();
            }
            else
            {
                MessageBox.Show("Вы должны вводить положительные числа!");
            }
            
            
        }

        private void ВывестиВсехВкладчиковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateNotes();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            UpdateNotes();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0 && this.dataGridView1.SelectedRows[0].Index != this.dataGridView1.Rows.Count - 1)
            {
                this.dataGridView1.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                int index = this.dataGridView1.SelectedRows[0].Index;
                investors.RemoveAt(index);
            }
            UpdateNotes();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            
            if (this.dataGridView1.SelectedRows.Count > 0 && this.dataGridView1.SelectedRows[0].Index != this.dataGridView1.Rows.Count - 1)
            {
                
                int index = this.dataGridView1.SelectedRows[0].Index;

                investors[index].InvestorName = textBox4.Text;
                investors[index].ContractNumber = Convert.ToInt32(textBox5.Text);
                investors[index].HomeAdress = textBox6.Text;
                investors[index].DepositAmount = Convert.ToInt32(textBox7.Text);
                investors[index].ContractTerm = Convert.ToInt32(textBox8.Text);
        }
            UpdateNotes();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            bool check = false;
            string surname = textBox1.Text;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string[] tmpsurname = (Convert.ToString(dataGridView1.Rows[i].Cells[0].Value).Split(' '));
                if (tmpsurname[0] != surname)
                {
                    dataGridView1.Rows[i].Visible = false;
                    check = true;
                }
            }
            if(check == false)
            {
                MessageBox.Show("Не удалось найти записей с такой фамилией!");
                dataGridView1.Rows.Clear();
            }
        
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            
            if (IsPositiveDigit(textBox2.Text)) {
                dataGridView1.Rows.Clear();
                int deposit = Convert.ToInt32(textBox2.Text);
                bool check = false;
                foreach (var investor in investors)
                {
                    if (investor.DepositAmount > deposit)
                    {
                        dataGridView1.Rows.Add(investor.InvestorName, investor.ContractNumber, investor.HomeAdress, investor.DepositAmount, investor.ContractTerm);
                        check = true;
                    }
                }
                if (check == false)
                {
                    MessageBox.Show("Не удалось найти записей!");
                    dataGridView1.Rows.Clear();
                }
            }
            else
            {
                MessageBox.Show("Вы должны вводить положительные числа!");
            }
            
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            bool check = false;
            string surname = textBox3.Text;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string[] tmpsurname = (Convert.ToString(dataGridView1.Rows[i].Cells[0].Value).Split(' '));
                if (tmpsurname[0] == surname)
                {
                    dataGridView1.Rows.RemoveAt(i);
                    check = true;
                }
            }
            if (check == false)
            {
                MessageBox.Show("Не удалось найти записей с такой фамилией!");
                dataGridView1.Rows.Clear();
            }

        }

        private void ВкладчикиДоговорКоторыхСвыше12МесToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            
            bool check = false;
            foreach (var investor in investors)
            {
                if (investor.ContractTerm > 12)
                {
                    dataGridView1.Rows.Add(investor.InvestorName, investor.ContractNumber, investor.HomeAdress, investor.DepositAmount, investor.ContractTerm);
                    check = true;
                }
            }
            if (check == false)
            {
                MessageBox.Show("Не удалось найти записей с таким именем!");
                dataGridView1.Rows.Clear();
            }
        }
       
        private void ОткрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            investors.Clear();

            saveFileDialog.Filter = "Excel (*.xlsx)|*.xlsx | Excel (*.xls)|*.xls";
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = openFileDialog.FileName;


            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook ExcelWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;

            //Opening
            ExcelWorkBook = ExcelApp.Workbooks.Open(filename, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false,
            false, 0, true, 1, 0);
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);

            for (int i = 1; i < ExcelApp.Rows.Count; i++)
            {
                if (ExcelApp.Cells[i, 1].Value != null)
                {
                    string a = ExcelWorkSheet.Rows[i].Columns[1].Value;
                    investors.Add(new Investor()
                    { 
                        InvestorName = ExcelWorkSheet.Rows[i].Columns[1].Value,
                        ContractNumber = Convert.ToInt32(ExcelWorkSheet.Rows[i].Columns[2].Value),
                        HomeAdress = ExcelWorkSheet.Rows[i].Columns[3].Value,
                        DepositAmount = Convert.ToInt32(ExcelWorkSheet.Rows[i].Columns[4].Value),
                        ContractTerm = Convert.ToInt32(ExcelWorkSheet.Rows[i].Columns[5].Value)
                    });
                    dataGridView1.Rows.Add(ExcelWorkSheet.Rows[i].Columns[1].Value, ExcelWorkSheet.Rows[i].Columns[2].Value, ExcelWorkSheet.Rows[i].Columns[3].Value, ExcelWorkSheet.Rows[i].Columns[4].Value, ExcelWorkSheet.Rows[i].Columns[5].Value);
                }
                else
                    break;
            }
            ExcelApp.Quit();

            UpdateNotes();
        }

        private void СохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Excel (*.xlsx)|*.xlsx | Excel (*.xls)|*.xls";
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog.FileName;

            Write(filename);

            MessageBox.Show("Файл сохранен");
        }

        private void УдалитьВсеЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            investors.Clear();
            dataGridView1.Rows.Clear();
            
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            bool check = false;
            string keyWord = textBox9.Text;
            switch (comboBox1.Text)
            {
                case "ФИО":

                    foreach (var investor in investors)
                    {
                        if (investor.InvestorName == keyWord)
                        {
                            dataGridView1.Rows.Add(investor.InvestorName,investor.ContractNumber,investor.HomeAdress,investor.DepositAmount,investor.ContractTerm);
                            check = true;
                        }
                    }
                    break;
                case "Номер договора":
                    
                    foreach (var investor in investors)
                    {
                        if (investor.ContractNumber == Convert.ToInt32(keyWord))
                        {
                            dataGridView1.Rows.Add(investor.InvestorName, investor.ContractNumber, investor.HomeAdress, investor.DepositAmount, investor.ContractTerm);
                            check = true;
                        }
                    }
                    break;
                case "Домашний адрес":
                    foreach (var investor in investors)
                    {
                        if (investor.HomeAdress == keyWord)
                        {
                            dataGridView1.Rows.Add(investor.InvestorName, investor.ContractNumber, investor.HomeAdress, investor.DepositAmount, investor.ContractTerm);
                            check = true;
                        }
                    }
                    break;
                case "Сумма вклада":
                    foreach (var investor in investors)
                    {
                        if (investor.DepositAmount == Convert.ToInt32(keyWord))
                        {
                            dataGridView1.Rows.Add(investor.InvestorName, investor.ContractNumber, investor.HomeAdress, investor.DepositAmount, investor.ContractTerm);
                            check = true;
                        }
                    }
                    break;
                case "Срок договора":
                    foreach (var investor in investors)
                    {
                        if (investor.ContractTerm == Convert.ToInt32(keyWord))
                        {
                            dataGridView1.Rows.Add(investor.InvestorName, investor.ContractNumber, investor.HomeAdress, investor.DepositAmount, investor.ContractTerm);
                            check = true;
                        }
                    }
                    break;
                default:
                    MessageBox.Show("Выберите поле поиска!");
                    break;

            }
        }

       
    }
}
