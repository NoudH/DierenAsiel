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
using static DierenAsiel.Logic.Modes;

namespace DierenAsiel.UI
{
    public partial class UserInterface : Form
    {
        private IAnimalLogic animalLogic = new AnimalLogicController(Mode.Production);
        private IEmployeeLogic employeeLogic = new EmployeeLogicController(Mode.Production);
        private ICaretakingLogic caretakingLogic = new CaretakingLogicController(Mode.Production);
        private IAuthenticationLogic authenticationLogic = new LoginAuthenticator(Mode.Production);
        private IVisitorLogic visitorLogic = new VisitorLogic(Mode.Production);
        private ITodoLogic uiLogic = new TodoLogic(Mode.Production);

        public UserInterface()
        {
            InitializeComponent();

            SetComboBoxes();

            UpdateAnimalList();
            UpdateAppointmentList();
            UpdateEmployeeList();

            ShowFood();

            DateTimePickersSettings();

            TodoToday();

            AppointmentsToday();
        }

        private void ShowFood()
        {
            TxtDogFood.Text = caretakingLogic.GetFood(Enums.Foodtype.Dogfood).ToString();
            TxtCatFood.Text = caretakingLogic.GetFood(Enums.Foodtype.Catfood).ToString();

            DtpEmptySupplies.Value = caretakingLogic.CalcDateWhenNoFoodLeft();
        }

        /// <summary>
        /// Shows all the appointments planned for today.
        /// </summary>
        private void AppointmentsToday()
        {
            LbAppointments.Items.Clear();
            foreach (string appointment in uiLogic.AppointmentsToday())
            {
                LbAppointments.Items.Add(appointment);
            }
        }

        /// <summary>
        /// Creates a todo list for today.
        /// </summary>
        private void TodoToday()
        {
            LbTodo.Items.Clear();
            foreach (string item in uiLogic.TodoToday())
            {
                LbTodo.Items.Add(item);
            }
        }

        /// <summary>
        /// Sets the settings of the datetimepickers
        /// </summary>
        private void DateTimePickersSettings()
        {
            DtpAppointmentDate.MinDate = DateTime.Today;
            DtpNewCleandate.MinDate = DateTime.Today.AddDays(-7);
            DtpNewFeedingDate.MinDate = DateTime.Today.AddDays(-7);
            DtpUitlaatDate.MaxDate = DateTime.Today.AddDays(-7);

            DtpNewCleandate.MaxDate = DateTime.Today.AddDays(1);
            DtpNewFeedingDate.MaxDate = DateTime.Today.AddDays(1);
            DtpUitlaatDate.MaxDate = DateTime.Today.AddDays(1);            
        }

        private void UpdateAnimalList()
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
                listViewItem.SubItems.Add(A.breed);
                listViewItem.SubItems.Add(A.cage.ToString());
                listViewItem.SubItems.Add(A.reserved ? "Ja" : "Nee");

                LvAnimalList.Items.Add(listViewItem);
            }

            LbDogs.Items.Clear();
            LbDogs.Items.AddRange(animalLogic.GetAnimalsOfType(Animal.Species.Dog).ToArray());
            if (LbDogs.Items.Count > 0) { LbDogs.SelectedIndex = 0; }


            LbCages.Items.Clear();
            LbCages.Items.AddRange(caretakingLogic.GetAllCages().ToArray());
            if (LbDogs.Items.Count > 0) { LbCages.SelectedIndex = 0; }

