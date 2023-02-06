using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

//Name: Chris Fraser
//Title: Assignment 1 - MP3 Player
//Date: 2023-02-05

namespace Assignment1_MP3Player
{
    public partial class MainWindow : Window
    {
        TagLib.File? currentFile;//decleared globaly so i can use it anywhere I need to

        public MainWindow()
        {
            InitializeComponent();
        }

        //Opens file dialog box (File explorer)
        private void OpenFile_Click(object sender, RoutedEventArgs e) 
        {
            mainMenu.Visibility = Visibility.Visible; //makes sure that the mainMenu is showing
            mp3FilePath.Visibility = Visibility.Visible; 

            OpenFileDialog fileDialog = new OpenFileDialog();

            if (fileDialog.ShowDialog() == true) //returns true if a valid file was selected
            {
                fileNameBox.Text = fileDialog.FileName; //Display name of selected file 

                //creating a tag lib file object for accessing mp3 metadata
                currentFile = TagLib.File.Create(fileDialog.FileName);

                //Media Player
                myMediaPlayer.Source = new Uri(fileDialog.FileName);
            }
        }

        private void ShowTags_Click(object sender, RoutedEventArgs e)
        {
            showSongTags.Visibility = Visibility.Visible;
            //Read tag data from currntly selected file
            if (currentFile != null)
            {
                var title = currentFile.Tag.Title;
                var year = currentFile.Tag.Year;
                var album = currentFile.Tag.Album;

                tagNameBox.Text = title;
                tagYearBox.Text = year.ToString();
                tagAlubmBox.Text = album;
            }
        }

        //----------------------------------On Click Media Controls---------------------------------------
        //Play (Button)
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            myMediaPlayer.Play();
        }

        //Pause (Button)
        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            myMediaPlayer.Pause();
        }

        //Stop (Button)
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            myMediaPlayer.Stop();
        }

        private void EditTags_Click(object sender, RoutedEventArgs e)
        {
            mainMenu.Visibility= Visibility.Hidden;
            editTags.Visibility= Visibility.Visible;
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            mainMenu.Visibility = Visibility.Visible;
            editTags.Visibility = Visibility.Hidden;
        }

        //------------------------------------Edit Tags Save Button---------------------------------------
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (songTitle.Text != null || songYear != null || songAlbum.Text != null)
                {
                    currentFile.Tag.Title = songTitle.Text;
                    currentFile.Tag.Year = Convert.ToUInt32(songYear.Text);
                    currentFile.Tag.Album = songAlbum.Text;
                    myMediaPlayer.Source = null;
                    currentFile.Save();
                    MessageBox.Show("Tags Updated");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ": All boxes must not be empty");
            }
        }

        //----------------------------------Comand Binding Media Controls---------------------------------------
        //Play (Menu Items)
        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = myMediaPlayer.Source != null;
        }
        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            myMediaPlayer.Play();
        }

        //Pause (Menu Items)
        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = myMediaPlayer.Source != null;
        }
        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            myMediaPlayer.Pause();
        }

        //Stop (Menu Items)
        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = myMediaPlayer.Source != null;
        }
        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            myMediaPlayer.Stop();
        }
    }
}
