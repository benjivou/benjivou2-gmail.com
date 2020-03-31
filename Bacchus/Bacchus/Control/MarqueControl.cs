﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bacchus.Model;

namespace Bacchus.Control
{
    /// <summary>
    /// Link Marque between Model and SQLite
    /// </summary>
    class MarqueControl : BaseControl<Marque>
    {
        /// <summary>
        /// Create a Marque Row in Database
        /// </summary>
        /// <param name="Objet"></param>
        /// <returns></returns>
        public override bool Insert(Marque Objet)
        {
            if(Objet.RefMarque > 0)
                return ExecuteUpdate("INSERT INTO Marques (RefMarque,Nom) VALUES (" + Objet.RefMarque + ",'" + Objet.Nom + "')");
            else
            {
                Marque Brand = GetLastInserted();
                // Pseodo Auto-Increment
                return ExecuteUpdate("INSERT INTO Marques (RefMarque,Nom) VALUES (" + (Brand.RefMarque + 1) + ",'" + Objet.Nom + "')");
            }
        }

        /// <summary>
        /// Delete a Marque Row in Database
        /// </summary>
        /// <param name="Objet"></param>
        /// <returns></returns>
        public override bool Delete(Marque Objet)
        {
            return ExecuteUpdate("DELETE FROM Marques WHERE RefMarque = " + Objet.RefMarque );
        }

        /// <summary>
        /// Get all element in Marques Table
        /// </summary>
        /// <returns></returns>
        public override HashSet<Marque> GetAll()
        {
            OpenConnection();
            HashSet<Marque> Liste = new HashSet<Marque>();
            var Result = ExecuteSelect("SELECT * FROM Marques");
            while (Result.Read())
            {
                Marque Brand = new Marque(Result.GetString(1), Result.GetInt16(0));
                Liste.Add(Brand);
            }
            CloseConnection();
            return Liste;
        }

        /// <summary>
        /// Update Marque element
        /// </summary>
        /// <param name="Objet"></param>
        /// <returns></returns>
        public override bool Update(Marque Objet)
        {
            if (Objet != null && Objet.RefMarque > 0)
                return ExecuteUpdate("UPDATE Marques SET Nom = '" + Objet.Nom + "' WHERE RefMarque = " + Objet.RefMarque);
            else
                return false;
        }

        /// <summary>
        /// Find a brand by his reference
        /// </summary>
        /// <param name="Ref"></param>
        /// <returns></returns>
        public Marque FindByRef(int Ref)
        {
            OpenConnection();
            var Result = ExecuteSelect("SELECT * FROM Marques WHERE RefMarque = " + Ref);
            Marque Brand;
            if (Result.Read())
            {
                Brand = new Marque(Result.GetString(1), Result.GetInt16(0));
            }
            else
                Brand = null;
            CloseConnection();
            return Brand;
        }

        /// <summary>
        /// Return the last inserted (with the max id)
        /// </summary>
        /// <returns></returns>
        public Marque GetLastInserted()
        {
            OpenConnection();
            var Result = ExecuteSelect("SELECT MAX(RefMarque), Nom FROM Marques");
            Marque Brand;
            if(Result.Read())
            {
                Brand = new Marque(Result.GetString(1), Result.GetInt16(0));
            }
            else
                Brand = null;

            CloseConnection();
            return Brand;
        }
    }

	class ArticleControl
	{

	} // TO-DO

	class FamilleControl { } // TO-DO

	class SousFamille{ } // TO-DO
}