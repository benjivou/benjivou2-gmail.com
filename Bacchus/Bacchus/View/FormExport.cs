﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bacchus.Control;

namespace Bacchus.View
{
    /// <summary>
    /// Window to export a database
    /// </summary>
    public partial class FormExport : Form
    {
        private string LastPath;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public FormExport()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Initialize the progress bar
        /// </summary>
        private void InitProgressBar()
        {
            ExportProgress.Minimum = 1;
            ExportProgress.Value = 1;
            ExportProgress.Step = 1;
            ExportProgress.Visible = true;
        }

        /// <summary>
        /// When the backbtn is pressed, close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// When the select csv is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectCsvBtn_Click(object sender, EventArgs e)
        {
            if(CsvName.Text == "" || CsvName.Text.Contains(" "))
            {
                MessageBoxes.DispError("ERREUR : Le nom du fichier n'est pas valide ou contient des espaces");
            }
            else
            {
                // change initial directory
                SaveDialog.InitialDirectory = LastPath;
                SaveDialog.FileName = CsvName.Text + ".csv";
                if (SaveDialog.ShowDialog() == DialogResult.OK)
                {
                    InitProgressBar();
                    ExportLab.Text = "Exportation de la base de données en cours...";
                    ExportLab.Visible = true;
                    if (FileControl.ExportFile(SaveDialog.FileName,ExportProgress))
                    {
                        MessageBoxes.DispInfo("L'export est terminé");
                        this.Close();
                    }
                    else
                    {
                        MessageBoxes.DispError("Une erreur est survenue lors de l'exportation");
                        ExportLab.Text = "L'opération a été intérompu, veuillez réessayer";
                    }

                    // save the path
                    LastPath = SaveDialog.FileName;
                    SaveLastPath();
                }
            }            
        }

        /// <summary>
        /// Save the last path used form the import
        /// </summary>
        private void SaveLastPath()
        {
            var Config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            // We will use custom settings
            Config.AppSettings.Settings["DefaultPath"].Value = "0";
            Config.AppSettings.Settings["LastPath"].Value = LastPath;
            // Final Save
            Config.Save(ConfigurationSaveMode.Full);
        }

        private void FormExport_Load(object sender, EventArgs e)
        {
            var Config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (int.Parse(Config.AppSettings.Settings["DefaultPath"].Value) != 1)
            {
                LastPath = Config.AppSettings.Settings["LastPath"].Value;
            }
            else
            {
                LastPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
        }
    }
}
