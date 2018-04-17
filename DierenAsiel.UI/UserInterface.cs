using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DierenAsiel.Logic;

namespace DierenAsiel.UI
{
    public partial class UserInterface : Form
    {
        ILogic logic = new LogicController();

        public UserInterface()
        {
            InitializeComponent();

            SetComboBoxes();

            PopulateListView();

            SetDateTimePickers();
        }

        private void PopulateListView()
        {
            LvAnimalList.Items.Clear();
            foreach (Animal A in logic.GetAllAnimals())
            {
                ListViewItem listViewItem = new ListViewItem(A.name);
                listViewItem.SubItems.Add(A.age.ToString());
                listViewItem.SubItems.Add(A.weight.ToString());
                listViewItem.SubItems.Add(Enum.GetName(typeof(Animal.Genders), A.gender));
                listViewItem.SubItems.Add(A.price.ToString());
                listViewItem.SubItems.Add(Enum.GetName(typeof(Animal.Species), A.species));
                listViewItem.SubItems.Add(A.cage.ToString());
                listViewItem.SubItems.Add(A.reserved ? "Ja" : "Nee");

                LvAnimalList.Items.Add(listViewItem);
            }

            LvEmployees.Items.Clear();
            foreach (Employee E in logic.GetAllEmployees())
            {
                ListViewItem listViewItem = new ListViewItem(E.name);
                listViewItem.SubItems.Add(E.age.ToString());
                listViewItem.SubItems.Add(E.gender.ToString());
                listViewItem.SubItems.Add(E.address);
                listViewItem.SubItems.Add(E.phoneNumber);

                LvEmployees.Items.Add(listViewItem);
            }

            LbDogs.Items.AddRange(logic.GetAnimalsOfType(Animal.Species.Dog).ToArray());
        }

        private void SetComboBoxes()
        {
            CbAnimalType.Items.AddRange(Enum.GetNames(typeof(Animal.Species)));
            CbAnimalType.SelectedIndex = 0;

            CbUitlaatEmployees.Items.AddRange(logic.GetAllEmployees().ToArray());
            if (CbUitlaatEmployees.Items.Count > 0)
            {
                CbUitlaatEmployees.SelectedIndex = 0;
            }            
        }

        private void SetDateTimePickers()
        {
            DtpLaatstUitgelaten.CustomFormat = "HH:mm dd/MM/yyyy";
            DtpUitlaatDate.CustomFormat = "HH:mm dd/MM/yyyy";
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Animal A = new Animal() { name = TxtAnimalName.Text, age = (int)NudAnimalAge.Value, weight = (int)NudAnimalWeight.Value, gender = (Animal.Genders)Convert.ToInt32(!RadAnimalMale.Checked), species = (Animal.Species)Enum.Parse(typeof(Animal.Species),CbAnimalType.Text), cage = (int)NudAnimalCage.Value, price = (float)NudAnimalPrice.Value };
            logic.AddAnimal(A);
            PopulateListView();
        }

        private void BtnAnimalDelete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LvAnimalList.SelectedItems)
            {
                Animal A = new Animal() { name = item.Text,
                    age = int.Parse(item.SubItems[1].Text),
                    weight = int.Parse(item.SubItems[2].Text),
                    gender = (Animal.Genders)Enum.Parse(typeof(Animal.Genders), item.SubItems[3].Text),
                    price = float.Parse(item.SubItems[4].Text),
                    species = (Animal.Species)Enum.Parse(typeof(Animal.Species), item.SubItems[5].Text),
                    cage = int.Parse(item.SubItems[6].Text),
                    reserved = (item.SubItems[7].Text == "Ja" ? true : false)};

                logic.RemoveAnimal(A);
                PopulateListView();
            }            
        }

        private void BtnUitlaten_Click(object sender, EventArgs e)
        {
            logic.SetUitlaatDate(logic.GetAnimalFromList(Animal.Species.Dog, LbDogs.SelectedIndex), logic.GetEmployeeByName(CbUitlaatEmployees.Text), DtpUitlaatDate.Value);
        }

        private void LbDogs_SelectedIndexChanged(object sender, EventArgs e)
        {
            Animal A = logic.GetAnimalFromList(Animal.Species.Dog, LbDogs.SelectedIndex);
            TxtUitlaatNaam.Text = A.name;
            NudUitlaatAge.Value = A.age;
            RadUitlaatFemale.Checked = (A.gender == Animal.Genders.Female) ? true : false;
            DtpLaatstUitgelaten.Value = logic.GetUitlaatDate(A);
        }

        private void BtnAddEmployee_Click(object sender, EventArgs e)
        {
            logic.AddEmployee(new Employee() { name = TxtEmployeeName.Text, age = (int)NudEmployeeAge.Value, gender = (Employee.Gender)Convert.ToInt32(!RadEmployeeMale.Checked), address = TxtEmployeeAddress.Text, phoneNumber = TxtEmployeePhone.Text});
            PopulateListView();
        }

        private void LvAnimalList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            List<ListViewItem> sortList = new List<ListViewItem>();
            foreach (ListViewItem item in LvAnimalList.Items)
            {
                sortList.Add(item);
            }
            sortList = sortList.OrderBy(x => x.SubItems[e.Column].Text).ToList();
            LvAnimalList.Items.Clear();
            LvAnimalList.Items.AddRange(sortList.ToArray());
        }
    }
}
