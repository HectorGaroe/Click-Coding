//using System;
//using UnityEngine;
//using UnityEngine.TextCore.Text;

//[Serializable]
//public class SaveData
//{

//    public MetaData metaData;
//    public Player playerInfo;

//    public SaveData()
//    {
//        metaData = new MetaData();
//        Player = new Player();
//    }

//    public SaveData(string name, string[] upgrades)
//    {
//        metaData = new MetaData(name, 0);
//        Player = new Player(upgrades);
//    }
//}

//[Serializable]
//public class MetaData
//{
//    public string name;
//    public float playTime;

//    public MetaData()
//    {
//        name = "Default";
//        playTime = 0;
//    }

//    public MetaData(string name, float playTime)
//    {
//        this.name = name;
//        this.playTime = playTime;
//    }
//}
