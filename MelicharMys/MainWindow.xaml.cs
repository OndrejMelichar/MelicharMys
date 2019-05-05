using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MelicharMys
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<string> profileNamesObservableCollection = new ObservableCollection<string>();
        private List<Profile> allProfiles = new List<Profile>();
        private Profile actualProfile;
        private JsonActions jsonActions = new JsonActions("profiles.json");
        private int profilesCount = 0;

        public MainWindow()
        {
            InitializeComponent();

            this.setProfiles();
            this.setInputValues();

            profilesComboBox.ItemsSource = this.profileNamesObservableCollection;

            /*Task.Run(async () => {
                await this.setDBProfiles();
            }).ConfigureAwait(true);*/

            this.setDBProfiles().Wait();
        }

        private async Task setDBProfiles()
        {
            WebAPIActions webAPIActions = new WebAPIActions();
            List<Profile> apiProfiles = await webAPIActions.LoadAllProfiles();
            await Task.Run(() => pom(apiProfiles));
        }

        private bool pom(List<Profile> apiProfiles)
        {
            foreach (Profile profile in apiProfiles)
            {
                profile.Name += " DB";
                profile.FromDB = true;
                this.allProfiles.Add(profile);
                this.profileNamesObservableCollection.Add(profile.Name);
            }

            return true;
        }

        private void setProfiles()
        {
            List<Profile> profiles = this.jsonActions.LoadProfiles();

            if (profiles != null)
            {
                foreach (Profile profile in profiles)
                {
                    this.allProfiles.Add(profile);
                    this.profileNamesObservableCollection.Add(profile.Name);
                }
            }
        }

        private void setInputValues()
        {
            int mouseSpeed = MouseOptions.MouseSpeed.GetMouseSpeed();
            int scrollSpeed = MouseOptions.ScrollSpeed.GetScrollSpeed();
            int doubleClickTime = MouseOptions.DoubleClickTime.GetDoubleClickTime();
            
            this.actualProfile = this.getActualProfile(mouseSpeed, scrollSpeed, doubleClickTime);
            profilesComboBox.SelectedItem = this.actualProfile.Name;
        }


        /* pomocné metody */
        private Profile getActualProfile(int mouseSpeed, int scrollSpeed, int doubleClickTime)
        {
            if (this.allProfiles != null)
            {
                foreach (Profile profile in this.allProfiles)
                {
                    if (profile.MouseSpeed == mouseSpeed && profile.ScrollSpeed == scrollSpeed && profile.DoubleClickTime == doubleClickTime)
                    {
                        return profile;
                    }
                }
            }

            Profile newProfile = new Profile() { FromDB = false, Name = "Profil " + (this.allProfiles.Count + 1).ToString(), MouseSpeed = mouseSpeed, ScrollSpeed = scrollSpeed, DoubleClickTime = doubleClickTime };
            this.profilesCount++;
            this.addProfile(newProfile);
            return newProfile;
        }

        private void addProfile(Profile profile)
        {
            this.profileNamesObservableCollection.Add(profile.Name);
            this.allProfiles.Add(profile);
            this.jsonActions.SaveProfiles(this.allProfiles);
        }


        /* ostatní eventy */
        private async void addToDatabase_Click(object sender, RoutedEventArgs e)
        {
            WebAPIActions webAPIActions = new WebAPIActions();
            await webAPIActions.SaveProfile(this.actualProfile);
        }

        private void profilesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = this.profilesComboBox.SelectedIndex;

            if (selectedIndex != -1)
            {
                this.actualProfile = this.allProfiles[selectedIndex];
            }

            if (this.actualProfile.FromDB)
            {
                addToDatabaseButton.IsEnabled = false;
            } else
            {
                addToDatabaseButton.IsEnabled = true;
            }

            mouseSpeedValueTextBox.Text = this.actualProfile.MouseSpeed.ToString();
            scrollSpeedValueTextBox.Text = this.actualProfile.ScrollSpeed.ToString();
            doubleClickTimeValueTextBox.Text = this.actualProfile.DoubleClickTime.ToString();
        }

        private void profilesComboBox_DropDownOpened(object sender, EventArgs e)
        {
            Task.Run(async () => {
                await this.setDBProfiles();
            }).ConfigureAwait(true);
        }

        private void newProfile_Click(object sender, RoutedEventArgs e)
        {
            string timeString = DateTime.Now.ToString("yyyyMMddHHmmss");
            Profile newProfile = new Profile() { FromDB = false, Name = "Profil " + (this.profilesCount + 1).ToString(), MouseSpeed = 10, ScrollSpeed = 3, DoubleClickTime = 500 };
            this.addProfile(newProfile);
            this.profilesCount++;
            this.actualProfile = newProfile;
            profilesComboBox.SelectedItem = newProfile.Name;
        }


        /* mouseSpeed */
        private void mouseSpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mouseSpeedSlider != null && mouseSpeedValueTextBox != null)
            {
                int newSpeed = (int)mouseSpeedSlider.Value;
                mouseSpeedValueTextBox.Text = newSpeed.ToString();
                MouseOptions.MouseSpeed.SetMouseSpeed(newSpeed);
            }

        }

        private void mouseSpeedValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mouseSpeedSlider != null && mouseSpeedValueTextBox != null)
            {
                int newSpeed;
                bool parse = int.TryParse(mouseSpeedValueTextBox.Text, out newSpeed);

                if (parse)
                {
                    if (newSpeed < 0)
                    {
                        newSpeed = 0;
                        mouseSpeedValueTextBox.Text = newSpeed.ToString();
                    }

                    if (newSpeed > 20)
                    {
                        newSpeed = 20;
                        mouseSpeedValueTextBox.Text = newSpeed.ToString();
                    }

                    this.actualProfile.MouseSpeed = newSpeed;
                    mouseSpeedSlider.Value = newSpeed;
                    MouseOptions.MouseSpeed.SetMouseSpeed(newSpeed);
                    this.jsonActions.SaveProfiles(this.allProfiles);
                }
            }

        }

        private void resetMouseSpeed_Click(object sender, RoutedEventArgs e)
        {
            MouseOptions.MouseSpeed.SetDefaultMouseSpeed();
            mouseSpeedValueTextBox.Text = MouseOptions.MouseSpeed.GetMouseSpeed().ToString();
        }

        /* scrollSpeed */
        private void scrollSpeedValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (scrollSpeedValueTextBox != null)
            {
                int newSpeed;
                bool parse = int.TryParse(scrollSpeedValueTextBox.Text, out newSpeed);

                if (parse)
                {
                    if (newSpeed < 0)
                    {
                        newSpeed = 0;
                        scrollSpeedValueTextBox.Text = newSpeed.ToString();
                    }

                    this.actualProfile.ScrollSpeed = newSpeed;
                    MouseOptions.ScrollSpeed.SetScrollSpeed(newSpeed);
                    this.jsonActions.SaveProfiles(this.allProfiles);
                }
            }

        }

        private void resetScrollSpeed_Click(object sender, RoutedEventArgs e)
        {
            MouseOptions.ScrollSpeed.SetDefaultScrollSpeed();
            scrollSpeedValueTextBox.Text = MouseOptions.ScrollSpeed.GetScrollSpeed().ToString();
        }

        /* doubleClickTime */
        private void doubleClickTimeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (doubleClickTimeSlider != null && doubleClickTimeValueTextBox != null)
            {
                int newTime = (int)doubleClickTimeSlider.Value;
                doubleClickTimeValueTextBox.Text = newTime.ToString();
                MouseOptions.DoubleClickTime.SetDoubleClickTime(newTime);
            }

        }

        private void doubleClickTimeValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (doubleClickTimeSlider != null && doubleClickTimeValueTextBox != null)
            {
                int newTime;
                bool parse = int.TryParse(doubleClickTimeValueTextBox.Text, out newTime);

                if (parse)
                {
                    if (newTime < 0)
                    {
                        newTime = 0;
                        doubleClickTimeValueTextBox.Text = newTime.ToString();
                    }

                    if (newTime > 5000)
                    {
                        newTime = 5000;
                        doubleClickTimeValueTextBox.Text = newTime.ToString();
                    }

                    this.actualProfile.DoubleClickTime = newTime;
                    doubleClickTimeSlider.Value = newTime;
                    MouseOptions.DoubleClickTime.SetDoubleClickTime(newTime);
                    this.jsonActions.SaveProfiles(this.allProfiles);
                }
            }

        }

        private void resetDoubleClickTime_Click(object sender, RoutedEventArgs e)
        {
            MouseOptions.DoubleClickTime.SetDefaultDoubleClickTime();
            doubleClickTimeValueTextBox.Text = MouseOptions.DoubleClickTime.GetDoubleClickTime().ToString();
        }

    }
}
