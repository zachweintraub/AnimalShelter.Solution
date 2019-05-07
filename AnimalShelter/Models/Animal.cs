using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace AnimalShelter.Models
{
  public class Animal
  {
    private int _id;
    private string _type;
    private string _name;
    private string _description;

    public Animal(string type, string name, string description, int id = 0)
    {
      _type = type;
      _name = name;
      _description = description;
      _id = id;
    }
    public int GetId()
    {
      return _id;
    }

    new public string GetType()
    {
      return _type;
    }

    public string GetName()
    {
      return _name;
    }

    public string GetDescription()
    {
      return _description;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO animal (type, name, description) VALUES (@AnimalType, @AnimalName, @AnimalDescription);";
      MySqlParameter type = new MySqlParameter();
      type.ParameterName = "@AnimalType";
      type.Value = _type;
      cmd.Parameters.Add(type);
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@AnimalName";
      name.Value = _name;
      cmd.Parameters.Add(name);
      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@AnimalDescription";
      description.Value = _description;
      cmd.Parameters.Add(description);
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Animal> GetAll()
    {
      List<Animal> allAnimals = new List<Animal>();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM animal;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string type = rdr.GetString(1);
        string name = rdr.GetString(2);
        string description = rdr.GetString(3);
        Animal thisAnimal = new Animal(type, name, description, id);
        allAnimals.Add(thisAnimal);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allAnimals;
    }

    public static Animal Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM animal WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int animalId = 0;
      string type = "";
      string name = "";
      string description = "";
      while (rdr.Read())
      {
        animalId = rdr.GetInt32(0);
        type = rdr.GetString(1);
        name = rdr.GetString(2);
        description = rdr.GetString(3);
      }
      Animal thisAnimal = new Animal(type, name, description, animalId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return thisAnimal;
    }

    public static void Delete(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM animal WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public void Edit(string newType, string newName, string newDescription)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE animal SET type = @newType, name = @newName, description = @newDescription WHERE id = @searchId;";
      MySqlParameter type = new MySqlParameter();
      type.ParameterName = "@newType";
      type.Value = newType;
      cmd.Parameters.Add(type);
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);
      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@newDescription";
      description.Value = newDescription;
      cmd.Parameters.Add(description);
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);
      cmd.ExecuteNonQuery();
      _type = newType;
      _name = newName;
      _description = newDescription;
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
