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
using DierenAsiel.Logic;

namespace DierenAsiel.UI
{
    public partial class UserInterface : Form
    {
        private IAnimalLogic animalLogic = new AnimalLogicController();
        private IEmployeeLogic employeeLogic = new EmployeeLogicController();
        private ICaretakingLogic caretakingLogic = new CaretakingLogicController();
        private IAuthenticationLogic authenticationLogic = new LoginAuthenticator();

        public UserInterface()
        {
            InitializeComponent();

            SetComboBoxes();

            PopulateListView();
        }

        private void PopulateListView()
        {
            LvAnimalList.Items.Clear();
            foreach (Animal A in animalLogic.GetAllAnimals())
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
            foreach (Employee E in employeeLogic.GetAllEmployees())
            {
                ListViewItem listViewItem = new ListViewItem(E.name);
                listViewItem.SubItems.Add(E.age.ToString());
                listViewItem.SubItems.Add(E.gender.ToString());
                listViewItem.SubItems.Add(E.address);
                listViewItem.SubItems.Add(E.phoneNumber);

                LvEmployees.Items.Add(listViewItem);
            }

            LbDogs.Items.Clear();
            LbDogs.Items.AddRange(animalLogic.GetAnimalsOfType(Animal.Species.Dog).ToArray());
            if (LbDogs.Items.Count > 0) {LbDogs.SelectedIndex = 0;}
            

            LbCages.Items.Clear();
            LbCages.Items.AddRange(caretakingLogic.GetAllCages().ToArray());
            if (LbDogs.Items.Count > 0) { LbCages.SelectedIndex = 0; }

            LbFeedingAnimals.Items.Clear();
            LbFeedingAnimals.Items.AddRange(animalLogic.GetAllAnimals().ToArray());
            if (LbDogs.Items.Count > 0) { LbFeedingAnimals.SelectedIndex = 0; }
            
        }

