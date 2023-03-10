using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UserStory_Airport.models;

namespace UserStory_Airport
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        private readonly List<Reisi> reisi;

        private readonly BindingSource BinSource;
        public Form1()
        {
            InitializeComponent();
            reisi = new List<Reisi>();
            BinSource = new BindingSource();
            BinSource.DataSource = reisi;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = BinSource;
        }
        

        private void About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа студентки группы ИП-20-3 Пшеничниковой М.В.", "Airпорт",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void AddTool_Click(object sender, EventArgs e)
        {
            FlightsForm infoForm = new FlightsForm();
            
            if (infoForm.ShowDialog(this) == DialogResult.OK)
            {
                reisi.Add(infoForm.Flights);
                BinSource.ResetBindings(false);
                InfoStatCal();
            }
        }


        private void DeleteTool_Click(object sender, EventArgs e)
        {
            Reisi id = (Reisi)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;
            if(MessageBox.Show($"Вы действительно хотите удалить запись рейса {id.nomer_reisa}?",
                "Удаление записи", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                reisi.Remove(id);
                BinSource.ResetBindings(false);
                InfoStatCal();
            }
        }

        private void ChangeTool_Click(object sender, EventArgs e)
        {
            Reisi id = (Reisi)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;
            FlightsForm infoForm = new FlightsForm(id);
            if (infoForm.ShowDialog(this) == DialogResult.OK)
            {
                id.nomer_reisa = infoForm.Flights.nomer_reisa;
                id.type = infoForm.Flights.type;
                id.prib_time = infoForm.Flights.prib_time;
                id.passagiers_count = infoForm.Flights.passagiers_count;
                id.ppassagiers__price = infoForm.Flights.ppassagiers__price;
                id.ek_count = infoForm.Flights.ek_count;
                id.ek_price = infoForm.Flights.ek_price;
                id.procent = infoForm.Flights.procent;
                BinSource.ResetBindings(false);
                InfoStatCal();
            }
        }


        private void FlightsDGV_SelectionChanged(object sender, EventArgs e)
        {
            pravka_delete.Enabled = dataGridView1.SelectedRows.Count > 0;
            pravka_change.Enabled = dataGridView1.SelectedRows.Count > 0;
            Delete.Enabled = dataGridView1.SelectedRows.Count > 0;
            Change.Enabled = dataGridView1.SelectedRows.Count > 0;
        }


        private void InfoStatCal()
        {
            reisi_count.Text = $"Количество рейсов {reisi.Count}";
            allpas.Text = $"Всего пассажиров {reisi.Sum(x => x.passagiers_count)}";
            allek.Text = $"Всего экипажа {reisi.Sum(x => x.ek_count)}";
            allmoney.Text = $"Общая сумма {reisi.Sum(x => x.allmoney)}";
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