            LbFeedingAnimals.Items.Clear();
            LbFeedingAnimals.Items.AddRange(animalLogic.GetAllAnimals().ToArray());
            if (LbDogs.Items.Count > 0) { LbFeedingAnimals.SelectedIndex = 0; }
        }

        private void UpdateEmployeeList()
        {
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
        }

        private void UpdateAppointmentList()
        {
            LvAppointments.Items.Clear();
            foreach (Appointment appointment in visitorLogic.GetAllAppointments())
            {
                ListViewItem appointmentItem = new ListViewItem(appointment.Name);
                appointmentItem.SubItems.Add(appointment.Visitor);
                appointmentItem.SubItems.Add(appointment.Date.ToString());

                LvAppointments.Items.Add(appointmentItem);
            }
        }

        /// <summary>
        /// Sets combobox related content.
        /// </summary>
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

        /// <summary>
        /// Button click event that when triggered adds the animal to the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (TxtAnimalName.Text != "" && NudAnimalWeight.Value != 0 && RtbCharacteristics.Text != "" && PbAnimalImage.Image != null)
            {
                byte[] imageBytes = File.ReadAllBytes(OfdImage.FileName);
                string imageBase64 = Convert.ToBase64String(imageBytes);

                Animal A = new Animal() { name = TxtAnimalName.Text, age = (int)NudAnimalAge.Value, weight = (int)NudAnimalWeight.Value, gender = (Animal.Genders)Convert.ToInt32(!RadAnimalMale.Checked), species = (Animal.Species)Enum.Parse(typeof(Animal.Species), CbAnimalType.Text), cage = (int)NudAnimalCage.Value, price = (float)NudAnimalPrice.Value, characteristics = RtbCharacteristics.Lines.ToList(), image = imageBase64, breed = TxtBreed.Text, about = RtbAbout.Text };
                animalLogic.AddAnimal(A);
                PbAnimalImage.Image = null;
                UpdateAnimalList();
                TodoToday();

                MessageBox.Show("Dier succesvol toegevoegd.", "Notice", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Informatie is onvolledig ingevoerd.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Button click event that when triggered that removes the specified animal from the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    UpdateAnimalList();
                    TodoToday();
                }
            }   
        }

        /// <summary>
        /// Button click event that when clicked updates the walking date.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnWalking_Click(object sender, EventArgs e)
        {
            if (CbUitlaatEmployees.Text != "")
            {
                caretakingLogic.SetWalkingDate(animalLogic.GetAnimalFromList(Animal.Species.Dog, LbDogs.SelectedIndex), employeeLogic.GetEmployeeByName(CbUitlaatEmployees.Text), DtpUitlaatDate.Value);
                LbDogs_SelectedIndexChanged(null, null);
                TodoToday();
            }
            else
            {
                MessageBox.Show("Er is geen werknemer gekozen.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event that triggers when another item in the listbox is clicked/focused.
        /// Shows the information of the chosen item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LbDogs_SelectedIndexChanged(object sender, EventArgs e)
        {            
                Animal A = animalLogic.GetAnimalFromList(Animal.Species.Dog, LbDogs.SelectedIndex);
                TxtUitlaatNaam.Text = A.name;
                NudUitlaatAge.Value = A.age;
                RadUitlaatFemale.Checked = (A.gender == Animal.Genders.Female) ? true : false;
                DtpLaatstUitgelaten.Value = caretakingLogic.GetWalkingDate(A);           
        }

        /// <summary>
        /// A button click event that when triggered uses the information to add a new employee.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddEmployee_Click(object sender, EventArgs e)
        {
            if (TxtEmployeeName.Text != "" && TxtEmployeeAddress.Text != "" && TxtEmployeePhone.Text != "" && (CheckCreateUserAccount.Checked ? (TxtUsername.Text != "" && TxtPassword.Text != "") : true))
            {
                employeeLogic.AddEmployee(new Employee() { name = TxtEmployeeName.Text, age = (int)NudEmployeeAge.Value, gender = (Employee.Gender)Convert.ToInt32(!RadEmployeeMale.Checked), address = TxtEmployeeAddress.Text, phoneNumber = TxtEmployeePhone.Text });
                UpdateEmployeeList();
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

        /// <summary>
        /// This event is triggered when a column on the list is clicked.
        /// It orders/sorts the list based on the chosen column.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView listView = (ListView)sender;
            List<ListViewItem> sortList = new List<ListViewItem>();
            foreach (ListViewItem item in listView.Items)
            {
                sortList.Add(item);
            }
            sortList = sortList.OrderBy(x => x.SubItems[e.Column].Text).ToList();
            LvAnimalList.Items.Clear();
            LvAnimalList.Items.AddRange(sortList.ToArray());
        }

        /// <summary>
        /// Button click event that when triggered removes the employee.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    UpdateEmployeeList();
                }
            }
        }

        /// <summary>
        /// Event that triggers when another item in the listbox is clicked/focused.
        /// Shows the information of the chosen item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LbCages_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cage C = caretakingLogic.GetCage(int.Parse(LbCages.SelectedItem.ToString()));
            DtpLastCleandate.Value = C.lastCleaningdate;
            LbCageAnimals.Items.Clear();
            LbCageAnimals.Items.AddRange(C.animals.ToArray());
        }

        /// <summary>
        /// Button click event that when triggered updates the the cleaning date of the cage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpdateCleandate_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(CbCleanEmployee.Text))
            {
                caretakingLogic.SetCleanDate(int.Parse(LbCages.SelectedItem.ToString()), DtpNewCleandate.Value, CbCleanEmployee.SelectedItem.ToString());
                LbCages_SelectedIndexChanged(null, null);
                TodoToday();
            }
            else
            {
                MessageBox.Show("Er is geen werknemer gekozen.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Mouseclick event that when triggered shows a context menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// A toolstrip click event that when triggered shows the "voeding" tab with the chosen animal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void geefEtenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LbFeedingAnimals.SelectedIndex = LbFeedingAnimals.FindString(LvAnimalList.FocusedItem.SubItems[0].Text);
            TcMain.SelectedTab = TcMain.TabPages["TpFeeding"];
        }

        /// <summary>
        /// A toolstrip click event that when triggered shows the "Hokken" tab with the cage of the chosen animal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// When checked, shows the controls needed to create a new user account.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckCreateUserAccount_CheckedChanged(object sender, EventArgs e)
        {
            GbUserInfo.Enabled = CheckCreateUserAccount.Checked;
            GbUserInfo.Visible = CheckCreateUserAccount.Checked;
        }

        /// <summary>
        /// When the userinterface form is closed, stop the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserInterface_FormClosed(object sender, FormClosedEventArgs e)
        {
            //This is needed because the application.run() in program.cs is not tied to this form.
            Application.Exit();
        }

        /// <summary>
        /// A toolstrip click event that when triggered shows the "uitlaten" tab with the chosen animal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uitlatenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LbDogs.SelectedIndex = LbDogs.FindString(LvAnimalList.FocusedItem.SubItems[0].Text);
            TcMain.SelectedTab = TcMain.TabPages["TpUitlaten"];
        }

        /// <summary>
        /// Event that triggers when another item in the listbox is clicked/focused.
        /// Shows the information of the chosen item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LbFeedingAnimals_SelectedIndexChanged(object sender, EventArgs e)
        {
            DtpLastFeedingDate.Value = caretakingLogic.GetFeedingDate(animalLogic.GetAnimalFromList(LbFeedingAnimals.SelectedIndex));
        }

        /// <summary>
        /// Event that triggers when another item in the listbox is clicked/focused.
        /// Shows the information of the chosen item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpdateFeeding_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(CbFeedingEmployee.Text) && LbFeedingAnimals.SelectedIndex != -1)
            {
                Animal temp = animalLogic.GetAnimalFromList(LbFeedingAnimals.SelectedIndex);
                if (caretakingLogic.GetFood((Enums.Foodtype)temp.species) > 0)
                {
                    caretakingLogic.SetFeedingDate(temp, DtpNewFeedingDate.Value, employeeLogic.GetEmployeeByName(CbFeedingEmployee.Text));
                    LbFeedingAnimals_SelectedIndexChanged(null, null);
                    ShowFood();
                    TodoToday();
                }
                else
                {
                    MessageBox.Show("Er is geen eten voor dit dier!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }                
            }
            else
            {
                MessageBox.Show("Er is geen werknemer gekozen.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Button click event that shows the openfiledialog which is used to add a picture to the animal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnImagepicker_Click(object sender, EventArgs e)
        {
            if (OfdImage.ShowDialog() == DialogResult.OK)
            {
                PbAnimalImage.Image = Image.FromFile(OfdImage.FileName);
            }
        }

        /// <summary>
        /// A toolstrip click event that when triggered toggles if the animal is reserved or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                breed = LvAnimalList.FocusedItem.SubItems[6].Text,
                cage = int.Parse(LvAnimalList.FocusedItem.SubItems[7].Text),
                reserved = LvAnimalList.FocusedItem.SubItems[8].Text == "Ja" ? false : true
            });
            UpdateAnimalList();
        }

        /// <summary>
        /// A toolstrip click event that when triggered shows the "EditForm" with the information of the chosen animal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wijzigDierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var focusedItem = LvAnimalList.FocusedItem;
            new EditForm(animalLogic.GetAllAnimals().Find(x =>
                x.name == focusedItem.SubItems[0].Text &&
                x.age == int.Parse(focusedItem.SubItems[1].Text) &&
                x.weight == int.Parse(focusedItem.SubItems[2].Text) &&
                x.gender == (Animal.Genders)Enum.Parse(typeof(Animal.Genders), focusedItem.SubItems[3].Text) &&
                x.price == float.Parse(focusedItem.SubItems[4].Text) &&
                x.species == (Animal.Species)Enum.Parse(typeof(Animal.Species), focusedItem.SubItems[5].Text) &&
                x.breed == focusedItem.SubItems[6].Text &&
                x.cage == int.Parse(focusedItem.SubItems[7].Text) &&
                x.reserved == (focusedItem.SubItems[8].Text == "Ja" ? true : false)
                )).ShowDialog();

            UpdateAnimalList();
        }

        /// <summary>
        /// Button event that when clicked adds the inserted amount of foodportions to the total amount of food portions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddFood_Click(object sender, EventArgs e)
        {
            if (NudDogFood.Value > 0)
            {
                caretakingLogic.AddFood(Enums.Foodtype.Dogfood, (int)NudDogFood.Value);
                TxtDogFood.Text = caretakingLogic.GetFood(Enums.Foodtype.Dogfood).ToString();
            }
            if (NudCatfood.Value > 0)
            {
                caretakingLogic.AddFood(Enums.Foodtype.Catfood, (int)NudCatfood.Value);
                TxtCatFood.Text = caretakingLogic.GetFood(Enums.Foodtype.Catfood).ToString();
            }
            DtpEmptySupplies.Value = caretakingLogic.CalcDateWhenNoFoodLeft();

            ShowFood();
            TodoToday();
        }

        private void BtnAddApointment_Click(object sender, EventArgs e)
        {
            visitorLogic.AddAppointment(TxtAppointment.Text, TxtVisitor.Text, DtpAppointmentDate.Value);
            UpdateAppointmentList();
            AppointmentsToday();
        }

        private void BtnRemoveAppointment_Click(object sender, EventArgs e)
        {
            if (LvAppointments.FocusedItem.Index != -1)
            {
                visitorLogic.RemoveAppointment(LvAppointments.FocusedItem.SubItems[0].Text, LvAppointments.FocusedItem.SubItems[1].Text, DateTime.Parse(LvAppointments.FocusedItem.SubItems[2].Text));
                UpdateAppointmentList();
                AppointmentsToday();
            }            
        }
    }
}