        private void SetComboBoxes()
        {
            CbAnimalType.Items.Clear();
            CbAnimalType.Items.AddRange(Enum.GetNames(typeof(Animal.Species)));
            CbAnimalType.SelectedIndex = 0;

            CbUitlaatEmployees.Items.Clear();
            CbUitlaatEmployees.Items.AddRange(employeeLogic.GetAllEmployees().ToArray());
            if (CbUitlaatEmployees.Items.Count > 0)
            {
                CbUitlaatEmployees.SelectedIndex = 0;
            }

            CbCleanEmployee.Items.Clear();
            CbCleanEmployee.Items.AddRange(employeeLogic.GetAllEmployees().ToArray());
            if (CbCleanEmployee.Items.Count > 0)
            {
                CbCleanEmployee.SelectedIndex = 0;
            }

            CbFeedingEmployee.Items.Clear();
            CbFeedingEmployee.Items.AddRange(employeeLogic.GetAllEmployees().ToArray());
            if (CbFeedingEmployee.Items.Count > 0)
            {
                CbFeedingEmployee.SelectedIndex = 0;
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (TxtAnimalName.Text != "" && NudAnimalWeight.Value != 0 && RtbCharacteristics.Text != "" && PbAnimalImage.Image != null)
            {
                byte[] imageBytes = File.ReadAllBytes(OfdImage.FileName);
                string imageBase64 = Convert.ToBase64String(imageBytes);

                Animal A = new Animal() { name = TxtAnimalName.Text, age = (int)NudAnimalAge.Value, weight = (int)NudAnimalWeight.Value, gender = (Animal.Genders)Convert.ToInt32(!RadAnimalMale.Checked), species = (Animal.Species)Enum.Parse(typeof(Animal.Species), CbAnimalType.Text), cage = (int)NudAnimalCage.Value, price = (float)NudAnimalPrice.Value, characteristics = RtbCharacteristics.Lines.ToList(), image = imageBase64 };
                animalLogic.AddAnimal(A);
                PbAnimalImage.Image = null;
                PopulateListView();

                MessageBox.Show("Dier succesvol toegevoegd.", "Notice", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Informatie is onvolledig ingevoerd.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAnimalDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Weet je zeker dat je {LvAnimalList.SelectedItems[0].Text} wilt verwijderen?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (ListViewItem item in LvAnimalList.SelectedItems)
                {
                    Animal A = new Animal() {
                        name = item.Text,
                        age = int.Parse(item.SubItems[1].Text),
                        weight = int.Parse(item.SubItems[2].Text),
                        gender = (Animal.Genders)Enum.Parse(typeof(Animal.Genders), item.SubItems[3].Text),
                        price = float.Parse(item.SubItems[4].Text),
                        species = (Animal.Species)Enum.Parse(typeof(Animal.Species), item.SubItems[5].Text),
                        cage = int.Parse(item.SubItems[6].Text),
                        reserved = (item.SubItems[7].Text == "Ja" ? true : false),
                    };

                    animalLogic.RemoveAnimal(A);
                    PopulateListView();
                }
            }   
        }

        private void BtnUitlaten_Click(object sender, EventArgs e)
        {
            if (CbUitlaatEmployees.Text != "")
            {
                caretakingLogic.SetUitlaatDate(animalLogic.GetAnimalFromList(Animal.Species.Dog, LbDogs.SelectedIndex), employeeLogic.GetEmployeeByName(CbUitlaatEmployees.Text), DtpUitlaatDate.Value);
                LbDogs_SelectedIndexChanged(null, null);
            }
            else
            {
                MessageBox.Show("Er is geen werknemer gekozen.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LbDogs_SelectedIndexChanged(object sender, EventArgs e)
        {            
                Animal A = animalLogic.GetAnimalFromList(Animal.Species.Dog, LbDogs.SelectedIndex);
                TxtUitlaatNaam.Text = A.name;
                NudUitlaatAge.Value = A.age;
                RadUitlaatFemale.Checked = (A.gender == Animal.Genders.Female) ? true : false;
                DtpLaatstUitgelaten.Value = caretakingLogic.GetUitlaatDate(A);           
        }

        private void BtnAddEmployee_Click(object sender, EventArgs e)
        {
            if (TxtEmployeeName.Text != "" && TxtEmployeeAddress.Text != "" && TxtEmployeePhone.Text != "" && (CheckCreateUserAccount.Checked ? (TxtUsername.Text != "" && TxtPassword.Text != "") : true))
            {
                employeeLogic.AddEmployee(new Employee() { name = TxtEmployeeName.Text, age = (int)NudEmployeeAge.Value, gender = (Employee.Gender)Convert.ToInt32(!RadEmployeeMale.Checked), address = TxtEmployeeAddress.Text, phoneNumber = TxtEmployeePhone.Text });
                PopulateListView();
                SetComboBoxes();

                if (CheckCreateUserAccount.Checked)
                {
                    authenticationLogic.CreateUser(TxtUsername.Text, TxtPassword.Text);
                }

                MessageBox.Show("Vrijwilliger succesvol toegevoegd.", "Notice", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Informatie is niet volledig ingevoerd.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void LvEmployees_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            List<ListViewItem> sortList = new List<ListViewItem>();
            foreach (ListViewItem item in LvEmployees.Items)
            {
                sortList.Add(item);
            }
            sortList = sortList.OrderBy(x => x.SubItems[e.Column].Text).ToList();
            LvEmployees.Items.Clear();
            LvEmployees.Items.AddRange(sortList.ToArray());
        }

        private void BtnRemoveEmployee_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Weet je zeker dat je {LvEmployees.SelectedItems[0].Text} wilt verwijderen?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (ListViewItem item in LvEmployees.SelectedItems)
                {
                    Employee E = new Employee()
                    {
                        name = item.Text,
                        age = int.Parse(item.SubItems[1].Text),
                        gender = (Employee.Gender)Enum.Parse(typeof(Employee.Gender), item.SubItems[2].Text),
                        address = item.SubItems[3].Text,
                        phoneNumber = item.SubItems[4].Text
                    };

                    employeeLogic.RemoveEmployee(E);
                    PopulateListView();
                }
            }
        }

        private void LbCages_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cage C = caretakingLogic.GetCage(int.Parse(LbCages.SelectedItem.ToString()));
            DtpLastCleandate.Value = C.lastCleaningdate;
            LbCageAnimals.Items.Clear();
            LbCageAnimals.Items.AddRange(C.animals.ToArray());
        }

        private void BtnUpdateCleandate_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(CbCleanEmployee.Text))
            {
                caretakingLogic.SetCleanDate(int.Parse(LbCages.SelectedItem.ToString()), DtpNewCleandate.Value, CbCleanEmployee.SelectedItem.ToString());
                LbCages_SelectedIndexChanged(null, null);
            }
            else
            {
                MessageBox.Show("Er is geen werknemer gekozen.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LvAnimalList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (LvAnimalList.FocusedItem.Bounds.Contains(e.Location))
                {
                    CmsAnimals.Items[2].Enabled = (LvAnimalList.FocusedItem.SubItems[5].Text == "Dog" ? true : false);
                    
                    CmsAnimals.Show(Cursor.Position);
                }
            }
        }

        private void geefEtenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LbFeedingAnimals.SelectedIndex = LbFeedingAnimals.FindString(LvAnimalList.FocusedItem.SubItems[0].Text);
            TcMain.SelectedTab = TcMain.TabPages["TpFeeding"];
        }

        private void verschoonHokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TcMain.SelectedTab = TcMain.TabPages["TpCages"];
            LbCages.SelectedItem = LbCages.Items[animalLogic.GetAllAnimals().Find(x => 
            x.name == LvAnimalList.FocusedItem.SubItems[0].Text &&
            x.age == int.Parse(LvAnimalList.FocusedItem.SubItems[1].Text) &&
            x.weight == int.Parse(LvAnimalList.FocusedItem.SubItems[2].Text) &&
            x.gender == (Animal.Genders)Enum.Parse(typeof(Animal.Genders), LvAnimalList.FocusedItem.SubItems[3].Text) &&
            x.price == float.Parse(LvAnimalList.FocusedItem.SubItems[4].Text) &&
            x.species == (Animal.Species)Enum.Parse(typeof(Animal.Species), LvAnimalList.FocusedItem.SubItems[5].Text)
            ).cage];
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            GbUserInfo.Enabled = CheckCreateUserAccount.Checked;
            GbUserInfo.Visible = CheckCreateUserAccount.Checked;
        }

        private void UserInterface_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void uitlatenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LbDogs.SelectedIndex = LbDogs.FindString(LvAnimalList.FocusedItem.SubItems[0].Text);
            TcMain.SelectedTab = TcMain.TabPages["TpUitlaten"];
        }

