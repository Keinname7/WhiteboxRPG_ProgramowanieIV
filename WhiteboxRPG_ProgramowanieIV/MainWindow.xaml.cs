using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data;

namespace WhiteboxRPG_ProgramowanieIV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string btn1 ="",btn2 = "",btn3 ="", btn4="",btn5="", btn6 = "";
        public string name = "Hero", playerClass;
        public int lv=0, exp=0, expNext=1000, HP=1, maxHP=1, MP=0, maxMP=0, AC=10, MR=5, tMR=5, BAB=0,FAB=0, DMG=0, gold=200;
        public item Weapon = new item("Old Dagger", 0, 0, 0, 0, 2, 1);
        public item Shield = new item("Nothing", 0, 0, 0, 0, 2, 2);
        public item Armor = new item("Robes", 0, 0, 0, 0, 2, 3);
        public item Trinket = new item("Old Ring", 0, 0, 0, 0, 2, 4);
        public item inv1, inv2, inv3, inv4, inv5;
        public int floor=0, encounter=0;

        public string enemyName;
        public int enemyHD, enemyHP;
        
        Random r = new Random();

        item treasure = null;

        public class item
        {
            public string Name;
            public int Hit, Dmg, Ac, Mr, Value,Type;
            public item(string name, int hit, int dmg, int ac, int mr, int value, int type)
            {
                Name = name;
                Hit = hit;
                Dmg = dmg;
                Ac = ac;
                Mr = mr;
                Value = value;
                Type = type;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            //generateItems();
            // 3 characters on left side:
            //Name, Class, HP

            //basic buttons: Inventory(+Status)
            NewGame();
        }


        public void NewGame()
        {
            textBlock.Text = "Welcome to the Whitebox Dungeon!";
            button1.Content = "New Game";
            button2.Content = "";
            button3.Content = "Load Game";
            btn1 = "chooseClass";
            btn2 = "";
            btn3 = "load";
            button1.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Hidden;
            button3.Visibility = Visibility.Visible;
            button4.Visibility = Visibility.Hidden;
            button5.Visibility = Visibility.Hidden;
            button6.Visibility = Visibility.Hidden;
        }
        public void ChooseClass()
        {
            textBlock.Text = "Choose your Class:";
            button1.Content = "Warrior";
            button2.Content = "Cleric";
            button3.Content = "Wizard";
            btn1 = "Warrior";
            btn2 = "Cleric";
            btn3 = "Wizard";
            button1.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Visible;
            button3.Visibility = Visibility.Visible;
            button4.Visibility = Visibility.Hidden;
            button5.Visibility = Visibility.Hidden;
            button6.Visibility = Visibility.Hidden;
        }

        public void Town()
        {
            floor = 1;
            textBlock.Text = "You find yourself in the town of Fellrock.";
            button1.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Visible;
            button3.Visibility = Visibility.Visible;
            button4.Visibility = Visibility.Hidden;
            button5.Visibility = Visibility.Hidden;
            button6.Visibility = Visibility.Hidden;
            button1.Content = "Castle";
            button2.Content = "Inn";
            button3.Content = "Dungeon";
            button5.Content = "Shop";
            button6.Content = "";
            btn1 = "castle";
            btn2 = "inn";
            btn3 = "dungeon";
            btn5 = "shop";
            btn6 = "";
            
        }

        public void Inn()
        {
            textBlock.Text = "Welcome to the Sleeping Dragon's Inn! \n A good night's sleep will cost you "+(exp/100)+" gold, taxes included! \n (The game saves when resting at the Inn) \n";
            button1.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Hidden;
            button3.Visibility = Visibility.Visible;
            button4.Visibility = Visibility.Hidden;
            button5.Visibility = Visibility.Hidden;
            button6.Visibility = Visibility.Hidden;
            button1.Content = "Leave";
            button2.Content = "";
            button3.Content = "Sleep";
            button4.Content = "";
            button5.Content = "";
            button6.Content = "";
            btn1 = "town";
            btn2 = "sleep";
            btn3 = "sleep";
            btn4 = "";
            btn5 = "";
            btn6 = "";
        }


        public void save()
        {
            //SQLConnection conn = new DBConnection();
            //string cnString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";
            string cnString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\terra\source\repos\WhiteboxRPG_ProgramowanieIV\WhiteboxRPG_ProgramowanieIV\Database1.mdf;Integrated Security=True";
            //@"C:\Users\terra\source\repos\WhiteboxRPG_ProgramowanieIV\WhiteboxRPG_ProgramowanieIV\Database1.mdf";
            SqlConnection conn = new SqlConnection(cnString);   
            if (conn.State != System.Data.ConnectionState.Open) conn.Open();
            //if (conn.State == ConnectionState.Open) textBlock.Text = textBlock.Text + "\n CONNECTED!!";

            string strCommand = "UPDATE Character SET Class='"+playerClass.ToString()+"', Lv='"+lv.ToString()+ "', Exp='" + exp.ToString() + "', gold='" + gold.ToString() + "', HP='" + HP.ToString() + "', maxHP='" + maxHP.ToString() + "', MP='" + MP.ToString() + "', maxMP='" + maxMP.ToString() + "', BAB='" + BAB.ToString() + "', MR='" + MR.ToString() + "' WHERE Id=1;";
            
            //string strCommand = "INSERT INTO Character VALUES ('')"
            SqlCommand myCommand = new SqlCommand(strCommand, conn);
            myCommand.ExecuteReader();
            
            //SqlCommand secondCommand = new SqlCommand("COMMIT;", conn);
            //secondCommand.ExecuteReader();
            //textBlock.Text = textBlock.Text + "\n" + strCommand;
            conn.Close();

            // UPDATING ITEMS

            conn = new SqlConnection(cnString);
            if (conn.State != System.Data.ConnectionState.Open) conn.Open();
            
            strCommand = "UPDATE Items SET Name='" + Weapon.Name.ToString() + "', hit='" + Weapon.Hit + "', dmg='" + Weapon.Dmg + "', ac='" + Weapon.Ac + "', mr='" + Weapon.Mr + "', value='" + Weapon.Value + "' WHERE Type=1;";
            myCommand = new SqlCommand(strCommand, conn);
            myCommand.ExecuteReader();            
            conn.Close();

            conn = new SqlConnection(cnString);
            if (conn.State != System.Data.ConnectionState.Open) conn.Open();

            strCommand = "UPDATE Items SET Name='" + Shield.Name.ToString() + "', hit='" + Shield.Hit + "', dmg='" + Shield.Dmg + "', ac='" + Shield.Ac + "', mr='" + Shield.Mr + "', value='" + Shield.Value + "' WHERE Type=2;";
            myCommand = new SqlCommand(strCommand, conn);
            myCommand.ExecuteReader();
            conn.Close();

            conn = new SqlConnection(cnString);
            if (conn.State != System.Data.ConnectionState.Open) conn.Open();

            strCommand = "UPDATE Items SET Name='" + Armor.Name.ToString() + "', hit='" + Armor.Hit + "', dmg='" + Armor.Dmg + "', ac='" + Armor.Ac + "', mr='" + Armor.Mr + "', value='" + Armor.Value + "' WHERE Type=3;";
            myCommand = new SqlCommand(strCommand, conn);
            myCommand.ExecuteReader();
            conn.Close();

            conn = new SqlConnection(cnString);
            if (conn.State != System.Data.ConnectionState.Open) conn.Open();

            strCommand = "UPDATE Items SET Name='" + Trinket.Name.ToString() + "', hit='" + Trinket.Hit + "', dmg='" + Trinket.Dmg + "', ac='" + Trinket.Ac + "', mr='" + Trinket.Mr + "', value='" + Trinket.Value + "' WHERE Type=4;";
            myCommand = new SqlCommand(strCommand, conn);
            myCommand.ExecuteReader();
            conn.Close();



        }

        public void load()
        {
            string cnString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\terra\source\repos\WhiteboxRPG_ProgramowanieIV\WhiteboxRPG_ProgramowanieIV\Database1.mdf;Integrated Security=True";
            //@"C:\Users\terra\source\repos\WhiteboxRPG_ProgramowanieIV\WhiteboxRPG_ProgramowanieIV\Database1.mdf";
            SqlConnection conn = new SqlConnection(cnString);
            if (conn.State != System.Data.ConnectionState.Open) conn.Open();
            //if (conn.State == ConnectionState.Open) textBlock.Text = textBlock.Text + "\n CONNECTED!!";

            string strCommand = "SELECT * FROM Character WHERE Id = '1';";
            

            SqlCommand myCommand = new SqlCommand(strCommand, conn);
            SqlDataReader reader = myCommand.ExecuteReader();
            while(reader.Read())
            {
                string a = reader["Id"].ToString();
                playerClass = reader["Class"].ToString();
                lv = int.Parse(reader["Lv"].ToString());
                exp = int.Parse(reader["Exp"].ToString());
                gold = int.Parse(reader["Gold"].ToString());
                HP = int.Parse(reader["HP"].ToString());
                maxHP = int.Parse(reader["MaxHP"].ToString());
                MP = int.Parse(reader["MP"].ToString());
                maxMP = int.Parse(reader["maxMP"].ToString());
                gold = int.Parse(reader["Gold"].ToString());
                BAB = int.Parse(reader["BAB"].ToString());
                MR = int.Parse(reader["MR"].ToString());
                updateStats();
            }

            if(playerClass=="Wizard")
            {
                expNext = 2500;
            }
            else if(playerClass=="Warrior")
            {
                expNext = 2000;
            }
            else
            {
                expNext = 1500;
            }

            for(int y = lv;y>1;y--)
            {
                expNext = expNext * 2;
            }



            //SqlCommand secondCommand = new SqlCommand("COMMIT;", conn);
            //secondCommand.ExecuteReader();
            //textBlock.Text = textBlock.Text + "\n" + strCommand;
            conn.Close();

            // UPDATING EQUIPMENT

            conn = new SqlConnection(cnString);
            if (conn.State != System.Data.ConnectionState.Open) conn.Open();
            strCommand = "SELECT * FROM Items WHERE Type = '1';";
            myCommand = new SqlCommand(strCommand, conn);
            reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                //string a = reader["Id"].ToString();
                Weapon.Name = reader["Name"].ToString();
                Weapon.Hit = int.Parse(reader["hit"].ToString());
                Weapon.Dmg = int.Parse(reader["dmg"].ToString());
                Weapon.Ac = int.Parse(reader["ac"].ToString());
                Weapon.Mr = int.Parse(reader["mr"].ToString());
                Weapon.Value = int.Parse(reader["value"].ToString());   
            }
            conn.Close();

            conn = new SqlConnection(cnString);
            if (conn.State != System.Data.ConnectionState.Open) conn.Open();
            strCommand = "SELECT * FROM Items WHERE Type = '4';";
            myCommand = new SqlCommand(strCommand, conn);
            reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                //string a = reader["Id"].ToString();
                Trinket.Name = reader["Name"].ToString();
                Trinket.Hit = int.Parse(reader["hit"].ToString());
                Trinket.Dmg = int.Parse(reader["dmg"].ToString());
                Trinket.Ac = int.Parse(reader["ac"].ToString());
                Trinket.Mr = int.Parse(reader["mr"].ToString());
                Trinket.Value = int.Parse(reader["value"].ToString());
            }
            conn.Close();

            conn = new SqlConnection(cnString);
            if (conn.State != System.Data.ConnectionState.Open) conn.Open();
            strCommand = "SELECT * FROM Items WHERE Type = '2';";
            myCommand = new SqlCommand(strCommand, conn);
            reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                //string a = reader["Id"].ToString();
                Shield.Name = reader["Name"].ToString();
                Shield.Hit = int.Parse(reader["hit"].ToString());
                Shield.Dmg = int.Parse(reader["dmg"].ToString());
                Shield.Ac = int.Parse(reader["ac"].ToString());
                Shield.Mr = int.Parse(reader["mr"].ToString());
                Shield.Value = int.Parse(reader["value"].ToString());
            }
            conn.Close();

            conn = new SqlConnection(cnString);
            if (conn.State != System.Data.ConnectionState.Open) conn.Open();
            strCommand = "SELECT * FROM Items WHERE Type = '3';";
            myCommand = new SqlCommand(strCommand, conn);
            reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                //string a = reader["Id"].ToString();
                Armor.Name = reader["Name"].ToString();
                Armor.Hit = int.Parse(reader["hit"].ToString());
                Armor.Dmg = int.Parse(reader["dmg"].ToString());
                Armor.Ac = int.Parse(reader["ac"].ToString());
                Armor.Mr = int.Parse(reader["mr"].ToString());
                Armor.Value = int.Parse(reader["value"].ToString());
            }
            conn.Close();


            updateStats();
            Inn();
        }

        public void sleep()
        {
            if(gold>=exp/100)
            {
                gold -= exp / 100;
                HP = maxHP;
                MP = maxMP;
                textBlock.Text = "You had a restful sleep. \n ===== \n Welcome to the Sleeping Dragon's Inn! \n A good night's sleep will cost you " + (exp / 100) + " gold, taxes included!";
                save();
            }
            else
            {
                textBlock.Text = "Oy! \n You need to pay" + (exp / 100) + " gold! Come back when ya ain't poor, scrub!";
            }
            updateStats();
        }

        public void Castle()
        {
            textBlock.Text = "You stand in the presence of His Majesty, King Randolf III! \n You may speak..";
            button1.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Visible;
            button3.Visibility = Visibility.Hidden;
            button4.Visibility = Visibility.Hidden;
            button5.Visibility = Visibility.Hidden;
            button6.Visibility = Visibility.Hidden;
            button1.Content = "Leave";
            button2.Content = "Level Up";
            button3.Content = "";
            button4.Content = "";
            button5.Content = "";
            button6.Content = "";
            btn1 = "town";
            btn2 = "xpCheck";
            btn3 = "";
            btn4 = "";
            btn5 = "";
            btn6 = "";
        }

        public void xpCheck()
        {
            if(exp>=expNext)
            {
                textBlock.Text = "For your great achievements, you have been granted a new level!";
                LvUp();
            }
            else
            {
                textBlock.Text = "Thou needest "+expNext+" experience points to level up!";
            }
        }

        public void LvUp()
        {
            lv += 1;
            expNext = expNext * 2;
            switch(playerClass)
            {
                case "Warrior":
                    maxHP = maxHP + r.Next(1, 6) + 1;
                    BAB = lv;
                    MR += 1;
                    break;
                case "Cleric":
                    maxHP = maxHP + r.Next(1, 6);
                    maxMP += lv / 2;
                    BAB = lv*2/3;
                    MR += 2;
                    break;
                case "Wizard":
                    maxHP = maxHP + r.Next(1, 6)-1;
                    maxMP += lv;
                    BAB = lv * 1 / 2;
                    MR += 1;
                    break;
            }
            updateStats();
        }

        public void Shop()
        {
            textBlock.Text = "Welcome to Gustav's General Store!";
            button1.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Visible;
            button3.Visibility = Visibility.Hidden;
            button4.Visibility = Visibility.Hidden;
            button5.Visibility = Visibility.Hidden;
            button6.Visibility = Visibility.Hidden;
            button1.Content = "Leave";
            button2.Content = "items";
            button3.Content = "Armor";
            button4.Content = "Sell";
            button5.Content = "";
            button6.Content = "";
            btn1 = "town";
            btn2 = "shopitem";
            btn3 = "shopArmor";
            btn4 = "sell";
            btn5 = "";
            btn6 = "";
        }


        public void shopitem()
        {
            textBlock.Text = "Here's what i got: \n Dagger: 1 gold \n Mace: 10 gold \n Longsword: 50 gold";
            button1.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Hidden;
            button3.Visibility = Visibility.Hidden;
            button4.Visibility = Visibility.Visible;
            button5.Visibility = Visibility.Visible;
            button6.Visibility = Visibility.Visible;
            button1.Content = "Back";
            button2.Content = "";
            button3.Content = "";
            button4.Content = "Dagger";
            button5.Content = "Mace";
            button6.Content = "Longsword";
            btn1 = "shop";
            btn2 = "buyDagger";
            btn3 = "buyMace";
            btn4 = "buyLongsword";
            btn5 = "";
            btn6 = "";
        }

        public void dungeonEntrance()
        {
            textBlock.Text = "You stand at the entrance of the dreaded Dungeon. \n Are you brave enough?";
            button1.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Hidden;
            button3.Visibility = Visibility.Visible;
            button4.Visibility = Visibility.Hidden;
            button5.Visibility = Visibility.Hidden;
            button6.Visibility = Visibility.Hidden;
            button1.Content = "No";
            button2.Content = "";
            button3.Content = "Yes";
            button4.Content = "";
            button5.Content = "";
            button6.Content = "";
            btn1 = "town";
            btn2 = "";
            btn3 = "dungeon";
            btn4 = "";
            btn5 = "";
            btn6 = "";
        }

        public void dungeon()
        {
            //random event:
            floor += 1;
            encounter = r.Next(1, 5);
            switch(encounter)
            {
                case 1:
                    int a = r.Next(1, 6);
                    if (r.Next(1, 20) > tMR+10)
                    {
                        textBlock.Text = "You fall into a devious trap and die. Game Over!";
                        gameOver();
                    }
                    else
                    {
                        textBlock.Text = "You manage to avoid a deadly trap!";
                        button1.Visibility = Visibility.Hidden;
                        button2.Visibility = Visibility.Visible;
                        button3.Visibility = Visibility.Hidden;
                        button4.Visibility = Visibility.Hidden;
                        button5.Visibility = Visibility.Hidden;
                        button6.Visibility = Visibility.Hidden;
                        button1.Content = "";
                        button2.Content = "Continue";
                        button3.Content = "";
                        button4.Content = "";
                        button5.Content = "";
                        button6.Content = "";
                        btn1 = "";
                        btn2 = "dungeon";
                        btn3 = "";
                        btn4 = "";
                        btn5 = "";
                        btn6 = "";
                    }
                    break;
                case 2:
                    battle();
                    break;
                case 3:
                    if(r.Next(1,6)==1)
                    {
                        newItem();
                    }
                    else
                    {
                        loot();
                    }
                    break;
                case 4:
                    escape();
                    break;
                

            }


        }

        public void escape()
        {
            textBlock.Text = "You find an exit!";
            button1.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Hidden;
            button3.Visibility = Visibility.Visible;
            button4.Visibility = Visibility.Hidden;
            button5.Visibility = Visibility.Hidden;
            button6.Visibility = Visibility.Hidden;
            button1.Content = "Go Deeper";
            button2.Content = "";
            button3.Content = "Escape";
            button4.Content = "";
            button5.Content = "";
            button6.Content = "";
            btn1 = "dungeon";
            btn2 = "";
            btn3 = "town";
            btn4 = "";
            btn5 = "";
            btn6 = "";
            updateStats();
        }



        public void loot()
        {
            int gp = r.Next(1, 100) * floor;
            textBlock.Text = "You find a treasure chest! It containts "+gp+" gold!";
            gold += gp;
            exp += gp;
            button1.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Hidden;
            button3.Visibility = Visibility.Hidden;
            button4.Visibility = Visibility.Hidden;
            button5.Visibility = Visibility.Hidden;
            button6.Visibility = Visibility.Hidden;
            button1.Content = "Continue";
            button2.Content = "";
            button3.Content = "";
            button4.Content = "";
            button5.Content = "";
            button6.Content = "";
            btn1 = "dungeon";
            btn2 = "";
            btn3 = "";
            btn4 = "";
            btn5 = "";
            btn6 = "";
            updateStats();
        }

        public void newItem()
        {
            
            int a = r.Next(1, 6);
            switch (a)
            {
                case 1:
                    treasure = new item("Longsword+1", 1, 2, 0, 0, 500,1);
                    break;
                case 2:
                    treasure = new item("Longsword+2", 2, 3, 0, 0, 5000,1);
                    break;
                case 3:    
                    treasure = new item("Longsword+3", 3, 4, 0, 0, 50000,1);
                    break;
                case 4:    
                    treasure = new item("Mace+2", 2, 2, 0, 0, 1000,1);
                    break;
                case 5:
                    treasure = new item("Dagger+1", 1, 0, 0, 0, 200,1);
                    break;
                case 6:
                    treasure = new item("Defender", 3, 4, 3, 0, 250000,1);
                    break;
                case 7:
                    treasure = new item("Shield+2", 0, 0, 3, 0, 2000,2);
                    break;
                case 8:
                    treasure = new item("Spellguard Shield", 0, 0, 1, 4, 200000,2);
                    break;
                case 9:
                    treasure = new item("Platemail+1", 0, 0, 7, 0, 7500,3);
                    break;
                case 10:
                    treasure = new item("Platemail+2", 0, 0, 8, 0, 75000,3);
                    break;
                case 11:
                    treasure = new item("Ring of Protection", 0, 0, 1, 1, 2000,4);
                    break;
                case 12:
                    treasure = new item("Cloak of Protection", 0, 0, 2, 2, 20000,4);
                    break;
            }

            textBlock.Text = "You found a" + treasure.Name+"! \n Do you wish to equip it?" ;
            button1.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Hidden;
            button3.Visibility = Visibility.Visible;
            button4.Visibility = Visibility.Hidden;
            button5.Visibility = Visibility.Hidden;
            button6.Visibility = Visibility.Hidden;
            button1.Content = "Yes";
            button2.Content = "";
            button3.Content = "No";
            button4.Content = "";
            button5.Content = "";
            button6.Content = "";
            btn1 = "equip";
            btn2 = "";
            btn3 = "dungeon";
            btn4 = "";
            btn5 = "";
            btn6 = "";
            updateStats();
        }

        public void equip()
        {
            if (treasure.Type == 1)
            {
                Weapon = treasure;
            }
            else if (treasure.Type == 2)
            {
                Shield = treasure;
            }
            else if (treasure.Type == 3)
            {
                Armor = treasure;
            }
            else if (treasure.Type == 4)
            {
                Trinket = treasure;
            }

            updateStats();
            dungeon();
        }

        public void updateStats()
        {
            FAB = BAB + Weapon.Hit;
            DMG = Weapon.Dmg;
            AC = 10+Weapon.Ac + Shield.Ac + Armor.Ac + Trinket.Ac;
            tMR = MR + Weapon.Mr + Shield.Mr + Armor.Mr + Trinket.Mr;
            Lv_Display.Text = "LV: " + lv.ToString();
            HP_Display.Text = "HP: " + HP.ToString() + "/" + maxHP.ToString();
            MP_Display.Text = "MP: " + MP.ToString() + "/" + maxMP.ToString();
            Exp_Display.Text = "Exp: " + exp.ToString();
            Gold_Display.Text = "Gold: " + gold.ToString();
            Weapon_Display.Text = "Weapon: " + Weapon.Name;
            Shield_Display.Text = "Shield: " + Shield.Name;
            Armor_Display.Text = "Armor: " + Armor.Name;
            Trinket_Display.Text = "Trinket: " + Trinket.Name;
            Misc_Display.Text = "AC:" + AC.ToString() + " MR:" + tMR.ToString() + " ATK:" + FAB.ToString();
        }

        public void battle()
        {
            generateEnemy();
            textBlock.Text = "A " + enemyName+" blocks the way!";
            button1.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Visible;
            button3.Visibility = Visibility.Visible;
            button4.Visibility = Visibility.Hidden;
            button5.Visibility = Visibility.Hidden;
            button6.Visibility = Visibility.Hidden;
            button1.Content = "Attack";
            button2.Content = "Magic";
            button3.Content = "Flee";
            button4.Content = "";
            button5.Content = "";
            button6.Content = "";
            btn1 = "attack";
            btn2 = "magic";
            btn3 = "flee";
            btn4 = "";
            btn5 = "";
            btn6 = "";
            updateStats();
        }

        public void attack()
        {
            int roll = r.Next(1, 20);
            int enemyRoll = r.Next(1, 20);
            int dmgA, dmgB;
            if(roll==20)
            {
                dmgA = r.Next(2, 12) + 2 * DMG;
                enemyHP -= dmgA;
                textBlock.Text = "Critical Hit! The " + enemyName + " takes "+ dmgA+ " damage! \n";
                if (enemyHP<1)
                {
                    textBlock.Text = "Victory! \n You gain "+enemyHD*enemyHD*100 + " experience points!" ;
                    exp += enemyHD * enemyHD * 100;
                    button1.Visibility = Visibility.Hidden;
                    button2.Visibility = Visibility.Visible;
                    button3.Visibility = Visibility.Hidden;
                    button4.Visibility = Visibility.Hidden;
                    button5.Visibility = Visibility.Hidden;
                    button6.Visibility = Visibility.Hidden;
                    button1.Content = "";
                    button2.Content = "Continue";
                    button3.Content = "";
                    button4.Content = "";
                    button5.Content = "";
                    button6.Content = "";
                    btn1 = "";
                    btn2 = "dungeon";
                    btn3 = "";
                    btn4 = "";
                    btn5 = "";
                    btn6 = "";


                }
            }
            else if(roll+FAB>10+enemyHD)
            {
                dmgA = r.Next(1, 6) + DMG;
                enemyHP -= dmgA;
                textBlock.Text = "You strike the " + enemyName + " for " + dmgA + " damage! \n";
                if (enemyHP < 1)
                {
                    textBlock.Text = textBlock.Text + "Victory! \n You gain " + enemyHD * enemyHD * 100 + " experience points!";
                    exp += enemyHD * enemyHD * 100;
                    button1.Visibility = Visibility.Hidden;
                    button2.Visibility = Visibility.Visible;
                    button3.Visibility = Visibility.Hidden;
                    button4.Visibility = Visibility.Hidden;
                    button5.Visibility = Visibility.Hidden;
                    button6.Visibility = Visibility.Hidden;
                    button1.Content = "";
                    button2.Content = "Continue";
                    button3.Content = "";
                    button4.Content = "";
                    button5.Content = "";
                    button6.Content = "";
                    btn1 = "";
                    btn2 = "dungeon";
                    btn3 = "";
                    btn4 = "";
                    btn5 = "";
                    btn6 = "";
                    return;
                }
            }
            else
            {
                textBlock.Text = "You miss the " + enemyName + "! \n";
            }
            if(enemyRoll+enemyHD>AC)
            {
                dmgB = r.Next(1, 6);
                textBlock.Text = textBlock.Text + "The " + enemyName + " hits you for " + dmgB + " damage! \n";
                HP -= dmgB;
                if(HP<1)
                {
                    gameOver();
                    return;
                }
            }
            else
            {
                textBlock.Text = "The " + enemyName + " misses you! \n";
            }
            button1.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Visible;
            button3.Visibility = Visibility.Visible;
            button4.Visibility = Visibility.Hidden;
            button5.Visibility = Visibility.Hidden;
            button6.Visibility = Visibility.Hidden;
            button1.Content = "Attack";
            button2.Content = "Magic";
            button3.Content = "Flee";
            button4.Content = "";
            button5.Content = "";
            button6.Content = "";
            btn1 = "attack";
            btn2 = "magic";
            btn3 = "flee";
            btn4 = "";
            btn5 = "";
            btn6 = "";
            updateStats();
        }

        public void magic()
        {
            button1.Visibility = Visibility.Hidden;
            button2.Visibility = Visibility.Hidden;
            button3.Visibility = Visibility.Hidden;
            button4.Visibility = Visibility.Visible;
            button5.Visibility = Visibility.Hidden;
            button6.Visibility = Visibility.Hidden;
            button1.Content = "";
            button2.Content = "";
            button3.Content = "";
            button4.Content = "Cancel";
            button5.Content = "";
            button6.Content = "";
            btn1 = "";
            btn2 = "";
            btn3 = "";
            btn4 = "cancelMagic";
            btn5 = "";
            btn6 = "";
            if(playerClass=="Wizard" && MP>=1)
            {
                button1.Visibility = Visibility.Visible;
                button1.Content = "Magic Missile";
                btn1 = "castMagicMissile";
            }
            if (playerClass == "Wizard" && lv >=5 && MP>=5)
            {
                button2.Visibility = Visibility.Visible;
                button2.Content = "Fireball";
                btn2 = "castFireball";
            }
            if (playerClass == "Wizard" && lv >= 9 && MP >= 10)
            {
                button3.Visibility = Visibility.Visible;
                button3.Content = "Finger of Death";
                btn3 = "castDeath";
            }
            if (playerClass == "Cleric" && lv >= 2 && MP >= 1)
            {
                button1.Visibility = Visibility.Visible;
                button1.Content = "Mend";
                btn1 = "castMend";
            }
            if (playerClass == "Cleric" && lv >= 4 && MP >= 3)
            {
                button2.Visibility = Visibility.Visible;
                button2.Content = "Heal";
                btn2 = "castHeal";
            }
            if (playerClass == "Cleric" && lv >= 7 && MP >= 7)
            {
                button3.Visibility = Visibility.Visible;
                button3.Content = "Revivify";
                btn3 = "castRevivify";
            }

        }

        public void cancelMagic()
        {
            button1.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Visible;
            button3.Visibility = Visibility.Visible;
            button4.Visibility = Visibility.Hidden;
            button5.Visibility = Visibility.Hidden;
            button6.Visibility = Visibility.Hidden;
            button1.Content = "Attack";
            button2.Content = "Magic";
            button3.Content = "Flee";
            button4.Content = "";
            button5.Content = "";
            button6.Content = "";
            btn1 = "attack";
            btn2 = "magic";
            btn3 = "flee";
            btn4 = "";
            btn5 = "";
            btn6 = "";
        }

        public void castMagicMissile()
        {
            int roll = r.Next(1, 20);
            int enemyRoll = r.Next(1, 20);
            int dmgA, dmgB;
            dmgA = (r.Next(1, 6)+1) * (1 + lv / 5);
            enemyHP -= dmgA;
            textBlock.Text = "The magic missile hits the " + enemyName + " for " + dmgA + " damage! \n";


            MP -= 1;


            if (enemyHP < 1)
            {
                    textBlock.Text = textBlock.Text + "Victory! \n You gain " + enemyHD * enemyHD * 100 + " experience points!";
                    exp += enemyHD * enemyHD * 100;
                    button1.Visibility = Visibility.Hidden;
                    button2.Visibility = Visibility.Visible;
                    button3.Visibility = Visibility.Hidden;
                    button4.Visibility = Visibility.Hidden;
                    button5.Visibility = Visibility.Hidden;
                    button6.Visibility = Visibility.Hidden;
                    button1.Content = "";
                    button2.Content = "Continue";
                    button3.Content = "";
                    button4.Content = "";
                    button5.Content = "";
                    button6.Content = "";
                    btn1 = "";
                    btn2 = "dungeon";
                    btn3 = "";
                    btn4 = "";
                    btn5 = "";
                    btn6 = "";
                return;
            }
            else if (enemyRoll + enemyHD > AC)
            {
                dmgB = r.Next(1, 6);
                textBlock.Text = textBlock.Text + "The " + enemyName + " hits you for " + dmgB + " damage! \n";
                HP -= dmgB;
                if (HP < 1)
                {
                    gameOver();
                    return;
                }
            }
            button1.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Visible;
            button3.Visibility = Visibility.Visible;
            button4.Visibility = Visibility.Hidden;
            button5.Visibility = Visibility.Hidden;
            button6.Visibility = Visibility.Hidden;
            button1.Content = "Attack";
            button2.Content = "Magic";
            button3.Content = "Flee";
            button4.Content = "";
            button5.Content = "";
            button6.Content = "";
            btn1 = "attack";
            btn2 = "magic";
            btn3 = "flee";
            btn4 = "";
            btn5 = "";
            btn6 = "";
            updateStats();
        }

        public void castFireball()
        {
            int roll = r.Next(1, 20);
            int enemyRoll = r.Next(1, 20);
            int dmgA, dmgB;


            MP -= 5;

            dmgA = r.Next(1, 6)*lv;

            if (roll>enemyHD)
            {
                enemyHP -= dmgA;
                textBlock.Text = "The fireball explodes in a direct hit, burning " + enemyName + " for " + dmgA + " damage! \n";
            }
            else
            {
                enemyHP -= dmgA / 2;
                textBlock.Text = "The fireball's explosion grazes the " + enemyName + " for " + dmgA/2 + " damage! \n";
            }
            

            if (enemyHP < 1)
            {
                textBlock.Text = textBlock.Text + "Victory! \n You gain " + enemyHD * enemyHD * 100 + " experience points!";
                exp += enemyHD * enemyHD * 100;
                button1.Visibility = Visibility.Hidden;
                button2.Visibility = Visibility.Visible;
                button3.Visibility = Visibility.Hidden;
                button4.Visibility = Visibility.Hidden;
                button5.Visibility = Visibility.Hidden;
                button6.Visibility = Visibility.Hidden;
                button1.Content = "";
                button2.Content = "Continue";
                button3.Content = "";
                button4.Content = "";
                button5.Content = "";
                button6.Content = "";
                btn1 = "";
                btn2 = "dungeon";
                btn3 = "";
                btn4 = "";
                btn5 = "";
                btn6 = "";
                return;
            }

            if (enemyRoll + enemyHD > AC)
            {
                dmgB = r.Next(1, 6);
                textBlock.Text = textBlock.Text + "The " + enemyName + " hits you for " + dmgB + " damage! \n";
                HP -= dmgB;
                if (HP < 1)
                {
                    gameOver();
                    return;
                }
            }
            button1.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Visible;
            button3.Visibility = Visibility.Visible;
            button4.Visibility = Visibility.Hidden;
            button5.Visibility = Visibility.Hidden;
            button6.Visibility = Visibility.Hidden;
            button1.Content = "Attack";
            button2.Content = "Magic";
            button3.Content = "Flee";
            button4.Content = "";
            button5.Content = "";
            button6.Content = "";
            btn1 = "attack";
            btn2 = "magic";
            btn3 = "flee";
            btn4 = "";
            btn5 = "";
            btn6 = "";
            updateStats();
        }

        public void castDeath()
        {
            int roll = r.Next(1, 20);
            int enemyRoll = r.Next(1, 20);
            int dmgA, dmgB;


            MP -= 10;

            dmgA = r.Next(1, 6) * lv;

            if (roll > enemyHD)
            {
                enemyHP =0;
                textBlock.Text = "You point your finger at the " + enemyName + " , killing it instantly! \n";
            }
            else
            {
                textBlock.Text = "The  " + enemyName + " avoids your pointing finger! \n";
            }


            if (enemyHP < 1)
            {
                textBlock.Text = textBlock.Text + "Victory! \n You gain " + enemyHD * enemyHD * 100 + " experience points!";
                exp += enemyHD * enemyHD * 100;
                button1.Visibility = Visibility.Hidden;
                button2.Visibility = Visibility.Visible;
                button3.Visibility = Visibility.Hidden;
                button4.Visibility = Visibility.Hidden;
                button5.Visibility = Visibility.Hidden;
                button6.Visibility = Visibility.Hidden;
                button1.Content = "";
                button2.Content = "Continue";
                button3.Content = "";
                button4.Content = "";
                button5.Content = "";
                button6.Content = "";
                btn1 = "";
                btn2 = "dungeon";
                btn3 = "";
                btn4 = "";
                btn5 = "";
                btn6 = "";
                return;
            }

            if (enemyRoll + enemyHD > AC)
            {
                dmgB = r.Next(1, 6);
                textBlock.Text = textBlock.Text + "The " + enemyName + " hits you for " + dmgB + " damage! \n";
                HP -= dmgB;
                if (HP < 1)
                {
                    gameOver();
                    return;
                }
            }
            button1.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Visible;
            button3.Visibility = Visibility.Visible;
            button4.Visibility = Visibility.Hidden;
            button5.Visibility = Visibility.Hidden;
            button6.Visibility = Visibility.Hidden;
            button1.Content = "Attack";
            button2.Content = "Magic";
            button3.Content = "Flee";
            button4.Content = "";
            button5.Content = "";
            button6.Content = "";
            btn1 = "attack";
            btn2 = "magic";
            btn3 = "flee";
            btn4 = "";
            btn5 = "";
            btn6 = "";
            updateStats();
        }

        public void castMend()
        {
            int enemyRoll = r.Next(1, 20);
            int dmgA, dmgB;

            MP -= 1;

            dmgA = r.Next(1, 6) + 1;
            HP += dmgA;
            if(HP>maxHP)
            {
                HP = maxHP;
            }

            textBlock.Text = "You heal for " + dmgA + " hitpoints!";


            if (enemyRoll + enemyHD > AC)
            {
                dmgB = r.Next(1, 6);
                textBlock.Text = textBlock.Text + "The " + enemyName + " hits you for " + dmgB + " damage! \n";
                HP -= dmgB;
                if (HP < 1)
                {
                    gameOver();
                    return;
                }
            }
            button1.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Visible;
            button3.Visibility = Visibility.Visible;
            button4.Visibility = Visibility.Hidden;
            button5.Visibility = Visibility.Hidden;
            button6.Visibility = Visibility.Hidden;
            button1.Content = "Attack";
            button2.Content = "Magic";
            button3.Content = "Flee";
            button4.Content = "";
            button5.Content = "";
            button6.Content = "";
            btn1 = "attack";
            btn2 = "magic";
            btn3 = "flee";
            btn4 = "";
            btn5 = "";
            btn6 = "";
            updateStats();
        }

        public void castHeal()
        {
            int enemyRoll = r.Next(1, 20);
            int dmgA, dmgB;

            MP -= 3;

            dmgA = r.Next(1, 6)*3 + 3;
            HP += dmgA;
            if (HP > maxHP)
            {
                HP = maxHP;
            }

            textBlock.Text = "You heal for " + dmgA + " hitpoints!";


            if (enemyRoll + enemyHD > AC)
            {
                dmgB = r.Next(1, 6);
                textBlock.Text = textBlock.Text + "The " + enemyName + " hits you for " + dmgB + " damage! \n";
                HP -= dmgB;
                if (HP < 1)
                {
                    gameOver();
                    return;
                }
            }
            button1.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Visible;
            button3.Visibility = Visibility.Visible;
            button4.Visibility = Visibility.Hidden;
            button5.Visibility = Visibility.Hidden;
            button6.Visibility = Visibility.Hidden;
            button1.Content = "Attack";
            button2.Content = "Magic";
            button3.Content = "Flee";
            button4.Content = "";
            button5.Content = "";
            button6.Content = "";
            btn1 = "attack";
            btn2 = "magic";
            btn3 = "flee";
            btn4 = "";
            btn5 = "";
            btn6 = "";
            updateStats();
        }

        public void castRevivify()
        {
            int enemyRoll = r.Next(1, 20);
            int dmgA, dmgB;
            MP -= 7;
            HP = maxHP;

            textBlock.Text = "You heal to full health!";


            if (enemyRoll + enemyHD > AC)
            {
                dmgB = r.Next(1, 6);
                textBlock.Text = textBlock.Text + "The " + enemyName + " hits you for " + dmgB + " damage! \n";
                HP -= dmgB;
                if (HP < 1)
                {
                    gameOver();
                    return;
                }
            }
            button1.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Visible;
            button3.Visibility = Visibility.Visible;
            button4.Visibility = Visibility.Hidden;
            button5.Visibility = Visibility.Hidden;
            button6.Visibility = Visibility.Hidden;
            button1.Content = "Attack";
            button2.Content = "Magic";
            button3.Content = "Flee";
            button4.Content = "";
            button5.Content = "";
            button6.Content = "";
            btn1 = "attack";
            btn2 = "magic";
            btn3 = "flee";
            btn4 = "";
            btn5 = "";
            btn6 = "";
            updateStats();
        }


        //new commands to add: magic, cancelMagic, 3 wizard spells, 3 cleric spells

        public void flee()
        {
            if(r.Next(1,6)<3)
            {
                dungeon();
            }
            else
            {
                int enemyRoll = r.Next(1, 20);
                int dmgA, dmgB;

                if (enemyRoll + enemyHD > AC)
                {
                    dmgB = r.Next(1, 6);
                    textBlock.Text = "The " + enemyName + " hits you for " + dmgB + " damage! \n";
                    HP -= dmgB;
                    if (HP < 1)
                    {
                        gameOver();
                        return;
                    }
                }
                else
                {
                    textBlock.Text = "The " + enemyName + " misses you! \n";
                }
                button1.Visibility = Visibility.Visible;
                button2.Visibility = Visibility.Visible;
                button3.Visibility = Visibility.Visible;
                button4.Visibility = Visibility.Hidden;
                button5.Visibility = Visibility.Hidden;
                button6.Visibility = Visibility.Hidden;
                button1.Content = "Attack";
                button2.Content = "Magic";
                button3.Content = "Flee";
                button4.Content = "";
                button5.Content = "";
                button6.Content = "";
                btn1 = "attack";
                btn2 = "";
                btn3 = "flee";
                btn4 = "";
                btn5 = "";
                btn6 = "";
                updateStats();
            }
        }

        public void generateEnemy()
        {
            int a = r.Next(1, 30) + floor;
            if(a<5)
            {
                enemyName = "Kobold";
                enemyHD = 0;
                enemyHP = 1;
            }
            else if(a<10)
            {
                enemyName = "Goblin";
                enemyHD = 0;
                enemyHP = 2;
            }
            else if (a < 15)
            {
                enemyName = "Orc";
                enemyHD = 1;
                enemyHP = 3;
            }
            else if (a < 20)
            {
                enemyName = "Skeleton";
                enemyHD = 1;
                enemyHP = 3;
            }
            else if (a < 25)
            {
                enemyName = "Zombie";
                enemyHD = 2;
                enemyHP = 6;
            }
            else if (a < 30)
            {
                enemyName = "Hobgoblin";
                enemyHD = 2;
                enemyHP = 7;
            }
            else if (a < 35)
            {
                enemyName = "Ogre";
                enemyHD = 4;
                enemyHP = 12;
            }
            else if (a < 40)
            {
                enemyName = "Giant Frog";
                enemyHD = 5;
                enemyHP = 15;
            }
            else if (a < 45)
            {
                enemyName = "Chimera";
                enemyHD = 6;
                enemyHP = 18;
            }
            else if (a < 50)
            {
                enemyName = "Giant";
                enemyHD = 7;
                enemyHP = 21;
            }
            else if (a < 55)
            {
                enemyName = "Great Worm";
                enemyHD = 8;
                enemyHP = 24;
            }
            else
            {
                enemyName = "Dragon";
                enemyHD = 9;
                enemyHP = floor/2;
            }
        }

        public void gameOver()
        {
            textBlock.Text = textBlock.Text + "\n GAME OVER!";
            button1.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Hidden;
            button3.Visibility = Visibility.Visible;
            button4.Visibility = Visibility.Hidden;
            button5.Visibility = Visibility.Hidden;
            button6.Visibility = Visibility.Hidden;
            button1.Content = "Load Save";
            button2.Content = "";
            button3.Content = "Quit";
            button4.Content = "";
            button5.Content = "";
            button6.Content = "";
            btn1 = "load";
            btn2 = "";
            btn3 = "quit";
            btn4 = "";
            btn5 = "";
            btn6 = "";


        }

        public void runFunc(string txt)
        {
            switch(txt)
            {
                case "load":
                    load();
                    break;
                case "quit":
                    Close();
                    break;
                case "chooseClass":
                    ChooseClass();
                    break;
                case "equip":
                    equip();
                    break;
                case "Warrior":
                    Class_Display.Text = "Class: Warrior";
                    playerClass = "Warrior";
                    LvUp();
                    expNext = 2000;
                    Town();
                    break;
                case "Cleric":
                    Class_Display.Text = "Class: Cleric";
                    playerClass = "Cleric";
                    LvUp();
                    expNext = 1500;
                    Town();
                    break;
                case "Wizard":
                    Class_Display.Text = "Class: Wizard";
                    playerClass = "Wizard";
                    LvUp();
                    expNext = 2500;
                    Town();
                    break;
                case "town":
                    Town();
                    break;
                case "Castle":
                    Castle();
                    break;
                case "lvUP":
                    LvUp();
                    break;
                case "xpCheck":
                    xpCheck();
                    break;
                case "dungeon":
                    dungeon();
                    break;
                case "attack":
                    attack();
                    break;
                case "flee":
                    flee();
                    break;
                case "loot":
                    loot();
                    break;
                case "escape":
                    escape();
                    break;
                case "battle":
                    battle();
                    break;
                case "inn":
                    Inn();
                    break;
                case "sleep":
                    sleep();
                    break;
                case "castle":
                    Castle();
                    break;
                case "magic":
                    magic();
                    break;
                case "cancelMagic":
                    cancelMagic();
                    break;
                case "castMagicMissile":
                    castMagicMissile();
                    break;
                case "castFireball":
                    castFireball();
                    break;
                case "castDeath":
                    castDeath();
                    break;
                case "castMend":
                    castMend();
                    break;
                case "castHeal":
                    castHeal();
                    break;
                case "castRevivify":
                    castRevivify();
                    break;
                    

                default:
                    break;


            }
                


        }

        public void generateItems()
        {
            /*
            item Longsword = new item("Longsword", 0, 1, 0, 0, 50);
            item Mace = new item("Mace", 0, 0, 0, 0, 10);
            item Dagger = new item("Dagger", 0, -1, 0, 0, 2);
            item Longsword1 = new item("Longsword+1", 1, 2, 0, 0, 500);
            item Longsword2 = new item("Longsword+2", 2, 3, 0, 0, 5000);
            item Longsword3 = new item("Longsword+3", 3, 4, 0, 0, 50000);
            item Mace1 = new item("Mace+1", 1, 1, 0, 0,100);
            item Mace2 = new item("Mace+2", 2, 2, 0, 0,1000);
            item Dagger1 = new item("Dagger+1", 1, 0, 0, 0,200);
            item Defender = new item("Defender", 3, 4, 3, 0,250000);
            shield Shield = new shield("Shield",0,0,1,0,20);
            shield Shield1 = new shield("Shield+1", 0, 0, 2, 0,200);
            shield Shield2 = new shield("Shield+2", 0, 0, 3, 0,2000);
            shield Shield3 = new shield("Shield+3", 0, 0, 4, 0,20000);
            shield Spellguard = new shield("Spellguard Shield", 0, 0, 1, 4,200000);
            armor LeatherArmor = new armor("Leather Armor", 0, 0, 2, 0,10);
            armor LeatherArmor1 = new armor("Leather Armor+1", 0, 0, 3, 0,200);
            armor Chainmail = new armor("Chainmail", 0, 0, 4, 0,50);
            armor Platemail = new armor("Platemail", 0, 0, 6, 0,1500);
            armor Platemail1 = new armor("Platemail+1", 0, 0, 7, 0,7500);
            armor Platemail2 = new armor("Platemail+2", 0, 0, 8, 0,75000);
            armor Platemail3 = new armor("Platemail+3", 0, 0, 9, 0,750000);
            trinket RingOfProt = new trinket("Ring of Protection", 0, 0, 1, 1,2000);
            trinket CloakOfProt = new trinket("Cloak of Protection", 0, 0, 2, 2,20000);
            */
        }


        private void button3_Click(object sender, RoutedEventArgs e)
        {
            runFunc(btn3);
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            runFunc(btn4);
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            runFunc(btn5);
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            runFunc(btn6);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            runFunc(btn2);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            runFunc(btn1);
        }



    }


}
