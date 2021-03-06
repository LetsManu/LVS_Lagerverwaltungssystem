﻿using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVS_Library
{
    //WEIM
    public class Property
    {
        private int id;
        private string name;
        private string description;

        //WEIM
        public Property(int id_, string _name, string _description)
        {
            ID = id_;
            Name = _name;
            Description = _description;
        }

        //WEIM
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        
        //WEIM
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        //WEIM
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        //Lerchner Felix
        public static void Save(Property property)
        {
            SQL_methods.SQL_exec(string.Format(
                "INSERT INTO properties " +
                "(property_name, property_description)" +
                "VALUES " +
                "('{0}', '{1}')",
                property.Name, property.Description));
        }

        //Lerchner Felix
        public static void Remove(Property property)
        {
            SQL_methods.SQL_exec(string.Format(
                "DELETE FROM properties " +
                "WHERE id = '{0}'",
                property.ID));
        }

        //Lerchner Felix
        public static List<Property> All_Properties( )
        {
            string sql = "SELECT id as id, property_name as name, property_description as description FROM properties";

            OdbcCommand cmd = new OdbcCommand(sql, DB.Connection);
            SQL_methods.Open();
            OdbcDataReader sqlReader = cmd.ExecuteReader();

            List<Property> properties = new List<Property>();

            while (sqlReader.Read())
            {
                properties.Add(new Property(( int ) sqlReader["id"], ( string ) sqlReader["name"], ( string ) sqlReader["description"]));
            }

            return properties;
        }

        /// <summary>
        /// Loads all storage to property ids 
        /// </summary>
        /// <returns>List with all n to n connections</returns>
        //Lerchner Felix
        public static List<NtoN> All_sn_to_pn( )
        {
            string sql = "SELECT storage_id, property_id FROM storage_properties";

            OdbcCommand cmd = new OdbcCommand(sql, DB.Connection);
            SQL_methods.Open();
            OdbcDataReader sqlReader = cmd.ExecuteReader();

            List<NtoN> sNtopN = new List<NtoN>();

            while (sqlReader.Read())
            {
                sNtopN.Add(new NtoN(( int ) sqlReader["storage_id"], ( int ) sqlReader["property_id"]));
            }

            return sNtopN;
        }

        //Lerchner Felix
        public override string ToString()
        {
            return Name;
        }
    }

    //Lerchner Felix
    public class NtoN
    {
        public int storage =  0;
        public int property = 0;

        //Lerchner Felix
        public NtoN(int storage, int property)
        {
            this.storage = storage;
            this.property = property;
        }
    }
}
