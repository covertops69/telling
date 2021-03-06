﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telling.Core
{
    public static class Constants
    {
        public static float MARGIN { get { return 15f; } }
        public static float BUTTON_HEIGHT { get { return MARGIN * 2.5f; } }
        public static string BASE_URL { get { return "http://telling.fullstack.co.za/api"; } }
        public static string API_ERROR_TIMEOUT { get { return "API_ERROR_TIMEOUT"; } }
        public static string API_ERROR_CRASH { get { return "API_ERROR_CRASH"; } }
        public static string API_ERROR_NONETWORK { get { return "API_ERROR_NONETWORK"; } }
        public static string API_ERROR_NO_MESSAGE { get { return "API_ERROR_NOMESSAGEBACK"; } }

        public const string INPUT_VALIDATION_ERROR = "Error";
        public const string INPUT_VALIDATION_TEXT = "Text";
        public const string INPUT_CLICK = "Click";
        public const string MENU_ITEM_VISIBILITY = "MenuItemVisibility";
        public const string MENU_ITEM_CLICK_EVENT = "MenuItemClickEvent";
        public const string SELECTED_PLAYERS_VIEW = "SelectedPlayers";
    }
}
