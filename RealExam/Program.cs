using System;
using System.Collections.Generic;
using System.IO;

namespace RealExam
{
    class Person
    {
        public string AllName;
        public string DivisionNumber;
        public int Money;
        public bool Leader;
        //public List<Person> people;
        public List<Person> PlantOne;
        public List<Person> PlantTwo;
        public List<Person> PlantThre;
    }
    class Program
    {
        public static List<Person> WriteListPersons(Person person, List<Person> people)
        {
            foreach (var item in people)
            {
                people.Add(person);
            }
            return people;
        }
        public static Person IdentificationPerson(string[] pattern)
        {
            Person person = new Person();
            person.AllName = pattern[0];
            person.DivisionNumber = pattern[1];
            person.Money = int.Parse(pattern[2]);
            person.Leader = ItsLeader(pattern);
            return person;
        }
        public static void WritePersonOnPlant(List<Person> people, List<Person> PlantOne, List<Person> PlantTwo, List<Person> PlantThre)
        {
            for (int i = 0; i < people.Count; i++)
            {
                if (people[i].DivisionNumber == "Цех 1")
                {
                    PlantOne.Add(people[i]);
                }
                else if (people[i].DivisionNumber == "Цех 2")
                {
                    PlantTwo.Add(people[i]);
                }
                else if (people[i].DivisionNumber == "Цех 3")
                {
                    PlantThre.Add(people[i]);
                }
            }
        }
        public static bool ItsLeader(string[] person)
        {
            if (person.Length == 4)
            {
                if (person[3] == "true")
                    return true;
            }
            return false;
        }
        public static string[] WritePerson(string[] list)
        {
            string[] pattern = new[] { "Иванов Иван Иванович", "Цех 1", "25000" };
            for (int i = 0; i < list.Length; i++)
            {
                pattern = list[i].Split(";");
            }
            return pattern;
        }
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>();
            string[] list = File.ReadAllLines("List.txt");
            for (int i = 0; i < list.Length; i++)
            {
                string[] pattern = WritePerson(list);
                Person person = IdentificationPerson(pattern);
                people.Add(person);
            }

        }
    }
}
