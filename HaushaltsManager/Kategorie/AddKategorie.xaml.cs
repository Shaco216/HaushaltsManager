﻿using HaushaltsManager.DBCreator;
using HaushaltsManager.Model;
using HaushaltsManager.Repository;
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
using System.Windows.Shapes;

namespace HaushaltsManager
{
    /// <summary>
    /// Interaktionslogik für AddKategorie.xaml
    /// </summary>
    public partial class AddKategorie : Window
    {
        BasicRepository _repository;
        int _highestId;
        private readonly KategorieOptionsWindow kategorieOptionsWindow;

        public AddKategorie(BasicRepository repo, int highestId, KategorieOptionsWindow kategorieOptionsWindow)
        {
            InitializeComponent();
            _repository = repo;
            _highestId = highestId;
            this.kategorieOptionsWindow = kategorieOptionsWindow;
        }

        private void InsertKategorie_Click(object sender, RoutedEventArgs e)
        {
            Kategorie kategorie = new Kategorie()
            {
                Name = KategorieName.Text,
                Beschreibung = KategorieBeschreibung.Text
            };
            _highestId += 1;
            _repository.DoNonQueryCommand(SQLStatementProvider.InsertKategorie.Replace("@KategorieName", kategorie.Name)
                .Replace("@Beschreibung", kategorie.Beschreibung)
                .Replace("@Id",_highestId.ToString()));
            kategorieOptionsWindow.LoadKategories();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            kategorieOptionsWindow.LoadKategories();
            this.Close();
        }
    }
}
