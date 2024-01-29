
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;



        // Ваши сохранения

        public int Money = 0;

        public bool IsFirstSwitchingOn;
        public GunData DefoltGun;
        public List<GunData> NonByedGuns;
        public List<GunData> ByedGuns;


        public SavesYG()
        {
            Money = 0;
            IsFirstSwitchingOn = true;
        }
    }
}
