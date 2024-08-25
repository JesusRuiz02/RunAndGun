using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAME_SERVICES_UI_MAIN_MENU : MonoBehaviour
{
   public void LogIn()
   {
      PlayGamesManager.GetInstance().SignIn();
   }

   public void ShowLeaderboard()
   {
      PlayGamesManager.GetInstance().ShowLeaderboard();
   }

   public void ShowAchievements()
   {
      PlayGamesManager.GetInstance().ShowAchievementUI();
   }
}
