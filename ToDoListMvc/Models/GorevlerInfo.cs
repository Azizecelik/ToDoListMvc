namespace ToDoListProject.Models
{
    public class GorevlerInfo
    {
        public int Id { get; set; }
        public string GorevAdi { get; set; }
        public string Aciklama { get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;
        public bool GorevDurumu { get; set; }
        
    }
}
