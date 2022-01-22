using System;

namespace IsleofCirca2
{
    public class Armor
    {
        //physical armor and magical armor, both will have armor points 
        private string name;
        private int rating;
        private int magicRating;

        public Armor(int rating, int magic, string name)
        { //constructor which takes base armor, magic armor, and name
            this.rating = rating;
            magicRating = magic;
            this.name = name;
        }

        public Armor(Armor a)
        {//copy constructor
            rating = a.getArmor(true);
            magicRating = a.getMagicRating();
            name = a.getName();
        }

        public void setRating(int rating)
        {//setting the rating of the armor
            if(rating<0)
                Console.WriteLine("Invalid armor rating");
            else
            {
                this.rating = rating;
            }
        }
        public void setMagic(int magic)
        {//setting the magic rating of the armor
            this.magicRating = magic;
        }
        public int getArmor(bool magicDampen)
        {//getting the armor points based off of dampening status
            if (magicDampen)
                return rating;//return base if there is magic dampening
            else
            {
                return rating + magicRating;// base + magic, otherwise
            }
        }
        //getting the name and magic defence
        public string getName()
        {
            return name;
        }
        public int getMagicRating()
        {
            return magicRating;
        }
        public override string ToString()
        {// a to string to print off values 
            return name + "| Armor rating: " + rating + ", Magical rating: " + magicRating;
        }
    }
}