using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteboxRPG_ProgramowanieIV.Models
{
    public class GameSession
    {

        //PLayer CurrentPLayer { get; set;}
        public GameSession()
        {
            //CurrentPlayer = new Player();
            //settery
        }

    }

    // TODO:
    // Add the 8 control buttons
    // Add Inventory, Magic, Party buttons
    // Add Tavern
    // Add Dungeon
    // Add game over state
    // 

    enum ItemCategory
    {
        Weapon,
        Offhand,
        Armor,
        Trinket
    }

    public class Item
    {
        string name { get; set; }
        ItemCategory slot { get; set; }
        int hit { get; set; }
        int dmg { get; set; }
        int ac { get; set; }
        int res { get; set; }

    }

    public class Inventory
    {
        Item weapon { get; set; }
        Item offhand { get; set; }
        Item armor { get; set; }
        Item trinket { get; set; }
    }

    public class Hero
    {
        string Name { get; set; }
        string title { get; set; }
        string charClass { get; set; }
        int lv { get; set; }
        int exp { get; set; }
        int hp { get; set; }
        int hit { get; set; }
        int dmg { get; set; }
        int ac { get; set; }
        int res { get; set; }
    }

    public class Ability
    {
        string Name;
    }
    public class Monster
    {
        string name;
        bool isUnholy;
        int lv, exp, hp, hit, dmg, ac, res;
    }
    ///


    public class Location
    {
        string name, description;
        string choice1, choice2, choice3, choice4, choice5, choice6;
    }

    public class Town : Location
    {
       public Town()
        {
            
        }
    }

    //Town: 
    //General Store:
    //Tavern:
    //
}
