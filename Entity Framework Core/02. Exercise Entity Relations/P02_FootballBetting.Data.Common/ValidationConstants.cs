﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data.Common
{
    public class ValidationConstants
    {
        //Team
        public const int TeamNameMaxLength = 50;
        public const int TeamLogoUrlMaxLength = 2048;
        public const int TeamInitialsMaxLength = 4;

        //Color
        public const int ColorNameMaxLength = 20;

        //Town
        public const int TownNameMaxLength = 60;

        //Country
        public const int CountryNameMaxLength = 60;

        //Player
        public const int PlayerNameMaxLength = 80;

        //Position
        public const int PositionNameMaxLength = 50;

        //Game
        public const int GameResultMaxLength = 10;

        //User
        public const int UserUsernameMaxLength = 36;
        public const int UserPasswordMaxLength = 256;
        public const int UserEmailMaxLength = 256;
        public const int UserNameMaxLength = 100;
    }
}
