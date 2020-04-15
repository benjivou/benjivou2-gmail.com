﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bacchus.View
{
    public partial class FormName : Form
    {
        public string NewName = null;
        private string InitialName;
        public bool IsApplicated = false;

        public FormName(string Title, string ActualName)
        {
            InitializeComponent();
            this.Text = Title;
            NameBox.Text = ActualName;
            InitialName = ActualName;
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            if(AreInputOK() == false)
            {
                MessageBoxes.DispError("Le champs ne peut pas etre vide");
            }
            else
            {
                NewName = NameBox.Text;
                if (InitialName != NewName)
                    IsApplicated = true;
                this.Close();
            }
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            IsApplicated = false;
            this.Close();
        }

        private void FormName_Load(object sender, EventArgs e)
        {

        }

        public bool AreInputOK()
        {
            if (NameBox.Text.Replace(" ", "") == "")
                return false;
            return true;
        }
    }
}