        private void LbFeedingAnimals_SelectedIndexChanged(object sender, EventArgs e)
        {
            DtpLastFeedingDate.Value = caretakingLogic.GetFeedingDate(animalLogic.GetAnimalFromList(LbFeedingAnimals.SelectedIndex));
        }

        private void BtnUpdateFeeding_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(CbFeedingEmployee.Text))
            {
                caretakingLogic.SetFeedingDate(animalLogic.GetAnimalFromList(LbFeedingAnimals.SelectedIndex), DtpNewFeedingDate.Value, employeeLogic.GetEmployeeByName(CbFeedingEmployee.Text));
                LbFeedingAnimals_SelectedIndexChanged(null, null);
            }
            else
            {
                MessageBox.Show("Er is geen werknemer gekozen.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnImagepicker_Click(object sender, EventArgs e)
        {
            if (OfdImage.ShowDialog() == DialogResult.OK)
            {
                PbAnimalImage.Image = Image.FromFile(OfdImage.FileName);
            }
        }

        private void toggleReserveringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            animalLogic.SetReserved(new Animal()
            {
                name = LvAnimalList.FocusedItem.SubItems[0].Text,
                age = int.Parse(LvAnimalList.FocusedItem.SubItems[1].Text),
                weight = int.Parse(LvAnimalList.FocusedItem.SubItems[2].Text),
                gender = (Animal.Genders)Enum.Parse(typeof(Animal.Genders), LvAnimalList.FocusedItem.SubItems[3].Text),
                price = float.Parse(LvAnimalList.FocusedItem.SubItems[4].Text),
                species = (Animal.Species)Enum.Parse(typeof(Animal.Species), LvAnimalList.FocusedItem.SubItems[5].Text),
                cage = int.Parse(LvAnimalList.FocusedItem.SubItems[6].Text),
                reserved = LvAnimalList.FocusedItem.SubItems[7].Text == "Ja" ? false : true
            });
            PopulateListView();
        }
    }
}
