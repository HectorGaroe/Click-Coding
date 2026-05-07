using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class Costs
{
    public string buttonName;
    public UpgradeType type;
    public int[] costs;
}

public class SaveData
{
    [SerializeField, JsonProperty] int playerClicks;
    [SerializeField, JsonProperty] int resources;
    [SerializeField, JsonProperty] float playTime;
    [SerializeField, JsonProperty] int[] upgradeLevels;

    public SaveData()
    {
        playerClicks = 0;
        resources = 0;
        playTime = 0f;
        upgradeLevels = new int[] { 0, 0, 0, 0 };
    }

    public void SetData(int playerClicks, int resources, float playTime, int[] upgradeLevels)
    {
        this.playerClicks = playerClicks;
        this.resources = resources;
        this.playTime += playTime;
        this.upgradeLevels = upgradeLevels;
    }

    # region Getters

        public int GetPlayerClicks() => playerClicks;
        public int GetResources() => resources;
        public float GetPlayTime() => playTime;//Se usará en futuras ampliaciones del juego para mostrar estadísticas al jugador
        public int[] GetUpgradeLevels() => upgradeLevels;

    #endregion


    #region Setters

        public void SetPlayerClicksAndResources(int playerClicks, int resources)
        {
            this.playerClicks = playerClicks;
            this.resources = resources;
        }
        public void SetPlayTime(float playTime) => this.playTime += playTime;
        public void SetUpgradeLevels(int[] upgradeLevels) => this.upgradeLevels = upgradeLevels;
    
    #endregion

}
