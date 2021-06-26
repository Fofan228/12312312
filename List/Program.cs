using System;
using System.IO;
using System.Collections.Generic;

namespace List
{
    class Person
    {
        public int Id;
        public string FirstName;
        public string LastName;
        public DateTime BirthDate;
        public List<PersonRelation> Relations;

        public string GetRelation(Person person)
        {
            foreach (var relation in Relations)
            {
                if (relation.PersonOne == person || relation.PresonTwo == person)
                {
                    if (relation.RelationType == RelationType.Parent)
                    {
                        if (relation.PresonTwo == person)
                        {
                            return "parent";
                        }
                        else
                        {
                            return "child";
                        }
                    }
                    else if (relation.RelationType == RelationType.Sibling)
                    {
                        return "sibling";
                    }
                    else
                    {
                        return "spouse";
                    }
                }
            }
            return string.Empty;
        }
    }
    class PersonRelation
    {
        public Person PersonOne;
        public Person PresonTwo;
        public RelationType RelationType;
    }
    enum RelationType
    {
        Spouse, Sibling, Parent
    }
    class Program
    {
        public static Person WritePerson(int Id, List<Person> people)
        {
            foreach (var person in people)
            {
                if (person.Id == Id)
                    return person;
            }
            return null;
        }
        public static List<Person> ReadPeople(string[] info)
        {
            var result = new List<Person>();
            int index = 0; //счётчик
            string[] pattern = new[] { "Id", "FirstName", "LastName", "BirthDate" }; //шаблон
            while (info[index] != string.Empty)
            {
                if (index == 0)
                {
                    pattern = info[index].Split(";");
                    index++;
                    continue;
                }
                result.Add(ReadPerson(info[index], pattern));
                index++;
            }
            index++;

            for (int i = index; i < info.Length; i++)
            {
                ReadPersonRelation(info[i], result);
            }
            return result;
        }
        
        public static Person ReadPerson(string personInfo, string[] pattern)
        {
            var person = new Person();
            person.Relations = new List<PersonRelation>();
            var parts = personInfo.Split(";");
            for (int i = 0; i < pattern.Length; i++)
            {
                if (pattern[i] == "Id")
                {
                    person.Id = int.Parse(parts[i]);
                }
                else if (pattern[i] == "FirstName")
                {
                    person.FirstName = parts[i];
                }
                else if (pattern[i] == "LastName")
                {
                    person.LastName = parts[i];
                }
                else person.BirthDate = DateTime.Parse(parts[i]);
            }
            return person;
        }

        public static void ReadPersonRelation(string personRelationInfo, List<Person> persons)
        {
            string[] parts1 = personRelationInfo.Split("<->");
            string[] parts2 = parts1[1].Split("=");

            int person1Id = int.Parse(parts1[0]);
            int person2Id = int.Parse(parts2[0]);

            string relationType = parts2[1];

            PersonRelation relation = new PersonRelation();
            relation.RelationType = GetRelationType(relationType);
            foreach (var person in persons)
            {
                if (person.Id == person1Id)
                {
                    relation.PersonOne = person;
                    person.Relations.Add(relation);
                }
                else if (person.Id == person2Id)
                {
                    relation.PresonTwo = person;
                    person.Relations.Add(relation);
                }
            }
        }
        public static RelationType GetRelationType(string relation)
        {
            return relation == "spouse"
                          ? RelationType.Spouse
                          : relation == "sibling"
                              ? RelationType.Sibling
                              : RelationType.Parent;
        }
        static void Main(string[] args)
        {
            string[] info = File.ReadAllLines("UseList.txt");
            var list = ReadPeople(info);
            while (true)
            {
                int numberIdOne = int.Parse(Console.ReadLine());
                int numberIdTwo = int.Parse(Console.ReadLine());
                Person personOne = WritePerson(numberIdOne, list);
                Person personTwo = WritePerson(numberIdTwo, list);
                Console.WriteLine(personOne.GetRelation(personTwo));
            }
        }
    }
}
