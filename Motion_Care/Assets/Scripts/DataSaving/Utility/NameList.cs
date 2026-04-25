using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public static class NameList
{
    private static List<string> nameList = new List<string>()
    {
        "Alex Carter",
        "Mia Rodriguez",
        "Liam Chen",
        "Noah Smith",
        "Emma Patel",
        "Lucas Müller",
        "Sofia Rossi",
        "Ethan Kim",
        "Olivia Brown",
        "Daniel García",

        "Ava Wilson",
        "Mateo Silva",
        "Isabella Nguyen",
        "James Anderson",
        "Charlotte Dubois",
        "Benjamin Lee",
        "Amelia Novak",
        "Elijah Khan",
        "Harper Singh",
        "William Costa",

        "Sophia Lopez",
        "Alexander Ivanov",
        "Evelyn Taylor",
        "Michael Hassan",
        "Abigail Jensen",
        "Jacob Kowalski",
        "Emily Santos",
        "Logan Petrov",
        "Madison Clarke",
        "Sebastian Romero",

        "Scarlett Fischer",
        "Jack Oliveira",
        "Victoria Park",
        "Aiden Scott",
        "Grace Yamamoto",
        "Samuel Becker",
        "Chloe Haddad",
        "Henry Nielsen",
        "Lily Fernandes",
        "David Torres",

        "Zoe Dimitrov",
        "Joseph Wright",
        "Nora Ali",
        "Gabriel Svensson",
        "Ellie Martins",
        "Carter Weber",
        "Hannah Costa",
        "Julian Rojas",
        "Stella Moreau",
        "Leo Schneider",

        "Aria Duarte",
        "Dylan Cohen",
        "Layla Aziz",
        "Owen Bakker",
        "Mila Popescu",
        "Wyatt Haddad",
        "Luna Navarro",
        "Isaac Russo",
        "Aurora Mendes",
        "Levi Costa",

        "Ellie Novak",
        "Theo Janssen",
        "Clara Ibrahim",
        "Mason Silva",
        "Lucy Petrescu",
        "Hugo Laurent",
        "Alice Khan",
        "Felix Schmidt",
        "Julia Romero",
        "Adam Costa",

        "Sara Marin",
        "Ryan O'Connor",
        "Eva Petrova",
        "Kevin Santos",
        "Nadia Haddad",
        "Oscar Lindberg",
        "Maya Costa",
        "Victor Alvarez",
        "Irene Kovacs",
        "Bruno Costa",

        "Laura Jimenez",
        "Marco Bianchi",
        "Paula Costa",
        "Sergio Duarte",
        "Carla Mendes",
        "Ruben Ferreira",
        "Teresa Silva",
        "Andre Costa",
        "Beatriz Navarro",
        "Diego Santos",

        "Lucia Ortega",
        "Rafael Costa",
        "Daniela Pereira",
        "Pablo Mendes",
        "Adriana Costa",
        "Fernando Ruiz",
        "Camila Torres",
        "Andres Vega",
        "Valeria Castro",
        "Nicolas Herrera"
    };

    public static string GetRandomName()
    {
        int index = Random.Range(0, nameList.Count);
        return nameList[index];
    }
}
