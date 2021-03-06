﻿using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVS_Library
{
    public class Category
    {
        private int id;
        private string name;
        private string descpription;

        //WEIM
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name"></param>
        /// <param name="Descritpion"></param>
        public Category(int id_, string _name, string _descritpion)
        {
            ID = id_;
            Name = _name;
            Description = _descritpion;
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
                return descpription;
            }
            set
            {
                descpription = value;
            }
        }

        public static void Save(Category category)
        {
            SQL_methods.SQL_exec(string.Format(
                "INSERT INTO categories " +
                "(category_name, category_description)" +
                "VALUES " +
                "('{0}', '{1}')",
                category.Name, category.Description));
        }

        public static void Remove(Category category)
        {
            SQL_methods.SQL_exec(string.Format(
                "DELETE FROM categories " +
                "WHERE id = '{0}'",
                category.ID));
        }

        //Lerchner Felix
        public static List<Category> All_Categories( )
        {
            string sql = "SELECT id as id, category_name as name, category_description as description FROM categories";

            OdbcCommand cmd = new OdbcCommand(sql, DB.Connection);
            SQL_methods.Open();
            OdbcDataReader sqlReader = cmd.ExecuteReader();

            List<Category> categories = new List<Category>();

            while (sqlReader.Read())
            {
                categories.Add(new Category(( int ) sqlReader["id"], ( string ) sqlReader["name"], ( string ) sqlReader["description"]));
            }

            return categories;
        }

        //Lerchner Felix
        public override string ToString( )
        {
            return Name;
        }
    }
}
