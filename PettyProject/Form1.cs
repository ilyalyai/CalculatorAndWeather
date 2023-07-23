using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Windows.Forms;
using WeatherConsoleApplication;

namespace PettyProject
{
    public partial class Form1 : Form
    {
        private string firstInt = "";
        private string secondInt = "";
        private string whatToDo = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            Button Btext = (Button)sender;
            if (!firstInt.EndsWith(" "))
                firstInt += Btext.Text;
            else if (!(label1.Text.Contains("+") || label1.Text.Contains("-") || label1.Text.Contains("*") || label1.Text.Contains(@"\")))
            {
                label1.Text = Btext.Text;
                firstInt = Btext.Text;
                return;
            }
            else
                secondInt += Btext.Text;

            label1.Text += Btext.Text;
        }

        private void Button2Click(object sender, EventArgs e)
        {
            Button Btext = (Button)sender;
            label1.Text += " ";
            label1.Text += Btext.Text + " ";
            if (!firstInt.EndsWith(" "))
            {
                firstInt += " ";
                whatToDo = Btext.Text;
                return;
            }
            else if (secondInt == "")
            {
                whatToDo = Btext.Text;
                return;
            }
            else
            {
                float result = 0;
                switch (whatToDo)
                {
                    case "+":
                        {
                            result = float.Parse(firstInt, CultureInfo.InvariantCulture.NumberFormat) +
                                     float.Parse(secondInt, CultureInfo.InvariantCulture.NumberFormat);
                            break;
                        }

                    case "-":
                        {
                            result = float.Parse(firstInt, CultureInfo.InvariantCulture.NumberFormat) -
                                     float.Parse(secondInt, CultureInfo.InvariantCulture.NumberFormat);
                            break;
                        }

                    case "*":
                        {
                            result = float.Parse(firstInt, CultureInfo.InvariantCulture.NumberFormat) *
                                     float.Parse(secondInt, CultureInfo.InvariantCulture.NumberFormat);
                            break;
                        }

                    case @"\":
                        {
                            result = float.Parse(firstInt, CultureInfo.InvariantCulture.NumberFormat) /
                                     float.Parse(secondInt, CultureInfo.InvariantCulture.NumberFormat);
                            break;
                        }
                }

                label1.Text = result.ToString(CultureInfo.InvariantCulture) + Btext.Text;
                secondInt = "";
                firstInt = result.ToString(CultureInfo.InvariantCulture) + " ";
                whatToDo = Btext.Text;
            }
        }

        private void Button3Click(object sender, EventArgs e)
        {
            if (firstInt.Length == 0 || secondInt.Length == 0 || whatToDo.Length == 0)
                return;
            float result = 0;
            switch (whatToDo)
            {
                case "+":
                    {
                        result = float.Parse(firstInt, CultureInfo.InvariantCulture.NumberFormat) +
                                 float.Parse(secondInt, CultureInfo.InvariantCulture.NumberFormat);
                        break;
                    }

                case "-":
                    {
                        result = float.Parse(firstInt, CultureInfo.InvariantCulture.NumberFormat) -
                                 float.Parse(secondInt, CultureInfo.InvariantCulture.NumberFormat);
                        break;
                    }

                case "*":
                    {
                        result = float.Parse(firstInt, CultureInfo.InvariantCulture.NumberFormat) *
                                 float.Parse(secondInt, CultureInfo.InvariantCulture.NumberFormat);
                        break;
                    }

                case @"\":
                    {
                        result = float.Parse(firstInt, CultureInfo.InvariantCulture.NumberFormat) /
                                 float.Parse(secondInt, CultureInfo.InvariantCulture.NumberFormat);
                        break;
                    }
            }
            label1.Text = result.ToString(CultureInfo.InvariantCulture);
            secondInt = "";
            firstInt = result.ToString(CultureInfo.InvariantCulture) + " ";
            whatToDo = "";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (label1.Text.Length != 0)
                label1.Text = label1.Text.Substring(0, label1.Text.Length - 1);
            if (!firstInt.EndsWith(" ") && firstInt.Length != 0)
                firstInt = firstInt.Substring(0, firstInt.Length - 1);
            else if (secondInt.Length != 0)
                secondInt = secondInt.Substring(0, secondInt.Length - 1);
        }

        private void Button15Click(object sender, EventArgs e)
        {
            Button Btext = (Button)sender;
            if (!firstInt.EndsWith(" "))
                firstInt += Btext.Text;
            else if (!(label1.Text.Contains("+") || label1.Text.Contains("-") || label1.Text.Contains("*") || label1.Text.Contains(@"\")))
                return;
            else
                secondInt += Btext.Text;

            label1.Text += Btext.Text;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            firstInt = "";
            secondInt = "";
            whatToDo = "";
        }

        private void button18_Click(object sender, EventArgs e)
        {
            string city = "London";
            object cityObj = comboBox1.SelectedItem;
            if (cityObj != null)
                city = cityObj.ToString();
            string apiKey = keys.weatherKey;

            string url = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "&units=metric&appid=" + apiKey;
            string urlExample =
                "http://api.openweathermap.org/data/2.5/weather?q=London,uk&APPID=" + apiKey;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            WeatherResponse weatherResponse;

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream ?? throw new InvalidOperationException());
                string responseFromServer = reader.ReadToEnd();

                weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(responseFromServer);
            }

            if (weatherResponse != null)
            {
                label2.Text = weatherResponse.Name;
                label3.Text = weatherResponse.Main.Temp.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}

namespace WeatherConsoleApplication
{
    public class TemperatureInfo
    {
        public float Temp { get; set; }
    }
}

namespace WeatherConsoleApplication
{
    public class WeatherResponse
    {
        public TemperatureInfo Main { get; set; }

        public string Name { get; set; }
    }
}