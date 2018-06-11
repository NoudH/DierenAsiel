using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DierenAsiel;
using DierenAsiel.Logic;

namespace DierenAsiel.UI
{
    public partial class EditForm : Form
    {
        IAnimalLogic animalLogic = new AnimalLogicController(Modes.Mode.Production);

        Animal oldAnimal;

        public EditForm(Animal _oldAnimal)
        {
            InitializeComponent();
            oldAnimal = _oldAnimal;

            TxtAnimalName.Text = oldAnimal.name;
            TxtBreed.Text = oldAnimal.breed;

            NudAnimalAge.Value = oldAnimal.age;
            NudAnimalCage.Value = oldAnimal.cage;
            NudAnimalPrice.Value = (decimal)oldAnimal.price;
            NudAnimalWeight.Value = oldAnimal.weight;

            CbAnimalType.Items.AddRange(Enum.GetNames(typeof(Animal.Species)));
            CbAnimalType.Text = oldAnimal.species.ToString();

            if (oldAnimal.gender == Animal.Genders.Female)
            {
                RadAnimalFemale.Checked = true;
            }

            foreach (string characteristic in oldAnimal.characteristics)
            {
                RtbCharacteristics.Text += characteristic + "\n";
            }
            RtbCharacteristics.Text.Remove(RtbCharacteristics.Text.Length - 1, 1);

            RtbAbout.Text = oldAnimal.about;

            byte[] imageBytes = Convert.FromBase64String(oldAnimal.image);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                PbAnimalImage.Image = Image.FromStream(ms, true);                
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string imageBase64;
            if (OfdImage.FileName != "")
            {
                byte[] imageBytes = File.ReadAllBytes(OfdImage.FileName);
                imageBase64 = Convert.ToBase64String(imageBytes);
            }
            else
            {
                imageBase64 = oldAnimal.image;
            }
            

            Animal newAnimal = new Animal()
            {
                name = TxtAnimalName.Text,
                breed = TxtBreed.Text,

                age = (int)NudAnimalAge.Value,
                cage = (int)NudAnimalCage.Value,
                price = (float)NudAnimalPrice.Value,
                weight = (int)NudAnimalWeight.Value,

                species = (Animal.Species)Enum.Parse(typeof(Animal.Species), CbAnimalType.Text),
                gender = RadAnimalMale.Checked ? Animal.Genders.Male : Animal.Genders.Female,

                characteristics = RtbCharacteristics.Text.Split('\n').ToList(),
                about = RtbAbout.Text,

                image = imageBase64,
                reserved = oldAnimal.reserved
            };

            animalLogic.EditAnimal(oldAnimal, newAnimal);
            this.Dispose();
        }

        private void BtnImagepicker_Click(object sender, EventArgs e)
        {
            if (OfdImage.ShowDialog() == DialogResult.OK)
            {
                PbAnimalImage.Image = Image.FromFile(OfdImage.FileName);
            }
        }
    }
}
