namespace MapleStoryFinal.Models.ViewModel
{
    //1
    public class MonsterSearchViewModel
    {
        //搜尋條件
        public MonsterSearchParms SearchParms { get; set; }
        //搜尋結果
        public List<TblMonster> Monsters { get; set; }

        public MonsterSearchViewModel()
        {
            SearchParms = new MonsterSearchParms();
            Monsters = new List<TblMonster>();
        }
    }
  
    public class MonsterSearchParms
    {
        public int? minlevel { get; set; }
        public int? maxlevel { get; set; }
        public int? minHp { get; set; }
        public int? maxHp { get; set; }
        public string? area { get; set; }
    }
}
